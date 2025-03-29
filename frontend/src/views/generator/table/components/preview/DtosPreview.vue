<template>
  <div class="dtos-preview">
    <code-preview :code="code" />
  </div>
</template>

<script lang="ts" setup>
defineProps<{
  code: string
}>()
</script>

<style lang="less" scoped>
.dtos-preview {
  height: 100%;
}
</style> 