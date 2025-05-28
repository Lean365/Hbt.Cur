<template>
  <a-dropdown v-model:open="open" :trigger="['click']">
    <div class="tenant-switch" @click="handleClick">
      <span class="tenant-name">{{ currentTenant?.label || t('common.tenant.select') }}</span>
      <down-outlined />
    </div>
    <template #overlay>
      <a-menu v-model:selectedKeys="selectedKeys" @click="handleSelect">
        <a-menu-item v-for="tenant in tenantList" :key="tenant.value">
          <template #icon>
            <check-outlined v-if="tenant.value === currentTenant?.value" />
          </template>
          {{ tenant.label }}
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { DownOutlined, CheckOutlined } from '@ant-design/icons-vue'
import { getTenantOptions } from '@/api/identity/tenant'
import { useUserStore } from '@/stores/user'
import type { HbtTenantOption } from '@/types/identity/tenant'
import type { MenuInfo } from 'ant-design-vue/es/menu/src/interface'

const { t } = useI18n()
const userStore = useUserStore()

// 下拉菜单状态
const open = ref(false)
const selectedKeys = ref<string[]>([])

// 租户列表
const tenantList = ref<HbtTenantOption[]>([])
const currentTenant = ref<HbtTenantOption>()

// 获取租户列表
const getTenantList = async () => {
  try {
    const { data } = await getTenantOptions()
    if (data.code === 200 && Array.isArray(data.data)) {
      tenantList.value = data.data
      // 设置当前租户
      const currentTenantId = userStore.getCurrentTenantId()
      currentTenant.value = tenantList.value.find(t => t.value === currentTenantId)
      if (currentTenant.value) {
        selectedKeys.value = [currentTenant.value.value.toString()]
      }
    }
  } catch (error) {
    console.error('[租户切换] 获取租户列表失败:', error)
    message.error(t('common.failed'))
  }
}

// 处理点击
const handleClick = () => {
  if (tenantList.value.length === 0) {
    getTenantList()
  }
}

// 处理选择
const handleSelect = async (info: MenuInfo) => {
  try {
    const tenantId = parseInt(info.key as string)
    // 更新当前租户
    currentTenant.value = tenantList.value.find(t => t.value === tenantId)
    // 更新用户Store中的租户ID
    await userStore.setCurrentTenantId(tenantId)
    message.success(t('common.tenant.switch.success'))
    // 刷新页面以应用新的租户设置
    window.location.reload()
  } catch (error) {
    console.error('[租户切换] 切换租户失败:', error)
    message.error(t('common.tenant.switch.failed'))
  }
}

onMounted(() => {
  getTenantList()
})
</script>

<style lang="less" scoped>
.tenant-switch {
  display: flex;
  align-items: center;
  cursor: pointer;
  padding: 0 12px;
  height: 100%;
  
  &:hover {
    background: rgba(0, 0, 0, 0.025);
  }

  .tenant-name {
    margin-right: 8px;
    max-width: 120px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }
}
</style> 