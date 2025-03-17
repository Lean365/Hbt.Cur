<template>
  <div class="user-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    >
      <template #queryForm>
        <a-form-item :label="t('identity.user.userName.label')">
          <a-input
            v-model:value="queryParams.userName"
            :placeholder="t('identity.user.userName.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.user.nickName.label')">
          <a-input
            v-model:value="queryParams.nickName"
            :placeholder="t('identity.user.nickName.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.user.phoneNumber.label')">
          <a-input
            v-model:value="queryParams.phoneNumber"
            :placeholder="t('identity.user.phoneNumber.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.user.email.label')">
          <a-input
            v-model:value="queryParams.email"
            :placeholder="t('identity.user.email.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.user.userType.label')">
          <hbt-select
            v-model:value="queryParams.userType"
            dict-type="sys_user_type"
            type="radio"
            :placeholder="t('identity.user.userType.placeholder')"
            allow-clear
          />
        </a-form-item>
        <a-form-item :label="t('identity.user.gender.label')">
          <hbt-select
            v-model:value="queryParams.gender"
            dict-type="sys_gender"
            type="radio"
            :placeholder="t('identity.user.gender.placeholder')"
            allow-clear
          />
        </a-form-item>
        <a-form-item :label="t('identity.user.status.label')">
          <hbt-select
            v-model:value="queryParams.status"
            dict-type="sys_normal_disable"
            type="radio"
            :placeholder="t('common.status.placeholder')"
            allow-clear
          />
        </a-form-item>
      </template>
    </hbt-query>

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
      :pagination="{
        total: total,
        current: queryParams.pageIndex,
        pageSize: queryParams.pageSize,
        showSizeChanger: true,
        showQuickJumper: true,
        showTotal: (total: number) => `共 ${total} 条`
      }"
      :row-key="(record: User) => String(record.userId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"     
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 用户类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'userType'">
          <hbt-dict-tag dict-type="sys_user_type" :value="record.userType" />
        </template>
        
        <!-- 性别列 -->
        <template v-if="column.dataIndex === 'gender'">
          <hbt-dict-tag dict-type="sys_gender" :value="record.gender" />
        </template>
        
        <!-- 状态列 -->
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['identity:user:update']"
            :show-delete="true"
            :delete-permission="['identity:user:delete']"
            :show-authorize="true"
            :authorize-permission="['identity:user:allocate']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
            @authorize="handleAuthorize"
          >
            <!-- 重置密码按钮 -->
            <template #extra>
              <a-tooltip :title="t('identity.user.resetPwd')">
                <a-button
                  v-hasPermi="['identity:user:resetPwd']"
                  type="link"
                  size="small"
                  @click.stop="handleResetPassword(record)"
                >
                  <template #icon><key-outlined /></template>
                </a-button>
              </a-tooltip>
            </template>
          </hbt-operation>
        </template>
      </template>
    </hbt-table>

    <!-- 用户表单对话框 -->
    <user-form
      v-model:visible="formVisible"
      :title="formTitle"
      :user-id="selectedUserId"
      @success="handleSuccess"
    />

    <!-- 重置密码表单对话框 -->
    <reset-pwd-form
      v-model:visible="resetPwdVisible"
      :user-id="selectedUserId"
      @success="handleResetPwdSuccess"
    />

    <!-- 角色分配对话框 -->
    <role-allocate
      v-model:visible="roleAllocateVisible"
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
          <a-checkbox :value="col.key">{{ col.title }}</a-checkbox>
        </div>
      </a-checkbox-group>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { useDictStore } from '@/stores/dict'
import type { User, UserQuery } from '@/types/identity/user'
import type { QueryField } from '@/types/components/query'
import { getPagedList, deleteUser } from '@/api/identity/user'
import UserForm from './components/UserForm.vue'
import ResetPwdForm from './components/ResetPwdForm.vue'
import RoleAllocate from './components/RoleAllocate.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 查询字段类型定义
type FieldType = 'input' | 'select' | 'date' | 'dateRange' | 'number' | 'radio' | 'checkbox' | 'cascader'

