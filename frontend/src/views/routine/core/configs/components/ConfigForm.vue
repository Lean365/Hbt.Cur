//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : ConfigForm.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 系统配置表单组件
//===================================================================

<template>
  <a-modal
    v-model:open="visible"
    :title="title"
    width="800px"
    :mask-closable="false"
    :confirm-loading="loading"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
      layout="horizontal"
    >
      <a-form-item :label="t('core.config.fields.configName.label')" name="configName">
        <a-input
          v-model:value="formState.configName"
          :placeholder="t('core.config.fields.configName.placeholder')"
          allow-clear
        />
      </a-form-item>

      <a-form-item :label="t('core.config.fields.configKey.label')" name="configKey">
        <a-input
          v-model:value="formState.configKey"
          :placeholder="t('core.config.fields.configKey.placeholder')"
          allow-clear
        />
      </a-form-item>

      <a-form-item :label="t('core.config.fields.configValue.label')" name="configValue">
        <a-textarea
          v-model:value="formState.configValue"
          :placeholder="t('core.config.fields.configValue.placeholder')"
          :rows="4"
          allow-clear
        />
      </a-form-item>

      <a-form-item :label="t('core.config.fields.isBuiltin.label')" name="isBuiltin">
        <hbt-select
          v-model:value="formState.isBuiltin"
          dict-type="sys_yes_no"
          type="radio"
          :show-all="false"
          :placeholder="t('core.config.fields.isBuiltin.placeholder')"
        />
      </a-form-item>

      <a-form-item :label="t('core.config.fields.orderNum.label')" name="orderNum">
        <a-input-number
          v-model:value="formState.orderNum"
          :min="0"
          :max="9999"
          :placeholder="t('core.config.fields.orderNum.placeholder')"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item :label="t('core.config.fields.status.label')" name="status">
        <hbt-select
          v-model:value="formState.status"
          dict-type="sys_normal_disable"
          type="radio"
          :show-all="false"
          :placeholder="t('core.config.fields.status.placeholder')"
        />
      </a-form-item>

      <a-form-item :label="t('core.config.fields.isEncrypted.label')" name="isEncrypted">
        <hbt-select
          v-model:value="formState.isEncrypted"
          dict-type="sys_yes_no"
          type="radio"
          :show-all="false"
          :placeholder="t('core.config.fields.isEncrypted.placeholder')"
        />
      </a-form-item>

      <a-form-item :label="t('table.columns.remark')" name="remark">
        <a-textarea
          v-model:value="formState.remark"
          :placeholder="t('table.columns.remark')"
          :rows="4"
          allow-clear
        />
      </a-form-item>
    </a-form>

    <template #footer>
      <a-space>
        <a-button @click="handleCancel" :disabled="loading">{{ t('common.button.cancel') }}</a-button>
        <a-button type="primary" @click="handleSubmit" :loading="loading">{{ t('common.button.submit') }}</a-button>
      </a-space>
    </template>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { HbtConfig, HbtConfigCreate, HbtConfigUpdate } from '@/types/routine/core/config'
import { getHbtConfig, createHbtConfig, updateHbtConfig } from '@/api/routine/core/config'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  title: string
  configId?: number
}>()

const emit = defineEmits<{
  (e: 'update:open', open: boolean): void
  (e: 'success'): void
}>()

// 计算属性：处理模态框的显示状态
const visible = computed({
  get: () => props.open,
  set: (val) => emit('update:open', val)
})

// 表单引用
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 表单初始状态
const initialFormState: HbtConfigCreate = {
  configName: '',
  configKey: '',
  configValue: '',
  isBuiltin: 0,
  orderNum: 0,
  remark: '',
  status: 0,
  isEncrypted: 0
}

// 表单数据
const formState = reactive<HbtConfigCreate>({ ...initialFormState })

// 表单校验规则
const rules: Record<string, Rule[]> = {
  configName: [
    { required: true, message: t('core.config.fields.configName.validation.required'), trigger: 'blur' },
    { max: 50, message: t('core.config.fields.configName.validation.maxLength'), trigger: 'blur' }
  ],
  configKey: [
    { required: true, message: t('core.config.fields.configKey.validation.required'), trigger: 'blur' },
    { max: 50, message: t('core.config.fields.configKey.validation.maxLength'), trigger: 'blur' },
    { pattern: /^[a-zA-Z][a-zA-Z0-9_.:]*$/, message: t('core.config.fields.configKey.validation.pattern'), trigger: 'blur' }
  ],
  configValue: [
    { required: true, message: t('core.config.fields.configValue.validation.required'), trigger: 'blur' },
    { max: 500, message: t('core.config.fields.configValue.validation.maxLength'), trigger: 'blur' }
  ],
  isBuiltin: [
    { required: true, message: t('core.config.fields.isBuiltin.validation.required'), trigger: 'change' }
  ],
  orderNum: [
    { required: true, message: t('core.config.fields.orderNum.validation.required'), trigger: 'blur' },
    { type: 'number', min: 0, max: 9999, message: t('core.config.fields.orderNum.validation.range'), trigger: 'blur' }
  ],
  status: [
    { required: true, message: t('core.config.fields.status.validation.required'), trigger: 'change' }
  ],
  isEncrypted: [
    { required: true, message: t('core.config.fields.isEncrypted.validation.required'), trigger: 'change' }
  ]
}

// 获取配置信息
const getInfo = async (configId: number) => {
  try {
    loading.value = true
    const res = await getHbtConfig(configId)
    if (res.data.code === 200) {
      Object.assign(formState, res.data.data)
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 重置表单
const resetForm = () => {
  Object.assign(formState, initialFormState)
  formRef.value?.resetFields()
}

// 处理提交
const handleSubmit = async () => {
  try {
    // 表单校验
    await formRef.value?.validate()
    loading.value = true

    // 提交数据
    const res = props.configId
      ? await updateHbtConfig({ ...formState, configId: props.configId })
      : await createHbtConfig(formState)

    if (res.data.code === 200) {
      message.success(t('admin.config.message.editSuccess'))
      emit('success')
      handleCancel()
      // 完全重置表单状态
      Object.assign(formState, initialFormState)
      formRef.value?.resetFields()
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 处理对话框显示状态变化
const handleCancel = () => {
  emit('update:open', false)
  resetForm()
}

// 监听配置ID变化
watch(
  () => props.configId,
  (val) => {
    if (val) {
      getInfo(val)
    } else {
      resetForm()
    }
  }
)

// 监听对话框显示状态
watch(
  () => props.open,
  (val) => {
    if (!val) {
      resetForm()
    }
  }
)
</script>

<style lang="less" scoped>
.ant-form {
  padding: 24px 0;
}

.ant-form-item {
  margin-bottom: 24px;
}

.ant-input-number {
  width: 100%;
}

// 调整标签和输入框的布局
:deep(.ant-form-item-label) {
  min-width: 90px;
  text-align: right;
  padding-right: 8px;
}

:deep(.ant-form-item-control) {
  flex: 1;
}

// 调整文本域的样式
:deep(.ant-input-textarea) {
  width: 100%;
}
</style> 