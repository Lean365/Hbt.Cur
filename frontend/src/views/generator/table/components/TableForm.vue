<template>
  <hbt-modal
    v-model:open="visible"
    :title="title"
    width="80%"
    :loading="loading"
    @cancel="handleCancel"
    @ok="handleOk"
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
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed } from 'vue'
import type { HbtGenTable } from '@/types/generator/genTable'
import { getTable, updateTable } from '@/api/generator/genTable'
import BasicInfo from './BasicInfo.vue'
import FieldInfo from './FieldInfo.vue'
import GenerateInfo from './GenerateInfo.vue'

const props = defineProps<{
  open: boolean
  title: string
  tableId?: number
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

// 加载状态
const loading = ref(false)

// 当前激活的标签页
const activeKey = ref('basic')

// 表单数据初始化
const initialFormData: HbtGenTable = {
  id: 0,
  tableId: 0,
  databaseName: '',
  tableName: '',
  tableComment: '',
  className: '',
  namespace: '',
  baseNamespace: '',
  csharpTypeName: '',
  parentTableName: '',
  parentTableFkName: '',
  status: 1,
  templateType: 1,
  moduleName: '',
  businessName: '',
  functionName: '',
  author: '',
  genMode: 1,
  genPath: '',
  options: '',
  tenantId: 0,
  createBy: '',
  createTime: '',
  isDeleted: 0
}

// 表单数据
const formData = reactive<HbtGenTable>({ ...initialFormData })

// 重置表单
const resetForm = () => {
  Object.assign(formData, initialFormData)
}

// 表单字段定义
const formFields = [
  {
    name: 'tableName',
    label: '表名',
    type: 'input',
    required: true,
    placeholder: '请输入表名'
  },
  {
    name: 'tableComment',
    label: '表描述',
    type: 'input',
    required: true,
    placeholder: '请输入表描述'
  },
  {
    name: 'className',
    label: '实体类名',
    type: 'input',
    required: true,
    placeholder: '请输入实体类名'
  },
  {
    name: 'namespace',
    label: '命名空间',
    type: 'input',
    required: true,
    placeholder: '请输入命名空间'
  },
  {
    name: 'baseNamespace',
    label: '基本命名空间前缀',
    type: 'input',
    required: true,
    placeholder: '请输入基本命名空间前缀'
  },
  {
    name: 'csharpTypeName',
    label: 'C#类名',
    type: 'input',
    required: true,
    placeholder: '请输入C#类名'
  },
  {
    name: 'templateType',
    label: '使用的模板',
    type: 'select',
    required: true,
    options: [
      { label: '单表', value: 1 },
      { label: '树表', value: 2 },
      { label: '主子表', value: 3 }
    ]
  },
  {
    name: 'moduleName',
    label: '模块名称',
    type: 'input',
    required: true,
    placeholder: '请输入模块名称'
  },
  {
    name: 'businessName',
    label: '业务名称',
    type: 'input',
    required: true,
    placeholder: '请输入业务名称'
  },
  {
    name: 'functionName',
    label: '功能名称',
    type: 'input',
    required: true,
    placeholder: '请输入功能名称'
  },
  {
    name: 'author',
    label: '作者',
    type: 'input',
    required: true,
    placeholder: '请输入作者'
  },
  {
    name: 'genMode',
    label: '生成方式',
    type: 'select',
    required: true,
    options: [
      { label: 'zip压缩包', value: 0 },
      { label: '自定义路径', value: 1 }
    ]
  },
  {
    name: 'genPath',
    label: '存放位置',
    type: 'input',
    placeholder: '请输入存放位置'
  },
  {
    name: 'options',
    label: '其他选项',
    type: 'textarea',
    placeholder: '请输入其他选项'
  },
  {
    name: 'status',
    label: '状态',
    type: 'radio',
    required: true,
    options: [
      { label: '正常', value: 1 },
      { label: '停用', value: 0 }
    ]
  }
]

// 监听tableId变化
watch(
  () => props.tableId,
  async (newVal) => {
    if (newVal) {
      try {
        const res = await getTable(newVal)
        if (res.data) {
          Object.assign(formData, res.data)
        }
      } catch (error) {
        console.error('获取代码生成表信息失败:', error)
      }
    } else {
      // 重置表单数据
      resetForm()
    }
  },
  { immediate: true }
)

/** 确定按钮点击事件 */
const handleOk = async () => {
  try {
    loading.value = true
    if (props.tableId) {
      await updateTable(formData)
      emit('success')
      handleCancel()
    }
  } catch (error) {
    console.error('保存代码生成表失败:', error)
  } finally {
    loading.value = false
  }
}

/** 取消按钮点击事件 */
const handleCancel = () => {
  emit('update:open', false)
}
</script>

<style lang="less" scoped>
:deep(.ant-tabs-content) {
  height: 600px;
  overflow-y: auto;
}
</style>
