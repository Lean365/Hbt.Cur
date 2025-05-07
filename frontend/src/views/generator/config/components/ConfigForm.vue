<template>
  <hbt-modal
    v-model:open="visible"
    :title="title"
    :width="800"
    :loading="loading"
    @cancel="handleCancel"
    @ok="handleOk"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 19 }"
    >
      <a-form-item label="表名" name="tableName">
        <a-input v-model:value="formData.tableName" placeholder="请输入表名" />
      </a-form-item>
      <a-form-item label="作者" name="author">
        <a-input v-model:value="formData.author" placeholder="请输入作者" />
      </a-form-item>
      <a-form-item label="模块名" name="moduleName">
        <a-input v-model:value="formData.moduleName" placeholder="请输入模块名" />
      </a-form-item>
      <a-form-item label="包名" name="packageName">
        <a-input v-model:value="formData.packageName" placeholder="请输入包名" />
      </a-form-item>
      <a-form-item label="业务名" name="businessName">
        <a-input v-model:value="formData.businessName" placeholder="请输入业务名" />
      </a-form-item>
      <a-form-item label="功能名" name="functionName">
        <a-input v-model:value="formData.functionName" placeholder="请输入功能名" />
      </a-form-item>
      <a-form-item label="生成类型" name="genType">
        <a-input-number v-model:value="formData.genType" :min="1" :max="10" />
      </a-form-item>
      <a-form-item label="生成模板" name="genTemplate">
        <a-textarea
          v-model:value="formData.genTemplate"
          :rows="4"
          placeholder="请输入生成模板"
        />
      </a-form-item>
      <a-form-item label="生成路径" name="genPath">
        <a-input v-model:value="formData.genPath" placeholder="请输入生成路径" />
      </a-form-item>
      <a-form-item label="选项配置" name="options">
        <a-textarea
          v-model:value="formData.options"
          :rows="4"
          placeholder="请输入选项配置"
        />
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import { getGenConfig, createGenConfig, updateGenConfig } from '@/api/generator/genConfig'

const props = defineProps<{
  open: boolean
  title: string
  configId?: number
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

// 表单引用
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 表单数据
const formData = reactive({
  id: 0,
  tableName: '',
  author: '',
  moduleName: '',
  packageName: '',
  businessName: '',
  functionName: '',
  genType: 1,
  genTemplate: '',
  genPath: '',
  options: '',
  tenantId: 0,
  templateName: '',
  templateType: 0,
  templateContent: '',
  status: 0,
  createTime: '',
  updateTime: '',
  createBy: '',
  updateBy: '',
  isDeleted: 0
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  tableName: [{ required: true, message: '请输入表名', trigger: 'blur' }],
  author: [{ required: true, message: '请输入作者', trigger: 'blur' }],
  moduleName: [{ required: true, message: '请输入模块名', trigger: 'blur' }],
  packageName: [{ required: true, message: '请输入包名', trigger: 'blur' }],
  businessName: [{ required: true, message: '请输入业务名', trigger: 'blur' }],
  functionName: [{ required: true, message: '请输入功能名', trigger: 'blur' }],
  genTemplate: [{ required: true, message: '请输入生成模板', trigger: 'blur' }],
  genPath: [{ required: true, message: '请输入生成路径', trigger: 'blur' }]
}

// 监听配置ID变化
watch(
  () => props.configId,
  async (newVal) => {
    if (newVal) {
      loading.value = true
      try {
        const res = await getGenConfig(newVal)
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
        id: 0,
        tableName: '',
        author: '',
        moduleName: '',
        packageName: '',
        businessName: '',
        functionName: '',
        genType: 1,
        genTemplate: '',
        genPath: '',
        options: '',
        tenantId: 0,
        templateName: '',
        templateType: 0,
        templateContent: '',
        status: 0,
        createTime: '',
        updateTime: '',
        createBy: '',
        updateBy: '',
        isDeleted: 0
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
      if (props.configId) {
        await updateGenConfig(props.configId, formData)
        message.success('修改成功')
      } else {
        await createGenConfig(formData)
        message.success('新增成功')
      }
      emit('success')
      emit('update:open', false)
    } catch (error) {
      message.error(props.configId ? '修改失败' : '新增失败')
    } finally {
      loading.value = false
    }
  })
}

/** 取消按钮点击事件 */
const handleCancel = () => {
  emit('update:open', false)
  formRef.value?.resetFields()
}
</script> 