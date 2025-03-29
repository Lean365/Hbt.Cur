<template>
  <div v-loading="loading" :style="'height:'+ height">
    <iframe :src="src" frameborder="no" style="width: 100%;height: 100%" scrolling="auto" />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'

const src = ref(import.meta.env.VITE_APP_BASE_API + '/swagger/index.html')
const height = ref(document.documentElement.clientHeight - 94.5 + 'px')
const loading = ref(true)

onMounted(() => {
  setTimeout(() => {
    loading.value = false
  }, 230)

  window.onresize = () => {
    height.value = document.documentElement.clientHeight - 94.5 + 'px'
  }
})
</script> 