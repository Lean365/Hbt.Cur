<template>
  <div class="footer">
    <div class="footer-content">
      <div class="footer-left">
        <span class="copyright">{{ t('footer.copyright') }}</span>
      </div>
      <div class="footer-right">
        <a-button type="text" href="https://lean365.cn/docs" target="_blank">
          <book-outlined />
          <span>{{ t('footer.docs') }}</span>
        </a-button>
        <a-button type="text" href="https://github.com/Lean365/Lean.Hbt" target="_blank">
          <github-outlined />
          <span>{{ t('footer.website') }}</span>
        </a-button>
        <a-button type="text" href="https://lean365.cn/terms" target="_blank">
          <file-text-outlined />
          <span>{{ t('footer.terms') }}</span>
        </a-button>
        <a-button type="text" href="https://lean365.cn/privacy" target="_blank">
          <safety-outlined />
          <span>{{ t('footer.privacy') }}</span>
        </a-button>
        <span class="icp">{{ t('footer.icp') }}</span>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import {
  BookOutlined,
  SafetyOutlined,
  FileTextOutlined,
  GithubOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()
</script>

<style lang="less" scoped>
.footer {
  padding: 8px 0;
  background: var(--ant-color-bg-container);
  border-top: 1px solid var(--ant-border-color-split);

  .footer-content {
    display: flex;
    justify-content: space-between;
    align-items: center;
    max-width: 1200px;
    margin: 0 auto;
    padding: 0 48px;

    .footer-left {
      padding-left: 0;
      .copyright {
        color: var(--ant-color-text-secondary);
        font-size: 14px;
      }
    }

    .footer-right {
      display: flex;
      gap: 16px;
      align-items: center;
      padding-right: 0;

      :deep(.anticon) {
        font-size: 16px;
        margin-right: 4px;
      }

      .icp {
        color: var(--ant-color-text-secondary);
        font-size: 14px;
      }
    }
  }
}

// 响应式布局
@media screen and (max-width: 768px) {
  .footer {
    .footer-content {
      flex-direction: column;
      gap: 16px;
      padding: 16px;

      .footer-left,
      .footer-right {
        width: 100%;
        justify-content: center;
      }
    }
  }
}
</style> 