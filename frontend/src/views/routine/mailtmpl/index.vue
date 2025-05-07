<template>
  <div class="mailtmpl-container">
    <!-- 查询区域 -->
    <hbt-query
      v-show="showSearch"
      :model="queryParams"
      :query-fields="queryFields"
      @search="handleQuery"
      @reset="resetQuery"
    >
      <template #queryForm>
        <a-form-item :label="t('mailtmpl.templateName')">
          <a-input
            v-model:value="queryParams.templateName"
            :placeholder="t('mailtmpl.placeholder.templateName')"
            allow-clear
            @keyup.enter="handleQuery"
          />
        </a-form-item>
        <a-form-item :label="t('mailtmpl.templateType')">
          <hbt-select
            v-model:value="queryParams.templateType"
            dict-type="email_template_type"
            type="radio"
            :placeholder="t('mailtmpl.placeholder.templateType')"
            allow-clear
          />
        </a-form-item>
        <a-form-item :label="t('mailtmpl.templateStatus')">
          <hbt-select
            v-model:value="queryParams.templateStatus"
            dict-type="email_template_status"
            type="radio"
            :placeholder="t('mailtmpl.placeholder.templateStatus')"
            allow-clear
          />
        </a-form-item>
      </template>
    </hbt-query>

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['routine:mailtmpl:create']"
      :show-edit="true"
      :edit-permission="['routine:mailtmpl:update']"
      :show-delete="true"
      :delete-permission="['routine:mailtmpl:delete']"
      :show-export="true"
      :export-permission="['routine:mailtmpl:export']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @export="handleExport"
      @refresh="fetchData"
      @column-setting="handleColumnSettingChange"
      @toggle-search="toggleSearch"
      @toggle-fullscreen="toggleFullscreen"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="defaultColumns"
      :pagination="false"
      :scroll="{ x: 'max-content' }"
      :default-height="594"
      :row-key="(record: HbtMailTemplateDto) => String(record.templateId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
      @change="handleTableChange"
      @row-click="handleRowClick"
    >
      <!-- 模板类型列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'templateType'">
          <hbt-dict-tag dict-type="email_template_type" :value="record.templateType" />
        </template>
        
        <!-- 模板状态列 -->
        <template v-if="column.dataIndex === 'templateStatus'">
          <hbt-dict-tag dict-type="email_template_status" :value="record.templateStatus" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-edit="true"
            :edit-permission="['routine:mailtmpl:update']"
            :show-delete="true"
            :delete-permission="['routine:mailtmpl:delete']"
            size="small"
            @edit="handleEdit"
            @delete="handleDelete"
          >
            <!-- 预览按钮 -->
            <template #extra>
              <a-tooltip :title="t('mailtmpl.preview')">
                <a-button
                  type="link"
                  size="small"
                  @click.stop="handlePreview(record)"
                >
                  <template #icon><eye-outlined /></template>
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
      @change="(page: number) => handleTableChange(page, queryParams.pageSize)"
      @showSizeChange="(page: number, pageSize: number) => handleTableChange(page, pageSize)"
    />

    <!-- 邮件模板表单对话框 -->
    <mailtmpl-form
      v-model:visible="formVisible"
      :title="formTitle"
      :template-id="selectedTemplateId"
      @success="handleSuccess"
    />

    <!-- 预览对话框 -->
    <a-modal
      v-model:visible="previewVisible"
      :title="t('mailtmpl.preview')"
      width="700px"
      :footer="null"
    >
      <div class="preview-content">
        <h3>{{ previewData.templateSubject }}</h3>
        <div v-html="previewData.templateContent"></div>
      </div>
    </a-modal>

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
import { ref, computed, onMounted, h } from 'vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { EyeOutlined } from '@ant-design/icons-vue'
import { useDictStore } from '@/stores/dict'
import type { HbtMailTemplateDto, HbtMailTemplateQueryDto } from '@/types/routine/mailtmpl'
import type { QueryField } from '@/types/components/query'
import { getMailTemplateList, deleteMailTemplate } from '@/api/routine/mailtmpl'
import MailtmplForm from './components/MailtmplForm.vue'

const { t } = useI18n()
const dictStore = useDictStore()

// 查询参数
const queryParams = ref<HbtMailTemplateQueryDto>({
  pageIndex: 1,
  pageSize: 10,
  templateName: undefined,
  templateType: undefined,
  templateStatus: undefined
})

// 查询字段配置
const queryFields = computed<QueryField[]>(() => [
  {
    name: 'templateName',
    label: t('mailtmpl.templateName'),
    type: 'input',
    placeholder: t('mailtmpl.placeholder.templateName')
  },
  {
    name: 'templateType',
    label: t('mailtmpl.templateType'),
    type: 'select',
    placeholder: t('mailtmpl.placeholder.templateType'),
    props: {
      dictType: 'email_template_type'
    }
  },
  {
    name: 'templateStatus',
    label: t('mailtmpl.templateStatus'),
    type: 'select',
    placeholder: t('mailtmpl.placeholder.templateStatus'),
    props: {
      dictType: 'email_template_status'
    }
  }
] as QueryField[])

