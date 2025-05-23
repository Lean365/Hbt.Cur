<template>
  <div class="dept-container">
    <!-- 搜索区域 -->
    <hbt-query
      v-model:show="showSearch"
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
      :show-edit="true"
      :show-delete="true"
      :show-export="true"
      :show-import="true"
      :add-permission="['identity:dept:create']"
      :edit-permission="['identity:dept:update']"
      :delete-permission="['identity:dept:delete']"
      :export-permission="['identity:dept:export']"
      :import-permission="['identity:dept:import']"
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
      :columns="columns.filter(col => col.key && columnSettings[col.key])"
      :data-source="deptList"
      :loading="loading"
      :pagination="false"
      :row-selection="{
        selectedRowKeys: selectedKeys,
        onChange: onSelectChange
      }"
      v-model:expanded-row-keys="expandedRowKeys"
      :indent-size="20"
      :children-column-name="'children'"
      row-key="deptId"
      size="middle"
      bordered
      :scroll="{ x: 1200, y: 'calc(100vh - 300px)' }"
      :virtual="true"
      :lazy="true"
      @expand="onExpand"
      @load-data="handleLoadData"
    >
      <template #bodyCell="{ column, record }">
        <!-- 部门名称 -->
        <template v-if="column.dataIndex === 'deptName'">
          <span>
            <folder-outlined />
            {{ record.deptName }}
          </span>
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
            :edit-permission="['identity:dept:update']"
            :delete-permission="['identity:dept:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          />
        </template>
      </template>
    </hbt-tree-table>

    <!-- 部门表单弹窗 -->
    <dept-form
      v-model:visible="formVisible"
      :title="formTitle"
      :dept-id="formDeptId"
      @success="getList"
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

    <!-- 分页组件 -->
    <hbt-pagination
      v-model:current="queryParams.pageIndex"
      v-model:pageSize="queryParams.pageSize"
      :total="total"
      :show-size-changer="true"
      :show-quick-jumper="true"
      :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import { FolderOutlined, ExpandOutlined, TeamOutlined } from '@ant-design/icons-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtDept, HbtDeptQuery } from '@/types/identity/dept'
import { getDeptTree, deleteDept, batchDeleteDept, exportDept, importDept, getTemplate } from '@/api/identity/dept'
import DeptForm from './components/DeptForm.vue'


const { t } = useI18n()

