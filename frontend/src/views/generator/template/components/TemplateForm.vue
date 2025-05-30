<template>
  <hbt-modal
    :open="visible"
    :title="title"
    :confirm-loading="submitLoading"
    @update:visible="handleVisibleChange"
    @ok="submitForm"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 19 }"
    >
      <a-form-item label="模板名称" name="templateName">
        <a-input v-model:value="form.templateName" placeholder="请输入模板名称" allow-clear />
      </a-form-item>
      <a-form-item label="文件名" name="fileName">
        <a-input v-model:value="form.fileName" placeholder="请输入文件名" allow-clear />
      </a-form-item>
      <a-form-item label="模板内容" name="content">
        <a-textarea
          v-model:value="form.content"
          :rows="15"
          placeholder="请输入模板内容"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-textarea
          v-model:value="form.remark"
          :rows="4"
          placeholder="请输入备注"
          allow-clear
        />
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script setup lang="ts">
import { ref, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import { message } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtGenTemplate } from '@/types/generator/genTemplate'
import { getGenTemplate as getTemplate, createGenTemplate as createTemplate, updateGenTemplate as updateTemplate } from '@/api/generator/genTemplate'

const props = defineProps<{
  visible: boolean
  title: string
  templateId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 提交按钮loading
const submitLoading = ref(false)

// 表单校验规则
const rules: Record<string, Rule[]> = {
  templateName: [
    { required: true, message: '请输入模板名称' },
    { max: 50, message: '模板名称不能超过50个字符' }
  ],
  fileName: [
    { required: true, message: '请输入文件名' },
    { max: 100, message: '文件名不能超过100个字符' }
  ],
  content: [
    { required: true, message: '请输入模板内容' }
  ],
  remark: [
    { max: 500, message: '备注不能超过500个字符' }
  ]
}

// 表单数据
const form = ref<Partial<HbtGenTemplate>>({
  templateName: '',
  fileName: '',
  content: '',
  remark: '',
  templateType: 1,
  templateCategory: 1,
  genPath: '',
  genRule: 1,
  orderNum: 0,
  status: 1
})

// 重置表单
const resetForm = () => {
  form.value = {
    templateName: '',
    fileName: '',
    content: '',
    remark: '',
    templateType: 1,
    templateCategory: 1,
    genPath: '',
    genRule: 1,
    orderNum: 0,
    status: 1
  }
  formRef.value?.resetFields()
}

// 处理弹窗显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:visible', val)
  if (!val) {
    resetForm()
  }
}

// 处理取消
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}

// 提交表单
const submitForm = async () => {
  try {
    await formRef.value?.validate()
    submitLoading.value = true

    if (props.templateId) {
      console.log('[模板管理] 开始更新模板, id:', props.templateId)
      console.log('[模板管理] 更新数据:', form.value)
      
      const updateData: HbtGenTemplate = {
        ...form.value,
        id: props.templateId
      } as HbtGenTemplate
      
      console.log('[模板管理] 发送更新请求:', updateData)
      const res = await updateTemplate(updateData)
      console.log('[模板管理] 更新响应:', res)

      if (res.data.code === 200) {
        message.success(t('common.update.success'))
        emit('update:visible', false)
        emit('success')
      } else {
        console.error('[模板管理] 更新失败:', res.data.msg)
        message.error(res.data.msg || t('common.update.failed'))
      }
    } else {
      console.log('[模板管理] 开始创建模板')
      console.log('[模板管理] 创建数据:', form.value)
      
      const res = await createTemplate(form.value as HbtGenTemplate)
      console.log('[模板管理] 创建响应:', res)

      if (res.data.code === 200) {
        message.success(t('common.create.success'))
        emit('update:visible', false)
        emit('success')
      } else {
        console.error('[模板管理] 创建失败:', res.data.msg)
        message.error(res.data.msg || t('common.create.failed'))
      }
    }
  } catch (error) {
    console.error('[模板管理] 提交表单出错:', error)
    message.error('操作失败：' + (error instanceof Error ? error.message : '未知错误'))
  } finally {
    submitLoading.value = false
  }
}

// 监听模板ID变化
watch(
  () => props.templateId,
  async (newVal: number | undefined) => {
    console.log('[模板管理] templateId changed:', newVal)
    if (newVal) {
      try {
        console.log('[模板管理] 开始获取模板详情, id:', newVal)
        const res = await getTemplate(newVal)
        console.log('[模板管理] 获取模板详情响应:', res)
        
        // 检查响应结构
        if (!res.data) {
          console.error('[模板管理] 响应数据为空')
          message.error('获取数据失败：响应数据为空')
          return
        }

        // 检查响应状态码
        if (res.data.code !== 200) {
          console.error('[模板管理] 响应状态码错误:', res.data.code, res.data.msg)
          message.error(res.data.msg || '获取数据失败')
          return
        }

        // 检查数据
        if (!res.data.data) {
          console.error('[模板管理] 响应数据为空')
          message.error('获取数据失败：数据为空')
          return
        }

        console.log('[模板管理] 设置表单数据:', res.data.data)
        form.value = res.data.data
      } catch (error) {
        console.error('[模板管理] 获取模板详情出错:', error)
        message.error('获取数据失败：' + (error instanceof Error ? error.message : '未知错误'))
      }
    } else {
      console.log('[模板管理] 重置表单')
      resetForm()
    }
  },
  { immediate: true }
)

// 监听弹窗显示状态
watch(
  () => props.visible,
  (newVal: boolean) => {
    console.log('[模板管理] visible changed:', newVal)
    if (!newVal) {
      resetForm()
    }
  }
)

defineExpose({
  resetForm
})

onMounted(() => {
  const initData = async () => {
    if (props.templateId) {
      const res = await getTemplate(props.templateId)
      if (res.data.code === 200) {
        form.value = res.data.data
      }
    }
  }
  initData()
})
</script> 