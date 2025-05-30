<template>
  <div class="post-container">
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
      :selected-count="selectedRowKeys.length"
      :show-select-count="true"
      :show-add="true"
      :show-edit="true"
      :show-delete="true"
      :show-export="true"
      :show-import="true"
      :add-permission="['identity:post:create']"
      :edit-permission="['identity:post:update']"
      :delete-permission="['identity:post:delete']"
      :export-permission="['identity:post:export']"
      :import-permission="['identity:post:import']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
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
        <a-button
          type="primary"
          :disabled="selectedRowKeys.length !== 1"
          @click="handleAssignUser({ postId: selectedRowKeys[0] })"
        >
          <template #icon><team-outlined /></template>
          分配用户
        </a-button>
      </template>
    </hbt-toolbar>

    <!-- 数据表格 -->
    <hbt-table
      :columns="columns.filter(col => col.key && columnSettings[col.key])"
      :data-source="list"
      :loading="loading"
      :pagination="false"
      :scroll="{ x: 600, y: 'calc(100vh - 500px)' }"
      row-key="postId"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="onTableChange"
    >
      <template #bodyCell="{ column, record }">
        <!-- 状态 -->
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>
        <!-- 操作 -->
        <template v-else-if="column.dataIndex === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :show-delete="true"
            :edit-permission="['identity:post:update']"
            :delete-permission="['identity:post:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          >
            <template #extra>
              <a-tooltip :title="t('identity.user.allocateDept')">
                <a-button
                  v-hasPermi="['identity:user:allocate']"
                  type="default"
                  size="small"
                  class="hbt-btn-reset"
                  @click="handleAssignUser(record)"
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

    <!-- 岗位表单弹窗 -->
    <post-form
      v-model:visible="formVisible"
      :title="formTitle"
      :post-id="formPostId"
      @success="getList"
    />

    <!-- 分配用户弹窗 -->
    <post-user-form v-model:visible="userFormVisible" :post-id="formPostId!" @success="getList" />

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
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { HbtPost, HbtPostQuery } from '@/types/identity/post'
import { getPostList, deletePost, batchDeletePost, exportPost, downloadTemplate, importPost } from '@/api/identity/post'
import PostForm from './components/PostForm.vue'
import PostUserForm from './components/PostUserForm.vue'
import { TeamOutlined } from '@ant-design/icons-vue'

const { t } = useI18n()

// 查询区域显示状态
const showSearch = ref(true)

// 查询字段定义
const queryFields: QueryField[] = [
  {
    name: 'postCode',
    label: t('identity.post.fields.postCode.label'),
    type: 'input',
    props: {
      placeholder: t('identity.post.fields.postCode.placeholder'),
      allowClear: true
    }
  },
  {
    name: 'postName',
    label: t('identity.post.fields.postName.label'),
    type: 'input',
    props: {
      placeholder: t('identity.post.fields.postName.placeholder'),
      allowClear: true
    }
  },
  {
    name: 'status',
    label: t('identity.post.fields.status.label'),
    type: 'select',
    props: {
      dictType: 'sys_normal_disable',
      type: 'radio',
      showAll: true
    }
  }
]

// 查询参数
const queryParams = ref<HbtPostQuery>({
  pageIndex: 1,
  pageSize: 10,
  postCode: undefined,
  postName: undefined,
  status: -1
})

// 加载状态
const loading = ref(false)

// 岗位列表数据
const list = ref<HbtPost[]>([])

// 总数
const total = ref(0)

// 选中的岗位ID
const selectedRowKeys = ref<(string | number)[]>([])

// 表单弹窗显示状态
const formVisible = ref(false)

// 表单标题
const formTitle = ref('')

// 当前编辑的岗位ID
const formPostId = ref<number>()

// 分配用户弹窗显示状态
const userFormVisible = ref(false)

