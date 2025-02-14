<template>
  <a-dropdown :trigger="['click']">
    <a-button type="text" class="holiday-theme">
      <template #icon>
        <component :is="holidayIcon" v-if="!currentHoliday" />
        <span v-else class="holiday-icon">{{ currentHoliday.icon }}</span>
      </template>
    </a-button>
    <template #overlay>
      <a-menu>
        <a-menu-item-group :title="t('holiday.themes')">
          <a-menu-item
            v-for="holiday in allHolidays"
            :key="holiday.id"
            @click="() => setHolidayTheme(holiday.id)"
          >
            <span class="holiday-icon">{{ holiday.icon }}</span>
            {{ t(`holiday.${holiday.id}`) }}
            <check-outlined v-if="currentHoliday?.id === holiday.id" class="selected-icon" />
          </a-menu-item>
        </a-menu-item-group>
        <a-menu-divider />
        <a-menu-item @click="showColorPicker = true">
          <template #icon>
            <highlight-outlined />
          </template>
          {{ t('holiday.customize') }}
        </a-menu-item>
        <a-menu-item @click="clearHolidayTheme">
          {{ t('holiday.clear') }}
        </a-menu-item>
        <a-menu-item @click="toggleAutoMode">
          <template #icon>
            <check-outlined v-if="isAutoMode" />
          </template>
          {{ t('holiday.autoMode') }}
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>

  <a-modal
    v-model:open="showColorPicker"
    :title="t('holiday.customizeTitle')"
    @ok="saveCustomTheme"
    :width="360"
    class="custom-theme-modal"
  >
    <div class="color-picker">
      <div class="color-item">
        <div class="color-label">{{ t('holiday.primaryColor') }}</div>
        <div class="color-input">
          <div class="color-preview" :style="{ backgroundColor: customColors.colorPrimary }">
            <input
              type="color"
              v-model="customColors.colorPrimary"
              class="color-picker-input"
            />
          </div>
          <input
            type="text"
            v-model="customColors.colorPrimary"
            class="color-code"
            @change="validateColor"
          />
        </div>
      </div>
      <div class="color-item">
        <div class="color-label">{{ t('holiday.bgColor') }}</div>
        <div class="color-input">
          <div class="color-preview" :style="{ backgroundColor: customColors.colorPrimaryBg }">
            <input
              type="color"
              v-model="customColors.colorPrimaryBg"
              class="color-picker-input"
            />
          </div>
          <input
            type="text"
            v-model="customColors.colorPrimaryBg"
            class="color-code"
            @change="validateBgColor"
          />
        </div>
      </div>
    </div>
  </a-modal>
</template>

<script lang="ts" setup>
import { computed, ref } from 'vue'
import { useI18n } from 'vue-i18n'
import { CalendarOutlined, CheckOutlined, HighlightOutlined } from '@ant-design/icons-vue'
import { useHolidayStore } from '@/stores/holiday'

const { t } = useI18n()
const holidayStore = useHolidayStore()

const currentHoliday = computed(() => holidayStore.holidayTheme)
const allHolidays = computed(() => Object.values(holidayStore.allHolidays))
const isAutoMode = computed(() => holidayStore.isAutoMode)
const holidayIcon = CalendarOutlined

const showColorPicker = ref(false)
const customColors = ref({
  colorPrimary: '#1677ff',
  colorPrimaryBg: '#e6f4ff'
})

const handlePrimaryColorChange = (color: any) => {
  customColors.value.colorPrimary = color.hex
}

const handleBgColorChange = (color: any) => {
  customColors.value.colorPrimaryBg = color.hex
}

const setHolidayTheme = (holidayId: string) => {
  holidayStore.setHolidayTheme(holidayId)
}

const clearHolidayTheme = () => {
  holidayStore.setHolidayTheme(null)
}

const toggleAutoMode = () => {
  holidayStore.toggleAutoMode()
}

