<template>
  <a-dropdown :trigger="['hover']" placement="bottom" class="tenant-dropdown">
    <a-button type="text" :loading="loading">
      <template #icon>
        <shop-outlined v-if="!loading" />
      </template>
    </a-button>
    <template #overlay>
      <a-menu @click="({ key }) => handleTenantChange(Number(key))">
        <a-menu-item v-for="tenant in tenantList" :key="tenant.value">
          <template #icon>
            <check-outlined v-if="currentTenantId === tenant.value" />
          </template>
          <span :class="{ 'current-tenant': currentTenantId === tenant.value }">
            {{ tenant.label }}
          </span>
        </a-menu-item>
        <a-menu-divider v-if="tenantList.length === 0" />
        <a-menu-item v-if="tenantList.length === 0" disabled>
          {{ loading ? t('common.loading') : t('common.noData') }}
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { ShopOutlined, CheckOutlined } from '@ant-design/icons-vue'
import { getTenantOptions } from '@/api/identity/tenant'
import { useUserStore } from '@/stores/user'
import type { HbtTenantOption } from '@/types/identity/tenant'

const { t } = useI18n()
const userStore = useUserStore()
const loading = ref(false)
const tenantList = ref<HbtTenantOption[]>([])
const currentTenantId = ref<number>()

// 获取租户列表
const getTenantList = async () => {
  loading.value = true
  try {
    const { data } = await getTenantOptions()
    if (data.code === 200 && Array.isArray(data.data)) {
      tenantList.value = data.data
      // 设置当前租户
      currentTenantId.value = userStore.getCurrentTenantId()
    }
  } catch (error) {
    console.error('[租户切换] 获取租户列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 处理租户切换
const handleTenantChange = async (tenantId: number) => {
  if (loading.value || tenantId === currentTenantId.value) return
  
  loading.value = true
  try {
    // 更新用户Store中的租户ID
    await userStore.setCurrentTenantId(tenantId)
    message.success(t('common.tenant.switch.success'))
    // 刷新页面以应用新的租户设置
    window.location.reload()
  } catch (error) {
    console.error('[租户切换] 切换租户失败:', error)
    message.error(t('common.tenant.switch.failed'))
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  getTenantList()
})
</script>

<style lang="less" scoped>
.tenant-dropdown {
  display: flex;
  align-items: center;
  justify-content: center;
}

:deep(.ant-dropdown-trigger) {
  display: flex;
  align-items: center;
  justify-content: center;
}

:deep(.ant-btn) {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 32px;
  width: 32px;
  padding: 0;
}

:deep(.anticon) {
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 14px;
  line-height: 1;
}

.current-tenant {
  font-weight: 500;
  color: #1890ff;
}
</style> 