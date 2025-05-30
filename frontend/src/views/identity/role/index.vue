<template>
  <div class="role-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <hbt-toolbar
      v-model:show-search="showSearch"
      :selected-count="selectedRowKeys.length"
      :show-selected-count="true"
      :show-add="true"
      :show-edit="true"
      :show-delete="true"
      :show-export="true"
      :show-import="true"
      :add-permission="['identity:role:create']"
      :edit-permission="['identity:role:update']"
      :delete-permission="['identity:role:delete']"
      :export-permission="['identity:role:export']"
      :import-permission="['identity:role:import']"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @export="handleExport"
      @import="handleImport"
      @template="handleTemplate"
      @column-setting="handleColumnSetting"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns.filter(col => columnSettings[col.key])"
      :pagination="false"
      :scroll="{ x: 600, y: 'calc(100vh - 500px)' }"
      :default-height="594"
      row-key="roleId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'status'">
          <a-switch
            :checked="record.status === 0"
            checked-children="正常"
            un-checked-children="停用"
            @change="val => handleStatusChange(record, !!val)"
            :loading="record.statusLoading"
            :disabled="record.roleKey === 'system_admin'"
          />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['identity:role:update']"
            :show-delete="record.roleKey !== 'system_admin'"
            :delete-permission="['identity:role:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          >
            <template #extra>
              <a-tooltip :title="t('identity.role.actions.authorize')">
                <a-button
                  v-hasPermi="['identity:role:allocate']"
                  :disabled="record.roleKey === 'system_admin'"
                  type="default"
                  size="small"
                  class="hbt-btn-menu"
                  @click.stop="handleAuthorize(record)"
                >
                  <template #icon><menu-outlined /></template>
                </a-button>
              </a-tooltip>
              <a-tooltip :title="t('identity.user.allocateDept')">
                <a-button
                  v-hasPermi="['identity:role:allocate']"
                  :disabled="record.roleKey === 'system_admin'"
                  type="default"
                  size="small"
                  class="hbt-btn-dept"
                  @click.stop="handleDeptAuthorize(record)"
                >
                  <template #icon><team-outlined /></template>
                </a-button>
              </a-tooltip>
            </template>
          </hbt-operation>
        </template>
      </template>
    </hbt-table>

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

    <!-- 角色表单弹窗 -->
    <role-form
      v-model:visible="formVisible"
      :title="formTitle"
      :role-id="formRoleId"
      @success="getList"
    />

    <!-- 菜单分配对话框 -->
    <menu-allocate
      v-model:visible="menuAllocateVisible"
      :role-id="selectedRoleId"
      @success="handleSuccess"
    />

    <!-- 部门分配对话框 -->
    <dept-allocate
      v-model:visible="deptAllocateVisible"
      :role-id="selectedRoleId"
      @success="handleSuccess"
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
        <div v-for="col in columns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, h, nextTick, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { useDictStore } from '@/stores/dict'