const saveCustomTheme = () => {
  holidayStore.setCustomTheme({
    colorPrimary: customColors.value.colorPrimary,
    colorBgContainer: customColors.value.colorPrimaryBg,
    colorBgLayout: customColors.value.colorPrimaryBg,
    colorText: customColors.value.colorPrimary,
    colorTextSecondary: customColors.value.colorPrimary,
    colorBorder: customColors.value.colorPrimary,
    colorSplit: customColors.value.colorPrimary
  })
  showColorPicker.value = false
}

const validateColor = () => {
  const color = customColors.value.colorPrimary
  if (!/^#[0-9A-Fa-f]{6}$/.test(color)) {
    customColors.value.colorPrimary = '#1677ff'
  }
}

const validateBgColor = () => {
  const color = customColors.value.colorPrimaryBg
  if (!/^#[0-9A-Fa-f]{6}$/.test(color)) {
    customColors.value.colorPrimaryBg = '#e6f4ff'
  }
}
</script>

<style lang="less" scoped>
.holiday-theme {
  display: inline-flex;
  align-items: center;
  padding: 0 4px;
  color: inherit;
}

.holiday-icon {
  font-size: 16px;
  line-height: 1;
}

.custom-theme-modal {
  :deep(.ant-modal-content) {
    padding: 20px;
  }

  :deep(.ant-modal-header) {
    margin-bottom: 20px;
  }

  :deep(.ant-modal-title) {
    font-size: 16px;
    font-weight: 600;
    color: var(--ant-color-text);
  }

  :deep(.ant-modal-footer) {
    margin-top: 20px;
    padding-top: 20px;
    border-top: 1px solid var(--ant-color-split);
  }
}

.color-picker {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.color-item {
  display: flex;
  flex-direction: column;
  gap: 8px;

  .color-label {
    font-size: 14px;
    color: var(--ant-color-text);
  }
}

.color-input {
  display: flex;
  gap: 8px;
  align-items: center;
  width: 100%;
}

.color-preview {
  position: relative;
  width: 40px;
  height: 40px;
  border-radius: 4px;
  border: 1px solid var(--ant-color-border);
  cursor: pointer;
  transition: all 0.3s;
  overflow: hidden;

  &:hover {
    border-color: var(--ant-color-primary);
  }

  .color-picker-input {
    position: absolute;
    inset: -4px;
    width: calc(100% + 8px);
    height: calc(100% + 8px);
    padding: 0;
    border: none;
    outline: none;
    opacity: 0;
    cursor: pointer;
  }
}

.color-code {
  flex: 1;
  height: 40px;
  padding: 4px 11px;
  color: var(--ant-color-text);
  font-size: 14px;
  line-height: 1.5714285714285714;
  background-color: var(--ant-color-bg-container);
  border: 1px solid var(--ant-color-border);
  border-radius: 6px;
  transition: all 0.2s;
  font-family: monospace;

  &:hover {
    border-color: var(--ant-color-primary);
  }

  &:focus {
    border-color: var(--ant-color-primary);
    box-shadow: 0 0 0 2px rgba(5, 145, 255, 0.1);
    outline: 0;
  }
}

:deep(.ant-dropdown-menu) {
  background: var(--ant-color-bg-container);
  color: var(--ant-color-text);
  border-color: var(--ant-color-border);

  .ant-dropdown-menu-item {
    color: var(--ant-color-text);
    
    &:hover {
      background: var(--ant-color-bg-container-hover);
    }
  }

  .ant-dropdown-menu-item-group-title {
    color: var(--ant-color-text-secondary);
  }
}

:deep(.ant-dropdown-menu-item) {
  display: flex;
  align-items: center;
  gap: 8px;
}

.selected-icon {
  margin-left: auto;
  color: var(--ant-color-primary);
}

/* 确保模态框也应用主题颜色 */
:deep(.ant-modal) {
  .ant-modal-content {
    background: var(--ant-color-bg-container);
    color: var(--ant-color-text);
  }

  .ant-modal-header {
    background: var(--ant-color-bg-container);
    color: var(--ant-color-text);
    border-color: var(--ant-color-border);
  }

  .ant-modal-title {
    color: var(--ant-color-text);
  }

  .ant-modal-close {
    color: var(--ant-color-text);
  }

  .ant-modal-footer {
    border-color: var(--ant-color-border);
  }
}
</style> 