<template>
  <div class="form-config-container">
    <fc-designer 
      ref="designer" 
      :height="height" 
      :config="config" 
      :locale="locale"
      @save="handleSave"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, watch } from 'vue'
import ZH from "@form-create/antd-designer/locale/zh-CN.js";

const props = defineProps<{
  value?: string
}>()

const emit = defineEmits<{
  (e: 'update:value', value: string): void
}>()

// 定义设计器类型
interface DesignerInstance {
  setRule: (rule: any[]) => void
  getRule: () => any[]
  getOption: () => any
}

const designer = ref<DesignerInstance | null>(null)
const height = ref('500px')
const locale = ref(ZH)

const config = ref({
  showSaveBtn: true
})

// 处理保存事件
const handleSave = () => {
  if (designer.value) {
    const formConfig = designer.value.getRule()
    const configStr = JSON.stringify({ rule: formConfig }, null, 2)
    emit('update:value', configStr)
  }
}

// 监听外部值变化（用于编辑时还原配置）
watch(() => props.value, (newValue) => {
  if (newValue && designer.value) {
    try {
      const config = JSON.parse(newValue)
      // 如果配置包含 rule 字段，使用 rule 字段的值
      const rule = config.rule || config
      designer.value.setRule(rule)
    } catch (error) {
      console.error('解析配置失败:', error)
    }
  }
}, { immediate: true })

onMounted(() => {
  // 编辑时还原配置
  if (props.value) {
    try {
      const config = JSON.parse(props.value)
      // 如果配置包含 rule 字段，使用 rule 字段的值
      const rule = config.rule || config
      designer.value?.setRule(rule)
    } catch (error) {
      console.error('解析初始配置失败:', error)
    }
  }
})
</script>

<style lang="less" scoped>
.form-config-container {
  width: 100%;
  height: 100%;
}
</style> 