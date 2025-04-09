<template>
  <a-button type="text" class="setting-button" @click="showDrawer = true">
    <template #icon>
      <skin-outlined />
    </template>
  </a-button>

  <a-drawer
    v-model:open="showDrawer"
    :title="t('header.settings.title')"
    placement="right"
    width="300"
    @close="onClose"
  >
    <div class="settings-content">
      <div class="settings-section">
        <h3>{{ t('settings.interface') }}</h3>
        
        <a-form layout="vertical">
          <a-form-item :label="t('settings.navMode.label')">
            <a-radio-group v-model:value="settings.navMode" button-style="solid">
              <a-radio-button value="side">{{ t('settings.navMode.side') }}</a-radio-button>
              <a-radio-button value="top">{{ t('settings.navMode.top') }}</a-radio-button>
              <a-radio-button value="mix">{{ t('settings.navMode.mix') }}</a-radio-button>
            </a-radio-group>
          </a-form-item>

          <a-form-item :label="t('settings.sidebarColor')">
            <div class="color-picker">
              <div 
                v-for="color in themeColors" 
                :key="color"
                class="color-block"
                :class="{ active: settings.sidebarColor === color }"
                :style="{ backgroundColor: color }"
                @click="settings.sidebarColor = color"
              ></div>
            </div>
          </a-form-item>

          <a-form-item :label="t('settings.primaryColor')">
            <div class="color-picker-wrapper" ref="colorPickerRef">
              <div 
                class="color-preview"
                :style="{ backgroundColor: tempColor }"
                @click="showColorPicker = true"
              ></div>
              <div v-show="showColorPicker" class="color-picker-container">
                <chrome-picker
                  v-model:modelValue="tempColor"
                  class="color-picker-panel"
                  @update:modelValue="handleColorChange"
                  :disableAlpha="true"
                  :swatches="[
                    '#E60012', '#8E453F', '#FFB61E', '#45B787', '#2B4490', '#003152', '#9D2933',
                    '#EF4136', '#F75C2F', '#F6C555', '#7BA23F', '#4C6CB3', '#74325C', '#66327C'
                  ]"
                />
                <div class="color-picker-footer">
                  <a-space>
                    <a-button size="small" @click="cancelColorPick">取消</a-button>
                    <a-button size="small" type="primary" @click="confirmColorPick">确定</a-button>
                  </a-space>
                </div>
              </div>
            </div>
          </a-form-item>

          <a-divider />

          <a-form-item class="setting-item">
            <span>{{ t('settings.showTabs') }}</span>
            <a-switch v-model:checked="settings.showTabs" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.showFooter') }}</span>
            <a-switch v-model:checked="settings.showFooter" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.showWatermark') }}</span>
            <a-switch v-model:checked="settings.showWatermark" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.showLogo') }}</span>
            <a-switch v-model:checked="settings.showLogo" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.animateTitle') }}</span>
            <a-switch v-model:checked="settings.animateTitle" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.keepTabs') }}</span>
            <a-switch v-model:checked="settings.keepTabs" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.showTabIcon') }}</span>
            <a-switch v-model:checked="settings.showTabIcon" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.autoHideHeader') }}</span>
            <a-switch v-model:checked="settings.autoHideHeader" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.fixedHeader') }}</span>
            <a-switch v-model:checked="settings.fixedHeader" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.showBreadcrumb') }}</span>
            <a-switch v-model:checked="settings.showBreadcrumb" />
          </a-form-item>
        </a-form>
      </div>

      <div class="settings-footer">
        <a-space >
          <a-button @click="resetSettings">
            <template #icon><ReloadOutlined /></template>
            {{ t('settings.reset') }}
          </a-button>
          <a-button type="primary" @click="saveSettings">
            <template #icon><SaveOutlined /></template>
            {{ t('settings.save') }}
          </a-button>
        </a-space>
      </div>
    </div>
  </a-drawer>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, onUnmounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { SkinOutlined, ReloadOutlined, SaveOutlined } from '@ant-design/icons-vue'
import { Chrome as ChromePicker } from '@ckpack/vue-color'
import { useAppStore } from '@/stores/app'
import { useThemeStore } from '@/stores/theme'

