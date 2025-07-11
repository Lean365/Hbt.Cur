<template>
  <div class="hbt-form">
    <!-- 表单设计器模式 -->
    <fc-designer
      v-if="!readonly"
      ref="designerRef"
      :height="height"
      :config="designerConfig"
      :locale="locale"
      @save="handleSave"
    />
    
    <!-- 表单预览/渲染模式 -->
    <form-create
      v-else
      :rule="formRule"
      :option="formOption"
      v-model="formValue"
      @submit="handleSubmit"
      @change="handleChange"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, watch, onMounted } from 'vue'
import ZH from "@form-create/antd-designer/locale/zh-CN.js"

const props = defineProps<{
  modelValue?: any
  formId?: number
  readonly?: boolean
  showSubmit?: boolean
  height?: string
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value: any): void
  (e: 'submit', value: any): void
  (e: 'change', value: any): void
}>()

const designerRef = ref()
const height = ref(props.height || '500px')
const locale = ref(ZH)

const formRule = ref<any[]>([])
const formOption = ref({})
const formValue = ref({})

const designerConfig = ref({
  showSaveBtn: true
})

// 处理设计器保存
const handleSave = () => {
  if (designerRef.value) {
    const formConfig = designerRef.value.getRule()
    const configStr = JSON.stringify({ rule: formConfig }, null, 2)
    emit('update:modelValue', configStr)
  }
}

// 处理表单提交
const handleSubmit = (formData: any) => {
  emit('submit', formData)
}

// 处理表单值变化
const handleChange = (formData: any) => {
  emit('change', formData)
}

// 监听modelValue变化
watch(() => props.modelValue, (newValue) => {
  console.log('watch modelValue:', newValue)
  if (newValue) {
    try {
      const config = typeof newValue === 'string' ? JSON.parse(newValue) : newValue
      console.log('解析后的配置:', config)
      // 设计器模式
      if (!props.readonly && designerRef.value) {
        console.log('designerRef.value:', designerRef.value)
        if (config.rule) {
          designerRef.value.setRule(config.rule)
        } else if (Array.isArray(config)) {
          designerRef.value.setRule(config)
        } else if (config.formItems) {
          designerRef.value.setRule(config.formItems)
        } else {
          console.warn('未知的配置格式:', config)
        }
      }
      // 预览模式
      if (props.readonly) {
        if (config.rule) {
          formRule.value = config.rule
        } else if (Array.isArray(config)) {
          formRule.value = config
        } else if (config.formItems) {
          formRule.value = config.formItems
        } else {
          formRule.value = []
        }
      }
    } catch (error) {
      console.error('解析配置失败:', error)
      if (props.readonly) formRule.value = []
    }
  } else {
    if (props.readonly) formRule.value = []
  }
}, { immediate: true })

onMounted(() => {
  console.log('onMounted designerRef:', designerRef.value)
  if (props.modelValue) {
    try {
      const config = typeof props.modelValue === 'string' ? JSON.parse(props.modelValue) : props.modelValue
      console.log('初始解析后的配置:', config)
      if (!props.readonly && designerRef.value) {
        if (config.rule) {
          designerRef.value.setRule(config.rule)
        } else if (Array.isArray(config)) {
          designerRef.value.setRule(config)
        } else if (config.formItems) {
          designerRef.value.setRule(config.formItems)
        } else {
          console.warn('未知的初始配置格式:', config)
        }
      }
      if (props.readonly) {
        if (config.rule) {
          formRule.value = config.rule
        } else if (Array.isArray(config)) {
          formRule.value = config
        } else if (config.formItems) {
          formRule.value = config.formItems
        } else {
          formRule.value = []
        }
      }
    } catch (error) {
      console.error('解析初始配置失败:', error)
      if (props.readonly) formRule.value = []
    }
  } else {
    if (props.readonly) formRule.value = []
  }
})
</script>

<style scoped>
.hbt-form {
  width: 100%;
  height: v-bind('props.height || "100%"');
}
</style> 