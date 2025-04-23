<template>
  <hbt-modal
    :title="translationId ? t('common.title.edit') : t('common.title.create')"
    :open="dialogVisible"
    width="600px"
    append-to-body
    destroy-on-close
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
    >
      <a-form-item :label="t('admin.language.translation.moduleName')" name="moduleName">
        <a-input v-model:value="form.moduleName" :placeholder="t('admin.language.translation.moduleNamePlaceholder')" />
      </a-form-item>
      <a-form-item :label="t('admin.language.translation.transKey')" name="transKey">
        <a-input v-model:value="form.transKey" :placeholder="t('admin.language.translation.transKeyPlaceholder')" />
      </a-form-item>
      <a-form-item :label="t('admin.language.translation.transValue')" name="transValue">
        <a-textarea
          v-model:value="form.transValue"
          :rows="3"
          :placeholder="t('admin.language.translation.transValuePlaceholder')"
        />
      </a-form-item>
      <a-form-item :label="t('admin.language.translation.orderNum')" name="orderNum">
        <a-input-number v-model:value="form.orderNum" :min="0" :max="999" style="width: 100%" />
      </a-form-item>
      <a-form-item :label="t('admin.language.translation.status')" name="status">
        <a-radio-group v-model:value="form.status">
          <a-radio :value="0">{{ t('common.status.enabled') }}</a-radio>
          <a-radio :value="1">{{ t('common.status.disabled') }}</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item :label="t('admin.language.translation.remark')" name="remark">
        <a-textarea
          v-model:value="form.remark"
          :rows="2"
          :placeholder="t('admin.language.translation.remarkPlaceholder')"
        />
      </a-form-item>
    </a-form>
    <template #footer>
      <div class="dialog-footer">
        <a-button @click="handleCancel">{{ t('common.button.cancel') }}</a-button>
        <a-button type="primary" :loading="loading" @click="handleSubmit">
          {{ t('common.button.confirm') }}
        </a-button>
      </div>
    </template>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtTranslation } from '@/types/admin/translation'
import { getHbtTranslation, createHbtTranslation, updateHbtTranslation } from '@/api/admin/translation'

const props = defineProps<{
  translationId?: number
  langCode: string
}>()

const emit = defineEmits(['update:visible', 'success'])

const { t } = useI18n()
const formRef = ref<FormInstance>()
const loading = ref(false)
const dialogVisible = ref(true)

const form = reactive<HbtTranslation>({
  id: 0,
  translationId: 0,
  langCode: props.langCode,
  moduleName: '',
  transKey: '',
  transValue: '',
  orderNum: 0,
  status: 0,
  remark: '',
  createTime: '',
  updateTime: '',
  createBy: '',
  updateBy: '',
  isDeleted: 0
})

const rules: Partial<Record<keyof HbtTranslation, Rule[]>> = {
  moduleName: [
    { required: true, type: 'string', message: t('admin.language.translation.moduleNameRequired'), trigger: 'blur' },
    { type: 'string', min: 2, max: 50, message: t('admin.language.translation.moduleNameLength'), trigger: 'blur' }
  ],
  transKey: [
    { required: true, type: 'string', message: t('admin.language.translation.transKeyRequired'), trigger: 'blur' },
    { type: 'string', min: 2, max: 100, message: t('admin.language.translation.transKeyLength'), trigger: 'blur' }
  ],
  transValue: [
    { required: true, type: 'string', message: t('admin.language.translation.transValueRequired'), trigger: 'blur' }
  ],
  orderNum: [
    { required: true, type: 'number', message: t('admin.language.translation.orderNumRequired'), trigger: 'blur' }
  ],
  status: [
    { required: true, type: 'number', message: t('admin.language.translation.statusRequired'), trigger: 'change' }
  ]
}

// 获取翻译详情
const getDetail = async () => {
  if (!props.translationId) return
  
  try {
    loading.value = true
    const res = await getHbtTranslation(props.translationId)
    if (res.code === 200) {
      Object.assign(form, res.data)
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取翻译详情失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 提交表单
const handleSubmit = async () => {
  if (!formRef.value) return
  
  try {
    await formRef.value.validate()
    loading.value = true
    
    const data = {
      ...form,
      langCode: props.langCode
    }
    
    const res = props.translationId
      ? await updateHbtTranslation(data)
      : await createHbtTranslation(data)
      
    if (res.code === 200) {
      message.success(t('common.message.saveSuccess'))
      emit('success')
      handleCancel()
    } else {
      message.error(res.msg || t('common.message.saveFailed'))
    }
  } catch (error) {
    console.error('保存失败:', error)
    message.error(t('common.message.saveFailed'))
  } finally {
    loading.value = false
  }
}

// 取消
const handleCancel = () => {
  dialogVisible.value = false
  emit('update:visible', false)
}

// 监听对话框可见性变化
watch(() => dialogVisible.value, (val) => {
  emit('update:visible', val)
})

// 初始化
if (props.translationId) {
  getDetail()
}
</script>

<style lang="less" scoped>
.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}
</style> 