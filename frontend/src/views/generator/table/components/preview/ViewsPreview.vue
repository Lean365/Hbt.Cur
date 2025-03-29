<template>
  <div class="views-preview">
    <code-preview :code="code" />
  </div>
</template>

<script lang="ts" setup>
defineProps<{
  code: string
}>()
</script>

<style lang="less" scoped>
.views-preview {
  height: 100%;
}
</style> 