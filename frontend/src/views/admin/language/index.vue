<template>
  <div class="app-container">
    <a-card :bordered="false">
      <template #title>{{ t('admin.language') }}</template>
      <template #extra>
        <a-space>
          <a-button type="primary" @click="handleAdd" v-hasPermi="['admin:language:add']">
            <template #icon><PlusOutlined /></template>{{ t('common.add') }}
          </a-button>
          <a-button @click="handleRefresh">
            <template #icon><ReloadOutlined /></template>{{ t('common.refresh') }}
          </a-button>
        </a-space>
      </template>

      <!-- 搜索区域 -->
      <a-form ref="queryFormRef" :model="queryParams" layout="inline" v-show="showSearch">
        <a-form-item :label="t('admin.language.code')" name="langCode">
          <a-input v-model:value="queryParams.langCode" :placeholder="t('admin.language.code.placeholder')" allow-clear style="width: 200px" @pressEnter="handleQuery" />
        </a-form-item>
        <a-form-item :label="t('admin.language.name')" name="langName">
          <a-input v-model:value="queryParams.langName" :placeholder="t('admin.language.name.placeholder')" allow-clear style="width: 200px" @pressEnter="handleQuery" />
        </a-form-item>
        <a-form-item :label="t('common.status')" name="status">
          <a-select v-model:value="queryParams.status" :placeholder="t('common.status.placeholder')" allow-clear style="width: 200px">
            <a-select-option value="0">{{ t('common.status.normal') }}</a-select-option>
            <a-select-option value="1">{{ t('common.status.disabled') }}</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item :label="t('common.createTime')" name="timeRange">
          <a-range-picker v-model:value="dateRange" :placeholder="[t('common.startDate'), t('common.endDate')]" style="width: 240px" @change="handleDateRangeChange" />
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button type="primary" @click="handleQuery">
              <template #icon><SearchOutlined /></template>{{ t('common.search') }}
            </a-button>
            <a-button @click="resetQuery">
              <template #icon><ReloadOutlined /></template>{{ t('common.reset') }}
            </a-button>
          </a-space>
        </a-form-item>
      </a-form>

      <!-- 操作按钮区域 -->
      <div class="table-operations">
        <a-space>
          <a-button type="primary" @click="handleAdd" v-hasPermi="['admin:language:add']">
            <template #icon><PlusOutlined /></template>{{ t('common.add') }}
          </a-button>
          <a-button type="primary" :disabled="single" @click="handleUpdateClick" v-hasPermi="['admin:language:edit']">
            <template #icon><EditOutlined /></template>{{ t('common.edit') }}
          </a-button>
          <a-button type="primary" danger :disabled="multiple" @click="handleDeleteClick" v-hasPermi="['admin:language:remove']">
            <template #icon><DeleteOutlined /></template>{{ t('common.delete') }}
          </a-button>
          <a-button @click="handleExport" v-hasPermi="['admin:language:export']">
            <template #icon><DownloadOutlined /></template>{{ t('common.export') }}
          </a-button>
          <a-button type="link" @click="() => showSearch = !showSearch">
            <template #icon><SearchOutlined /></template>{{ showSearch ? t('common.collapse.search') : t('common.expand.search') }}
          </a-button>
        </a-space>
      </div>

      <!-- 数据表格 -->
      <a-table
        :loading="loading"
        :columns="columns"
        :data-source="languageList"
        :row-selection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
        :pagination="{
          total: pagination.total,
          current: queryParams.pageNum,
          pageSize: queryParams.pageSize,
          onChange: handleTableChange,
          showSizeChanger: true,
          showTotal: (total: number) => t('common.total', { total })
        }"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'langIcon'">
            <a-image
              v-if="record.langIcon"
              :src="record.langIcon"
              :width="25"
              :height="25"
            />
          </template>
          <template v-else-if="column.key === 'status'">
            <a-tag :color="record.status === '0' ? 'success' : 'error'">
              {{ record.status === '0' ? t('common.status.normal') : t('common.status.disabled') }}
            </a-tag>
          </template>
          <template v-else-if="column.key === 'createTime'">
            {{ dayjs(record.createTime).format('YYYY-MM-DD HH:mm:ss') }}
          </template>
          <template v-else-if="column.key === 'action'">
            <a-space>
              <a @click="handleUpdate(record as Language)" v-hasPermi="['admin:language:edit']">{{ t('common.edit') }}</a>
              <a-divider type="vertical" />
              <a @click="handleDelete(record as Language)" v-hasPermi="['admin:language:remove']">{{ t('common.delete') }}</a>
            </a-space>
          </template>
        </template>
      </a-table>

      <!-- 添加或修改语言对话框 -->
      <a-modal
        :title="title"
        :visible="open"
        @ok="submitForm"
        @cancel="cancel"
        :confirmLoading="submitLoading"
      >
        <language-form
          ref="formRef"
          v-model:formData="form"
          :visible="open"
          @update:visible="(val) => open = val"
        />
      </a-modal>
    </a-card>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message, Modal } from 'ant-design-vue'
