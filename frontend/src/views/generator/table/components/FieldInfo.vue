<template>
  <div class="field-info">
    <a-table
      :data-source="formData.columns"
      :columns="columns"
      :pagination="false"
      :scroll="{ y: 400 }"
    >
      <template #bodyCell="{ column, record, index }">
        <template v-if="column.key === 'isInsert'">
          <a-checkbox v-model:checked="record.isInsert" />
        </template>
        <template v-else-if="column.key === 'isEdit'">
          <a-checkbox v-model:checked="record.isEdit" />
        </template>
        <template v-else-if="column.key === 'isList'">
          <a-checkbox v-model:checked="record.isList" />
        </template>
        <template v-else-if="column.key === 'isQuery'">
          <a-checkbox v-model:checked="record.isQuery" />
        </template>
        <template v-else-if="column.key === 'queryType'">
          <a-select v-model:value="record.queryType" style="width: 100%">
            <a-select-option value="EQ">等于</a-select-option>
            <a-select-option value="NE">不等于</a-select-option>
            <a-select-option value="GT">大于</a-select-option>
            <a-select-option value="GTE">大于等于</a-select-option>
            <a-select-option value="LT">小于</a-select-option>
            <a-select-option value="LTE">小于等于</a-select-option>
            <a-select-option value="LIKE">模糊</a-select-option>
            <a-select-option value="BETWEEN">范围</a-select-option>
            <a-select-option value="IN">包含</a-select-option>
          </a-select>
        </template>
        <template v-else-if="column.key === 'htmlType'">
          <a-select v-model:value="record.htmlType" style="width: 100%">
            <a-select-option value="input">文本框</a-select-option>
            <a-select-option value="textarea">文本域</a-select-option>
            <a-select-option value="select">下拉框</a-select-option>
            <a-select-option value="radio">单选框</a-select-option>
            <a-select-option value="checkbox">复选框</a-select-option>
            <a-select-option value="datetime">日期时间</a-select-option>
            <a-select-option value="imageUpload">图片上传</a-select-option>
            <a-select-option value="fileUpload">文件上传</a-select-option>
            <a-select-option value="editor">富文本编辑器</a-select-option>
          </a-select>
        </template>
        <template v-else-if="column.key === 'dictType'">
          <a-select
            v-model:value="record.dictType"
            style="width: 100%"
            placeholder="请选择字典类型"
            allow-clear
          >
            <a-select-option
              v-for="dict in dictOptions"
              :key="dict.dictType"
              :value="dict.dictType"
            >
              {{ dict.dictName }}
            </a-select-option>
          </a-select>
        </template>
      </template>
    </a-table>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import type { HbtGenTableDto } from '@/types/generator/table'
import type { HbtDictType } from '@/types/admin/dictType'
import { getHbtDictTypeList } from '@/api/admin/dictType'

const props = defineProps<{
  modelValue: HbtGenTableDto
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value: HbtGenTableDto): void
}>()

// 表单数据
const formData = reactive<HbtGenTableDto>({
  ...props.modelValue
})

// 字典类型选项
const dictOptions = ref<HbtDictType[]>([])

// 表格列定义
const columns = [
  {
    title: '字段名',
    dataIndex: 'columnName',
    key: 'columnName',
    width: 120
  },
  {
    title: '字段类型',
    dataIndex: 'columnType',
    key: 'columnType',
    width: 120
  },
  {
    title: '字段注释',
    dataIndex: 'columnComment',
    key: 'columnComment',
    width: 120
  },
  {
    title: '插入',
    dataIndex: 'isInsert',
    key: 'isInsert',
    width: 60
  },
  {
    title: '编辑',
    dataIndex: 'isEdit',
    key: 'isEdit',
    width: 60
  },
  {
    title: '列表',
    dataIndex: 'isList',
    key: 'isList',
    width: 60
  },
  {
    title: '查询',
    dataIndex: 'isQuery',
    key: 'isQuery',
    width: 60
  },
  {
    title: '查询方式',
    dataIndex: 'queryType',
    key: 'queryType',
    width: 120
  },
  {
    title: '显示类型',
    dataIndex: 'htmlType',
    key: 'htmlType',
    width: 120
  },
  {
    title: '字典类型',
    dataIndex: 'dictType',
    key: 'dictType',
    width: 120
  }
]

// 获取字典类型列表
const getDictTypes = async () => {
  try {
    const res = await getHbtDictTypeList({
      pageIndex: 1,
      pageSize: 100
    })
    if (res.data?.rows) {
      dictOptions.value = res.data.rows
    }
  } catch (error) {
    console.error('获取字典类型列表失败:', error)
  }
}

// 监听表单数据变化
watch(
  () => formData,
  (newVal) => {
    emit('update:modelValue', newVal)
  },
  { deep: true }
)

// 初始化
onMounted(() => {
  getDictTypes()
})
</script>

<style lang="less" scoped>
.field-info {
  padding: 24px;
}
</style> 