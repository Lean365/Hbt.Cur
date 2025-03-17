<template>
  <a-modal
    :visible="visible"
    :title="title"
    :confirm-loading="confirmLoading"
    @update:visible="(val) => $emit('update:visible', val)"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 19 }"
    >
      <a-form-item label="岗位编码" name="postCode">
        <a-input
          v-model:value="formState.postCode"
          placeholder="请输入岗位编码"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="岗位名称" name="postName">
        <a-input
          v-model:value="formState.postName"
          placeholder="请输入岗位名称"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="显示顺序" name="postSort">
        <a-input-number
          v-model:value="formState.postSort"
          placeholder="请输入显示顺序"
          style="width: 100%"
          :min="0"
          :max="9999"
        />
      </a-form-item>
      <a-form-item label="状态" name="status">
        <a-radio-group v-model:value="formState.status">
          <a-radio :value="0">正常</a-radio>
          <a-radio :value="1">停用</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-textarea
          v-model:value="formState.remark"
          placeholder="请输入备注"
          :rows="4"
          allow-clear
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import { message } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { Post } from '@/types/identity/post'
import { createPost, updatePost } from '@/api/identity/post'

interface Props {
  visible: boolean
  formType: 'create' | 'update'
  formData?: Partial<Post>
}

interface Emits {
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  formType: 'create',
  formData: () => ({})
})

const emit = defineEmits<Emits>()

// 标题
const title = computed(() => (props.formType === 'create' ? '新增岗位' : '修改岗位'))

// 表单
const formRef = ref<FormInstance>()
const formState = ref<Partial<Post>>({
  postCode: '',
  postName: '',
  postSort: 0,
  status: 0
})

// 验证规则
const rules: Record<string, Rule[]> = {
  postCode: [
    { required: true, message: '请输入岗位编码' },
    { max: 64, message: '岗位编码长度不能超过64个字符' }
  ],
  postName: [
    { required: true, message: '请输入岗位名称' },
    { max: 50, message: '岗位名称长度不能超过50个字符' }
  ],
  postSort: [
    { required: true, message: '请输入显示顺序' },
    { type: 'number', message: '显示顺序必须为数字' }
  ],
  status: [{ required: true, message: '请选择状态' }],
  remark: [{ max: 500, message: '备注长度不能超过500个字符' }]
}

// 确认加载
const confirmLoading = ref(false)

// 监听表单数据变化
watch(
  () => props.formData,
  (val) => {
    if (val) {
      formState.value = { ...val }
    }
  },
  { immediate: true, deep: true }
)

// 提交表单
const handleSubmit = () => {
  formRef.value?.validate().then(async () => {
    try {
      confirmLoading.value = true
      if (props.formType === 'create') {
        await createPost(formState.value as Post)
        message.success('新增成功')
      } else {
        await updatePost(formState.value as Post)
        message.success('修改成功')
      }
      emit('success')
    } catch (err) {
      console.error(props.formType === 'create' ? '新增岗位失败:' : '修改岗位失败:', err)
    } finally {
      confirmLoading.value = false
    }
  })
}
</script> 