const { t } = useI18n()
const appStore = useAppStore()
const themeStore = useThemeStore()

const showDrawer = ref(false)
const colorPickerRef = ref<HTMLElement | null>(null)
const showColorPicker = ref(false)
const settings = reactive({
  navMode: 'side' as 'side' | 'top' | 'mix',
  fixedHeader: true,
  showBreadcrumb: true,
  showTabs: true,
  showFooter: true,
  autoHideHeader: false,
  sidebarColor: '#001529',
  primaryColor: '#1890ff',
  showWatermark: false,
  showLogo: true,
  animateTitle: false,
  keepTabs: false,
  showTabIcon: true
})

const themeColors = [
  // 中国传统色（当前启用7个）
  '#E60012',  // 中国红，最具代表性的中国传统色，用于喜庆场合
  '#8E453F',  // 枣红，典雅古朴，体现东方韵味
  '#FFB61E',  // 藤黄，源自藤黄树的树脂，明亮温暖
  '#8E8100',  // 蟹壳青，带着一丝黄的青色，体现文人雅致
  '#45B787',  // 松花绿，取自松树针叶的颜色，清新自然
  '#2B4490',  // 宝蓝，深邃典雅的蓝色，传统织物常用
  '#003152',  // 藏蓝，深沉内敛，传统服饰常用

  // 日本传统色（当前启用7个）
  '#EF4136',  // 红丹 Akani，源自氧化铁，古建筑常用
  '#BF6766',  // 退红 Taikou，典雅柔和的红色
  '#F6C555',  // 玉子色 Tamago-iro，如鸡蛋般温暖的黄色
  '#7BA23F',  // 若竹色 Wakatake-iro，春天竹子的嫩绿色
  '#4C6CB3',  // 瑠璃色 Ruri-iro，像宝石般的深蓝色
  '#74325C',  // 古代紫 Kodai-murasaki，古老高贵的紫色
  '#66327C',  // 江戸紫 Edo-murasaki，江户时代流行的紫色

  /*
  // 中国传统色（另外23个）
  '#9D2933',  // 胭脂，古代女性最爱的口红色，温婉娴静
  '#DC3023',  // 赤，象征生命与热情的大红色
  '#FF4C00',  // 朱红，最传统的红色颜料，用于宫殿建筑
  '#F05654',  // 粉红，温柔娇嫩的红色
  '#C3272B',  // 茜色，似火如霞的红色
  '#8B6B4E',  // 赭石，古画常用的暖褐色
  '#B35C44',  // 赤茶，红褐色，体现古朴气息
  '#B36D61',  // 绾，像绸缎般柔和的红褐色
  '#D4574E',  // 檀，源自檀木的红褐色，沉稳大气
  '#ED5736',  // 妃色，华贵优雅的红色
  '#F35336',  // 彤，似火的红色
  '#F8ABA6',  // 洋红，明艳动人的红色
  '#894E3F',  // 栗色，深沉内敛的褐色
  '#89916B',  // 苔绿，如苔藓般清新的绿色
  '#9EC1CF',  // 天蓝，清澈明亮的蓝色
  '#A1AFC9',  // 蓝灰色，含蓄优雅的灰蓝色
  '#51C4D3',  // 湖蓝，如湖水般清澈的蓝色
  '#21A675',  // 青碧，介于蓝绿之间的清雅颜色
  '#126E82',  // 靛青，传统染料颜色，沉稳内敛
  '#789262',  // 竹青，如竹子般清新雅致
  '#A3E2C5',  // 青白，清淡素雅的青色
  '#E0EEE8',  // 素，纯净淡雅的白色
  '#F0F5E5'   // 草白，带着自然气息的白色

  // 日本传统色（另外23个）
  '#F75C2F',  // 朱红 Shu-iro，传统建筑常用的明亮红色
  '#2EA9DF',  // 空色 Sora-iro，晴空的淡蓝色，清澈透明
  '#0089A7',  // 浅葱色 Asagi-iro，传统浮世绘常用色
  '#336774',  // 铁色 Tetsu-iro，金属质感的深灰色
  '#6A4C9C',  // 菫色 Sumire-iro，如堇菜花般的优雅紫色
  '#B28FCE',  // 藤色 Fuji-iro，如紫藤花般的淡紫色
  '#E16B8C',  // 牡丹色 Botan-iro，富贵优雅的深粉色
  '#8B4513',  // 茶色 Cha-iro，温暖沉稳的褐色
  '#D05A6E',  // 今様色 Imayou-iro，现代感的红色
  '#DB4D6D',  // 桜色 Sakura-iro，樱花般柔美的粉色
  '#F596AA',  // 鸨色 Toki-iro，淡雅的粉红色
  '#FB966E',  // 珊瑚色 Sango-iro，温暖的橙红色
  '#724938',  // 海老茶 Ebicha-iro，深褐色
  '#2D6D4B',  // 常盤色 Tokiwa-iro，四季常青的深绿色
  '#465D4C',  // 青丹 Aooni-iro，深沉的青绿色
  '#24936E',  // 緑青色 Rokusho-iro，古铜绿色
  '#86C166',  // 若草色 Wakakusa-iro，新生嫩草的黄绿色
  '#5B622E',  // 松叶色 Matsuba-iro，松针般的深绿色
  '#D7C4BB',  // 灰桜色 Haizakura-iro，淡雅的灰粉色
  '#B4A582',  // 亜麻色 Ama-iro，亚麻般的浅褐色
  '#7D532C',  // 琥珀色 Kohaku-iro，琥珀般的金褐色
  '#91989F',  // 錫色 Suzu-iro，锡器般的灰色
  '#787878'   // 鈍色 Nibi-iro，低调的灰色
  */
]

