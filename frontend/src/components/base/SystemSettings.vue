<template>
  <a-dropdown :trigger="['click']" placement="bottom" class="settings-dropdown">
    <a-button type="text">
      <template #icon>
        <skin-outlined />
      </template>
    </a-button>
    <template #overlay>
      <div style="display: none"></div>
    </template>
  </a-dropdown>
  <a-drawer
    :title="t('header.settings.title')"
    placement="right"
    :visible="visible"
    @close="onClose"
  >
    <div class="settings-content">
      <div class="settings-section">
        <h3>{{ t('settings.interface') }}</h3>
        
        <a-form layout="vertical">
          <a-form-item :label="t('settings.navMode')">
            <a-radio-group v-model:value="settings.navMode">
              <a-radio value="side">{{ t('settings.navMode.side') }}</a-radio>
              <a-radio value="top">{{ t('settings.navMode.top') }}</a-radio>
              <a-radio value="mix">{{ t('settings.navMode.mix') }}</a-radio>
            </a-radio-group>
          </a-form-item>

          <a-form-item>
            <a-checkbox v-model:checked="settings.fixedHeader">
              {{ t('settings.fixedHeader') }}
            </a-checkbox>
          </a-form-item>

          <a-form-item>
            <a-checkbox v-model:checked="settings.showBreadcrumb">
              {{ t('settings.showBreadcrumb') }}
            </a-checkbox>
          </a-form-item>

          <a-form-item>
            <a-checkbox v-model:checked="settings.showTabs">
              {{ t('settings.showTabs') }}
            </a-checkbox>
          </a-form-item>

          <a-form-item>
            <a-checkbox v-model:checked="settings.showFooter">
              {{ t('settings.showFooter') }}
            </a-checkbox>
          </a-form-item>

          <a-form-item>
            <a-checkbox v-model:checked="settings.autoHideHeader">
              {{ t('settings.autoHideHeader') }}
            </a-checkbox>
          </a-form-item>
        </a-form>
      </div>

      <div class="settings-footer">
        <a-space>
          <a-button @click="resetSettings">{{ t('settings.reset') }}</a-button>
          <a-button type="primary" @click="saveSettings">{{ t('settings.save') }}</a-button>
        </a-space>
      </div>
    </div>
  </a-drawer>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import { useI18n } from 'vue-i18n'
import { SkinOutlined } from '@ant-design/icons-vue'

const { t } = useI18n()
const visible = ref(false)
const settings = reactive({
  navMode: 'side',
  fixedHeader: true,
  showBreadcrumb: true,
  showTabs: true,
  showFooter: true,
  autoHideHeader: false
})

const showDrawer = () => {
  visible.value = true
}

const onClose = () => {
  visible.value = false
}

const resetSettings = () => {
  settings.navMode = 'side'
  settings.fixedHeader = true
  settings.showBreadcrumb = true
  settings.showTabs = true
  settings.showFooter = true
  settings.autoHideHeader = false
}

const saveSettings = () => {
  // TODO: Save settings to store/localStorage
  onClose()
}
</script>

<style lang="less" scoped>
.settings-dropdown {
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
  font-size: 16px;
  line-height: 1;
}

.settings-content {
  display: flex;
  flex-direction: column;
  height: 100%;

  .settings-section {
    flex: 1;
    padding: 24px;

    h3 {
      margin-bottom: 24px;
      color: rgba(0, 0, 0, 0.85);
      font-weight: 500;
      font-size: 16px;
    }
  }

  .settings-footer {
    padding: 24px;
    text-align: right;
    border-top: 1px solid #f0f0f0;
  }
}
</style> 