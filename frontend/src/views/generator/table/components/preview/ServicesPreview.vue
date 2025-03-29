<template>
  <div class="services-preview">
    <code-preview :code="code" />
  </div>
</template>

<script lang="ts" setup>
defineProps<{
  code: string
}>()
</script>

<style lang="less" scoped>
.services-preview {
  height: 100%;
}
</style> 