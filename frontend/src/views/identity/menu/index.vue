<template>
  <div class="menu-container">
    <!-- 搜索区域 -->
    <hbt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="handleReset"
    >
    </hbt-query>

    <!-- 工具栏 -->
    <hbt-toolbar
      v-model:show-search="showSearch"
      :selected-count="selectedKeys.length"
      :show-select-count="true"
      :show-add="true"
      :show-edit="false"
      :show-delete="false"
      :show-export="true"
      :show-import="true"
      :add-permission="['identity:menu:create']"
      :edit-permission="['identity:menu:update']"
      :delete-permission="['identity:menu:delete']"
      :export-permission="['identity:menu:export']"
      :import-permission="['identity:menu:import']"
      :disabled-edit="selectedKeys.length !== 1"
      :disabled-delete="selectedKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @export="handleExport"
      @import="handleImport"
      @template="handleTemplate"
      @refresh="getList"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    >
      <!-- 自定义按钮 -->
      <template #extra>
        <a-button @click="toggleExpand" class="hbt-btn-expand">
          <template #icon><expand-outlined /></template>
          {{ isExpanded ? '收缩' : '展开' }}
        </a-button>
      </template>
    </hbt-toolbar>

    <!-- 数据表格 -->
    <hbt-tree-table
      ref="treeTableRef"
      :columns="columns.filter((col: any) => col.key && columnSettings[col.key])"
      :data-source="list"
      :loading="loading"
      :row-selection="{
        selectedRowKeys: selectedKeys,
        onChange: onSelectChange
      }"
      v-model:expanded-row-keys="expandedRowKeys"
      :indent-size="20"
      :children-column-name="'children'"
      row-key="menuId"
      size="middle"
      bordered
      :scroll="{ x: 1200, y: 'calc(100vh - 300px)' }"
      :virtual="true"
      :lazy="true"
      :height="500"
      :item-size="54"
      :overscan="5"
      @expand="onExpand"
      @load-data="handleLoadData"
    >
      <template #bodyCell="{ column, record }">
        <!-- 菜单名称 -->
        <template v-if="column.dataIndex === 'menuName'">
          <span>
            <folder-outlined v-if="record.menuType === 0" />
            <menu-outlined v-else-if="record.menuType === 1" />
            <tool-outlined v-else />
            {{ record.menuName }}
          </span>
        </template>
        <!-- 图标 -->
        <template v-else-if="column.dataIndex === 'icon'">
          <span v-if="record.icon">
            <component :is="record.icon" />
            {{ record.icon }}
          </span>
        </template>
        <!-- 权限标识 -->
        <template v-else-if="column.dataIndex === 'perms'">
          <a-tag v-if="record.perms">{{ record.perms }}</a-tag>
        </template>
        <!-- 组件路径 -->
        <template v-else-if="column.dataIndex === 'component'">
          <a-tag v-if="record.component">{{ record.component }}</a-tag>
        </template>
        <!-- 状态 -->
        <template v-else-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>
        <!-- 操作 -->
        <template v-else-if="column.dataIndex === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :show-delete="true"
            :edit-permission="['identity:menu:update']"
            :delete-permission="['identity:menu:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          />
        </template>
      </template>
    </hbt-tree-table>

    <!-- 表单弹窗 -->
    <menu-tabs
      :visible="formVisible"
      :title="formTitle"
      :menu-id="formMenuId"
      :menu-type="formMenuType"
      @success="getList"
      @update:visible="formVisible = $event"
    />

    <!-- 列设置抽屉 -->
    <a-drawer
      :visible="columnSettingVisible"
      title="列设置"
      placement="right"
      width="300"
      @close="columnSettingVisible = false"
    >
      <a-checkbox-group
        :value="Object.keys(columnSettings).filter(key => columnSettings[key])"
        @change="handleColumnSettingChange"
        class="column-setting-group"
      >
        <div v-for="col in defaultColumns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>

    <MenuType
      v-model:visible="menuTypeDialogVisible"
      :title="t('identity.menu.actions.selectType')"
      :ok-text="t('common.ok')"
      :cancel-text="t('common.cancel')"
      @ok="onMenuTypeOk"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, h, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import { FolderOutlined, MenuOutlined, ToolOutlined, ExpandOutlined, CompressOutlined } from '@ant-design/icons-vue'
import type { TableColumnsType } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtMenu, HbtMenuTreeQuery } from '@/types/identity/menu'
import {
  getMenuTree,
  deleteMenu,
  batchDeleteMenu,
  exportMenu,
  importMenu,
  getTemplate
} from '@/api/identity/menu'
import MenuTabs from './components/MenuTabs.vue'
import MenuType from './components/MenuType.vue'