import type { TableColumnsType, TablePaginationConfig } from 'ant-design-vue'
import type { Dayjs } from 'dayjs'
import type { Language, LanguageQuery, LanguageCreate, LanguageUpdate } from '@/types/admin/language'
import { 
  getLanguageList,
  deleteLanguage,
  batchDeleteLanguage,
  updateLanguageStatus,
  importLanguage,
  exportLanguage,
  getLanguageTemplate,
  getLanguage,
  updateLanguage,
  createLanguage
} from '@/api/admin/language'
import LanguageForm from './components/LanguageForm.vue'
import ImportDialog from '@/components/ImportDialog/index.vue'
import {
  SearchOutlined,
  ReloadOutlined,
  PlusOutlined,
  EditOutlined,
  DeleteOutlined,
  DownloadOutlined
} from '@ant-design/icons-vue'
import dayjs from 'dayjs'

const { t } = useI18n()

const showSearch = ref(true)

// 计算属性
const single = computed(() => selectedRowKeys.value.length !== 1)
const multiple = computed(() => !selectedRowKeys.value.length)

interface HbtPageResult<T> {
  total: number
  pageNum: number
  pageSize: number
  data: T
}

// 列定义
const columns: TableColumnsType = [
  { title: t('admin.language.id'), dataIndex: 'langId', key: 'langId' },
  { title: t('admin.language.code'), dataIndex: 'langCode', key: 'langCode' },
  { title: t('admin.language.name'), dataIndex: 'langName', key: 'langName' },
  { title: t('admin.language.icon'), dataIndex: 'langIcon', key: 'langIcon' },
  { title: t('common.sort'), dataIndex: 'orderNum', key: 'orderNum' },
  { title: t('common.status'), dataIndex: 'status', key: 'status' },
  { title: t('common.createTime'), dataIndex: 'createTime', key: 'createTime' },
  { title: t('common.action'), key: 'action', fixed: 'right', width: 150 }
]

// 查询参数
const queryFormRef = ref()
const queryParams = reactive<LanguageQuery>({
  pageNum: 1,
  pageSize: 10,
  langCode: '',
  langName: '',
  status: undefined,
  beginTime: '',
  endTime: ''
})
const dateRange = ref<[Dayjs, Dayjs] | undefined>()

// 表格数据
const loading = ref(false)
const languageList = ref<Language[]>([])
const open = ref(false)
const title = ref('')
const submitLoading = ref(false)
const pagination = reactive<TablePaginationConfig>({
  total: 0,
  current: 1,
  pageSize: 10,
  showSizeChanger: true,
  showTotal: (total: number) => t('common.pagination.total', { total })
})

// 选择行配置
const selectedRowKeys = ref<(string | number)[]>([])
const onSelectChange = (keys: (string | number)[], selectedRows: Language[]) => {
  selectedRowKeys.value = keys
}

// 表单参数
const form = reactive<LanguageCreate | LanguageUpdate>({
  langCode: '',
  langName: '',
  langIcon: '',
  orderNum: 0,
  status: '0',
  remark: ''
})

// 表单校验规则
const rules = {
  langCode: [
    { required: true, message: t('admin.language.code.required') },
    { min: 2, max: 20, message: t('admin.language.code.length') }
  ],
  langName: [
    { required: true, message: t('admin.language.name.required') },
    { min: 2, max: 30, message: t('admin.language.name.length') }
  ],
  orderNum: [
    { required: true, message: t('common.sort.required') }
  ]
}

