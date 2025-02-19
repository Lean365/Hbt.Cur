import request from '@/utils/request'
import type { InternalAxiosRequestConfig } from 'axios'

// API响应包装
export interface HbtApiResult<T> {
  code: number;
  msg: string;
  data: T;
}

// 滑块验证码DTO
export interface SliderCaptchaDto {
  backgroundImage: string;
  sliderImage: string;
  token: string;
}

// 滑块验证请求DTO
export interface SliderValidateDto {
  token: string;
  xOffset: number;
}

// 验证码结果DTO
export interface CaptchaResultDto {
  success: boolean;
  message?: string;
}

// 获取滑块验证码
export function getCaptcha() {
  return request<HbtApiResult<SliderCaptchaDto>>({
    url: '/api/HbtCaptcha/slider',
    method: 'get'
  })
}

// 验证滑块验证码
export function verifyCaptcha(data: SliderValidateDto) {
  return request<HbtApiResult<CaptchaResultDto>>({
    url: '/api/HbtCaptcha/slider/validate',
    method: 'post',
    data
  })
} 