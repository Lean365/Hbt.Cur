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
    :open="visible"
    :title="title"
    :confirm-loading="loading"
    @ok="handleOk"
    @cancel="handleCancel"
    width="800px"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      layout="horizontal"
    >
      <a-row :gutter="24">
        <a-col :span="12">
          <a-form-item name="configName" label="配置名称">
            <a-input
              v-model:value="formState.configName"
              placeholder="请输入配置名称"
              allow-clear
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="configKey" label="配置键名">
            <a-input
              v-model:value="formState.configKey"
              placeholder="请输入配置键名"
              allow-clear
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="configType" label="配置类型">
            <hbt-select
              v-model:value="formState.configType"
              :options="configTypeOptions"
              placeholder="请选择配置类型"
              allow-clear
            />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item name="status" label="状态">
            <hbt-select
              v-model:value="formState.status"
              :options="statusOptions"
              placeholder="请选择状态"
              allow-clear
            />
          </a-form-item>
        </a-col>
        <a-col :span="24">
          <a-form-item name="configValue" label="配置值">
            <a-textarea
              v-model:value="formState.configValue"
              placeholder="请输入配置值"
              :auto-size="{ minRows: 3, maxRows: 5 }"
              allow-clear
            />
          </a-form-item>
        </a-col>
        <a-col :span="24">
          <a-form-item name="remark" label="备注">
            <a-textarea
              v-model:value="formState.remark"
              placeholder="请输入备注"
              :auto-size="{ minRows: 2, maxRows: 4 }"
              allow-clear
            />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { HbtConfig } from '@/types/admin/config'
import { useDictData } from '@/hooks/useDictData'

const { t } = useI18n()

// === 属性定义 ===
interface Props {
  visible: boolean
  title: string
  loading?: boolean
  model?: Partial<HbtConfig>
}

const props = withDefaults(defineProps<Props>(), {
  loading: false,
  model: () => ({})
})

// === 事件定义 ===
const emit = defineEmits(['update:visible', 'ok', 'cancel'])

// === 字典数据 ===
const { dictDataMap } = useDictData([
  'sys_config_type',
  'sys_normal_disable'
])

// 计算属性：配置类型选项
const configTypeOptions = computed(() => {
  return dictDataMap.value['sys_config_type'] || []
})

// 计算属性：状态选项
const statusOptions = computed(() => {
  return dictDataMap.value['sys_normal_disable'] || []
})

// === 表单状态 ===
const formRef = ref<FormInstance>()
const formState = ref<Partial<HbtConfig>>({
  configName: '',
  configKey: '',
  configValue: '',
  configType: undefined,
  status: undefined,
  remark: ''
})

// === 表单校验规则 ===
const rules = {
  configName: [
    { required: true, message: '请输入配置名称', trigger: 'blur' },
    { min: 2, max: 100, message: '长度在 2 到 100 个字符', trigger: 'blur' }
  ],
  configKey: [
    { required: true, message: '请输入配置键名', trigger: 'blur' },
    { min: 2, max: 100, message: '长度在 2 到 100 个字符', trigger: 'blur' }
  ],
  configValue: [
    { required: true, message: '请输入配置值', trigger: 'blur' }
  ],
  configType: [
    { required: true, message: '请选择配置类型', trigger: 'change' }
  ],
  status: [
    { required: true, message: '请选择状态', trigger: 'change' }
  ]
}

// === 监听数据变化 ===
watch(
  () => props.model,
  (val) => {
    formState.value = { ...formState.value, ...val }
  },
  { deep: true, immediate: true }
)

// === 方法定义 ===
const handleOk = async () => {
  try {
    await formRef.value?.validate()
    emit('ok', formState.value)
  } catch (error) {
    // 校验失败
  }
}

const handleCancel = () => {
  formRef.value?.resetFields()
  emit('cancel')
}
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