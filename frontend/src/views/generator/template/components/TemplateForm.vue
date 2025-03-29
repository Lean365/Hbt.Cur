<template>
  <a-modal
    :visible="visible"
    :title="title"
    :confirm-loading="loading"
    width="800px"
    @ok="handleOk"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 19 }"
    >
      <a-form-item label="模板名称" name="templateName">
        <a-input v-model:value="formData.templateName" placeholder="请输入模板名称" />
      </a-form-item>
      <a-form-item label="文件名" name="fileName">
        <a-input v-model:value="formData.fileName" placeholder="请输入文件名" />
      </a-form-item>
      <a-form-item label="模板内容" name="content">
        <a-textarea
          v-model:value="formData.content"
          :rows="15"
          placeholder="请输入模板内容"
        />
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-textarea
          v-model:value="formData.remark"
          :rows="4"
          placeholder="请输入备注"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import { getTemplate, createTemplate, updateTemplate } from '@/api/generator/template'

const props = defineProps<{
  visible: boolean
  title: string
  templateId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

// 表单引用
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 表单数据
const formData = reactive({
  templateName: '',
  fileName: '',
  content: '',
  remark: ''
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  templateName: [{ required: true, message: '请输入模板名称', trigger: 'blur' }],
  fileName: [{ required: true, message: '请输入文件名', trigger: 'blur' }],
  content: [{ required: true, message: '请输入模板内容', trigger: 'blur' }]
}

// 监听模板ID变化
watch(
  () => props.templateId,
  async (newVal) => {
    if (newVal) {
      loading.value = true
      try {
        const res = await getTemplate(newVal)
        if (res.data) {
          Object.assign(formData, res.data)
        }
      } catch (error) {
        message.error('获取数据失败')
      } finally {
        loading.value = false
      }
    } else {
      Object.assign(formData, {
        templateName: '',
        fileName: '',
        content: '',
        remark: ''
      })
    }
  },
  { immediate: true }
)

/** 确定按钮点击事件 */
const handleOk = () => {
  formRef.value?.validate().then(async () => {
    loading.value = true
    try {
      if (props.templateId) {
        await updateTemplate(props.templateId, formData)
        message.success('修改成功')
      } else {
        await createTemplate(formData)
        message.success('新增成功')
      }
      emit('success')
      emit('update:visible', false)
    } catch (error) {
      message.error(props.templateId ? '修改失败' : '新增失败')
    } finally {
      loading.value = false
    }
  })
}

/** 取消按钮点击事件 */
const handleCancel = () => {
  emit('update:visible', false)
  formRef.value?.resetFields()
}
</script> 