// 添加预设颜色数组
const presetColors = [
  // 中国传统色（15个）
  '#E60012',  // 中国红
  '#8E453F',  // 枣红
  '#FFB61E',  // 藤黄
  '#9D2933',  // 胭脂
  '#DC3023',  // 赤
  '#FF4C00',  // 朱红
  '#F05654',  // 粉红
  '#C3272B',  // 茜色
  '#45B787',  // 松花绿
  '#21A675',  // 青碧
  '#2B4490',  // 宝蓝
  '#003152',  // 藏蓝
  '#126E82',  // 靛青
  '#789262',  // 竹青
  '#A3E2C5',  // 青白

  // 日本传统色（15个）
  '#EF4136',  // 红丹
  '#F75C2F',  // 朱红
  '#F6C555',  // 玉子色
  '#7BA23F',  // 若竹色
  '#4C6CB3',  // 瑠璃色
  '#74325C',  // 古代紫
  '#66327C',  // 江戸紫
  '#2EA9DF',  // 空色
  '#0089A7',  // 浅葱色
  '#6A4C9C',  // 菫色
  '#B28FCE',  // 藤色
  '#DB4D6D',  // 桜色
  '#2D6D4B',  // 常盤色
  '#86C166',  // 若草色
  '#5B622E',  // 松叶色
]

const tempColor = ref(settings.primaryColor)

// 处理点击外部关闭颜色选择器
const handleClickOutside = (event: MouseEvent) => {
  if (colorPickerRef.value && !colorPickerRef.value.contains(event.target as Node)) {
    showColorPicker.value = false
  }
}

onMounted(() => {
  document.addEventListener('click', handleClickOutside)
  const savedSettings = appStore.getSettings()
  if (savedSettings) {
    // 确保即使有保存的设置，也使用 'side' 作为默认导航模式
    Object.assign(settings, { ...savedSettings, navMode: 'side' })
  }
  tempColor.value = settings.primaryColor
})

onUnmounted(() => {
  document.removeEventListener('click', handleClickOutside)
})

const onClose = () => {
  showDrawer.value = false
}

const resetSettings = () => {
  settings.navMode = 'side'
  settings.fixedHeader = true
  settings.showBreadcrumb = true
  settings.showTabs = true
  settings.showFooter = true
  settings.autoHideHeader = false
  settings.sidebarColor = '#001529'
  settings.primaryColor = '#1890ff'
  settings.showWatermark = false
  settings.showLogo = true
  settings.animateTitle = false
  settings.keepTabs = false
  settings.showTabIcon = true
}

