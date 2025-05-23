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
      <a-form-item label="岗位编码" name="postCode">
        <a-input
          v-model:value="form.postCode"
          placeholder="请输入岗位编码"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="岗位名称" name="postName">
        <a-input
          v-model:value="form.postName"
          placeholder="请输入岗位名称"
          allow-clear
        />
      </a-form-item>
      <a-form-item label="显示顺序" name="orderNum">
        <a-input-number
          v-model:value="form.orderNum"
          placeholder="请输入显示顺序"
          style="width: 100%"
          :min="0"
          :max="9999"
        />
      </a-form-item>
      <a-form-item label="状态" name="status">
        <a-radio-group v-model:value="form.status">
          <a-radio :value="0">正常</a-radio>
          <a-radio :value="1">停用</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item label="备注" name="remark">
        <a-textarea
          v-model:value="form.remark"
          placeholder="请输入备注"
          :rows="4"
          allow-clear
        />
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script setup lang="ts">
import { ref, computed, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import { message } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtPost } from '@/types/identity/post'
import { getPost, createPost, updatePost } from '@/api/identity/post'

const props = defineProps<{
  visible: boolean
  title: string
  postId?: number
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
  postCode: [
    { required: true, message: t('identity.post.fields.postCode.validation.required') },
    { max: 64, message: t('identity.post.fields.postCode.validation.maxLength') }
  ],
  postName: [
    { required: true, message: t('identity.post.fields.postName.validation.required') },
    { max: 50, message: t('identity.post.fields.postName.validation.maxLength') }
  ],
  orderNum: [
    { required: true, message: t('identity.post.fields.orderNum.validation.required') },
    { type: 'number', message: t('identity.post.fields.orderNum.validation.type') }
  ],
  status: [{ required: true, message: t('identity.post.fields.status.validation.required') }],
  remark: [{ max: 500, message: t('identity.post.fields.remark.validation.maxLength') }]
}

// 表单数据
const form = ref<Partial<HbtPost>>({
  postId: undefined,
  postCode: '',
  postName: '',
  orderNum: 0,
  status: 0,
  remark: ''
})

// 监听岗位ID变化
watch(
  () => props.postId,
  async (newVal: number | undefined) => {
    if (newVal) {
      try {
        const res = await getPost(newVal)
        if (res.data.code === 200) {
          const { postId, ...rest } = res.data.data
          form.value = rest
        } else {
          message.error(res.data.msg || t('common.failed'))
        }
      } catch (error) {
        console.error('[岗位管理] 获取岗位详情出错:', error)
        message.error(t('common.failed'))
      }
    } else {
      resetForm()
    }
  }
)

// 重置表单
const resetForm = () => {
  form.value = {
    postId: undefined,
    postCode: '',
    postName: '',
    orderNum: 0,
    status: 0,
    remark: ''
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

    if (props.postId) {
      const updateData: HbtPost = {
        ...form.value,
        postId: props.postId
      } as HbtPost
      const res = await updatePost(updateData)
      if (res.data.code === 200) {
        message.success(t('common.update.success'))
        emit('update:visible', false)
        emit('success')
      } else {
        message.error(res.data.msg || t('common.update.failed'))
      }
    } else {
      const res = await createPost(form.value as HbtPost)
      if (res.data.code === 200) {
        message.success(t('common.create.success'))
        emit('update:visible', false)
        emit('success')
      } else {
        message.error(res.data.msg || t('common.create.failed'))
      }
    }
  } catch (error) {
    console.error('[岗位管理] 提交表单出错:', error)
    message.error(t('common.failed'))
  } finally {
    submitLoading.value = false
  }
}

defineExpose({
  resetForm
})

onMounted(() => {
  const initData = async () => {
    if (props.postId) {
      const res = await getPost(props.postId)
      if (res.data.code === 200) {
        form.value = res.data.data
      }
    }
  }
  initData()
})
</script> 