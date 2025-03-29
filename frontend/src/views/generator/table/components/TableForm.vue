<template>
  <a-modal
    :visible="visible"
    :title="title"
    width="80%"
    :confirm-loading="loading"
    @ok="handleOk"
    @cancel="handleCancel"
  >
    <a-tabs v-model:activeKey="activeKey">
      <a-tab-pane key="basic" tab="基本信息">
        <basic-info v-model="formData" />
      </a-tab-pane>
      <a-tab-pane key="field" tab="字段信息">
        <field-info v-model="formData" />
      </a-tab-pane>
      <a-tab-pane key="generate" tab="生成信息">
        <generate-info v-model="formData" />
      </a-tab-pane>
    </a-tabs>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import type { HbtGenTableDto } from '@/types/generator/table'
import { getGenTable, createGenTable, updateGenTable } from '@/api/generator/genTable'
import BasicInfo from './BasicInfo.vue'
import FieldInfo from './FieldInfo.vue'
import GenerateInfo from './GenerateInfo.vue'

const props = defineProps<{
  visible: boolean
  title: string
  tableId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

// 加载状态
const loading = ref(false)

// 当前激活的标签页
const activeKey = ref('basic')

// 表单数据
const formData = reactive<HbtGenTableDto>({
  tableName: '',
  tableComment: '',
  className: '',
  moduleName: '',
  packageName: '',
  businessName: '',
  functionName: '',
  functionAuthor: '',
  remark: '',
  columns: [],
  options: []
})

// 监听tableId变化
watch(
  () => props.tableId,
  async (newVal) => {
    if (newVal) {
      try {
        const res = await getGenTable(newVal)
        if (res.data) {
          Object.assign(formData, res.data)
        }
      } catch (error) {
        console.error('获取代码生成表信息失败:', error)
      }
    } else {
      // 重置表单数据
      Object.assign(formData, {
        tableName: '',
        tableComment: '',
        className: '',
        moduleName: '',
        packageName: '',
        businessName: '',
        functionName: '',
        functionAuthor: '',
        remark: '',
        columns: [],
        options: []
      })
    }
  },
  { immediate: true }
)

/** 确定按钮点击事件 */
const handleOk = async () => {
  try {
    loading.value = true
    if (props.tableId) {
      await updateGenTable(props.tableId, formData)
    } else {
      await createGenTable(formData)
    }
    emit('success')
    handleCancel()
  } catch (error) {
    console.error('保存代码生成表失败:', error)
  } finally {
    loading.value = false
  }
}

/** 取消按钮点击事件 */
const handleCancel = () => {
  emit('update:visible', false)
}
</script>

<style lang="less" scoped>
:deep(.ant-tabs-content) {
  height: 600px;
  overflow-y: auto;
}
</style>
