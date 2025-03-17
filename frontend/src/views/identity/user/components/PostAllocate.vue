<template>
  <a-modal
    :visible="visible"
    title="分配岗位"
    :confirm-loading="confirmLoading"
    @update:visible="(val) => $emit('update:visible', val)"
    @ok="handleSubmit"
  >
    <a-transfer
      v-model:targetKeys="targetKeys"
      :data-source="postList"
      :titles="['未分配', '已分配']"
      :render="(item) => item.title"
      :disabled="false"
    />
  </a-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { TransferItem } from 'ant-design-vue/es/transfer'
import type { Post } from '@/types/identity/post'
import { getPagedList } from '@/api/identity/post'
import { getUserPostList, allocateUserPost } from '@/api/identity/userPost'

interface Props {
  visible: boolean
  userId?: number
}

const props = withDefaults(defineProps<Props>(), {
  visible: false,
  userId: undefined
})

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

// 岗位列表
const postList = ref<TransferItem[]>([])
// 已选岗位
const targetKeys = ref<string[]>([])
// 确认加载
const confirmLoading = ref(false)

// 加载岗位列表
const loadPostList = async () => {
  try {
    const { data } = await getPagedList({
      pageIndex: 1,
      pageSize: 100
    })
    postList.value = data.rows.map(post => ({
      key: post.postId.toString(),
      title: post.postName,
      description: post.remark
    }))
  } catch (err) {
    console.error('加载岗位列表失败:', err)
  }
}

// 加载用户岗位
const loadUserPosts = async () => {
  if (!props.userId) return
  try {
    const { data } = await getUserPostList(props.userId)
    targetKeys.value = data.map(id => id.toString())
  } catch (err) {
    console.error('加载用户岗位失败:', err)
  }
}

// 提交分配
const handleSubmit = async () => {
  if (!props.userId) return
  try {
    confirmLoading.value = true
    await allocateUserPost({
      userId: props.userId,
      postIds: targetKeys.value.map(key => parseInt(key))
    })
    message.success('分配成功')
    emit('success')
    emit('update:visible', false)
  } catch (err) {
    console.error('分配岗位失败:', err)
  } finally {
    confirmLoading.value = false
  }
}

// 监听弹窗显示
watch(
  () => props.visible,
  (val) => {
    if (val) {
      loadPostList()
      loadUserPosts()
    } else {
      targetKeys.value = []
    }
  }
)
</script> 