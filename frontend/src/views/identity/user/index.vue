<template>
  <div class="user-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['identity:user:create']"
      :show-edit="true"
      :edit-permission="['identity:user:update']"
      :show-delete="true"
      :delete-permission="['identity:user:delete']"
      :show-import="true"
      :import-permission="['identity:user:import']"
      :show-export="true"
      :export-permission="['identity:user:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @import="handleImport"
      @template="handleTemplate"
      @export="handleExport"
      @refresh="fetchData"
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
      row-key="userId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 头像列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'avatar'">
          <a-image
            :src="getAvatarUrl(record.avatar)"
            :width="32"
            :height="32"
            :preview="{
              src: getAvatarUrl(record.avatar)
            }"
            style="border-radius: 50%; object-fit: cover"
          />
        </template>

        <!-- 用户类型列 -->
        <template v-if="column.dataIndex === 'userType'">
          <hbt-dict-tag dict-type="sys_user_type" :value="record.userType" />
        </template>

        <!-- 性别列 -->
        <template v-if="column.dataIndex === 'gender'">
          <hbt-dict-tag dict-type="sys_gender" :value="record.gender" />
        </template>

        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <a-switch
            :checked="record.status === 0"
            checked-children="正常"
            un-checked-children="停用"
            @change="val => handleStatusChange(record, !!val)"
            :loading="record.statusLoading"
            :disabled="record.userName === 'admin'"
          />
        </template>

        <!-- 锁定时间列 -->
        <template v-if="column.dataIndex === 'lockEndTime'">
          {{ record.lockEndTime ? new Date(record.lockEndTime).toLocaleString() : '未锁定' }}
        </template>

        <!-- 锁定原因列 -->
        <template v-if="column.dataIndex === 'lockReason'">
          {{ record.lockReason || '无' }}
        </template>

        <!-- 锁定状态列 -->
        <template v-if="column.dataIndex === 'isLock'">
          <a-tag :color="record.isLock === 1 ? 'red' : 'green'">
            {{
              record.isLock === 1
                ? t('common.status.base.disabled')
                : t('common.status.base.normal')
            }}
          </a-tag>
        </template>

        <!-- 错误次数列 -->
        <template v-if="column.dataIndex === 'errorLimit'">
          {{ record.errorLimit || '无' }}
        </template>

        <!-- 登录次数列 -->
        <template v-if="column.dataIndex === 'loginCount'">
          {{ record.loginCount || '无' }}
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['identity:user:update']"
            :show-delete="record.userName !== 'admin'"
            :delete-permission="['identity:user:delete']"
            :show-authorize="true"
            :authorize-permission="['identity:user:allocate']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @authorize="handleAuthorize"
          >
            <template #extra>
              <a-dropdown>
                <a-button>
                  {{ t('common.actions.more') }}
                  <DownOutlined />
                </a-button>
                <template #overlay>
                  <a-menu>
                    <a-tooltip
                      v-if="record.userName !== 'admin'"
                      :title="t('identity.user.resetPwd')"
                    >
                      <a-button
                        v-hasPermi="['identity:user:resetpwd']"
                        type="default"
                        size="small"
                        class="hbt-btn-reset"
                        @click.stop="handleResetPassword(record)"
                      >
                        <template #icon><key-outlined /></template>
                      </a-button>
                    </a-tooltip>
                    <a-tooltip :title="t('identity.user.allocateDept')">
                      <a-button
                        v-hasPermi="['identity:user:allocate']"
                        type="default"
                        size="small"
                        class="hbt-btn-dept"
                        @click.stop="handleAllocateDept(record)"
                      >
                        <template #icon><team-outlined /></template>
                      </a-button>
                    </a-tooltip>
                    <a-tooltip :title="t('identity.user.allocatePost')">
                      <a-button
                        v-hasPermi="['identity:user:allocate']"
                        type="default"
                        size="small"
                        class="hbt-btn-post"
                        @click.stop="handleAllocatePost(record)"
                      >
                        <template #icon><solution-outlined /></template>
                      </a-button>
                    </a-tooltip>
                  </a-menu>
                </template>
              </a-dropdown>
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
      :show-total="(total: number, range: [number, number]) => h('span', null, t('table.pagination.total', { total }))"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 用户表单对话框 -->
    <user-form
      v-model:visible="userFormVisible"
      :title="formTitle"
      :user-id="selectedUserId"
      @success="handleSuccess"
    />

    <!-- 重置密码表单对话框 -->
    <reset-pwd-form
      v-model:visible="resetPwdVisible"
      :user-id="selectedUserId!"
      @success="handleResetPwdSuccess"
    />

    <!-- 角色分配对话框 -->
    <role-allocate
      v-model:visible="roleDialogVisible"
      :user-id="selectedUserId!"
      @success="handleSuccess"
    />

    <!-- 部门分配对话框 -->
    <dept-allocate
      v-if="deptDialogVisible && selectedUserId !== undefined"
      v-model:visible="deptDialogVisible"
      :user-id="selectedUserId"
      @success="handleSuccess"
    />

    <!-- 岗位分配对话框 -->
    <post-allocate
      v-if="postDialogVisible && selectedUserId !== undefined"
      v-model:visible="postDialogVisible"
      :user-id="selectedUserId"
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
        <div v-for="col in defaultColumns" :key="col.key" class="column-setting-item">
          <a-checkbox :value="col.key" :disabled="col.key === 'action'">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ref, computed, onMounted, h, nextTick, watch } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { useDictStore } from '@/stores/dict'
