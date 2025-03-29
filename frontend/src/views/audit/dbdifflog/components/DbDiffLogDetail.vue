<template>
  <hbt-modal
    v-model:visible="visible"
    title="数据差异详情"
    :width="800"
    :footer="null"
  >
    <a-descriptions bordered :column="2">
      <a-descriptions-item label="日志级别">
        {{ diffInfo?.logLevel }}
      </a-descriptions-item>
      <a-descriptions-item label="表名">
        {{ diffInfo?.tableName }}
      </a-descriptions-item>
      <a-descriptions-item label="变更类型">
        {{ diffInfo?.changeType }}
      </a-descriptions-item>
      <a-descriptions-item label="列名">
        {{ diffInfo?.columnName }}
      </a-descriptions-item>
      <a-descriptions-item label="原数据类型">
        {{ diffInfo?.oldDataType }}
      </a-descriptions-item>
      <a-descriptions-item label="新数据类型">
        {{ diffInfo?.newDataType }}
      </a-descriptions-item>
      <a-descriptions-item label="原长度">
        {{ diffInfo?.oldLength }}
      </a-descriptions-item>
      <a-descriptions-item label="新长度">
        {{ diffInfo?.newLength }}
      </a-descriptions-item>
      <a-descriptions-item label="原是否允许为空">
        {{ diffInfo?.oldIsNullable ? '是' : '否' }}
      </a-descriptions-item>
      <a-descriptions-item label="新是否允许为空">
        {{ diffInfo?.newIsNullable ? '是' : '否' }}
      </a-descriptions-item>
      <a-descriptions-item label="变更描述" :span="2">
        {{ diffInfo?.changeDescription }}
      </a-descriptions-item>
      <a-descriptions-item label="执行的SQL语句" :span="2">
        <pre>{{ diffInfo?.executeSql }}</pre>
      </a-descriptions-item>
      <a-descriptions-item label="SQL参数" :span="2">
        <pre>{{ diffInfo?.sqlParameters }}</pre>
      </a-descriptions-item>
      <a-descriptions-item label="变更前数据" :span="2">
        <pre>{{ diffInfo?.beforeData }}</pre>
      </a-descriptions-item>
      <a-descriptions-item label="变更后数据" :span="2">
        <pre>{{ diffInfo?.afterData }}</pre>
      </a-descriptions-item>
    </a-descriptions>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, defineProps, defineExpose } from 'vue'
import type { HbtDbDiffLogDto } from '@/types/audit/dbDiffLog'

const props = defineProps<{
  diffInfo?: HbtDbDiffLogDto
}>()

const visible = ref(false)

/** 打开弹窗 */
const open = () => {
  visible.value = true
}

/** 关闭弹窗 */
const close = () => {
  visible.value = false
}

// 暴露方法给父组件
defineExpose({
  open,
  close
})
</script>

<style lang="less" scoped>
:deep(.ant-descriptions-item-label) {
  width: 120px;
}

:deep(.ant-descriptions-item-content) {
  pre {
    margin: 0;
    white-space: pre-wrap;
    word-wrap: break-word;
    max-height: 300px;
    overflow-y: auto;
    background-color: #f5f5f5;
    padding: 8px;
    border-radius: 4px;
  }
}
</style> 