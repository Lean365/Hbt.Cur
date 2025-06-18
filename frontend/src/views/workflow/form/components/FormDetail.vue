<template>
  <hbt-modal
    v-model:open="visible"
    :title="t('workflow.form.actions.view')"
    :width="1200"
    :footer="null"
  >
    <div class="workflow-form-detail">
      <a-tabs v-model:activeKey="activeTab" class="form-tabs">
        <a-tab-pane key="1" :tab="t('workflow.form.tabs.basic')">
          <!-- 基本信息 -->
          <a-descriptions :column="2" bordered>
            <a-descriptions-item :label="t('workflow.form.fields.formName')">
              {{ detail.formName }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('workflow.form.fields.formCategory')">
              <hbt-dict-tag dict-type="workflow_form_category" :value="detail.formCategory" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('workflow.form.fields.formVersion')">
              {{ detail.formVersion }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('workflow.form.fields.status')">
              <hbt-dict-tag dict-type="workflow_form_status" :value="detail.status" />
            </a-descriptions-item>
            <a-descriptions-item :label="t('workflow.form.fields.formDesc')" :span="2">
              {{ detail.formDesc }}
            </a-descriptions-item>
            <a-descriptions-item :label="t('workflow.form.fields.formConfig')" :span="2">
              <pre>{{ detail.formConfig }}</pre>
            </a-descriptions-item>
            <a-descriptions-item :label="t('workflow.form.fields.remark')" :span="2">
              {{ detail.remark }}
            </a-descriptions-item>
          </a-descriptions>

          <!-- 时间信息 -->
          <div class="time-info">
            <h3>{{ t('common.timeInfo') }}</h3>
            <a-descriptions :column="2" bordered>
              <a-descriptions-item :label="t('common.createBy')">
                {{ detail.createBy }}
              </a-descriptions-item>
              <a-descriptions-item :label="t('common.createTime')">
                {{ detail.createTime }}
              </a-descriptions-item>
              <a-descriptions-item :label="t('common.updateBy')">
                {{ detail.updateBy }}
              </a-descriptions-item>
              <a-descriptions-item :label="t('common.updateTime')">
                {{ detail.updateTime }}
              </a-descriptions-item>
            </a-descriptions>
          </div>
        </a-tab-pane>
        <a-tab-pane key="2" :tab="t('workflow.form.tabs.designer')">
          <form-config v-model:value="detail.formConfig" :disabled="true" />
        </a-tab-pane>
      </a-tabs>
    </div>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { HbtForm } from '@/types/workflow/form'
import { getForm } from '@/api/workflow/form'
import FormConfig from './FormConfig.vue'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  formId?: number
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

const activeTab = ref('1')
const detail = ref<HbtForm>({
  formId: 0,
  formName: '',
  formDesc: '',
  formConfig: '',
  formVersion: '',
  formCategory: 0,
  status: 0,
  remark: '',
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: '',
  isDeleted: 0
} as HbtForm)

// 获取表单详情
const fetchData = async () => {
  if (!props.formId) {
    return
  }
  try {
    const response = await getForm(props.formId)
    if (response.data.code === 200) {
      detail.value = response.data.data
    }
  } catch (error) {
    console.error('获取表单详情失败:', error)
  }
}

// 监听formId变化
watch(() => props.formId, (newVal) => {
  if (newVal) {
    fetchData()
  }
}, { immediate: true })

// 处理可见性变化
const handleVisibleChange = (value: boolean) => {
  if (!value) {
    activeTab.value = '1'
  }
}
</script>

<style lang="less" scoped>
.workflow-form-detail {
  .form-tabs {
    :deep(.ant-tabs-content) {
      height: calc(100vh - 200px);
      overflow-y: auto;
    }
  }

  .time-info {
    margin-top: 24px;
  }

  h3 {
    margin-bottom: 16px;
    font-weight: 500;
  }

  pre {
    margin: 0;
    padding: 8px;
    background-color: #f5f5f5;
    border-radius: 4px;
    white-space: pre-wrap;
    word-wrap: break-word;
  }
}
</style> 