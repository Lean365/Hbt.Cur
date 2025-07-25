<template>
  <a-modal
    :open="visible"
    :title="t('identity.user.allocateTenant')"
    :width="500"
    :mask-closable="false"
    @cancel="handleCancel"
    @ok="handleSubmit"
  >
    <a-form :model="formState" :rules="rules" ref="formRef">
      <a-form-item name="configIds" :label="t('identity.user.configIds')">
        <a-select
          v-model:value="formState.configIds"
          mode="multiple"
          :options="tenantOptions"
          :placeholder="t('identity.user.configIds.placeholder')"
          style="width: 100%"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { getUserTenants, allocateUserTenants } from '@/api/identity/user'
import { getTenantOptions } from '@/api/identity/tenant'

const { t } = useI18n()

const props = defineProps<{
  visible: boolean
  userId: number
}>()

const emit = defineEmits(['update:visible', 'success'])

const formRef = ref()
const tenantOptions = ref<{ label: string; value: string }[]>([])

const formState = reactive({
  configIds: [] as string[]
})

const rules = {
  configIds: [{ required: true, message: t('identity.user.configIds.required') }]
}

// 获取租户列表
const fetchTenantList = async () => {
  try {
    console.log('开始获取租户列表数据')
    const res = await getTenantOptions()
    console.log('租户列表接口返回数据:', res)
    if (res.data.code === 200) {
      tenantOptions.value = res.data.data.map((item: any) => ({
        label: item.label,
        value: item.value
      }))
      console.log('租户选项数据:', tenantOptions.value)
      return true
    }
    return false
  } catch (error) {
    console.error('获取租户列表失败:', error)
    message.error(t('common.failed'))
    return false
  }
}

// 获取用户租户
const fetchUserTenants = async () => {
  try {
    console.log('开始获取用户租户数据, userId:', props.userId)
    const res = await getUserTenants(props.userId)
    console.log('用户租户接口返回数据:', res)
    if (res.data.code === 200) {
      const configIds = res.data.data.map((item: any) => item.configId)
      console.log('转换后的用户租户配置ID:', configIds)
      return configIds
    }
    return []
  } catch (error) {
    console.error('获取用户租户失败:', error)
    message.error(t('common.failed'))
    return []
  }
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
}

// 提交
const handleSubmit = async () => {
  try {
    console.log('开始提交租户分配')
    await formRef.value.validate()
    console.log('表单验证通过')
    
    // 确保 configIds 不为空
    if (!formState.configIds || formState.configIds.length === 0) {
      console.warn('租户配置ID为空')
      message.warning(t('identity.user.configIds.required'))
      return
    }
    
    console.log('准备提交的租户配置ID:', formState.configIds)
    const res = await allocateUserTenants(props.userId, formState.configIds)
    console.log('分配租户接口返回:', res)
    
    if (res.data.code === 200) {
      message.success(t('common.success'))
      emit('success')
      formState.configIds = []
      handleCancel()
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('分配租户失败:', error)
    message.error(t('common.failed'))
  }
}

// 监听弹窗显示
watch(
  () => props.visible,
  async (val) => {
    console.log('弹窗显示状态变化:', val)
    if (val) {
      // 先获取租户列表
      const listLoaded = await fetchTenantList()
      console.log('租户列表加载状态:', listLoaded)
      if (!listLoaded) {
        message.error(t('common.failed'))
        return
      }
      
      // 等待列表渲染完成
      await nextTick()
      console.log('列表渲染完成')
      
      // 获取用户租户并设置选中值
      const configIds = await fetchUserTenants()
      console.log('获取到的用户租户配置ID:', configIds)
      if (configIds.length > 0) {
        formState.configIds = configIds
        console.log('设置选中租户:', formState.configIds)
      }
    } else {
      formState.configIds = []
      console.log('清空选中租户')
    }
  },
  { immediate: true }
)
</script> 