// 查询区域显示状态
const showSearch = ref(true)

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'deptName',
    label: t('identity.dept.fields.deptName.label'),
    placeholder: t('identity.dept.fields.deptName.placeholder'),
    type: 'input',
    props: {
     
      allowClear: true
    }
  },
  {
    name: 'status',
    label: t('identity.dept.fields.status.label'),
    placeholder: t('identity.dept.fields.status.placeholder'),
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
const queryParams = ref<HbtDeptQuery>({
  pageIndex: 1,
  pageSize: 10,
  deptName: undefined,
  status: -1
})

// 加载状态
const loading = ref(false)

// 部门列表数据
const deptList = ref<HbtDept[]>([])

// 选中的部门ID
const selectedKeys = ref<(string | number)[]>([])

// 表单弹窗显示状态
const formVisible = ref(false)

// 表单标题
const formTitle = ref('')

// 当前编辑的部门ID
const formDeptId = ref<number>()

// 表格列定义
interface TableColumnsType {
  title: string
  dataIndex?: string
  key: string
  width?: number
  ellipsis?: boolean
  fixed?: 'left' | 'right'
}

// 表格列定义
const columns: TableColumnsType[] = [
  {
    title: t('identity.dept.fields.deptName.label'),
    dataIndex: 'deptName',
    key: 'deptName',
    width: 200,
    ellipsis: true
  },
  {
    title: t('identity.dept.fields.orderNum.label'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.dept.fields.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.dept.fields.leader.label'),
    dataIndex: 'leader',
    key: 'leader',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.dept.fields.phone.label'),
    dataIndex: 'phone',
    key: 'phone',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.dept.fields.email.label'),
    dataIndex: 'email',
    key: 'email',
    width: 180,
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
    title: t('common.table.header.operation'),
    dataIndex: 'action',
    key: 'action',
    width: 150,
    fixed: 'right',
    ellipsis: true
  }
]

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns
const columnSettings = ref<Record<string, boolean>>({})

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('userColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = defaultColumns.filter(col => col.key !== 'action').slice(0, 9)

  // 设置前9列为true
  firstNineColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 总数
const total = ref(0)

// 处理分页变化
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  getList()
}

// 处理每页条数变化
const handleSizeChange = (size: number) => {
  queryParams.value.pageSize = size
  getList()
}

// 获取部门列表
const getList = async () => {
  try {
    loading.value = true
    const res = await getDeptTree(queryParams.value)
    if (res.data.code === 200) {
      deptList.value = res.data.data
      total.value = res.data.data.length
      // 如果当前是展开状态，更新展开的节点
      if (isExpanded.value) {
        expandedRowKeys.value = getAllKeys(deptList.value)
      } else {
        expandedRowKeys.value = []
      }
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error: any) {
    console.error('[部门管理] 获取部门列表出错:', error)
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

// 处理查询
const handleQuery = (values?: any) => {
  if (values) {
    Object.assign(queryParams.value, values)
  }
  getList()
}

// 处理重置
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    deptName: undefined,
    status: -1
  }
  getList()
}

// 处理选择变化
const onSelectChange = (keys: (string | number)[], _: HbtDept[]) => {
  selectedKeys.value = keys
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('identity.dept.dialog.create')
  formDeptId.value = undefined
  formVisible.value = true
}

// 处理编辑选中项
const handleEditSelected = () => {
  const deptId = selectedKeys.value[0]
  formTitle.value = t('identity.dept.dialog.update')
  formDeptId.value = Number(deptId)
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: Record<string, any>) => {
  formTitle.value = t('identity.dept.dialog.update')
  formDeptId.value = Number(record.deptId)
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: Record<string, any>) => {
  try {
    const res = await deleteDept(Number(record.deptId))
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
        const res = await batchDeleteDept(selectedKeys.value.map(id => Number(id)))
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

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('userColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportDept({
      deptName: queryParams.value.deptName,
      status: queryParams.value.status,
      //sheetName: '部门信息',
      pageIndex: 1,
      pageSize: 10
    })

    // 检查响应类型
    if (res.data instanceof Blob) {
      const blob = new Blob([res.data], {
        type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
      })
      const url = window.URL.createObjectURL(blob)
      const link = document.createElement('a')
      link.href = url
      link.download = `部门列表_${new Date().getTime()}.xlsx`
      document.body.appendChild(link)
      link.click()
      document.body.removeChild(link)
      window.URL.revokeObjectURL(url)
      message.success(t('common.export.success'))
    } else {
      message.error(t('common.export.failed'))
    }
  } catch (error: any) {
    console.error('导出部门失败:', error)
    if (error.response?.status === 500) {
      message.error(t('identity.dept.messages.exportFailed'))
    } else {
      message.error(error.response?.data?.msg || t('common.export.failed'))
    }
  }
}

const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

const toggleFullscreen = () => {
  // 处理全屏切换
  console.log('全屏切换')
}

const treeTableRef = ref()
const isExpanded = ref(false)
const expandedRowKeys = ref<(string | number)[]>([])

// 递归获取所有节点的ID
const getAllKeys = (data: HbtDept[]): (string | number)[] => {
  return data.flatMap(item => [
    item.deptId,
    ...(item.children ? getAllKeys(item.children) : [])
  ])
}

// 处理展开/收缩事件
const onExpand = (expanded: boolean, record: HbtDept) => {
  isExpanded.value = expandedRowKeys.value.length > 0
}

// 切换展开/收缩
const toggleExpand = () => {
  isExpanded.value = !isExpanded.value
  if (isExpanded.value) {
    treeTableRef.value?.expandAll()
  } else {
    treeTableRef.value?.collapseAll()
  }
}

// 处理懒加载数据
const handleLoadData = async (record: HbtDept) => {
  try {
    loading.value = true
    const res = await getDeptTree({
      ...queryParams.value,
      parentId: record.deptId
    })
    if (res.data.code === 200) {
      // 更新子节点数据
      record.children = res.data.data
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error: any) {
    console.error('[部门管理] 加载子节点数据出错:', error)
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
      const res = await importDept(file)
      const { success = 0, fail = 0 } = (res.data as any).Data || {}
      console.log(
        'fail:',
        (res.data as any).Data?.fail,
        'success:',
        (res.data as any).Data?.success
      )

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
    link.download = `部门导入模板_${new Date().getTime()}.xlsx`
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

// 将方法暴露给模板
defineExpose({
  onExpand
})
</script>

<style lang="less" scoped>
.dept-container {
  padding: 16px;
  background-color: #fff;
  height: 100%;
  display: flex;
  flex-direction: column;

  .ant-table-wrapper {
    flex: 1;
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