const { t } = useI18n()

// 查询区域显示状态
const showSearch = ref(true)

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = [
  {
    title: t('identity.menu.columns.menuName'),
    dataIndex: 'menuName',
    key: 'menuName',
    width: 220
  },
  {
    title: t('identity.menu.columns.id'),
    dataIndex: 'menuId',
    key: 'menuId',
    width: 80
  },
  {
    title: t('identity.menu.columns.transKey'),
    dataIndex: 'transKey',
    key: 'transKey',
    width: 150
  },
  {
    title: t('identity.menu.columns.parentId'),
    dataIndex: 'parentId',
    key: 'parentId',
    width: 100
  },
  {
    title: t('identity.menu.columns.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 80
  },
  {
    title: t('identity.menu.columns.path'),
    dataIndex: 'path',
    key: 'path',
    width: 200
  },
  {
    title: t('identity.menu.columns.component'),
    dataIndex: 'component',
    key: 'component',
    width: 150
  },
  {
    title: t('identity.menu.columns.queryParams'),
    dataIndex: 'queryParams',
    key: 'queryParams',
    width: 150
  },
  {
    title: t('identity.menu.columns.isExternal'),
    dataIndex: 'isExternal',
    key: 'isExternal',
    width: 100
  },
  {
    title: t('identity.menu.columns.isCache'),
    dataIndex: 'isCache',
    key: 'isCache',
    width: 100
  },
  {
    title: t('identity.menu.columns.menuType'),
    dataIndex: 'menuType',
    key: 'menuType',
    width: 100
  },
  {
    title: t('identity.menu.columns.visible'),
    dataIndex: 'visible',
    key: 'visible',
    width: 100
  },
  {
    title: t('identity.menu.columns.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('identity.menu.columns.perms'),
    dataIndex: 'perms',
    key: 'perms',
    width: 150
  },
  {
    title: t('identity.menu.columns.icon'),
    dataIndex: 'icon',
    key: 'icon',
    width: 100
  },
  {
    title: t('identity.menu.columns.tenantId'),
    dataIndex: 'tenantId',
    key: 'tenantId',
    width: 100
  },

  {
    title: t('identity.menu.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120
  },
  {
    title: t('identity.menu.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: t('identity.menu.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120
  },
  {
    title: t('identity.menu.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: t('identity.menu.columns.deleteBy'),
    dataIndex: 'deleteBy',
    key: 'deleteBy',
    width: 120
  },
  {
    title: t('identity.menu.columns.deleteTime'),
    dataIndex: 'deleteTime',
    key: 'deleteTime',
    width: 180
  },
  {
    title: t('identity.menu.columns.isDeleted'),
    dataIndex: 'isDeleted',
    key: 'isDeleted',
    width: 100
  },
  {
    title: t('identity.menu.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 200
  },
  {
    title: t('identity.menu.columns.action'),
    dataIndex: 'action',
    key: 'action',
    width: 150,
    fixed: 'right' as const
  }
]
const columnSettings = ref<Record<string, boolean>>({})

// 初始化列设置
const initColumnSettings = () => {
  localStorage.removeItem('menuColumnSettings')
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))
  // 默认显示前7列（不含操作列）
  const firstColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 7)
  firstColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })
  columnSettings.value['action'] = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('menuColumnSettings', JSON.stringify(settings))
}

const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'menuName',
    label: t('identity.menu.fields.menuName.label'),
    placeholder: t('identity.menu.fields.menuName.placeholder'),
    type: 'input',
    props: {
      allowClear: true
    }
  },
  {
    name: 'menuType',
    label: t('identity.menu.fields.menuType.label'),
    placeholder: t('identity.menu.fields.menuType.placeholder'),
    type: 'select',
    props: {
      dictType: 'sys_menu_type',
      type: 'radio',
      showAll: true,
      allowClear: true
    }
  },
  {
    name: 'visible',
    label: t('identity.menu.fields.visible.label'),
    placeholder: t('identity.menu.fields.visible.placeholder'),
    type: 'select',
    props: {
      dictType: 'sys_is_visible',
      type: 'radio',
      showAll: true,
      allowClear: true
    }
  },
  {
    name: 'isExternal',
    label: t('identity.menu.fields.isExternal.label'),
    placeholder: t('identity.menu.fields.isExternal.placeholder'),
    type: 'select',
    props: {
      dictType: 'sys_yes_no',
      type: 'radio',
      showAll: true,
      allowClear: true
    }
  },
  {
    name: 'isCache',
    label: t('identity.menu.fields.isCache.label'),
    placeholder: t('identity.menu.fields.isCache.placeholder'),
    type: 'select',
    props: {
      dictType: 'sys_yes_no',
      type: 'radio',
      showAll: true,
      allowClear: true
    }
  },
  {
    name: 'status',
    label: t('identity.menu.fields.status.label'),
    placeholder: t('identity.menu.fields.status.placeholder'),
    type: 'select',
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true,
      allowClear: true
    }
  }
]

