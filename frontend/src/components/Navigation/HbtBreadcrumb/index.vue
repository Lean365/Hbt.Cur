<template>
  <div class="breadcrumb">
    <a-breadcrumb>
      <a-breadcrumb-item v-for="item in breadcrumbItems" :key="item.path">
        <router-link :to="item.path">
          <component :is="item.icon" v-if="item.icon" />
          <span>{{ item.title }}</span>
        </router-link>
      </a-breadcrumb-item>
    </a-breadcrumb>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { useI18n } from 'vue-i18n'
import * as Icons from '@ant-design/icons-vue'

const { t } = useI18n()
const route = useRoute()

// 计算面包屑项
const breadcrumbItems = computed(() => {
  const matched = route.matched.filter(item => item.meta && item.meta.title)
  return matched.map(item => ({
    path: item.path,
    title: t(item.meta?.title as string),
    icon: item.meta?.icon ? Icons[item.meta.icon as keyof typeof Icons] : undefined
  }))
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