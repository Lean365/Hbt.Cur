<template>
  <div class="slider-captcha">
    <div class="captcha-image" ref="imageRef">
      <img v-if="captchaData" :src="captchaData.backgroundImage" :alt="t('identity.auth.captcha.bgImage')" class="bg-image" />
      <img v-if="captchaData" :src="captchaData.sliderImage" :alt="t('identity.auth.captcha.sliderImage')" class="slider-image"
        :style="{ left: `${sliderLeft}px` }" />
    </div>
    <div class="slider-container">
      <div class="slider-track">
        <div class="slider-bar" :style="{ width: `${sliderLeft}px` }"></div>
        <div class="slider-button" ref="sliderRef" :class="{ 'is-moving': isMoving }"
          @mousedown="handleMouseDown" @touchstart.passive="handleTouchStart">
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
import { getCaptcha, verifyCaptcha } from '@/api/security/captcha'
import type { SliderCaptchaDto } from '@/api/security/captcha'

const { t } = useI18n()
const emit = defineEmits(['success', 'error'])

const imageRef = ref<HTMLElement>()
const sliderRef = ref<HTMLElement>()
const captchaData = ref<SliderCaptchaDto | null>(null)
const sliderLeft = ref(0)
const startX = ref(0)
const isMoving = ref(false)
const verified = ref(false)
const retryCount = ref(0)
const MAX_RETRY = 3

const sliderText = computed(() => {
  if (verified.value) return t('identity.auth.captcha.success')
  if (isMoving.value) return t('identity.auth.captcha.moving')
  return t('identity.auth.captcha.default')
})

// 初始化验证码
const initCaptcha = async () => {
  try {
    if (captchaData.value) {
      console.log('[验证码] 已有验证码数据，跳过初始化')
      return
    }

    console.log('[验证码] 开始获取验证码')
    const response = await getCaptcha()
    console.log('[验证码] 获取响应:', response)
    
    // 检查响应数据结构
    if (!response?.data?.data) {
      console.error('[验证码] 响应数据为空')
      emit('error', t('identity.auth.captcha.error.dataEmpty'))
      return
    }

    const data = response.data.data
    console.log('[验证码] 解析数据:', data)

    // 检查数据完整性
    if (!data.backgroundImage || !data.sliderImage || !data.token) {
      console.error('[验证码] 数据不完整:', data)
      emit('error', t('identity.auth.captcha.error.dataIncomplete'))
      return
    }

    // 设置验证码数据
    captchaData.value = data
    sliderLeft.value = 0
    verified.value = false
    retryCount.value = 0
    console.log('[验证码] 设置数据成功:', captchaData.value)
    
  } catch (error: any) {
    console.error('[验证码] 获取失败:', error)
    if (error.response?.status === 429) {
      const waitSeconds = error.response.data?.remainingSeconds || 60
      emit('error', t('identity.auth.captcha.error.tooManyRequests', { seconds: waitSeconds }))
    } else {
      emit('error', t('identity.auth.captcha.error.getFailed'))
    }
  }
}

// 刷新验证码
const refresh = async () => {
  captchaData.value = null
  await initCaptcha()
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
  await verifySlider()
}

// 处理触摸结束
const handleTouchEnd = async () => {
  if (!isMoving.value) return
  isMoving.value = false
  document.removeEventListener('touchmove', handleTouchMove)
  document.removeEventListener('touchend', handleTouchEnd)
  await verifySlider()
}

// 验证滑块位置
const verifySlider = async () => {
  if (!captchaData.value) return
  
  try {
    const params = {
      token: captchaData.value.token,
      xOffset: Math.round(sliderLeft.value)
    }
    
    const response = await verifyCaptcha(params)
    const result = response.data.data
    
    if (result.success) {
      verified.value = true
      retryCount.value = 0
      emit('success', {
        token: captchaData.value.token,
        xOffset: Math.round(sliderLeft.value)
      })
    } else {
      verified.value = false
      retryCount.value++
      sliderLeft.value = 0
      message.error(result.message || t('identity.auth.captcha.failed'))
      if (retryCount.value >= MAX_RETRY) {
        message.error(t('identity.auth.captcha.maxRetryReached'))
        emit('error', 'MAX_RETRY_REACHED')
      }
    }
  } catch (error: any) {
    verified.value = false
    retryCount.value++
    sliderLeft.value = 0
    message.error(t('identity.auth.captcha.verifyError'))
    if (retryCount.value >= MAX_RETRY) {
      message.error(t('identity.auth.captcha.maxRetryReached'))
      emit('error', 'MAX_RETRY_REACHED')
    }
  }
}

// 暴露方法
defineExpose({
  initCaptcha,
  refresh,
  captchaData
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