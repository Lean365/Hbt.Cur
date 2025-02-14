<template>
  <div class="slider-captcha">
    <div class="captcha-image" ref="imageRef">
      <img v-if="captchaData" :src="captchaData.backgroundImage" :alt="t('captcha.bgImage')" class="bg-image" />
      <img v-if="captchaData" :src="captchaData.sliderImage" :alt="t('captcha.sliderImage')" class="slider-image"
        :style="{ left: `${sliderLeft}px` }" />
    </div>
    <div class="slider-container">
      <div class="slider-track">
        <div class="slider-bar" :style="{ width: `${sliderLeft}px` }"></div>
        <div class="slider-button" ref="sliderRef" :class="{ 'is-moving': isMoving }"
          @mousedown="handleMouseDown" @touchstart="handleTouchStart">
          <right-outlined v-if="verified" style="color: #52c41a" />
          <holder-outlined v-else />
        </div>
      </div>
      <div class="slider-text">{{ sliderText }}</div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { RightOutlined, HolderOutlined } from '@ant-design/icons-vue'
import { getCaptcha, verifyCaptcha } from '@/api/captcha'
import type { SliderCaptchaDto } from '@/api/captcha'

const { t } = useI18n()
const emit = defineEmits(['success', 'error'])

const imageRef = ref<HTMLElement>()
const sliderRef = ref<HTMLElement>()
const captchaData = ref<SliderCaptchaDto | null>(null)
const sliderLeft = ref(0)
const startX = ref(0)
const isMoving = ref(false)
const verified = ref(false)

const sliderText = computed(() => {
  if (verified.value) return t('captcha.success')
  if (isMoving.value) return t('captcha.moving')
  return t('captcha.default')
})

// 初始化验证码
const initCaptcha = async () => {
  try {
    const response = await getCaptcha()
    
    if (!response.data?.data) {
      throw new Error(t('captcha.invalidData'))
    }

    const { backgroundImage, sliderImage, token } = response.data.data
    if (!backgroundImage || !sliderImage || !token) {
      throw new Error(t('captcha.incompleteData'))
    }

    // 更新组件状态
    captchaData.value = {
      backgroundImage,
      sliderImage,
      token
    }
    sliderLeft.value = 0
    verified.value = false
  } catch (error: any) {
    message.error(t('captcha.loadError'))
    emit('error', t('captcha.loadError'))
  }
}

// 处理鼠标按下
const handleMouseDown = (e: MouseEvent) => {
  if (verified.value) return
  isMoving.value = true
  startX.value = e.clientX
  document.addEventListener('mousemove', handleMouseMove)
  document.addEventListener('mouseup', handleMouseUp)
}

// 处理触摸开始
const handleTouchStart = (e: TouchEvent) => {
  if (verified.value) return
  isMoving.value = true
  startX.value = e.touches[0].clientX
  document.addEventListener('touchmove', handleTouchMove)
  document.addEventListener('touchend', handleTouchEnd)
}

// 处理鼠标移动
const handleMouseMove = (e: MouseEvent) => {
  if (!isMoving.value) return
  e.preventDefault()
  const moveX = e.clientX - startX.value
  updateSliderPosition(moveX)
}

// 处理触摸移动
const handleTouchMove = (e: TouchEvent) => {
  if (!isMoving.value) return
  e.preventDefault()
  const moveX = e.touches[0].clientX - startX.value
  updateSliderPosition(moveX)
}

// 更新滑块位置
const updateSliderPosition = (moveX: number) => {
  const maxWidth = imageRef.value!.clientWidth - sliderRef.value!.clientWidth
  let newLeft = moveX
  if (newLeft < 0) newLeft = 0
  if (newLeft > maxWidth) newLeft = maxWidth
  sliderLeft.value = newLeft
}

// 处理鼠标松开
const handleMouseUp = async () => {
  if (!isMoving.value) return
  isMoving.value = false
  document.removeEventListener('mousemove', handleMouseMove)
  document.removeEventListener('mouseup', handleMouseUp)
  await verifyPosition()
}

// 处理触摸结束
const handleTouchEnd = async () => {
  if (!isMoving.value) return
  isMoving.value = false
  document.removeEventListener('touchmove', handleTouchMove)
  document.removeEventListener('touchend', handleTouchEnd)
  await verifyPosition()
}

// 验证位置
const verifyPosition = async () => {
  if (!captchaData.value?.token) {
    message.error(t('captcha.verifyError'))
    emit('error', t('captcha.verifyError'))
    return
  }
  
  try {
    const params = {
      token: captchaData.value.token,
      xOffset: Math.round(sliderLeft.value)
    }
    
    const response = await verifyCaptcha(params)
    
    if (response.data?.data?.success) {
      verified.value = true
      // 发送验证成功事件，包含token和偏移量
      emit('success', {
        token: captchaData.value.token,
        xOffset: Math.round(sliderLeft.value)
      })
    } else {
      verified.value = false
      message.error(response.data?.data?.message || t('captcha.failed'))
      emit('error', response.data?.data?.message || t('captcha.failed'))
      await initCaptcha()
    }
  } catch (error: any) {
    verified.value = false
    const errorMsg = error.response?.data?.message || error.message || t('captcha.verifyError')
    message.error(errorMsg)
    emit('error', errorMsg)
    await initCaptcha()
  }
}

// 暴露方法
defineExpose({
  initCaptcha
})
</script>

<style lang="less" scoped>
.slider-captcha {
  width: 100%;
  background: var(--ant-color-bg-container);
  border-radius: 8px;
  overflow: hidden;
}

.captcha-image {
  position: relative;
  width: 100%;
  height: 150px;
  background: #f5f5f5;
  overflow: hidden;

  img {
    display: block;
    width: 100%;
    height: 100%;
    object-fit: cover;
  }

  .slider-image {
    position: absolute;
    top: 0;
    left: 0;
    height: 100%;
    transition: left 0.1s;
  }
}

.slider-container {
  padding: 16px;
}

.slider-track {
  position: relative;
  height: 40px;
  background: var(--ant-color-bg-container-hover);
  border-radius: 20px;
  
  .slider-bar {
    position: absolute;
    left: 0;
    top: 0;
    height: 100%;
    background: var(--ant-color-primary-bg);
    border-radius: 20px;
    transition: width 0.1s;
  }
  
  .slider-button {
    position: absolute;
    top: 0;
    left: 0;
    width: 40px;
    height: 40px;
    background: var(--ant-color-bg-container);
    border: 1px solid var(--ant-color-border);
    border-radius: 50%;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    transition: transform 0.2s;
    
    &:hover {
      transform: scale(1.05);
    }
    
    &.is-moving {
      transform: scale(1.1);
    }
    
    .anticon {
      font-size: 16px;
      color: var(--ant-color-text-quaternary);
    }
  }
}

.slider-text {
  margin-top: 8px;
  text-align: center;
  color: var(--ant-color-text-secondary);
  font-size: 14px;
}
</style> 