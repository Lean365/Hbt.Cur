import request from '@/utils/request'

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
export function getCaptcha(): Promise<SliderCaptchaDto> {
  return request({
    url: '/api/HbtCaptcha/slider',
    method: 'get'
  })
}

// 验证滑块验证码
export function verifyCaptcha(data: SliderValidateDto): Promise<CaptchaResultDto> {
  return request({
    url: '/api/HbtCaptcha/slider/validate',
    method: 'post',
    data
  })
} 