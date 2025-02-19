<template>
  <a-modal
    :title="title"
    :visible="visible"
    :confirm-loading="loading"
    @update:visible="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item :label="t('identity.dept.parentDept')" name="parentId">
        <dept-tree-select v-model:value="form.parentId" />
      </a-form-item>
      <a-form-item :label="t('identity.dept.deptName')" name="deptName">
        <a-input v-model:value="form.deptName" :placeholder="t('identity.dept.deptName.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.dept.orderNum')" name="orderNum">
        <a-input-number v-model:value="form.orderNum" :min="0" :max="999" style="width: 100%" />
      </a-form-item>
      <a-form-item :label="t('identity.dept.leader')" name="leader">
        <a-input v-model:value="form.leader" :placeholder="t('identity.dept.leader.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.dept.phone')" name="phone">
        <a-input v-model:value="form.phone" :placeholder="t('identity.dept.phone.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.dept.email')" name="email">
        <a-input v-model:value="form.email" :placeholder="t('identity.dept.email.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('common.status')" name="status">
        <a-radio-group v-model:value="form.status">
          <a-radio value="0">{{ t('common.status.normal') }}</a-radio>
          <a-radio value="1">{{ t('common.status.disabled') }}</a-radio>
        </a-radio-group>
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { Dept, DeptCreate, DeptUpdate } from '@/types/identity/dept'
import { createDept, updateDept } from '@/api/identity/dept'
import DeptTreeSelect from '@/components/DeptTreeSelect/index.vue'

const props = defineProps<{
  visible: boolean
  title: string
  formData?: Dept
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const form = reactive<DeptCreate>({
  deptName: '',
  parentId: undefined,
  orderNum: 0,
  leader: '',
  phone: '',
  email: '',
  status: '0'
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  deptName: [
    { required: true, message: t('identity.dept.deptName.validation.required') },
    { min: 2, max: 50, message: t('identity.dept.deptName.validation.length') }
  ],
  orderNum: [
    { required: true, message: t('identity.dept.orderNum.validation.required') }
  ],
  phone: [
    { pattern: /^1[3-9]\d{9}$/, message: t('identity.dept.phone.validation.format') }
  ],
  email: [
    { type: 'email', message: t('identity.dept.email.validation.format') }
  ]
}

// 加载状态
const loading = ref(false)

// 监听表单数据变化
watch(
  () => props.formData,
  (val) => {
    if (val) {
      Object.assign(form, val)
    } else {
      formRef.value?.resetFields()
      Object.assign(form, {
        deptName: '',
        parentId: undefined,
        orderNum: 0,
        leader: '',
        phone: '',
        email: '',
        status: '0'
      })
    }
  },
  { immediate: true }
)

// 提交表单
const handleSubmit = () => {
  formRef.value?.validate().then(async () => {
    loading.value = true
    try {
      if (props.formData?.id) {
        const res = await updateDept({
          ...form,
          id: props.formData.id
        })
        if (res.code === 200) {
          message.success(t('identity.dept.update.success'))
          emit('success')
          handleVisibleChange(false)
        } else {
          message.error(res.msg || t('identity.dept.update.failed'))
        }
      } else {
        const res = await createDept(form)
        if (res.code === 200) {
          message.success(t('identity.dept.create.success'))
          emit('success')
          handleVisibleChange(false)
        } else {
          message.error(res.msg || t('identity.dept.create.failed'))
        }
      }
    } catch (error) {
      console.error(error)
      message.error(t('common.failed'))
    }
    loading.value = false
  })
}

// 处理对话框显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:visible', val)
  if (!val) {
    formRef.value?.resetFields()
  }
}
</script> 