<template>
  <div class="menu-container">
    <!-- 搜索区域 -->
    <hbt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="handleReset"
    >
      <template #queryForm>
        <a-form-item :label="t('identity.menu.fields.menuName.label')">
          <a-input
            v-model:value="queryParams.menuName"
            :placeholder="t('identity.menu.fields.menuName.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.menu.fields.status.label')">
          <hbt-select
            v-model:value="queryParams.status"
            dict-type="sys_normal_disable"
            :placeholder="t('identity.menu.fields.status.placeholder')"
            allow-clear
          />
        </a-form-item>
      </template>
    </hbt-query>

    <!-- 工具栏 -->
    <hbt-toolbar
      v-model:show-search="showSearch"
      :selected-count="selectedKeys.length"
      :show-select-count="true"
      :show-add="true"
      :show-edit="true"
      :show-delete="true"
      :show-export="true"
      :add-permission="['identity:menu:create']"
      :edit-permission="['identity:menu:update']"
      :delete-permission="['identity:menu:delete']"
      :export-permission="['identity:menu:export']"
      :disabled-edit="selectedKeys.length !== 1"
      :disabled-delete="selectedKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @export="handleExport"
      @refresh="getList"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <a-table
      :columns="columns.filter(col => columnSettings[col.key])"
      :data-source="list"
      :loading="loading"
      :pagination="false"
      :row-selection="{
        selectedRowKeys: selectedKeys,
        onChange: onSelectChange
      }"
      row-key="menuId"
      size="middle"
      bordered
      :default-expand-all="true"
      :scroll="{ x: 1200, y: 'calc(100vh - 300px)' }"
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
    </a-table>

    <!-- 表单弹窗 -->
    <menu-tabs
      :visible="formVisible"
      :title="formTitle"
      :menu-id="formMenuId"
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
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, h, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import {
  FolderOutlined,
  MenuOutlined,
  ToolOutlined
} from '@ant-design/icons-vue'
import type { TableColumnsType } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { Menu, MenuQuery } from '@/types/identity/menu'
import { getMenuList, deleteMenu, batchDeleteMenu, exportMenu, getMenuTree } from '@/api/identity/menu'
import MenuTabs from './components/MenuTabs.vue'

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
    type: 'input',
    props: {
      placeholder: t('identity.menu.fields.menuName.placeholder'),
      allowClear: true
    }
  },
  {
    name: 'status',
    label: t('identity.menu.fields.status.label'),
    type: 'select',
    props: {
      dictType: 'sys_normal_disable',
      placeholder: t('identity.menu.fields.status.placeholder'),
      allowClear: true
    }
  }
]

// 查询参数
const queryParams = ref<MenuQuery>({
  pageIndex: 1,
  pageSize: 10,
  menuName: undefined,
  status: undefined
})

// 加载状态
const loading = ref(false)

// 菜单列表数据
const list = ref<Menu[]>([])

// 选中的菜单ID
const selectedKeys = ref<(string | number)[]>([])

// 总记录数
const total = ref(0)

// 表单弹窗显示状态
const formVisible = ref(false)

// 表单标题
const formTitle = ref('')

// 当前编辑的菜单ID
const formMenuId = ref<number>()

// 表格列定义（用于表格渲染）
const columns = defaultColumns

// 在 script 部分添加
const menuTabsRef = ref()

// 获取菜单列表
const getList = async () => {
  try {
    loading.value = true
    const { data } = await getMenuTree()
    if (data.code === 200) {
      list.value = data.data
    } else {
      message.error(data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('[菜单管理] 获取菜单列表出错:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 处理查询
const handleQuery = () => {
  selectedKeys.value = []
  getList()
}

// 处理重置
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    menuName: undefined,
    status: undefined
  }
  getList()
}

// 处理选择变化
const onSelectChange = (keys: (string | number)[], _: Menu[]) => {
  selectedKeys.value = keys
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('identity.menu.dialog.create')
  formMenuId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: Record<string, any>) => {
  formTitle.value = t('identity.menu.dialog.update')
  formMenuId.value = Number(record.menuId)
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: Record<string, any>) => {
  try {
    const { data } = await deleteMenu(Number(record.menuId))
    if (data.code === 200) {
      message.success(t('common.delete.success'))
      getList()
    } else {
      message.error(data.msg || t('common.delete.failed'))
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
        const { data } = await batchDeleteMenu(selectedKeys.value.map(id => Number(id)))
        if (data.code === 200) {
          message.success(t('common.delete.success'))
          selectedKeys.value = []
          getList()
        } else {
          message.error(data.msg || t('common.delete.failed'))
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
    background-color: #fff;
    
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
