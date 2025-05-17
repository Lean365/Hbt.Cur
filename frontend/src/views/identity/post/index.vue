<template>
  <div class="post-container">
    <!-- 搜索区域 -->
    <hbt-query
      v-model:show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="handleReset"
    >
      <template #queryForm>
        <a-form-item :label="t('identity.post.fields.postCode.label')">
          <a-input
            v-model:value="queryParams.postCode"
            :placeholder="t('identity.post.fields.postCode.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.post.fields.postName.label')">
          <a-input
            v-model:value="queryParams.postName"
            :placeholder="t('identity.post.fields.postName.placeholder')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('identity.post.fields.status.label')">
          <hbt-select
            v-model:value="queryParams.status"
            dict-type="sys_normal_disable"
            :placeholder="t('identity.post.fields.status.placeholder')"
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
      :add-permission="['identity:post:create']"
      :edit-permission="['identity:post:update']"
      :delete-permission="['identity:post:delete']"
      :export-permission="['identity:post:export']"
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
      :pagination="pagination"
      :row-selection="{
        selectedRowKeys: selectedKeys,
        onChange: onSelectChange
      }"
      row-key="postId"
      size="middle"
      bordered
      :scroll="{ x: 1200, y: 'calc(100vh - 300px)' }"
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
          />
        </template>
      </template>
    </a-table>

    <!-- 表单弹窗 -->
    <post-form
      v-model:visible="formVisible"
      :form-type="formPostId ? 'update' : 'create'"
      :form-data="formPostId ? { postId: formPostId } : undefined"
      @success="getList"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { QueryField } from '@/types/components/query'
import type { Post, PostQuery } from '@/types/identity/post'
import { getPagedList, deletePost, batchDeletePost, exportPost } from '@/api/identity/post'
import PostForm from './components/PostForm.vue'

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
      placeholder: t('identity.post.fields.status.placeholder'),
      allowClear: true
    }
  }
]

// 查询参数
const queryParams = ref<PostQuery>({
  pageIndex: 1,
  pageSize: 10,
  postCode: undefined,
  postName: undefined,
  status: undefined
})

// 加载状态
const loading = ref(false)

// 岗位列表数据
const list = ref<Post[]>([])

// 分页配置
const pagination = ref<TablePaginationConfig>({
  total: 0,
  current: 1,
  pageSize: 10,
  showSizeChanger: true,
  showQuickJumper: true
})

// 选中的岗位ID
const selectedKeys = ref<(string | number)[]>([])

// 表单弹窗显示状态
const formVisible = ref(false)

// 表单标题
const formTitle = ref('')

// 当前编辑的岗位ID
const formPostId = ref<number>()

// 表格列定义
const columns: TableColumnsType = [
  {
    title: t('identity.post.fields.postCode.label'),
    dataIndex: 'postCode',
    width: 120
  },
  {
    title: t('identity.post.fields.postName.label'),
    dataIndex: 'postName',
    width: 150
  },
  {
    title: t('identity.post.fields.postSort.label'),
    dataIndex: 'postSort',
    width: 100
  },
  {
    title: t('identity.post.fields.status.label'),
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

// 获取岗位列表
const getList = async () => {
  try {
    loading.value = true
    const res = await getPagedList(queryParams.value)
    if (res.data.code === 200) {
      list.value = res.data.data.rows
      pagination.value.total = res.data.data.totalNum
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('[岗位管理] 获取岗位列表出错:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 处理查询
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  selectedKeys.value = []
  getList()
}

// 处理重置
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    postCode: undefined,
    postName: undefined,
    status: undefined
  }
  getList()
}

// 处理表格变化
const onTableChange = (pagination: TablePaginationConfig) => {
  queryParams.value.pageIndex = pagination.current as number
  queryParams.value.pageSize = pagination.pageSize as number
  getList()
}

// 处理选择变化
const onSelectChange = (keys: (string | number)[], _: Post[]) => {
  selectedKeys.value = keys
}

// 处理新增
const handleAdd = () => {
  formTitle.value = t('identity.post.dialog.create')
  formPostId.value = undefined
  formVisible.value = true
}

// 处理编辑选中项
const handleEditSelected = () => {
  const postId = selectedKeys.value[0]
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
    content: t('common.delete.message', { count: selectedKeys.value.length }),
    async onOk() {
      try {
        const res = await batchDeletePost(selectedKeys.value.map(id => Number(id)))
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
    const res = await exportPost()
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

onMounted(() => {
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
</style> 