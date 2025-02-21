//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-02-21
// 版本号 : v1.0.0
// 描述    : 系统配置管理组件
//===================================================================

<template>
  <div class="config-container">
    <a-card title="系统配置" :bordered="false">
      <a-form :model="formState" :rules="rules" ref="formRef">
        <!-- 系统名称 -->
        <a-form-item label="系统名称" name="systemName">
          <a-input v-model:value="formState.systemName" placeholder="请输入系统名称" />
        </a-form-item>

        <!-- 系统版本 -->
        <a-form-item label="系统版本" name="version">
          <a-input v-model:value="formState.version" placeholder="请输入系统版本" />
        </a-form-item>

        <!-- 系统描述 -->
        <a-form-item label="系统描述" name="description">
          <a-textarea v-model:value="formState.description" placeholder="请输入系统描述" :rows="4" />
        </a-form-item>

        <!-- 按钮组 -->
        <a-form-item>
          <a-space>
            <a-button type="primary" @click="handleSubmit">保存</a-button>
            <a-button @click="handleReset">重置</a-button>
          </a-space>
        </a-form-item>
      </a-form>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'

interface FormState {
  systemName: string
  version: string
  description: string
}

// 表单状态
const formState = reactive<FormState>({
  systemName: '',
  version: '',
  description: ''
})

// 表单规则
const rules: Partial<Record<keyof FormState, Rule[]>> = {
  systemName: [{ required: true, message: '请输入系统名称', trigger: 'blur' }],
  version: [{ required: true, message: '请输入系统版本', trigger: 'blur' }],
  description: [{ required: true, message: '请输入系统描述', trigger: 'blur' }]
}

// 表单引用
const formRef = ref<FormInstance>()

// 提交表单
const handleSubmit = async () => {
  try {
    const values = await formRef.value?.validate()
    console.log('表单数据:', values)
    message.success('保存成功')
  } catch (error) {
    console.error('表单验证失败:', error)
  }
}

// 重置表单
const handleReset = () => {
  formRef.value?.resetFields()
}
</script>

<style lang="less" scoped>
.config-container {
  padding: 24px;
  background: #f0f2f5;
  min-height: 100%;

  :deep(.ant-card-head) {
    border-bottom: 1px solid #f0f0f0;
  }

  :deep(.ant-form) {
    max-width: 600px;
    margin: 0 auto;
  }
}
</style> 