// 查询参数
const queryParams = ref<HbtMenuTreeQuery>({
  menuName: '',
  status: -1
})

// 加载状态
const loading = ref(false)

// 菜单列表数据
const list = ref<HbtMenu[]>([])

// 选中的菜单ID
const selectedKeys = ref<(string | number)[]>([])

// 表单弹窗显示状态
const formVisible = ref(false)

// 表单标题
const formTitle = ref('')

// 当前编辑的菜单ID
const formMenuId = ref<number>()

// 表格列定义
const columns: TableColumnsType = [
  {
    title: t('identity.menu.fields.menuName.label'),
    dataIndex: 'menuName',
    key: 'menuName',
    width: 200,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.icon.label'),
    dataIndex: 'icon',
    key: 'icon',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.orderNum.label'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.perms.label'),
    dataIndex: 'perms',
    key: 'perms',
    width: 150,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.path.label'),
    dataIndex: 'path',
    key: 'path',
    width: 150,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.component.label'),
    dataIndex: 'component',
    key: 'component',
    width: 150,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.query.label'),
    dataIndex: 'query',
    key: 'query',
    width: 150,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.isFrame.label'),
    dataIndex: 'isFrame',
    key: 'isFrame',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.isCache.label'),
    dataIndex: 'isCache',
    key: 'isCache',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.menuType.label'),
    dataIndex: 'menuType',
    key: 'menuType',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.visible.label'),
    dataIndex: 'visible',
    key: 'visible',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.menu.fields.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.deleteBy'),
    dataIndex: 'deleteBy',
    key: 'deleteBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.deleteTime'),
    dataIndex: 'deleteTime',
    key: 'deleteTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.isDeleted'),
    dataIndex: 'isDeleted',
    key: 'isDeleted',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 200,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.action'),
    dataIndex: 'action',
    key: 'action',
    width: 150,
    fixed: 'right',
    ellipsis: true
  }
]

// 在 script 部分添加
const menuTabsRef = ref()

// 在 script 部分添加
const formMenuType = ref<number>(0)

// 在 script 部分添加
const menuTypeDialogVisible = ref(false)

// 在 script 部分修改
const treeTableRef = ref()
const isExpanded = ref(false)
const expandedRowKeys = ref<(string | number)[]>([])

// 递归获取所有节点的ID
const getAllKeys = (data: HbtMenu[]): (string | number)[] => {
  return data.flatMap(item => [
    item.menuId,
    ...(item.children ? getAllKeys(item.children) : [])
  ])
}

// 处理展开/收缩事件
const onExpand = async (expanded: boolean, record: HbtMenu) => {
  if (expanded && !record.children) {
    try {
      loading.value = true
      const res = await getMenuTree({
        ...queryParams.value,
        parentId: record.menuId
      })
      if (res.data.code === 200) {
        record.children = res.data.data
      } else {
        message.error(res.data.msg || t('common.failed'))
      }
    } catch (error: any) {
      console.error('[菜单管理] 加载子节点数据出错:', error)
      message.error(t('common.failed'))
    } finally {
      loading.value = false
    }
  }
  isExpanded.value = expandedRowKeys.value.length > 0
}

// 切换展开/收缩
const toggleExpand = () => {
  isExpanded.value = !isExpanded.value
  if (isExpanded.value) {
    // 只展开第一层
    expandedRowKeys.value = list.value.map(item => item.menuId)
  } else {
    expandedRowKeys.value = []
  }
}

