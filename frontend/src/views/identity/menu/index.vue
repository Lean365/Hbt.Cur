<template>
  <div class="menu-container">
    <!-- 搜索区域 -->
    <hbt-query
      v-model:show="showSearch"
      :model="queryParams"
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
    />

    <!-- 数据表格 -->
    <a-table
      :columns="columns"
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
            :show-authorize="true"
            :edit-permission="['identity:menu:update']"
            :delete-permission="['identity:menu:delete']"
            :authorize-permission="['identity:menu:allocate']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @authorize="handleAuthorize"
          />
        </template>
      </template>
    </a-table>

    <!-- 表单弹窗 -->
    <menu-form
      v-model:visible="formVisible"
      :title="formTitle"
      :menu-id="formMenuId"
      @success="getList"
    />

    <!-- 权限分配对话框 -->
    <menu-allocate
      v-model:visible="menuAllocateVisible"
      :menu-id="formMenuId"
      @success="getList"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import {
  SearchOutlined,
  RedoOutlined,
  PlusOutlined,
  DeleteOutlined,
  DownloadOutlined,
  FolderOutlined,
  MenuOutlined,
  ToolOutlined
} from '@ant-design/icons-vue'
import type { TableColumnsType } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { Menu, MenuQuery } from '@/types/identity/menu'
import { getMenuList, deleteMenu, batchDeleteMenu, updateMenuStatus, exportMenu, getMenuTree } from '@/api/identity/menu'
import MenuForm from './components/MenuForm.vue'
import MenuAllocate from './components/MenuAllocate.vue'

const { t } = useI18n()

// 查询区域显示状态
const showSearch = ref(true)

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

// 权限分配对话框显示状态
const menuAllocateVisible = ref(false)

// 表格列定义
const columns: TableColumnsType = [
  {
    title: t('identity.menu.fields.menuName.label'),
    dataIndex: 'menuName',
    width: 220
  },
  {
    title: t('identity.menu.fields.icon.label'),
    dataIndex: 'icon',
    width: 100
  },
  {
    title: t('identity.menu.fields.orderNum.label'),
    dataIndex: 'orderNum',
    width: 80
  },
  {
    title: t('identity.menu.fields.permission.label'),
    dataIndex: 'perms',
    width: 150
  },
  {
    title: t('identity.menu.fields.component.label'),
    dataIndex: 'component',
    width: 150
  },
  {
    title: t('identity.menu.fields.status.label'),
    dataIndex: 'status',
    width: 100
  },
  {
    title: t('common.createTime'),
    dataIndex: 'createTime',
    width: 180
  },
  {
    title: t('common.action'),
    dataIndex: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 获取菜单列表
const getList = async () => {
  try {
    loading.value = true
    const res = await getMenuTree()
    if (res.code === 200) {
      list.value = res.data
    } else {
      message.error(res.msg || t('common.failed'))
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
    const res = await deleteMenu(Number(record.menuId))
    if (res.code === 200) {
      message.success(t('common.delete.success'))
      getList()
    } else {
      message.error(res.msg || t('common.delete.failed'))
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
        if (res.code === 200) {
          message.success(t('common.delete.success'))
          selectedKeys.value = []
          getList()
        } else {
          message.error(res.msg || t('common.delete.failed'))
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
    const res = await exportMenu(queryParams.value)
    const blob = new Blob([res], { type: 'application/vnd.ms-excel' })
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
  const record = list.value.find(item => String(item.menuId) === String(selectedKeys.value[0]))
  if (record) {
    handleEdit(record)
  }
}

// 处理授权
const handleAuthorize = (record: Menu) => {
  formTitle.value = t('identity.menu.actions.authorize')
  formMenuId.value = Number(record.menuId)
  menuAllocateVisible.value = true
}

// 初始化
onMounted(() => {
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
</style>
