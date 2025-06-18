<template>
  <div class="workflow-config">
    <hbt-workflow-designer v-model:value="workflowConfig" />
  </div>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'

const props = defineProps<{
  value: string
}>()

const emit = defineEmits<{
  (e: 'update:value', value: string): void
}>()

const workflowConfig = ref(props.value)

watch(() => props.value, (newVal) => {
  workflowConfig.value = newVal
})

watch(workflowConfig, (newVal) => {
  emit('update:value', newVal)
})
</script>

<style lang="less" scoped>
.workflow-config {
  height: 100%;
  border: 1px solid #d9d9d9;
  border-radius: 2px;
}
</style> 