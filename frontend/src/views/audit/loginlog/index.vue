<template>
  <div class="login-log-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    />

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-refresh="true"
      :show-export="true"
      :show-delete="true"
      :delete-permission="['audit:loginlog:delete']"
      :export-permission="['audit:loginlog:export']"
      :disabled-delete="selectedRowKeys.length === 0"
      @refresh="fetchData"
      @export="handleExport"
      @delete="handleBatchDelete"
      @toggle-search="toggleSearch"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns"
      :pagination="{
        total: total,
        current: queryParams.pageIndex,
        pageSize: queryParams.pageSize,
        showSizeChanger: true,
        showQuickJumper: true,
        showTotal: (total: number) => `共 ${total} 条`
      }"
      :row-key="(record: HbtLoginLogDto) => record.id"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      :scroll="{ x: 1200 }"
      @change="handleTableChange"
    >
      <!-- 登录状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'success'">
          <a-tag :color="record.success ? 'success' : 'error'">
            {{ record.success ? '成功' : '失败' }}
          </a-tag>
        </template>
      </template>
    </hbt-table>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtLoginLogDto, HbtLoginLogQueryDto } from '@/types/audit/loginLog'
import { getLoginLogs, exportLoginLogs, clearLoginLogs } from '@/api/audit/loginLog'
import { useI18n } from 'vue-i18n'

const { t } = useI18n()
// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'userName',
    label: '用户名称',
    type: 'input',
    placeholder: '请输入用户名称'
  },
  {
    name: 'ipAddress',
    label: 'IP地址',
    type: 'input',
    placeholder: '请输入IP地址'
  },
  {
    name: 'success',
    label: '登录状态',
    type: 'select',
    placeholder: '请选择登录状态',
    options: [
      { label: '成功', value: 1 },
      { label: '失败', value: 0 }
    ]
  },
  {
    name: 'loginType',
    label: '登录类型',
    type: 'select',
    placeholder: '请选择登录类型',
    options: [
      { label: '账号密码', value: 0 },
      { label: '手机验证码', value: 1 },
      { label: '邮箱验证码', value: 2 },
      { label: '第三方登录', value: 3 }
    ]
  },
  {
    name: 'startTime',
    label: '开始时间',
    type: 'date',
    placeholder: '请选择开始时间'
  },
  {
    name: 'endTime',
    label: '结束时间',
    type: 'date',
    placeholder: '请选择结束时间'
  }
]

// 表格列定义
const columns = [
  {
    title: '用户名称',
    dataIndex: 'userName',
    key: 'userName',
    width: 120
  },
  {
    title: 'IP地址',
    dataIndex: 'ipAddress',
    key: 'ipAddress',
    width: 130
  },
  {
    title: '用户代理',
    dataIndex: 'userAgent',
    key: 'userAgent',
    width: 200,
    ellipsis: true
  },
  {
    title: '登录类型',
    dataIndex: 'loginType',
    key: 'loginType',
    width: 100
  },
  {
    title: '登录状态',
    dataIndex: 'success',
    key: 'success',
    width: 100
  },
  {
    title: '登录来源',
    dataIndex: 'loginSource',
    key: 'loginSource',
    width: 100
  },
  {
    title: '消息',
    dataIndex: 'message',
    key: 'message',
    width: 200,
    ellipsis: true
  },
  {
    title: '登录时间',
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180
  }
]

// 状态定义
const loading = ref(false)
const tableData = ref<HbtLoginLogDto[]>([])
const total = ref(0)
const queryParams = reactive<HbtLoginLogQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  userName: undefined,
  ipAddress: undefined,
  success: undefined,
  loginType: undefined,
  loginStatus: undefined,
  startTime: undefined,
  endTime: undefined
})
const selectedRowKeys = ref<(string | number)[]>([])
const showSearch = ref(true)

/** 获取表格数据 */
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getLoginLogs(queryParams)
    if (res.code === 200) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取登录日志失败:', error)
    message.error('获取登录日志失败')
  } finally {
    loading.value = false
  }
}

/** 搜索按钮操作 */
const handleQuery = () => {
  queryParams.pageIndex = 1
  fetchData()
}

/** 重置按钮操作 */
const resetQuery = () => {
  queryParams.userName = undefined
  queryParams.ipAddress = undefined
  queryParams.success = undefined
  queryParams.loginType = undefined
  queryParams.loginStatus = undefined
  queryParams.startTime = undefined
  queryParams.endTime = undefined
  queryParams.pageIndex = 1
  fetchData()
}

/** 表格变化事件 */
const handleTableChange = (pagination: TablePaginationConfig) => {
  queryParams.pageIndex = pagination.current || 1
  queryParams.pageSize = pagination.pageSize || 10
  fetchData()
}

/** 导出登录日志 */
const handleExport = async () => {
  try {
    await exportLoginLogs(queryParams, '登录日志')
    message.success('导出成功')
  } catch (error) {
    console.error('导出登录日志失败:', error)
    message.error('导出登录日志失败')
  }
}

/** 批量删除 */
const handleBatchDelete = async () => {
  if (!selectedRowKeys.value.length) {
    message.warning('请选择要删除的记录')
    return
  }
  try {
    await clearLoginLogs()
    message.success('清空成功')
    selectedRowKeys.value = []
    fetchData()
  } catch (error) {
    console.error('清空登录日志失败:', error)
    message.error('清空登录日志失败')
  }
}

/** 切换搜索区域显示状态 */
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 初始化加载数据
fetchData()
</script>

<style lang="less" scoped>
.login-log-container {
  padding: 16px;
}
</style> 