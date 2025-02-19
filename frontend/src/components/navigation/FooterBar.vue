<template>
  <a-layout-footer class="footer">
    <div class="footer-content">
      <div class="footer-left">
        <div class="copyright">
          Copyright © {{ new Date().getFullYear() }} Lean.Hbt. All rights reserved.
        </div>
        <div class="beian">
          <a href="https://beian.miit.gov.cn/" target="_blank" rel="noopener noreferrer">
            粤ICP备88889999号
          </a>
        </div>
      </div>
      <div class="links">
        <a-space>
          <a href="https://github.com/Lean365/Lean.Hbt" target="_blank">
            <github-outlined />
            <span>GitHub</span>
          </a>
          <a-divider type="vertical" />
          <a href="https://lean365.cn" target="_blank">
            <global-outlined />
            <span>官网</span>
          </a>
          <a-divider type="vertical" />
          <a href="https://lean365.cn/docs" target="_blank">
            <book-outlined />
            <span>文档</span>
          </a>
        </a-space>
      </div>
    </div>
  </a-layout-footer>
</template>

<script lang="ts" setup>
import {
  GithubOutlined,
  GlobalOutlined,
  BookOutlined
} from '@ant-design/icons-vue'
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
  }

  .footer-left {
    .beian {
      margin-top: 4px;
      
      a {
        color: var(--ant-text-color-secondary);
        transition: color 0.3s;

        &:hover {
          color: var(--ant-primary-color);
        }
      }
    }
  }

  .links {
    a {
      display: inline-flex;
      align-items: center;
      color: var(--ant-text-color-secondary);
      transition: color 0.3s;

      .anticon {
        margin-right: 4px;
      }

      &:hover {
        color: var(--ant-primary-color);
      }
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
      text-align: center;
    }

    .footer-left {
      .beian {
        margin-top: 2px;
      }
    }
  }
}
</style> 