// 查询列表
const getList = async () => {
  loading.value = true
  try {
    const res = await getLanguageList(queryParams)
    const result = res.data as unknown as HbtPageResult<Language[]>
    languageList.value = result.data
    pagination.total = result.total
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
  loading.value = false
}

// 表格选择事件
const handleTableChange = (page: number, pageSize: number) => {
  queryParams.pageNum = page
  queryParams.pageSize = pageSize
  getList()
}

// 搜索按钮操作
const handleQuery = () => {
  queryParams.pageNum = 1
  getList()
}

// 重置按钮操作
const resetQuery = () => {
  dateRange.value = undefined
  Object.assign(queryParams, {
    langCode: undefined,
    langName: undefined,
    status: undefined,
    beginTime: undefined,
    endTime: undefined
  })
  handleQuery()
}

// 日期范围变化
const handleDateRangeChange = (value: [Dayjs, Dayjs] | [string, string], dateStrings: [string, string]) => {
  if (value) {
    queryParams.beginTime = dateStrings[0]
    queryParams.endTime = dateStrings[1]
  } else {
    queryParams.beginTime = undefined
    queryParams.endTime = undefined
  }
}

// 取消按钮
const cancel = () => {
  open.value = false
  resetForm()
}

// 表单重置
const resetForm = () => {
  Object.assign(form, {
    langCode: '',
    langName: '',
    langIcon: '',
    orderNum: 0,
    status: '0',
    remark: ''
  })
}

// 新增按钮操作
const handleAdd = () => {
  resetForm()
  open.value = true
  title.value = '添加语言'
}

// 修改按钮操作
const handleUpdate = async (row?: Language) => {
  resetForm()
  const langId = row?.langId || selectedRowKeys.value[0]
  try {
    const response = await getLanguage(Number(langId))
    Object.assign(form, response.data)
    open.value = true
    title.value = '修改语言'
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 添加表单引用
const formRef = ref()

// 修改 submitForm 方法
const submitForm = async () => {
  try {
    await formRef.value.validate()
    submitLoading.value = true
    const formData = formRef.value.form
    if ('langId' in formData) {
      await updateLanguage(formData as LanguageUpdate)
    } else {
      await createLanguage(formData as LanguageCreate)
    }
    message.success(t('common.success'))
    open.value = false
    getList()
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
  submitLoading.value = false
}

// 删除按钮操作
const handleDelete = async (row?: Language) => {
  const langIds = row?.langId ? [row.langId] : selectedRowKeys.value
  try {
    await Modal.confirm({
      title: t('common.warning'),
      content: t('common.confirm.delete', { ids: langIds.join(',') }),
      okText: t('common.confirm'),
      okType: 'danger',
      cancelText: t('common.cancel')
    })
    await Promise.all(langIds.map(id => deleteLanguage(Number(id))))
    message.success(t('common.success'))
    getList()
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 导出按钮操作
const handleExport = async () => {
  try {
    await Modal.confirm({
      title: t('common.warning'),
      content: t('common.confirm.export'),
      okText: t('common.confirm'),
      okType: 'danger',
      cancelText: t('common.cancel')
    })
    await exportLanguage(queryParams)
    message.success(t('common.success'))
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 状态修改
const handleStatusChange = async (row: Language) => {
  const text = row.status === '0' ? t('common.enable') : t('common.disable')
  try {
    await Modal.confirm({
      title: t('common.warning'),
      content: t('common.confirm.statusChange', { name: row.langName, action: text }),
      okText: t('common.confirm'),
      okType: 'danger',
      cancelText: t('common.cancel')
    })
    await updateLanguageStatus({
      langId: row.langId,
      status: row.status
    })
    message.success(t('common.success'))
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
    row.status = row.status === '0' ? '1' : '0'
  }
}

// 刷新方法
const handleRefresh = () => {
  getList()
}

// 组件挂载时获取数据
onMounted(() => {
  getList()
})

const handleUpdateClick = (e: MouseEvent) => handleUpdate()
const handleDeleteClick = (e: MouseEvent) => handleDelete()
</script>

<style lang="less" scoped>
.app-container {
  padding: 24px;
  background-color: var(--ant-color-bg-layout);
  
  .table-operations {
    margin-bottom: 16px;
  }
  
  .ant-upload-text {
    margin-top: 8px;
    color: #666;
  }
}
</style> 