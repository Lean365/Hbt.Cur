<template>
  <a-modal
    :open="visible"
    title="表列信息"
    width="1000px"
    :confirm-loading="loading"
    @cancel="handleCancel"
    :footer="null"
  >
    <hbt-table
      :columns="columns"
      :data-source="columnList"
      :loading="loading"
      :scroll="{ y: 400 }"
      :pagination="false"
    >
      <!-- 数据类型列 -->
      <template #dataType="{ record }">
        <a-tag>{{ record.dataType }}</a-tag>
      </template>
      <!-- 是否主键列 -->
      <template #isPrimaryKey="{ record }">
        <a-tag v-if="record.isPrimaryKey" color="blue">是</a-tag>
        <a-tag v-else color="default">否</a-tag>
      </template>
      <!-- 是否可空列 -->
      <template #isNullable="{ record }">
        <a-tag v-if="record.isNullable" color="orange">是</a-tag>
        <a-tag v-else color="green">否</a-tag>
      </template>
    </hbt-table>
    <!-- 分页组件 -->
    <hbt-pagination
      v-model:current="pagination.current"
      v-model:page-size="pagination.pageSize"
      :total="pagination.total"
      :show-size-changer="true"
      :show-quick-jumper="true"
      @change="handlePageChange"
      @show-size-change="handleSizeChange"
    />
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, watch, reactive } from 'vue'
import { message } from 'ant-design-vue'
import type { HbtGenTable } from '@/types/generator/genTable'
import { getTableColumnList } from '@/api/generator/genTable'
import HbtTable from '@/components/Business/HbtTable/index.vue'
import HbtPagination from '@/components/Business/HbtPagination/index.vue'

const props = defineProps<{
  visible: boolean
  databaseName: string
  table: HbtGenTable | null
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
}>()

// 表格列定义
const columns = [
  { title: '表名', dataIndex: 'tableName', key: 'tableName', width: 120 },
  { title: '表ID', dataIndex: 'tableId', key: 'tableId', width: 80 },
  { title: '列名', dataIndex: 'dbColumnName', key: 'dbColumnName', width: 150 },
  { title: '属性名', dataIndex: 'propertyName', key: 'propertyName', width: 120 },
  { title: '数据类型', dataIndex: 'dataType', key: 'dataType', width: 120 },
  { title: 'Oracle类型', dataIndex: 'oracleDataType', key: 'oracleDataType', width: 120 },
  { title: '属性类型', dataIndex: 'propertyType', key: 'propertyType', width: 120 },
  { title: '长度', dataIndex: 'length', key: 'length', width: 80 },
  { title: '列描述', dataIndex: 'columnDescription', key: 'columnDescription', width: 180 },
  { title: '默认值', dataIndex: 'defaultValue', key: 'defaultValue', width: 120 },
  { title: '可空', dataIndex: 'isNullable', key: 'isNullable', width: 80 },
  { title: '自增', dataIndex: 'isIdentity', key: 'isIdentity', width: 80 },
  { title: '主键', dataIndex: 'isPrimarykey', key: 'isPrimarykey', width: 80 },
  { title: '值', dataIndex: 'value', key: 'value', width: 100 },
  { title: '小数位数', dataIndex: 'decimalDigits', key: 'decimalDigits', width: 100 },
  { title: '精度', dataIndex: 'scale', key: 'scale', width: 80 },
  { title: '数组', dataIndex: 'isArray', key: 'isArray', width: 80 },
  { title: 'Json', dataIndex: 'isJson', key: 'isJson', width: 80 },
  { title: '无符号', dataIndex: 'isUnsigned', key: 'isUnsigned', width: 80 },
  { title: '建表字段排序', dataIndex: 'createTableFieldSort', key: 'createTableFieldSort', width: 120 },
  { title: '插入服务器时间', dataIndex: 'insertServerTime', key: 'insertServerTime', width: 120 },
  { title: '插入SQL', dataIndex: 'insertSql', key: 'insertSql', width: 120 },
  { title: '更新服务器时间', dataIndex: 'updateServerTime', key: 'updateServerTime', width: 120 },
  { title: '更新SQL', dataIndex: 'updateSql', key: 'updateSql', width: 120 },
  { title: '参数类型', dataIndex: 'sqlParameterDbType', key: 'sqlParameterDbType', width: 120 },
]

// 列信息列表
const columnList = ref<any[]>([])
// 加载状态
const loading = ref(false)

// 分页配置
const pagination = reactive({
  current: 1,
  pageSize: 10,
  total: 0
})

// 监听弹窗显示状态
watch(
  () => props.visible,
  async (newVal) => {
    if (newVal && props.databaseName && props.table) {
      await fetchColumnList()
    } else {
      // 关闭弹窗时重置状态
      handleCancel()
    }
  }
)

/** 获取表列信息 */
const fetchColumnList = async () => {
  if (!props.databaseName || !props.table) {
    message.warning('数据库或表信息不完整')
    return
  }

  loading.value = true
  try {
    const res = await getTableColumnList(props.databaseName, props.table.tableName)
    if (res.data?.data) {
      // 更新总数
      pagination.total = res.data.data.length
      
      // 计算分页数据
      const start = (pagination.current - 1) * pagination.pageSize
      const end = start + pagination.pageSize
      columnList.value = res.data.data.slice(start, end)
    } else {
      columnList.value = []
      pagination.total = 0
      message.warning('未获取到列信息')
    }
  } catch (error) {
    console.error('获取表列信息失败:', error)
    message.error('获取表列信息失败')
    columnList.value = []
    pagination.total = 0
  } finally {
    loading.value = false
  }
}

/** 页码改变事件 */
const handlePageChange = (page: number) => {
  pagination.current = page
  fetchColumnList()
}

/** 每页条数改变事件 */
const handleSizeChange = (current: number, size: number) => {
  pagination.current = current
  pagination.pageSize = size
  fetchColumnList()
}

/** 取消按钮点击事件 */
const handleCancel = () => {
  emit('update:visible', false)
  // 重置状态
  columnList.value = []
  loading.value = false
  pagination.current = 1
  pagination.pageSize = 10
  pagination.total = 0
}
</script>

<style lang="less" scoped>
:deep(.ant-tag) {
  margin: 0;
}
</style> 