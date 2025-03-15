<template>
  <div class="slider-captcha">
    <div class="captcha-image" ref="imageRef">
      <img v-if="captchaData" :src="captchaData.backgroundImage" :alt="t('identity.auth.captcha.bgImage')" class="bg-image" />
      <img v-if="captchaData" :src="captchaData.sliderImage" :alt="t('identity.auth.captcha.sliderImage')" class="slider-image"
        :style="{ left: `${sliderLeft}px` }" />
    </div>
    <div class="slider-container">
      <a-slider
        v-model:value="sliderValue"
        :max="maxSliderValue"
        :disabled="verified"
        :range="false"
        :tooltip-open="false"
        @change="handleSliderChange"
        @after-change="handleSliderAfterChange"
      />
      <div class="slider-text">{{ sliderText }}</div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { getCaptcha, verifyCaptcha } from '@/api/security/captcha'
import type { SliderCaptchaDto, HbtApiResult, CaptchaResultDto, SliderValidateDto } from '@/api/security/captcha'

const { t } = useI18n()
const emit = defineEmits(['success', 'error'])

const imageRef = ref<HTMLElement>()
const captchaData = ref<SliderCaptchaDto | null>(null)
const sliderValue = ref(0)
const maxSliderValue = ref(200)
const sliderLeft = ref(0)
const isMoving = ref(false)
const verified = ref(false)
const retryCount = ref(0)
const MAX_RETRY = 3

// 监听 sliderValue 变化，更新滑块位置
watch(sliderValue, (newValue) => {
  if (imageRef.value) {
    const containerWidth = imageRef.value.clientWidth
    const sliderWidth = 48 // 滑块宽度
    const maxOffset = containerWidth - sliderWidth
    // 使用更精确的计算方式
    sliderLeft.value = Math.round((newValue / maxSliderValue.value) * maxOffset * 100) / 100
  }
})

const sliderText = computed(() => {
  if (verified.value) return t('identity.auth.captcha.success')
  if (isMoving.value) return t('identity.auth.captcha.moving')
  return t('identity.auth.captcha.default')
})

// 处理滑块变化
const handleSliderChange = (value: number | [number, number]) => {
  if (typeof value === 'number' && value > 0) {
    isMoving.value = true
  }
}

// 处理滑块释放
const handleSliderAfterChange = async (value: number | [number, number]) => {
  // 如果没有移动过或者无效值，直接返回
  if (!isMoving.value || typeof value !== 'number' || value === 0) {
    return
  }

  try {
    ////console.log('[验证码] 滑块释放，开始验证，值:', value)
    //console.log('[验证码] 当前滑块位置:', {
     // sliderValue: value,
     // sliderLeft: sliderLeft.value,
     // roundedOffset: Math.round(sliderLeft.value),
      //containerWidth: imageRef.value?.clientWidth,
      //imageWidth: imageRef.value?.querySelector('.bg-image')?.clientWidth
    //})
    isMoving.value = false
    
    if (!captchaData.value) {
      console.warn('[验证码] 无验证码数据')
      return
    }

    if (verified.value) {
      //console.log('[验证码] 已验证成功，跳过重复验证')
      return
    }

    const params: SliderValidateDto = {
      token: captchaData.value.token,
      xOffset: Math.round(sliderLeft.value)
    }
    //console.log('[验证码] 发送验证请求，Token详情:', {
      //当前使用的Token: params.token,
      //验证码数据中的Token: captchaData.value.token,
      //时间戳: new Date().toISOString()
    //})
    
    const response = await verifyCaptcha(params) as unknown as CaptchaResultDto
    //console.log('[验证码] 验证响应:', response)
    
    if (response.success) {
      //console.log('[验证码] 验证成功')
      verified.value = true
      retryCount.value = 0
      emit('success', {
        token: captchaData.value.token,
        xOffset: Math.round(sliderLeft.value)
      })
    } else {
      //console.log('[验证码] 验证失败:', response.message)
      verified.value = false
      retryCount.value++
      sliderValue.value = 0
      message.error(response.message || t('identity.auth.captcha.failed'))
      if (retryCount.value >= MAX_RETRY) {
        message.error(t('identity.auth.captcha.maxRetryReached'))
        emit('error', 'MAX_RETRY_REACHED')
      }
    }
  } catch (error: any) {
    console.error('[验证码] 验证请求出错:', error)
    verified.value = false
    retryCount.value++
    sliderValue.value = 0
    message.error(t('identity.auth.captcha.verifyError'))
    if (retryCount.value >= MAX_RETRY) {
      message.error(t('identity.auth.captcha.maxRetryReached'))
      emit('error', 'MAX_RETRY_REACHED')
    }
  }
}

// 初始化验证码
const initCaptcha = async () => {
  try {
    if (captchaData.value) {
      //console.log('[验证码] 已有验证码数据，跳过初始化')
      return
    }

    //console.log('[验证码] 开始获取验证码')
    const result = await getCaptcha() as any
    //console.log('[验证码] 原始响应:', result)
    
    // 检查响应是否存在
    if (!result) {
      console.error('[验证码] 响应为空')
      emit('error', t('identity.auth.captcha.error.noResponse'))
      return
    }

    const response = {
      backgroundImage: result.backgroundImage,
      sliderImage: result.sliderImage,
      token: result.token
    }
    
    // 检查数据完整性
    if (!response.backgroundImage) {
      console.error('[验证码] 背景图片URL为空')
      emit('error', t('identity.auth.captcha.error.noBackgroundImage'))
      return
    }

    if (!response.sliderImage) {
      console.error('[验证码] 滑块图片URL为空')
      emit('error', t('identity.auth.captcha.error.noSliderImage'))
      return
    }

    if (!response.token) {
      console.error('[验证码] 验证码token为空')
      emit('error', t('identity.auth.captcha.error.noToken'))
      return
    }

    // 设置验证码数据
    captchaData.value = response
    sliderValue.value = 0
    verified.value = false
    retryCount.value = 0
    //console.log('[验证码] 设置数据成功:', {
    //  token: response.token,
    //  hasBackgroundImage: !!response.backgroundImage,
    //  hasSliderImage: !!response.sliderImage
    //})
    
  } catch (error: any) {
    console.error('[验证码] 获取失败:', error)
    console.error('[验证码] 错误详情:', {
      message: error.message,
      response: error.response,
      request: error.request,
      config: error.config
    })
    
    if (error.response?.status === 429) {
      const waitSeconds = error.response.data?.remainingSeconds || 60
      emit('error', t('identity.auth.captcha.error.tooManyRequests', { seconds: waitSeconds }))
    } else {
      emit('error', t('identity.auth.captcha.error.getFailed'))
    }
    throw error // 重新抛出错误，让调用者知道初始化失败
  }
}

// 刷新验证码
const refresh = async () => {
  captchaData.value = null
  sliderValue.value = 0
  await initCaptcha()
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
    object-fit: contain;
  }

  .slider-image {
    position: absolute;
    top: 50%;
    left: 0;
    width: 48px;
    height: 48px;
    transform: translateY(-50%);
    transition: all 0.05s linear;
    will-change: left;
    pointer-events: none;
  }
}

.slider-container {
  padding: 16px;
  
  :deep(.ant-slider) {
    margin: 10px 0;
    
    .ant-slider-handle {
      transition: all 0.05s linear;
    }
    
    .ant-slider-track {
      transition: all 0.05s linear;
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