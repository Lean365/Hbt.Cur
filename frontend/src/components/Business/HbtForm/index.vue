<template>
  <div class="hbt-form">
    <v-form-designer
      ref="designerRef"
      :designer-config="designerConfig"
      :form-config="formConfig"
      @save="handleSave"
    />
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import VFormDesigner from 'vform3-builds'
import 'vform3-builds/dist/designer.umd.min.css'

const props = defineProps<{
  modelValue?: any
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value: any): void
}>()

const designerRef = ref()
const formConfig = ref(props.modelValue || {
  modelName: 'formData',
  size: 'default',
  labelPosition: 'left',
  labelWidth: 80,
  formItems: []
})

const designerConfig = {
  languageMenu: false,
  clearable: true,
  previewState: false,
  toolbarConfig: {
    showToolbar: true,
    showFormTemplates: true,
    showFormAttrs: true,
    showFormItems: true,
    showFormRules: true,
    showFormEvents: true,
    showFormMethods: true,
    showFormData: true
  }
}

const handleSave = (formJson: any) => {
  emit('update:modelValue', formJson)
}
</script>

<style scoped>
.hbt-form {
  width: 100%;
  height: 100%;
}
</style> 