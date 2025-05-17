<template>
  <a-modal
    :open="visible"
    :title="t('identity.user.allocatePost')"
    :width="500"
    :mask-closable="false"
    @cancel="handleCancel"
    @ok="handleSubmit"
  >
    <a-form :model="formState" :rules="rules" ref="formRef">
      <a-form-item name="postIds" :label="t('identity.user.postIds')">
        <a-select
          v-model:value="formState.postIds"
          mode="multiple"
          :options="postOptions"
          :placeholder="t('identity.user.postIds.placeholder')"
          style="width: 100%"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, watch, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { getUserPosts, allocateUserPosts } from '@/api/identity/user'
import { getPostOptions } from '@/api/identity/post'

const { t } = useI18n()

const props = defineProps<{
  visible: boolean
  userId: number
}>()

const emit = defineEmits(['update:visible', 'success'])

const formRef = ref()
const postOptions = ref<{ label: string; value: number }[]>([])

const formState = reactive({
  postIds: [] as number[]
})

const rules = {
  postIds: [{ required: true, message: t('identity.user.postIds.required') }]
}

// 获取岗位列表
const fetchPostList = async () => {
  try {
    console.log('开始获取岗位列表数据')
    const res = await getPostOptions()
    console.log('岗位列表接口返回数据:', res)
    if (res.data.code === 200) {
      postOptions.value = res.data.data
      console.log('岗位选项数据:', postOptions.value)
      return true
    }
    return false
  } catch (error) {
    console.error('获取岗位列表失败:', error)
    message.error(t('common.failed'))
    return false
  }
}

// 获取用户岗位
const fetchUserPosts = async () => {
  try {
    console.log('开始获取用户岗位数据, userId:', props.userId)
    const res = await getUserPosts(props.userId)
    console.log('用户岗位接口返回数据:', res)
    if (res.data.code === 200) {
      const postIds = res.data.data.map((item: any) => Number(item.postId))
      console.log('转换后的用户岗位ID:', postIds)
      return postIds
    }
    return []
  } catch (error) {
    console.error('获取用户岗位失败:', error)
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
    console.log('开始提交岗位分配')
    await formRef.value.validate()
    console.log('表单验证通过')
    
    // 确保 postIds 不为空
    if (!formState.postIds || formState.postIds.length === 0) {
      console.warn('岗位ID为空')
      message.warning(t('identity.user.postIds.required'))
      return
    }
    
    console.log('准备提交的岗位ID:', formState.postIds)
    const res = await allocateUserPosts(props.userId, formState.postIds)
    console.log('分配岗位接口返回:', res)
    
    if (res.data.code === 200) {
      message.success(t('common.success'))
      emit('success')
      formState.postIds = []
      handleCancel()
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('分配岗位失败:', error)
    message.error(t('common.failed'))
  }
}

// 监听弹窗显示
watch(
  () => props.visible,
  async (val) => {
    console.log('弹窗显示状态变化:', val)
    if (val) {
      // 先获取岗位列表
      const listLoaded = await fetchPostList()
      console.log('岗位列表加载状态:', listLoaded)
      if (!listLoaded) {
        message.error(t('common.failed'))
        return
      }
      
      // 等待列表渲染完成
      await nextTick()
      console.log('列表渲染完成')
      
      // 获取用户岗位并设置选中值
      const postIds = await fetchUserPosts()
      console.log('获取到的用户岗位ID:', postIds)
      if (postIds.length > 0) {
        formState.postIds = postIds
        console.log('设置选中岗位:', formState.postIds)
      }
    } else {
      formState.postIds = []
      console.log('清空选中岗位')
    }
  },
  { immediate: true }
)
</script> 