// 表格列定义
const columns = [
  {
    title: t('identity.user.userName.label'),
    dataIndex: 'userName',
    key: 'userName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.user.nickName.label'),
    dataIndex: 'nickName',
    key: 'nickName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.user.englishName.label'),
    dataIndex: 'englishName',
    key: 'englishName',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.user.userType.label'),
    dataIndex: 'userType',
    key: 'userType',
    width: 100
  },
  {
    title: t('identity.user.email.label'),
    dataIndex: 'email',
    key: 'email',
    width: 180,
    ellipsis: true
  },
  {
    title: t('identity.user.phoneNumber.label'),
    dataIndex: 'phoneNumber',
    key: 'phoneNumber',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.user.gender.label'),
    dataIndex: 'gender',
    key: 'gender',
    width: 80
  },
  {
    title: t('identity.user.avatar.label'),
    dataIndex: 'avatar',
    key: 'avatar',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.user.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('identity.user.lastPasswordChangeTime.label'),
    dataIndex: 'lastPasswordChangeTime',
    key: 'lastPasswordChangeTime',
    width: 180,
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
    title: t('common.table.header.operation'),
    key: 'action',
    width: 150,
    fixed: 'right'
  }
]

// 查询参数
const queryParams = ref<UserQuery>({
  pageIndex: 1,
  pageSize: 10,
  userName: '',
  nickName: '',
  phoneNumber: '',
  email: '',
  gender: undefined,
  status: undefined,
  userType: undefined,
  deptId: undefined
})

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userName',
    label: t('identity.user.userName.label'),
    type: 'input' as const
  },
  {
    name: 'nickName',
    label: t('identity.user.nickName.label'),
    type: 'input' as const
  },
  {
    name: 'phoneNumber',
    label: t('identity.user.phoneNumber.label'),
    type: 'input' as const
  },
  {
    name: 'email',
    label: t('identity.user.email.label'),
    type: 'input' as const
  },
  {
    name: 'userType',
    label: t('identity.user.userType.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_user_type',
      type: 'radio'
    }
  },
  {
    name: 'gender',
    label: t('identity.user.gender.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_gender',
      type: 'radio'
    }
  },
  {
    name: 'status',
    label: t('identity.user.status.label'),
    type: 'select' as const,
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio'
    }
  }
]

// 表格相关
const loading = ref(false)
const total = ref(0)
const tableData = ref<User[]>([])
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

// 表单对话框
const formVisible = ref(false)
const formTitle = ref('')
const selectedUserId = ref<number>()

// 重置密码表单
const resetPwdVisible = ref(false)

// 角色分配弹窗
const roleAllocateVisible = ref(false)

// 列设置相关
const columnSettingVisible = ref(false)
const defaultColumns = columns

// 初始化列设置
const initColumnSettings = () => {
  const savedSettings = localStorage.getItem('userColumnSettings')
  if (savedSettings) {
    columnSettings.value = JSON.parse(savedSettings)
  } else {
    columnSettings.value = Object.fromEntries(defaultColumns.map(col => [col.key, true]))
  }
}
const columnSettings = ref<Record<string, boolean>>({})

// 获取表格数据
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getPagedList(queryParams.value)
    if (res.code === 200) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
  loading.value = false
}

// 查询方法
const handleQuery = () => {
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
    gender: undefined,
    status: undefined,
    userType: undefined,
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
  formTitle.value = t('common.add')
  selectedUserId.value = undefined
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: User) => {
  formTitle.value = t('common.edit')
  selectedUserId.value = record.userId
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: User) => {
  try {
    const res = await deleteUser(record.userId)
    if (res.code === 200) {
      message.success(t('common.delete.success'))
      fetchData()
    } else {
      message.error(res.msg || t('common.delete.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.delete.failed'))
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
  
  const record = tableData.value.find(item => String(item.userId) === String(selectedRowKeys.value[0]))
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
  
  try {
    const results = await Promise.all(selectedRowKeys.value.map(id => deleteUser(Number(id))))
    const hasError = results.some(res => res.code !== 200)
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

// 列设置
const handleColumnSetting = () => {
  columnSettingVisible.value = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    settings[col.key] = checkedValue.includes(col.key)
  })
  columnSettings.value = settings
  localStorage.setItem('userColumnSettings', JSON.stringify(settings))
}

// 处理行点击
const handleRowClick = (record: User) => {
  console.log('行点击:', record)
}

// 处理重置密码
const handleResetPassword = (record: User) => {
  selectedUserId.value = record.userId
  resetPwdVisible.value = true
}

// 处理重置密码成功
const handleResetPwdSuccess = () => {
  resetPwdVisible.value = false
  message.success(t('identity.user.messages.resetPasswordSuccess'))
}

// 处理导入
const handleImport = () => {
  message.info(t('common.developing'))
}

// 处理下载模板
const handleTemplate = () => {
  message.info(t('common.developing'))
}

// 处理授权
const handleAuthorize = (record: User) => {
  selectedUserId.value = record.userId
  roleAllocateVisible.value = true
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
  padding: 24px;
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