// 表格列定义
const columns: TableColumnsType = [
  {
    title: t('identity.post.fields.postCode.label'),
    dataIndex: 'postCode',
    key: 'postCode',
    width: 120,
    ellipsis: true
  },
  {
    title: t('identity.post.fields.postName.label'),
    dataIndex: 'postName',
    key: 'postName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('identity.post.fields.postSort.label'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100,
    ellipsis: true
  },
  {
    title: t('identity.post.fields.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100,
    ellipsis: true
  },
  {
    title: t('common.table.columns.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 120,
    ellipsis: true
  },
  {
    title: t('common.table.columns.createBy'),
    dataIndex: 'createBy',
    key: 'createBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('common.table.columns.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('common.table.columns.updateBy'),
    dataIndex: 'updateBy',
    key: 'updateBy',
    width: 120,
    ellipsis: true
  },
  {
    title: t('common.table.columns.updateTime'),
    dataIndex: 'updateTime',
    key: 'updateTime',
    width: 180,
    ellipsis: true
  },
  {
    title: t('common.table.header.operation'),
    dataIndex: 'action',
    key: 'action',
    width: 150,
    fixed: 'right',
    align: 'center',
    ellipsis: true
  }
]

// 获取岗位列表
const getList = async () => {
  try {
    loading.value = true
    console.log('查询参数:', {
      ...queryParams.value
    })
    const res = await getPostList(queryParams.value)
    if (res.data.code === 200) {
      list.value = res.data.data.rows
      total.value = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error: any) {
    console.error('[岗位管理] 获取岗位列表出错:', error)
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
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  selectedRowKeys.value = []
  getList()
}

// 处理重置
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    postCode: undefined,
    postName: undefined,
    status: -1
  }
  getList()
}

// 处理表格变化
const onTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current as number
  queryParams.value.pageSize = pagination.pageSize as number
  getList()
}

// 分页处理
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  getList()
}

const handleSizeChange = (size: number) => {
  queryParams.value.pageSize = size
  getList()
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('identity.post.dialog.create')
  formPostId.value = undefined
  formVisible.value = true
}

// 处理编辑选中项
const handleEditSelected = () => {
  const postId = selectedRowKeys.value[0]
  formTitle.value = t('identity.post.dialog.update')
  formPostId.value = Number(postId)
  formVisible.value = true
}

// 处理编辑
const handleEdit = (record: Record<string, any>) => {
  formTitle.value = t('identity.post.dialog.update')
  formPostId.value = Number(record.postId)
  formVisible.value = true
}

// 处理删除
const handleDelete = async (record: Record<string, any>) => {
  try {
    const res = await deletePost(Number(record.postId))
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
    content: t('common.delete.message', { count: selectedRowKeys.value.length }),
    async onOk() {
      try {
        const res = await batchDeletePost(selectedRowKeys.value.map(id => Number(id)))
        if (res.data.code === 200) {
          message.success(t('common.delete.success'))
          selectedRowKeys.value = []
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
    const res = await exportPost(queryParams.value)
    if (res.data.code === 200) {
      message.success(t('common.export.success'))
    } else {
      message.error(res.data.msg || t('common.export.failed'))
    }
  } catch (error: any) {
    console.error(error)
    if (error.response?.status === 500) {
      message.error(t('common.export.failed'))
    } else {
      message.error(error.response?.data?.msg || t('common.export.failed'))
    }
  }
}

// 处理下载模板
const handleTemplate = async () => {
  try {
    const res = await downloadTemplate()
    const link = document.createElement('a')
    link.href = window.URL.createObjectURL(new Blob([res.data]))
    link.download = `岗位导入模板_${new Date().getTime()}.xlsx`
    link.click()
    window.URL.revokeObjectURL(link.href)
  } catch (error: any) {
    console.error('下载模板失败:', error)
    message.error(error.message || t('common.template.failed'))
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
      const res = await importPost(file)
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
    if (col.key) {
      columnSettings.value[col.key] = true
    }
  })

  // 确保操作列显示
  columnSettings.value['action'] = true
}

// 处理列设置变更
const handleColumnSettingChange = (checkedValue: Array<string | number | boolean>) => {
  const settings: Record<string, boolean> = {}
  defaultColumns.forEach(col => {
    // 操作列始终为true
    if (col.key === 'action') {
      settings[col.key] = true
    } else if (col.key) {
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

const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

const toggleFullscreen = () => {
  // 处理全屏切换
  console.log('全屏切换')
}

// 处理分配用户
const handleAssignUser = (record: Record<string, any>) => {
  try {
    console.log('分配用户, record:', record)
    if (!record.postId) {
      message.error('岗位ID不能为空')
      return
    }
    formPostId.value = Number(record.postId)
    console.log('设置formPostId:', formPostId.value)
    userFormVisible.value = true
  } catch (error: any) {
    console.error('分配用户失败:', error)
    if (error.response?.status === 401) {
      message.error('登录已过期，请重新登录')
      window.location.href = '/login'
    } else {
      message.error('分配用户失败')
    }
  }
}

onMounted(() => {
  initColumnSettings()
  getList()
})
</script>

<style lang="less" scoped>
.post-container {
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
