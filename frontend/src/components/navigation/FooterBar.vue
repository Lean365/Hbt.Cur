<template>
  <a-layout-footer class="footer">
    <div class="footer-content">
      <!-- 左侧版权信息 -->
      <div class="footer-left">
        <span class="copyright">
          {{ t('footer.copyright', { year: new Date().getFullYear() }) }}
        </span>
      </div>

      <!-- 中间链接 -->
      <div class="footer-center">
        <a-space>
          <a href="https://beian.miit.gov.cn/" target="_blank" rel="noopener noreferrer" class="beian">
            {{ t('footer.icp') }}
          </a>
          <a-divider type="vertical" />
          <a href="/terms" class="terms">{{ t('footer.terms') }}</a>
          <a-divider type="vertical" />
          <a href="/privacy" class="privacy">{{ t('footer.privacy') }}</a>
        </a-space>
      </div>

      <!-- 右侧图标链接 -->
      <div class="footer-right">
        <a-space>
          <a href="https://github.com/Lean365/Lean.Hbt" target="_blank">
            <github-outlined />
            <span>GitHub</span>
          </a>
          <a-divider type="vertical" />
          <a href="https://lean365.cn" target="_blank">
            <global-outlined />
            <span>{{ t('footer.website') }}</span>
          </a>
          <a-divider type="vertical" />
          <a href="https://lean365.cn/docs" target="_blank">
            <book-outlined />
            <span>{{ t('footer.docs') }}</span>
          </a>
        </a-space>
      </div>
    </div>
  </a-layout-footer>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import {
  GithubOutlined,
  GlobalOutlined,
  BookOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()
</script>

<style lang="less" scoped>
.footer {
  padding: 16px 24px;
  background: var(--ant-component-background);
  color: var(--ant-text-color-secondary);
  font-size: 14px;

  .footer-content {
    display: flex;
    justify-content: space-between;
    align-items: center;
    gap: 16px;
  }

  .footer-left {
    min-width: 280px;
    text-align: left;
  }

  .footer-center {
    flex: 1;
    display: flex;
    justify-content: center;
  }

  .footer-right {
    min-width: 280px;
    text-align: right;
  }

  a, span {
    color: var(--ant-text-color-secondary);
    transition: color 0.3s;
  }

  a {
    display: inline-flex;
    align-items: center;

    .anticon {
      margin-right: 4px;
    }

    &:hover {
      color: var(--ant-primary-color);
    }
  }

  :deep(.ant-divider) {
    background-color: var(--ant-border-color-split);
  }
}

// 响应式布局
@media screen and (max-width: 768px) {
  .footer {
    padding: 12px;
    
    .footer-content {
      flex-direction: column;
      gap: 8px;
    }

    .footer-left,
    .footer-center,
    .footer-right {
      width: 100%;
      min-width: unset;
      text-align: center;
    }

    :deep(.ant-space) {
      display: flex;
      flex-direction: column;
      gap: 4px;
      width: 100%;
    }

    :deep(.ant-divider) {
      display: none;
    }
  }
}
</style> 