import type { HbtRole, HbtRoleQuery } from '@/types/identity/role'
import type { QueryField } from '@/types/components/query'
import { getRoleList, deleteRole, exportRole, updateRoleStatus, getTemplate, importRole } from '@/api/identity/role'
import RoleForm from './components/RoleForm.vue'
import MenuAllocate from './components/MenuAllocate.vue'
import DeptAllocate from './components/DeptAllocate.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 表格列定义
const columns = [
  {
    title: t('identity.role.fields.roleName.label'),
    dataIndex: 'roleName',
    key: 'roleName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.role.fields.roleKey.label'),
    dataIndex: 'roleKey',
    key: 'roleKey',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.role.fields.roleSort.label'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: t('identity.role.fields.dataScope.label'),
    dataIndex: 'dataScope',
    key: 'dataScope',
    width: 120
  },
  {
    title: t('identity.role.fields.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('identity.role.fields.description.label'),
    dataIndex: 'remark',
    key: 'remark',
    width: 200,
    ellipsis: true
  },
  {
    title: t('common.datetime.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('common.datetime.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('common.datetime.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('common.datetime.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('common.table.header.operation'),
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'roleName',
    label: t('identity.role.fields.roleName.label'),
    type: 'input' as const
  },
  {
    name: 'roleKey',
    label: t('identity.role.fields.roleKey.label'),
    type: 'input' as const
  },
  {
    name: 'status',
    label: t('identity.role.fields.status.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  }
]

// 查询参数
const queryParams = ref<HbtRoleQuery>({
  pageIndex: 1,
  pageSize: 10,
  roleName: '',
  roleKey: '',
  status: -1
})

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<HbtRole[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 表单对话框
const formVisible = ref(false)
const formTitle = ref('')
const selectedRoleId = ref<number>()
const formRoleId = ref<number>()

// 菜单分配弹窗
const menuAllocateVisible = ref(false)
// 部门分配弹窗
const deptAllocateVisible = ref(false)

// 列设置相关
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({})

// 初始化列设置
const initColumnSettings = () => {
  // 每次刷新页面时清除localStorage
  localStorage.removeItem('roleColumnSettings')

  // 初始化所有列为false
  columnSettings.value = Object.fromEntries(columns.map(col => [col.key, false]))

  // 获取前9列（不包含操作列）
  const firstNineColumns = columns.filter(col => col.key !== 'action').slice(0, 9)

  // 设置前9列为true
  firstNineColumns.forEach(col => {
    columnSettings.value[col.key] = true
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  columns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else {
      settings[col.key] = checkedValue.includes(col.key)
    }
  })
  columnSettings.value = settings
  localStorage.setItem('roleColumnSettings', JSON.stringify(settings))
}

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })
    const res = await getRoleList(queryParams.value)
    if (res.data.code === 200) {
      tableData.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
  loading.value = false
}

// 查询方法
const handleQuery = (values?: any) => {
  if (values) {
    Object.assign(queryParams.value, values)
  }
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置查询
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    roleName: '',
    roleKey: '',
    status: -1
  }
  fetchData()
}

// 表格变化
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current ?? 1
  queryParams.value.pageSize = pagination.pageSize ?? 10
  fetchData()
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('common.add')
  formRoleId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: HbtRole) => {
  formTitle.value = t('common.edit')
  formRoleId.value = record.roleId
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: HbtRole) => {
  try {
    const res = await deleteRole(record.roleId)
    if (res.data.code === 200) {
      message.success(t('common.delete.success'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
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
      const res = await importRole(file)
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
      if (success > 0) fetchData()
    }
    input.click()
  } catch (error: any) {
    console.error('导入失败:', error)
    message.error(error.message || t('common.import.failed'))
  }
}


// 处理导出
const handleExport = () => {
  message.info(t('common.developing'))
}

// 处理表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// 编辑选中记录
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.selectOne'))
    return
  }

  const record = tableData.value.find(item => String(item.roleId) === String(selectedRowKeys.value[0]))
  if (record) {
    formTitle.value = t('common.edit')
    formRoleId.value = record.roleId
    formVisible.value = true
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('common.selectAtLeastOne'))
    return
  }

  try {
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteRole(Number(id))))
    const hasError = results.some(res => res.data.code !== 200)
    if (!hasError) {
      message.success(t('common.delete.success'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
}

// 处理授权
const handleAuthorize = (record: HbtRole) => {
  if (record.roleKey === 'system_admin') return
  formTitle.value = t('identity.role.actions.authorize')
  selectedRoleId.value = record.roleId
  menuAllocateVisible.value = true
}

// 处理状态变化
const handleStatusChange = async (record: HbtRole, checked: boolean) => {
  // @ts-ignore
  record.statusLoading = true
  try {
    const newStatus = checked ? 0 : 1
    const res = await updateRoleStatus({ roleId: record.roleId, status: newStatus })
    if (res.data.code === 200) {
      record.status = newStatus
      message.success('状态更新成功')
    } else {
      message.error(res.data.msg || '状态更新失败')
    }
  } catch (error) {
    message.error('状态更新失败')
  }
  // @ts-ignore
  record.statusLoading = false
}

// 处理行点击
const handleRowClick = (record: HbtRole) => {
  console.log('行点击:', record)
}

// 分页处理
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

const handleSizeChange = (size: number) => {
  queryParams.value.pageSize = size
  fetchData()
}

// 处理部门授权
const handleDeptAuthorize = (record: HbtRole) => {
  if (record.roleKey === 'system_admin') return
  formTitle.value = t('identity.role.actions.deptAuthorize')
  selectedRoleId.value = record.roleId
  deptAllocateVisible.value = true
}

// 处理列表刷新
const getList = () => {
  fetchData()
}

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `角色导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['sys_normal_disable'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.role-container {
  padding: 16px;
  background-color: #fff;
}
</style>
