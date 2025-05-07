<template>
  <div class="hbt-qrcode">
    <div ref="qrcodeRef" class="qrcode-container"></div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, watch } from 'vue'
import QRCode from 'qrcodejs2-fix'

const props = defineProps<{
  text: string
  width?: number
  height?: number
  colorDark?: string
  colorLight?: string
  correctLevel?: number
}>()

const qrcodeRef = ref<HTMLElement>()
let qrcode: QRCode | null = null

const initQRCode = () => {
  if (!qrcodeRef.value) return

  // 清除旧的二维码
  qrcodeRef.value.innerHTML = ''
  
  // 创建新的二维码
  qrcode = new QRCode(qrcodeRef.value, {
    text: props.text,
    width: props.width || 128,
    height: props.height || 128,
    colorDark: props.colorDark || '#000000',
    colorLight: props.colorLight || '#ffffff',
    correctLevel: props.correctLevel || QRCode.CorrectLevel.H
  })
}

// 监听属性变化
watch(() => props.text, () => {
  initQRCode()
})

watch(() => props.width, () => {
  initQRCode()
})

watch(() => props.height, () => {
  initQRCode()
})

watch(() => props.colorDark, () => {
  initQRCode()
})

watch(() => props.colorLight, () => {
  initQRCode()
})

watch(() => props.correctLevel, () => {
  initQRCode()
})

onMounted(() => {
  initQRCode()
})
</script>

<style scoped>
.hbt-qrcode {
  display: inline-block;
}

.qrcode-container {
  display: inline-block;
}
</style> 