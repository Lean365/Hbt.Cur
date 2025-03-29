<template>
  <div class="locales-preview">
    <code-preview :code="code" />
  </div>
</template>

<script lang="ts" setup>
defineProps<{
  code: string
}>()
</script>

<style lang="less" scoped>
.locales-preview {
  height: 100%;
}
</style> 