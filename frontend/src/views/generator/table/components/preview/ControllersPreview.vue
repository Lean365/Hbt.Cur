<template>
  <div class="controllers-preview">
    <code-preview :code="code" />
  </div>
</template>

<script lang="ts" setup>
defineProps<{
  code: string
}>()
</script>

<style lang="less" scoped>
.controllers-preview {
  height: 100%;
}
</style> 