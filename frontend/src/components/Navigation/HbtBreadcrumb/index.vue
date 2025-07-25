<template>
  <div v-if="!hideBreadcrumb" class="breadcrumb">
    <a-breadcrumb>
      <a-breadcrumb-item v-for="item in breadcrumbItems" :key="item.path">
        <router-link :to="item.path">
          <component :is="item.icon" v-if="item.icon && configStore.isShowTabIcon" :key="item.icon" />
          <span>{{ item.title }}</span>
        </router-link>
      </a-breadcrumb-item>
    </a-breadcrumb>
  </div>
</template>

<script lang="ts" setup>
import { computed, watch } from 'vue'
import { useRoute } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { useConfigStore } from '@/stores/config'
import * as Icons from '@ant-design/icons-vue'

const { t } = useI18n()
const route = useRoute()
const configStore = useConfigStore()

// 检查是否隐藏面包屑
const hideBreadcrumb = computed(() => {
  return route.meta?.hideBreadcrumb === true
})

// 监听showTabIcon配置变化
watch(
  () => configStore.isShowTabIcon,
  (newShowTabIcon) => {
    console.log('[HbtBreadcrumb] showTabIcon配置变化:', newShowTabIcon)
  }
)

// 计算面包屑项
const breadcrumbItems = computed(() => {
  const matched = route.matched.filter(item => item.meta && (item.meta.title || item.meta.transKey))
  return matched.map(item => {
    // 优先使用 transKey，如果没有则使用 title
    const titleKey = item.meta?.transKey || item.meta?.title
    const title = titleKey ? t(titleKey as string) : item.path
    
    return {
      path: item.path,
      title: title,
      icon: item.meta?.icon ? Icons[item.meta.icon as keyof typeof Icons] : undefined
    }
  })
})
</script>

<style lang="less" scoped>
.breadcrumb {
  padding: 8px 16px;
  background: var(--ant-color-bg-container);
  border-bottom: 1px solid var(--ant-border-color-split);

  :deep(.ant-breadcrumb) {
    color: var(--ant-color-text);
  }

  :deep(.ant-breadcrumb-link) {
    color: var(--ant-color-text);
  }

  :deep(.ant-breadcrumb-separator) {
    color: var(--ant-color-text-secondary);
  }
}

// 响应式布局
@media screen and (max-width: 768px) {
  .breadcrumb {
    padding: 8px;
  }
}
</style>