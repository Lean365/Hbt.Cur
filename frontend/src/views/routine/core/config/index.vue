<template>
  <div class="config-page">
    <a-card title="系统配置管理" :bordered="false">
      <a-tabs v-model:activeKey="activeTab">
        <!-- 水印配置 -->
        <a-tab-pane key="watermark" tab="水印配置">
          <a-form
            :model="watermarkForm"
            :label-col="{ span: 6 }"
            :wrapper-col="{ span: 18 }"
            layout="horizontal"
          >
            <a-form-item label="启用水印">
              <a-switch v-model:checked="watermarkForm.enabled" />
            </a-form-item>
            
            <a-form-item label="水印内容">
              <a-input
                v-model:value="watermarkForm.content"
                :disabled="!watermarkForm.enabled"
                placeholder="请输入水印内容"
              />
            </a-form-item>
            
            <a-form-item label="字体大小">
              <a-input-number
                v-model:value="watermarkForm.fontSize"
                :disabled="!watermarkForm.enabled"
                :min="12"
                :max="32"
                style="width: 100%"
              />
            </a-form-item>
            
            <a-form-item label="水印颜色">
              <a-input
                v-model:value="watermarkForm.color"
                :disabled="!watermarkForm.enabled"
                type="color"
                style="width: 100px"
              />
            </a-form-item>
            
            <a-form-item label="透明度">
              <a-slider
                v-model:value="watermarkForm.opacity"
                :disabled="!watermarkForm.enabled"
                :min="0.1"
                :max="1"
                :step="0.1"
                style="width: 100%"
              />
            </a-form-item>
            
            <a-form-item label="旋转角度">
              <a-slider
                v-model:value="watermarkForm.rotate"
                :disabled="!watermarkForm.enabled"
                :min="-45"
                :max="45"
                style="width: 100%"
              />
            </a-form-item>
            
            <a-form-item label="水印间距">
              <a-row :gutter="16">
                <a-col :span="12">
                  <a-input-number
                    v-model:value="watermarkForm.gap[0]"
                    :disabled="!watermarkForm.enabled"
                    placeholder="水平间距"
                    style="width: 100%"
                  />
                </a-col>
                <a-col :span="12">
                  <a-input-number
                    v-model:value="watermarkForm.gap[1]"
                    :disabled="!watermarkForm.enabled"
                    placeholder="垂直间距"
                    style="width: 100%"
                  />
                </a-col>
              </a-row>
            </a-form-item>
          </a-form>
        </a-tab-pane>

        <!-- 注册配置 -->
        <a-tab-pane key="register" tab="注册配置">
          <a-form
            :model="registerForm"
            :label-col="{ span: 6 }"
            :wrapper-col="{ span: 18 }"
            layout="horizontal"
          >
            <a-form-item label="显示注册入口">
              <a-switch v-model:checked="registerForm.showRegister" />
            </a-form-item>
            
            <a-form-item label="允许用户注册">
              <a-switch v-model:checked="registerForm.allowRegister" />
            </a-form-item>
            
            <a-form-item label="需要邮箱验证">
              <a-switch v-model:checked="registerForm.requireEmailVerification" />
            </a-form-item>
            
            <a-form-item label="需要手机验证">
              <a-switch v-model:checked="registerForm.requirePhoneVerification" />
            </a-form-item>
          </a-form>
        </a-tab-pane>

        <!-- 功能配置 -->
        <a-tab-pane key="features" tab="功能配置">
          <a-form
            :model="featuresForm"
            :label-col="{ span: 6 }"
            :wrapper-col="{ span: 18 }"
            layout="horizontal"
          >
            <a-form-item label="显示帮助文档">
              <a-switch v-model:checked="featuresForm.showHelp" />
            </a-form-item>
            
            <a-form-item label="显示反馈功能">
              <a-switch v-model:checked="featuresForm.showFeedback" />
            </a-form-item>
            
            <a-form-item label="显示在线用户">
              <a-switch v-model:checked="featuresForm.showOnlineUsers" />
            </a-form-item>
            
            <a-form-item label="显示系统公告">
              <a-switch v-model:checked="featuresForm.showAnnouncement" />
            </a-form-item>
          </a-form>
        </a-tab-pane>

        <!-- 安全配置 -->
        <a-tab-pane key="security" tab="安全配置">
          <a-form
            :model="securityForm"
            :label-col="{ span: 6 }"
            :wrapper-col="{ span: 18 }"
            layout="horizontal"
          >
            <a-form-item label="启用验证码">
              <a-switch v-model:checked="securityForm.enableCaptcha" />
            </a-form-item>
            
            <a-form-item label="密码最小长度">
              <a-input-number
                v-model:value="securityForm.passwordMinLength"
                :min="6"
                :max="20"
                style="width: 100%"
              />
            </a-form-item>
            
            <a-form-item label="密码复杂度要求">
              <a-checkbox-group v-model:value="passwordComplexity">
                <a-checkbox value="uppercase">必须包含大写字母</a-checkbox>
                <a-checkbox value="lowercase">必须包含小写字母</a-checkbox>
                <a-checkbox value="numbers">必须包含数字</a-checkbox>
                <a-checkbox value="special">必须包含特殊字符</a-checkbox>
              </a-checkbox-group>
            </a-form-item>
            
            <a-form-item label="登录失败锁定次数">
              <a-input-number
                v-model:value="securityForm.loginFailLockCount"
                :min="3"
                :max="10"
                style="width: 100%"
              />
            </a-form-item>
            
            <a-form-item label="锁定时间（分钟）">
              <a-input-number
                v-model:value="securityForm.loginFailLockTime"
                :min="5"
                :max="1440"
                style="width: 100%"
              />
            </a-form-item>
          </a-form>
        </a-tab-pane>

        <!-- 主题配置 -->
        <a-tab-pane key="theme" tab="主题配置">
          <a-form
            :model="themeForm"
            :label-col="{ span: 6 }"
            :wrapper-col="{ span: 18 }"
            layout="horizontal"
          >
            <a-form-item label="主题模式">
              <a-radio-group v-model:value="themeForm.mode">
                <a-radio value="light">浅色模式</a-radio>
                <a-radio value="dark">深色模式</a-radio>
                <a-radio value="auto">自动模式</a-radio>
              </a-radio-group>
            </a-form-item>
            
            <a-form-item label="主色调">
              <a-input
                v-model:value="themeForm.primaryColor"
                type="color"
                style="width: 100px"
              />
            </a-form-item>
            
            <a-form-item label="启用动画">
              <a-switch v-model:checked="themeForm.enableAnimation" />
            </a-form-item>
            
            <a-form-item label="紧凑模式">
              <a-switch v-model:checked="themeForm.compact" />
            </a-form-item>
          </a-form>
        </a-tab-pane>
      </a-tabs>

      <!-- 操作按钮 -->
      <div class="config-actions">
        <a-space>
          <a-button type="primary" @click="saveConfig" :loading="saving">
            保存配置
          </a-button>
          <a-button @click="resetConfig">
            重置配置
          </a-button>
          <a-button @click="loadConfig">
            重新加载
          </a-button>
        </a-space>
      </div>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, computed, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import { useConfigStore } from '@/stores/config'