// 表格数据
const loading = ref(false)
const tableData = ref<HbtMailTemplateDto[]>([])
const total = ref(0)
const selectedRowKeys = ref<string[]>([])
const selectedTemplateId = ref<string>()

// 表单对话框
const formVisible = ref(false)
const formTitle = ref('')
const previewVisible = ref(false)
const previewData = ref<HbtMailTemplateDto>({} as HbtMailTemplateDto)

// 列设置
const columnSettingVisible = ref(false)
const columnSettings = ref<Record<string, boolean>>({})
const defaultColumns = [
  { key: 'id', title: 'ID', dataIndex: 'templateId' },
  { key: 'templateName', title: t('mailtmpl.templateName'), dataIndex: 'templateName' },
  { key: 'templateType', title: t('mailtmpl.templateType'), dataIndex: 'templateType' },
  { key: 'templateSubject', title: t('mailtmpl.templateSubject'), dataIndex: 'templateSubject' },
  { key: 'templateStatus', title: t('mailtmpl.templateStatus'), dataIndex: 'templateStatus' },
  { key: 'createTime', title: t('mailtmpl.createTime'), dataIndex: 'templateCreateTime' },
  { key: 'action', title: t('common.operation'), fixed: 'right', width: 200 }
]

// 显示搜索条件
const showSearch = ref(true)

// 获取数据列表
const fetchData = async () => {
  loading.value = true
  try {
    const res = await getMailTemplateList(queryParams.value)
    tableData.value = res.data.data.rows
    total.value = res.data.data.total
  } catch (error) {
    console.error('获取邮件模板列表失败:', error)
  } finally {
    loading.value = false
  }
}

// 搜索按钮操作
const handleQuery = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置按钮操作
const resetQuery = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    templateName: undefined,
    templateType: undefined,
    templateStatus: undefined
  }
  handleQuery()
}

// 表格变化
const handleTableChange = (page: number, pageSize: number) => {
  queryParams.value.pageIndex = page
  queryParams.value.pageSize = pageSize
  fetchData()
}

// 行点击
const handleRowClick = (record: HbtMailTemplateDto) => {
  selectedTemplateId.value = String(record.templateId)
}

// 新增按钮操作
const handleAdd = () => {
  selectedTemplateId.value = undefined
  formTitle.value = t('mailtmpl.add')
  formVisible.value = true
}

// 修改按钮操作
const handleEdit = (record: HbtMailTemplateDto) => {
  selectedTemplateId.value = String(record.templateId)
  formTitle.value = t('mailtmpl.edit')
  formVisible.value = true
}

// 修改选中按钮操作
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.pleaseSelectOneRecord'))
    return
  }
  const record = tableData.value.find(item => String(item.templateId) === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 删除按钮操作
const handleDelete = async (record: HbtMailTemplateDto) => {
  try {
    await deleteMailTemplate(BigInt(record.templateId))
    message.success(t('mailtmpl.operation.success.delete'))
    fetchData()
  } catch (error) {
    console.error('删除邮件模板失败:', error)
  }
}

// 批量删除按钮操作
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('common.pleaseSelectRecord'))
    return
  }
  try {
    await deleteMailTemplate(BigInt(selectedRowKeys.value[0]))
    message.success(t('mailtmpl.operation.success.delete'))
    fetchData()
  } catch (error) {
    console.error('批量删除邮件模板失败:', error)
  }
}

// 导出按钮操作
const handleExport = () => {
  // TODO: 实现导出功能
}

// 预览按钮操作
const handlePreview = (record: HbtMailTemplateDto) => {
  previewData.value = record
  previewVisible.value = true
}

// 表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// 切换搜索显示
const toggleSearch = () => {
  showSearch.value = !showSearch.value
}

// 切换全屏显示
const toggleFullscreen = () => {
  // TODO: 实现全屏切换功能
}

// 列设置变化
const handleColumnSettingChange = (checkedValues: (string | number | boolean)[]) => {
  columnSettings.value = defaultColumns.reduce((acc, col) => {
    acc[col.key] = checkedValues.includes(col.key)
    return acc
  }, {} as Record<string, boolean>)
}

// 页面加载时获取数据
onMounted(() => {
  fetchData()
  // 初始化列设置
  columnSettings.value = defaultColumns.reduce((acc, col) => {
    acc[col.key] = true
    return acc
  }, {} as Record<string, boolean>)
})
</script>

<style scoped>
.mailtmpl-container {
  padding: 24px;
}

.preview-content {
  padding: 20px;
  background-color: #f5f5f5;
  border-radius: 4px;
}

.preview-content h3 {
  margin-bottom: 20px;
  color: #333;
}

.column-setting-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.column-setting-item {
  display: flex;
  align-items: center;
}
</style> 