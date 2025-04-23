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
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('admin.config.name')" name="configName">
            <a-input
              v-model:value="formState.configName"
              :placeholder="t('admin.config.placeholder.name')"
              allow-clear
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('admin.config.key')" name="configKey">
            <a-input
              v-model:value="formState.configKey"
              :placeholder="t('admin.config.placeholder.key')"
              allow-clear
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('admin.config.value')" name="configValue">
            <a-input
              v-model:value="formState.configValue"
              :placeholder="t('admin.config.placeholder.value')"
              allow-clear
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('admin.config.builtin')" name="configBuiltin">
            <hbt-select
              v-model:value="formState.configBuiltin"
              dict-type="sys_yes_no"
              type="radio"
              :placeholder="t('admin.config.placeholder.builtin')"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item :label="t('admin.config.order')" name="orderNum">
            <a-input-number
              v-model:value="formState.orderNum"
              :min="0"
              :max="9999"
              :placeholder="t('admin.config.placeholder.order')"
              style="width: 100%"
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item :label="t('admin.config.status')" name="status">
            <hbt-select
              v-model:value="formState.status"
              dict-type="sys_normal_disable"
              type="radio"
              :placeholder="t('admin.config.placeholder.status')"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="24">
        <a-col :span="24">
          <a-form-item :label="t('admin.config.remark')" name="remark">
            <a-textarea
              v-model:value="formState.remark"
              :placeholder="t('admin.config.placeholder.remark')"
              :rows="4"
              allow-clear
            />
          </a-form-item>
        </a-col>
      </a-row>
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
import type { HbtConfig, HbtConfigCreate, HbtConfigUpdate } from '@/types/admin/config'
import { getHbtConfig, createHbtConfig, updateHbtConfig } from '@/api/admin/config'

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
  configBuiltin: 0,
  orderNum: 0,
  remark: '',
  status: 0
}

// 表单数据
const formState = reactive<HbtConfigCreate>({ ...initialFormState })

// 表单校验规则
const rules: Record<string, Rule[]> = {
  configName: [
    { required: true, message: t('admin.config.validation.name.required'), trigger: 'blur' },
    { max: 100, message: t('admin.config.validation.name.maxLength'), trigger: 'blur' }
  ],
  configKey: [
    { required: true, message: t('admin.config.validation.key.required'), trigger: 'blur' },
    { max: 100, message: t('admin.config.validation.key.maxLength'), trigger: 'blur' },
    { pattern: /^[a-zA-Z][a-zA-Z0-9_.:]*$/, message: t('admin.config.validation.key.pattern'), trigger: 'blur' }
  ],
  configValue: [
    { required: true, message: t('admin.config.validation.value.required'), trigger: 'blur' },
    { max: 500, message: t('admin.config.validation.value.maxLength'), trigger: 'blur' }
  ],
  configBuiltin: [
    { required: true, message: t('admin.config.validation.builtin.required'), trigger: 'change' }
  ],
  orderNum: [
    { required: true, message: t('admin.config.validation.order.required'), trigger: 'blur' },
    { type: 'number', min: 0, max: 9999, message: t('admin.config.validation.order.range'), trigger: 'blur' }
  ],
  status: [
    { required: true, message: t('admin.config.validation.status.required'), trigger: 'change' }
  ]
}

// 获取配置信息
const getInfo = async (configId: number) => {
  try {
    loading.value = true
    const res = await getHbtConfig(configId)
    if (res.code === 200) {
      Object.assign(formState, res.data)
    } else {
      message.error(res.msg || t('common.failed'))
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

// 提交表单
const handleSubmit = () => {
  formRef.value?.validate().then(async () => {
    try {
      loading.value = true
      let res
      if (props.configId) {
        const updateData: HbtConfigUpdate = {
          configId: props.configId,
          ...formState
        }
        res = await updateHbtConfig(updateData)
      } else {
        res = await createHbtConfig(formState)
      }
      if (res.code === 200) {
        message.success(t('common.success'))
        emit('success')
        handleCancel()
      } else {
        message.error(res.msg || t('common.failed'))
      }
    } catch (error) {
      console.error(error)
      message.error(t('common.failed'))
    } finally {
      loading.value = false
    }
  })
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