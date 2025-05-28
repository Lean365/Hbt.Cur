<template>
  <div class="language-table">
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns"
      :pagination="false"
      :scroll="{ x: 'max-content', y: 400 }"
      :row-key="(record) => String(record.languageId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>
        <template v-if="column.dataIndex === 'isDefault'">
          <hbt-dict-tag dict-type="sys_yes_no" :value="record.isDefault" />
        </template>
        <template v-if="column.dataIndex === 'isBuiltin'">
          <hbt-dict-tag dict-type="sys_yes_no" :value="record.isBuiltin" />
        </template>
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-view="true"
            :view-permission="['core:language:query']"
            :show-edit="true"
            :edit-permission="['core:language:update']"
            :show-delete="true"
            :delete-permission="['core:language:delete']"
            size="small"
            @view="handleView"
            @edit="handleEdit"
            @delete="handleDelete"
          />
        </template>
      </template>
    </hbt-table>

    <!-- 语言表单对话框 -->
    <language-form
      v-model:open="formVisible"
      :language-id="selectedLanguageId"
      @success="handleSuccess"
    />

    <!-- 语言详情对话框 -->
    <language-detail
      v-model:open="detailVisible"
      :language-id="selectedLanguageId"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtLanguage } from '@/types/core/language'
import { getHbtLanguageList, deleteHbtLanguage } from '@/api/core/language'
import LanguageForm from './Language.vue'
import LanguageDetail from './LanguageDetail.vue'

const props = defineProps<{
  loading?: boolean
}>()

const emit = defineEmits(['update:selectedRowKeys', 'success'])

const { t } = useI18n()
const tableData = ref<HbtLanguage[]>([])
const selectedRowKeys = ref<string[]>([])
const selectedLanguageId = ref<number>()
const formVisible = ref(false)
const detailVisible = ref(false)

// 表格列定义
const columns = [
  {
    title: t('core.language.fields.langCode.label'),
    dataIndex: 'langCode',
    key: 'langCode',
    width: 150,
    ellipsis: true
  },
  {
    title: t('core.language.fields.langName.label'),
    dataIndex: 'langName',
    key: 'langName',
    width: 150,
    ellipsis: true
  },
  {
    title: t('core.language.fields.isDefault.label'),
    dataIndex: 'isDefault',
    key: 'isDefault',
    width: 100
  },
  {
    title: t('core.language.fields.isBuiltin.label'),
    dataIndex: 'isBuiltin',
    key: 'isBuiltin',
    width: 100
  },
  {
    title: t('core.language.fields.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('core.language.fields.remark.label'),
    dataIndex: 'remark',
    key: 'remark',
    ellipsis: true
  },
  {
    title: t('table.columns.action'),
    key: 'action',
    width: 180,
    fixed: 'right'
  }
]

// 获取数据
const fetchData = async () => {
  try {
    const res = await getHbtLanguageList()
    if (res.data.code === 200) {
      tableData.value = res.data.data
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载语言列表失败:', error)
    message.error(t('common.failed'))
  }
}

// 查看
const handleView = (record: HbtLanguage) => {
  selectedLanguageId.value = record.languageId
  detailVisible.value = true
}

// 编辑
const handleEdit = (record: HbtLanguage) => {
  selectedLanguageId.value = record.languageId
  formVisible.value = true
}

// 删除
const handleDelete = async (record: HbtLanguage) => {
  try {
    const res = await deleteHbtLanguage(record.languageId)
    if (res.data.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      fetchData()
    } else {
      message.error(res.data.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 表单提交成功
const handleSuccess = () => {
  fetchData()
}

// 初始化
fetchData()
</script>

<style lang="less" scoped>
.language-table {
  padding: 24px;
}
</style> 