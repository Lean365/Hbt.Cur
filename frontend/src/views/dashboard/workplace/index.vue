<template>
  <div class="workplace-container">
    <a-card :bordered="false">
      <template #title>{{ t('dashboard.workplace') }}</template>
      <a-row :gutter="24">
        <a-col :span="6">
          <a-statistic
            :title="t('dashboard.totalVisits')"
            :value="112893"
            style="margin-right: 50px"
          >
            <template #suffix>
              <rise-outlined style="color: #3f8600" />
            </template>
          </a-statistic>
        </a-col>
        <a-col :span="6">
          <a-statistic
            :title="t('dashboard.totalUsers')"
            :value="112893"
            style="margin-right: 50px"
          >
            <template #suffix>
              <user-outlined style="color: #cf1322" />
            </template>
          </a-statistic>
        </a-col>
        <a-col :span="6">
          <a-statistic
            :title="t('dashboard.totalOrders')"
            :value="112893"
            style="margin-right: 50px"
          >
            <template #suffix>
              <shopping-cart-outlined style="color: #3f8600" />
            </template>
          </a-statistic>
        </a-col>
        <a-col :span="6">
          <a-statistic
            :title="t('dashboard.totalSales')"
            :value="112893"
            style="margin-right: 50px"
          >
            <template #suffix>
              <dollar-outlined style="color: #cf1322" />
            </template>
          </a-statistic>
        </a-col>
      </a-row>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { 
  RiseOutlined,
  UserOutlined,
  ShoppingCartOutlined,
  DollarOutlined 
} from '@ant-design/icons-vue'

const { t } = useI18n()
</script>

<style lang="less" scoped>
.workplace-container {
  padding: 24px;
  background-color: var(--ant-color-bg-layout);
}
</style> 