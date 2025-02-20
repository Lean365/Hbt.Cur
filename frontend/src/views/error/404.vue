//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : 404.vue
// 创建者 : Claude
// 创建时间: 2024-02-19
// 描述   : 404 页面未找到
//===================================================================

<template>
  <div class="error-page">
    <div class="error-content">
      <h1>404</h1>
      <h2>{{ t('error.404.title') }}</h2>
      <p>{{ t('error.404.description') }}</p>
      <a-button type="primary" @click="goHome">
        {{ t('error.404.backHome') }}
      </a-button>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'

const { t } = useI18n()
const router = useRouter()

const goHome = () => {
  router.push('/')
}
</script>

<style lang="less" scoped>
.error-page {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
  min-height: 500px;
  background: var(--ant-bg-base);

  .error-content {
    text-align: center;

    h1 {
      font-size: 120px;
      font-weight: bold;
      color: var(--ant-primary-color);
      margin-bottom: 16px;
      line-height: 1.2;
    }

    h2 {
      font-size: 24px;
      color: var(--ant-text-color);
      margin-bottom: 16px;
    }

    p {
      font-size: 16px;
      color: var(--ant-text-color-secondary);
      margin-bottom: 24px;
    }
  }
}
</style> 