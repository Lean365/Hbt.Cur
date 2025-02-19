<template>
  <div class="monitor-container">
    <a-card :bordered="false">
      <template #title>{{ t('dashboard.monitor') }}</template>
      <a-row :gutter="24">
        <a-col :span="24">
          <a-card :bordered="false">
            <template #title>{{ t('dashboard.realtime') }}</template>
            <a-empty :description="t('dashboard.noData')" />
          </a-card>
        </a-col>
      </a-row>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
const { t } = useI18n()
</script>

<style lang="less" scoped>
.monitor-container {
  padding: 24px;
  background-color: var(--ant-color-bg-layout);
}
</style> 