// 获取菜单列表
const getList = async () => {
  try {
    loading.value = true
    const res = await getMenuTree(queryParams.value)
    if (res.data.code === 200) {
      list.value = res.data.data
      // 如果当前是展开状态，只展开第一层
      if (isExpanded.value) {
        expandedRowKeys.value = list.value.map(item => item.menuId)
      } else {
        expandedRowKeys.value = []
      }
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error: any) {
    console.error('[菜单管理] 获取菜单列表出错:', error)
    if (error.response?.status === 401) {
      message.error('登录已过期，请重新登录')
      window.location.href = '/login'
    } else {
      message.error(t('common.failed'))
    }
  } finally {
    loading.value = false
  }
}

// 查询方法
const handleQuery = (values?: HbtMenuTreeQuery) => {
  if (values) {
    Object.assign(queryParams.value, values)
  }
  getList()
}

// 重置查询
const handleReset = () => {
  queryParams.value = {
    menuName: '',
    status: -1
  }
  getList()
}

// 处理选择变化
const onSelectChange = (keys: (string | number)[], _: HbtMenu[]) => {
  selectedKeys.value = keys
}

// 处理新增
const handleAdd = () => {
  menuTypeDialogVisible.value = true
}

function onMenuTypeOk(type: number) {
  formMenuType.value = type
  formTitle.value =
    type === 0
      ? t('identity.menu.actions.addDirectory')
      : type === 1
        ? t('identity.menu.actions.addMenu')
        : t('identity.menu.actions.addButton')
  formMenuId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: HbtMenu) => {
  formTitle.value = t('identity.menu.actions.edit')
  formMenuId.value = record.menuId
  formMenuType.value = record.menuType
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: Record<string, any>) => {
  try {
    const res = await deleteMenu(Number(record.menuId))
    if (res.data.code === 200) {
      message.success(t('common.delete.success'))
      getList()
    } else {
      message.error(res.data.msg || t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 处理批量删除
const handleBatchDelete = () => {
  Modal.confirm({
    title: t('common.delete.confirm'),
    content: t('common.delete.message', { count: selectedKeys.value.length }),
    async onOk() {
      try {
        const res = await batchDeleteMenu(selectedKeys.value.map(id => Number(id)))
        if (res.data.code === 200) {
          message.success(t('common.delete.success'))
          selectedKeys.value = []
          getList()
        } else {
          message.error(res.data.msg || t('common.delete.failed'))
        }
      } catch (error) {
        console.error(error)
        message.error(t('common.delete.failed'))
      }
    }
  })
}

// 处理导出
const handleExport = async () => {
  try {
    const { data } = await exportMenu(queryParams.value)
    const blob = new Blob([data], { type: 'application/vnd.ms-excel' })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = `menu_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error(error)
    message.error(t('common.export.failed'))
  }
}

// 处理编辑选中记录
const handleEditSelected = () => {
  if (selectedKeys.value.length !== 1) {
    message.warning(t('common.selectOne'))
    return
  }
  const record = list.value.find(item => String(item.menuId) === String(selectedKeys.value[0]))
  if (record) {
    handleEdit(record)
  }
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 处理表单提交
const handleFormSubmit = () => {
  menuTabsRef.value?.handleSubmit()
}

// 处理懒加载数据
const handleLoadData = async (record: HbtMenu) => {
  try {
    loading.value = true
    const res = await getMenuTree({
      ...queryParams.value,
      parentId: record.menuId
    })
    if (res.data.code === 200) {
      // 更新子节点数据
      record.children = res.data.data
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error: any) {
    console.error('[菜单管理] 加载子节点数据出错:', error)
    if (error.response?.status === 401) {
      message.error('登录已过期，请重新登录')
      window.location.href = '/login'
    } else {
      message.error(t('common.failed'))
    }
  } finally {
    loading.value = false
  }
}

// 处理导入
const handleImport = async () => {
  try {
    const input = document.createElement('input')
    input.type = 'file'
    input.accept = '.xlsx,.xls'
    input.onchange = async (e: Event) => {
      const file = (e.target as HTMLInputElement).files?.[0]
      if (!file) return
      const res = await importMenu(file)
      const { success = 0, fail = 0 } = (res.data as any).Data || {}
      if (success > 0 && fail === 0) {
        message.success(`导入成功${success}条，全部成功！`)
      } else if (success > 0 && fail > 0) {
        message.warning(`导入成功${success}条，失败${fail}条`)
      } else if (success === 0 && fail > 0) {
        message.error(`全部导入失败，共${fail}条`)
      } else {
        message.info('未读取到任何数据')
      }
      if (success > 0) getList()
    }
    input.click()
  } catch (error: any) {
    console.error('导入失败:', error)
    message.error(error.message || t('common.import.failed'))
  }
}

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `菜单导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

onMounted(() => {
  initColumnSettings()
  getList()
})
</script>

<style lang="less" scoped>
.menu-container {
  height: 100%;
  padding: 16px;
  background-color: #f0f2f5;
  display: flex;
  flex-direction: column;

  :deep(.ant-table-wrapper) {
    flex: 1;
    margin-top: 16px;
    background-color: var(--ant-color-bg-container);

    .ant-spin-nested-loading {
      height: 100%;

      .ant-spin-container {
        height: 100%;
        display: flex;
        flex-direction: column;

        .ant-table {
          flex: 1;
          overflow: hidden;

          .ant-table-container {
            height: 100%;
          }
        }
      }
    }
  }
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  padding: 8px;
  border-bottom: 1px solid var(--ant-color-split);

  &:last-child {
    border-bottom: none;
  }
}
</style>