import type { HbtWatermarkConfig, HbtRegisterConfig, HbtFeatureConfig, HbtSecurityConfig, HbtThemeConfig } from '@/types/common/config'

const configStore = useConfigStore()

// 当前激活的标签页
const activeTab = ref('watermark')

// 保存状态
const saving = ref(false)

// 水印配置表单
const watermarkForm = reactive<HbtWatermarkConfig>({
  enabled: false,
          content: configStore.config.title,
  fontSize: 16,
  color: '#000000',
  opacity: 0.1,
  rotate: -15,
  gap: [100, 100]
})

// 注册配置表单
const registerForm = reactive<HbtRegisterConfig>({
  showRegister: true,
  allowRegister: true,
  requireEmailVerification: true,
  requirePhoneVerification: false
})

// 功能配置表单
const featuresForm = reactive<HbtFeatureConfig>({
  showHelp: true,
  showFeedback: true,
  showOnlineUsers: true,
  showAnnouncement: true
})

// 安全配置表单
const securityForm = reactive<HbtSecurityConfig>({
  enableCaptcha: true,
  passwordMinLength: 6,
  passwordComplexity: {
    requireUppercase: true,
    requireLowercase: true,
    requireNumbers: true,
    requireSpecialChars: false
  },
  loginFailLockCount: 5,
  loginFailLockTime: 30
})

// 主题配置表单
const themeForm = reactive<HbtThemeConfig>({
  mode: 'light',
  primaryColor: '#1890ff',
  enableAnimation: true,
  compact: false
})

// 密码复杂度选项
const passwordComplexity = computed({
  get: () => {
    const complexity = securityForm.passwordComplexity
    const options = []
    if (complexity.requireUppercase) options.push('uppercase')
    if (complexity.requireLowercase) options.push('lowercase')
    if (complexity.requireNumbers) options.push('numbers')
    if (complexity.requireSpecialChars) options.push('special')
    return options
  },
  set: (value: string[]) => {
    securityForm.passwordComplexity = {
      requireUppercase: value.includes('uppercase'),
      requireLowercase: value.includes('lowercase'),
      requireNumbers: value.includes('numbers'),
      requireSpecialChars: value.includes('special')
    }
  }
})

/**
 * 加载配置到表单
 */
const loadConfigToForm = () => {
  const config = configStore.config
  
  // 加载水印配置
  Object.assign(watermarkForm, config.watermark)
  
  // 加载注册配置
  Object.assign(registerForm, config.register)
  
  // 加载功能配置
  Object.assign(featuresForm, config.features)
  
  // 加载安全配置
  Object.assign(securityForm, config.security)
  
  // 加载主题配置
  Object.assign(themeForm, config.theme)
}

/**
 * 保存配置
 */
const saveConfig = async () => {
  try {
    saving.value = true
    
    // 更新配置
    configStore.setConfig({
      watermark: { ...watermarkForm },
      register: { ...registerForm },
      features: { ...featuresForm },
      security: { ...securityForm },
      theme: { ...themeForm }
    })
    
    // 保存到本地存储
    configStore.saveToStorage()
    
    message.success('配置保存成功')
  } catch (error) {
    console.error('保存配置失败:', error)
    message.error('配置保存失败')
  } finally {
    saving.value = false
  }
}

/**
 * 重置配置
 */
const resetConfig = () => {
  configStore.resetConfig()
  loadConfigToForm()
  message.success('配置已重置为默认值')
}

/**
 * 重新加载配置
 */
const loadConfig = () => {
  configStore.loadFromStorage()
  loadConfigToForm()
  message.success('配置已重新加载')
}

// 组件挂载时加载配置
onMounted(() => {
  loadConfigToForm()
})
</script>

<style lang="less" scoped>
.config-page {
  padding: 24px;
}

.config-actions {
  margin-top: 24px;
  padding-top: 16px;
  border-top: 1px solid var(--ant-color-border-split);
  text-align: center;
}

:deep(.ant-form-item-label) {
  font-weight: 500;
}

:deep(.ant-tabs-content-holder) {
  padding: 24px 0;
}
</style> 