<template>
  <div class="entities-preview">
    <code-preview :code="code" />
  </div>
</template>

<script lang="ts" setup>
defineProps<{
  code: string
}>()
</script>

<style lang="less" scoped>
.entities-preview {
  height: 100%;
}
</style> 