import type { HbtUser, HbtUserQuery } from '@/types/identity/user'
import type { QueryField } from '@/types/components/query'
import {
  getUserList,
  deleteUser,
  exportUser,
  importUser,
  getTemplate,
  getUser,
  createUser,
  updateUser,
  resetPassword,
  updateUserStatus
} from '@/api/identity/user'
import UserForm from './components/UserForm.vue'
import ResetPwdForm from './components/ResetPwdForm.vue'
import RoleAllocate from './components/RoleAllocate.vue'
import DeptAllocate from './components/DeptAllocate.vue'
import PostAllocate from './components/PostAllocate.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 获取头像完整URL
const getAvatarUrl = (avatar: string | null) => {
  const baseUrl = 'https://localhost:7249'
  return baseUrl + (avatar || '/avatar/default.gif')
}

// 查询字段类型定义
type FieldType =
  | 'input'
  | 'select'
  | 'date'
  | 'dateRange'
  | 'number'
  | 'radio'
  | 'checkbox'
  | 'cascader'

// 表格列定义
interface TableColumn {
  title: string
  dataIndex?: string
  key: string
  width?: number
  ellipsis?: boolean
  fixed?: string
}

const columns: TableColumn[] = [
  {
    title: 'ID',
    dataIndex: 'userId',
    key: 'userId',
    width: 80,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.userName'),
    dataIndex: 'userName',
    key: 'userName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.nickName'),
    dataIndex: 'nickName',
    key: 'nickName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.englishName'),
    dataIndex: 'englishName',
    key: 'englishName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.userType'),
    dataIndex: 'userType',
    key: 'userType',
    width: 100
  },
  {
    title: t('identity.user.table.columns.email'),
    dataIndex: 'email',
    key: 'email',
    width: 180,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.phoneNumber'),
    dataIndex: 'phoneNumber',
    key: 'phoneNumber',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.gender'),
    dataIndex: 'gender',
    key: 'gender',
    width: 80
  },
  {
    title: t('identity.user.table.columns.avatar'),
    dataIndex: 'avatar',
    key: 'avatar',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('identity.user.table.columns.lockEndTime'),
    dataIndex: 'lockEndTime',
    key: 'lockEndTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.lockReason'),
    dataIndex: 'lockReason',
    key: 'lockReason',
    width: 180,
    ellipsis: true
  },
  {
    title: t('identity.user.table.columns.isLock'),
    dataIndex: 'isLock',
    key: 'isLock',
    width: 100
  },
  {
    title: t('identity.user.table.columns.errorLimit'),
    dataIndex: 'errorLimit',
    key: 'errorLimit',
    width: 120
  },
  {
    title: t('identity.user.table.columns.loginCount'),
    dataIndex: 'loginCount',
    key: 'loginCount',
    width: 120
  },
  {
    title: t('identity.user.table.columns.lastPasswordChangeTime'),
    dataIndex: 'lastPasswordChangeTime',
    key: 'lastPasswordChangeTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120
  },
  {
    title: t('table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120
  },
  {
    title: t('table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  },
  {
    title: t('table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120
  },
  {
    title: t('table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180
  },
  {
    title: t('table.columns.deleteBy'),
    dataIndex: 'deleteBy',
    key: 'deleteBy',
    width: 120
  },
  {
    title: t('table.columns.deleteTime'),
    dataIndex: 'deleteTime',
    key: 'deleteTime',
    width: 180
  },
  {
    title: t('table.columns.operation'),
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 查询参数
const queryParams = ref<HbtUserQuery>({
  pageIndex: 1,
  pageSize: 10,
  userName: '',
  nickName: '',
  phoneNumber: '',
  email: '',
  gender: -1,
  status: -1,
  userType: -1,
  deptId: undefined
})

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userName',
    label: t('identity.user.table.columns.userName'),
    type: 'input' as const,
  },
  {
    name: 'nickName',
    label: t('identity.user.table.columns.nickName'),
    type: 'input' as const
  },
  {
    name: 'phoneNumber',
    label: t('identity.user.table.columns.phoneNumber'),
    type: 'input' as const
  },
  {
    name: 'email',
    label: t('identity.user.table.columns.email'),
    type: 'input' as const
  },
  {
    name: 'userType',
    label: t('identity.user.table.columns.userType'),
    type: 'select' as const,
    props: {
      dictType: 'sys_user_type',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'gender',
    label: t('identity.user.table.columns.gender'),
    type: 'select' as const,
    props: {
      dictType: 'sys_gender',
      type: 'radio',
      showAll: true
    }
  },
  {
    name: 'status',
    label: t('identity.user.table.columns.status'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  }
]

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<HbtUser[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 表单对话框
const userFormVisible = ref(false)
const formTitle = ref('')
const selectedUserId = ref<number>()

// 重置密码表单
const resetPwdVisible = ref(false)

// 角色分配弹窗
const roleDialogVisible = ref(false)

// 部门分配弹窗
const deptDialogVisible = ref(false)

// 岗位分配弹窗
const postDialogVisible = ref(false)

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

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    console.log('查询参数:', {
      ...queryParams.value
    })

    const res = await getUserList(queryParams.value)
    console.log('res:', res)
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
    userName: '',
    nickName: '',
    phoneNumber: '',
    email: '',
    gender: -1,
    status: -1,
    userType: -1,
    deptId: undefined
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
  formTitle.value = t('common.actions.add')
  selectedUserId.value = undefined
  userFormVisible.value = true
}

// 处理编辑
const handleEdit = (record: HbtUser) => {
  formTitle.value = t('common.actions.edit')
  selectedUserId.value = record.userId
  userFormVisible.value = true
}

// 处理删除
const handleDelete = async (record: HbtUser) => {
  if (record.userName === 'admin') {
    message.warning('不能删除超级管理员账号！')
    return
  }
  try {
    const res = await deleteUser(record.userId)
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

// 处理导出
const handleExport = async () => {
  try {
    const res = await exportUser({
      ...queryParams.value
      //sheetName: '用户信息'
    })
    // 动态获取文件名
    console.log('res.headers', res.headers)
    const disposition =
      res.headers && (res.headers['content-disposition'] || res.headers['Content-Disposition'])
    console.log('disposition', disposition)
    let fileName = ''
    if (disposition) {
      // 优先匹配 filename*（带中文）
      let match = disposition.match(/filename\*=UTF-8''([^;]+)/)
      if (match && match[1]) {
        fileName = decodeURIComponent(match[1])
      } else {
        // 再匹配 filename
        match = disposition.match(/filename="?([^";]+)"?/)
        if (match && match[1]) {
          fileName = match[1]
        }
      }
    }
    if (!fileName) {
      fileName = `用户数据_${new Date().getTime()}.xlsx`
    }
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(res.data)
    link.download = fileName
    link.click()
    window.URL.revokeObjectURL(link.href)
    message.success(t('common.export.success'))
  } catch (error: any) {
    console.error('导出失败:', error)
    message.error(error.message || t('common.export.failed'))
  }
}

// 处理表单提交成功
const handleSuccess = () => {
  userFormVisible.value = false
  fetchData()
}

// 切换搜索显示
const toggleSearch = (visible: boolean) => {
  showSearch.value = visible
}

// 切换全屏
const toggleFullscreen = (isFullscreen: boolean) => {
  console.log('切换全屏状态:', isFullscreen)
}

// 编辑选中记录
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.selectOne'))
    return
  }

  const record = tableData.value.find(
    item => String(item.userId) === String(selectedRowKeys.value[0])
  )
  if (record) {
    handleEdit(record)
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning(t('common.selectAtLeastOne'))
    return
  }

  // 判断是否包含 admin 用户
  const selectedUsers = tableData.value.filter(item =>
    selectedRowKeys.value.includes(String(item.userId))
  )
  console.log('selectedUsers:', selectedUsers)
  if (selectedUsers.some(user => user.userName === 'admin')) {
    message.warning('不能删除超级管理员账号！')
    return
  }

  // 只有不包含 admin 时才会执行下面的删除逻辑
  try {
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteUser(Number(id))))
    const hasError = results.some(res => res.data.code !== 200)
    if (!hasError) {
      message.success(t('common.delete.success'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      const firstError = results.find(res => res.data.code !== 200)
      message.error(firstError?.data.msg || t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
  }
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

// 处理行点击
const handleRowClick = (record: HbtUser) => {
  console.log('行点击:', record)
}

// 处理重置密码
const handleResetPassword = (record: HbtUser) => {
  // 这里假设有resetPassword API，传递userId和默认密码
  resetPassword({ userId: record.userId, password: 'Hbt@123852' })
    .then(res => {
      if (res) {
        message.success(t('identity.user.messages.resetPasswordSuccess'))
      } else {
        message.error(t('common.failed'))
      }
    })
    .catch(error => {
      console.error(error)
      message.error(t('common.failed'))
    })
}

// 处理重置密码成功
const handleResetPwdSuccess = () => {
  resetPwdVisible.value = false
  message.success(t('identity.user.messages.resetPasswordSuccess'))
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
      const res = await importUser(file)
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

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await getTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `用户导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
  }
}

// 处理授权
const handleAuthorize = (record: HbtUser) => {
  selectedUserId.value = record.userId
  roleDialogVisible.value = true
}

// 处理分配部门
const handleAllocateDept = (record: HbtUser) => {
  selectedUserId.value = record.userId
  deptDialogVisible.value = true
}

// 处理分配岗位
const handleAllocatePost = (record: HbtUser) => {
  selectedUserId.value = record.userId
  postDialogVisible.value = true
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

// 处理状态变化
const handleStatusChange = async (record: HbtUser, checked: boolean) => {
  // @ts-ignore
  record.statusLoading = true
  try {
    const newStatus = checked ? 0 : 1
    const res = await updateUserStatus({ userId: record.userId, status: newStatus })
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

onMounted(() => {
  // 加载字典数据
  dictStore.loadDicts(['sys_normal_disable', 'sys_gender', 'sys_user_type'])
  // 初始化列设置
  initColumnSettings()
  // 加载表格数据
  fetchData()
})
</script>

<style lang="less" scoped>
.user-container {
  height: 100%;
  background-color: #fff;
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