const saveSettings = () => {
  appStore.saveSettings({
    navMode: settings.navMode,
    fixedHeader: settings.fixedHeader,
    showBreadcrumb: settings.showBreadcrumb,
    showTabs: settings.showTabs,
    showFooter: settings.showFooter,
    autoHideHeader: settings.autoHideHeader,
    sidebarColor: settings.sidebarColor,
    primaryColor: settings.primaryColor,
    showWatermark: settings.showWatermark,
    showLogo: settings.showLogo,
    animateTitle: settings.animateTitle,
    keepTabs: settings.keepTabs,
    showTabIcon: settings.showTabIcon
  })
  themeStore.updateTheme({ primaryColor: settings.primaryColor })
  showDrawer.value = false
}

const handleColorChange = (color: any) => {
  const hexColor = color.hex
  tempColor.value = hexColor
  settings.primaryColor = hexColor
  themeStore.updateTheme({ primaryColor: hexColor })
}

const cancelColorPick = () => {
  tempColor.value = settings.primaryColor
  showColorPicker.value = false
}

const confirmColorPick = () => {
  showColorPicker.value = false
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
}

:deep(.anticon) {
  font-size: 16px;
  line-height: 1;
}

.settings-content {
  display: flex;
  flex-direction: column;
  height: 100%;

  .settings-section {
    flex: 1;
    padding: 12px 24px;

    h3 {
      margin-bottom: 12px;
      color: rgba(0, 0, 0, 0.88);
      font-weight: 500;
      font-size: 15px;
    }

    :deep(.ant-form-item) {
      margin-bottom: 4px;

      .ant-form-item-label {
        padding-bottom: 4px;
      }

      &.setting-item {
        margin: 0;
        padding: 8px 0;
        border-bottom: 1px solid rgba(0, 0, 0, 0.06);

        &:last-child {
          border-bottom: none;
        }
      }
    }

    .ant-divider {
      margin: 12px 0;
    }
  }

  .settings-footer {
    padding: 16px 24px;
    text-align: right;
    border-top: 1px solid rgba(0, 0, 0, 0.06);
    background: #fff;

    :deep(.ant-btn) {
      display: inline-flex;
      align-items: center;
      height: 32px;
      padding: 4px 15px;
    }
  }
}

.color-picker {
  display: grid;
  grid-template-columns: repeat(7, 24px);
  gap: 6px;
  margin-top: 4px;

  .color-block {
    width: 24px;
    height: 24px;
    border-radius: 2px;
    cursor: pointer;
    border: 2px solid transparent;
    transition: all 0.2s ease-in-out;
    position: relative;
    box-sizing: border-box;

    &:hover {
      transform: scale(1.1);
      box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
    }

    &.active {
      border-color: #fff;
      box-shadow: 0 0 0 2px var(--ant-color-primary);
      transform: scale(1.1);

      &::after {
        content: '';
        position: absolute;
        width: 6px;
        height: 6px;
        border-radius: 50%;
        background: #fff;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        box-shadow: 0 0 2px rgba(0, 0, 0, 0.3);
      }
    }
  }
}

.ant-divider {
  margin: 16px 0;
}

.setting-item {
  position: relative;
  padding: 10px 24px;
  border-bottom: 1px solid rgba(0, 0, 0, 0.06);

  &:last-child {
    border-bottom: none;
  }

  span {
    color: rgba(0, 0, 0, 0.88);
    font-size: 14px;
  }

  :deep(.ant-switch) {
    position: absolute;
    right: 24px;
    top: 50%;
    transform: translateY(-50%);
  }
}

.color-picker-wrapper {
  position: relative;
  
  .color-preview {
    width: 100%;
    height: 32px;
    border-radius: 6px;
    border: 1px solid #d9d9d9;
    cursor: pointer;
    transition: all 0.3s;

    &:hover {
      border-color: var(--ant-color-primary);
    }
  }

  .color-picker-container {
    position: absolute;
    top: 100%;
    left: 0;
    margin-top: 8px;
    z-index: 1000;
    background: #fff;
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
    
    .color-picker-panel {
      padding: 8px;
    }

    .color-picker-footer {
      padding: 8px;
      text-align: right;
      border-top: 1px solid rgba(0, 0, 0, 0.06);
    }
  }
}
</style> 