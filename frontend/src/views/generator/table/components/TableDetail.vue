<template>
  <a-modal
    :open="open"
    :title="t('generator.table.detail')"
    width="800px"
    :footer="null"
    @cancel="handleCancel"
    @update:open="(val) => emit('update:open', val)"
  >
    <a-descriptions :column="2" bordered>
      <a-descriptions-item :label="t('generator.table.name')" :span="2">
        {{ tableInfo?.tableName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.comment')" :span="2">
        {{ tableInfo?.tableComment }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.className')" :span="2">
        {{ tableInfo?.className }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('generator.table.createTime')" :span="2">
        {{ formatDateTime(tableInfo?.createTime) }}
      </a-descriptions-item>
    </a-descriptions>

    <a-divider>{{ t('generator.table.columns') }}</a-divider>

    <a-table
      :columns="columns"
      :data-source="tableInfo?.columns"
      :pagination="false"
      :scroll="{ x: 800 }"
    >
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'isRequired'">
          <a-tag :color="record.isRequired ? 'red' : 'green'">
            {{ record.isRequired ? t('common.yes') : t('common.no') }}
          </a-tag>
        </template>
        <template v-if="column.key === 'isList'">
          <a-tag :color="record.isList ? 'blue' : 'default'">
            {{ record.isList ? t('common.yes') : t('common.no') }}
          </a-tag>
        </template>
        <template v-if="column.key === 'isQuery'">
          <a-tag :color="record.isQuery ? 'purple' : 'default'">
            {{ record.isQuery ? t('common.yes') : t('common.no') }}
          </a-tag>
        </template>
      </template>
    </a-table>
  </a-modal>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { ref, watch } from 'vue'
import type { HbtGenTableDefine } from '@/types/generator/genTableDefine'
import { getTableDefineInfo } from '@/api/generator/genTableDefine'
import { formatDateTime } from '@/utils/datetime'

const props = defineProps<{
  open: boolean
  tableId?: number
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
}>()

const { t } = useI18n()

// 表格信息
const tableInfo = ref<HbtGenTableDefine>()

// 列定义
const columns = [
  {
    title: t('generator.table.column.name'),
    dataIndex: 'columnName',
    key: 'columnName',
    width: 150
  },
  {
    title: t('generator.table.column.comment'),
    dataIndex: 'columnComment',
    key: 'columnComment',
    width: 150
  },
  {
    title: t('generator.table.column.type'),
    dataIndex: 'dbColumnType',
    key: 'dbColumnType',
    width: 100
  },
  {
    title: t('generator.table.column.isRequired'),
    dataIndex: 'isRequired',
    key: 'isRequired',
    width: 100
  },
  {
    title: t('generator.table.column.isList'),
    dataIndex: 'isList',
    key: 'isList',
    width: 100
  },
  {
    title: t('generator.table.column.isQuery'),
    dataIndex: 'isQuery',
    key: 'isQuery',
    width: 100
  }
]

// 监听表格ID变化
watch(
  () => props.tableId,
  async (newVal) => {
    if (newVal) {
      const res = await getTableDefineInfo(newVal)
      if (res.data) {
        tableInfo.value = res.data.data
      }
    }
  },
  { immediate: true }
)

// 关闭对话框
const handleCancel = () => {
  emit('update:open', false)
}
</script>

<style scoped>
.ant-descriptions {
  margin-bottom: 24px;
}
</style> 