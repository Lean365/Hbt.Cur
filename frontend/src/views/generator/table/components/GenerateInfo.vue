<template>
  <div class="generate-info">
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
    >
      <!-- 基本信息 -->
      <a-form-item label="基本信息" name="basicInfo">
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="生成模板" name="tplCategory">
              <hbt-select
                v-model:value="formData.tplCategory"
                dict-type="gen_template_type"
                type="radio"
                :allow-clear="true"
                :show-all="false"
                placeholder="请选择生成模板"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="前端类型" name="frontendType">
              <hbt-select
                v-model:value="formData.frontTpl"
                dict-type="gen_frontend_type"
                type="radio"
                :show-all="false"
                placeholder="请选择前端类型"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="前端布局" name="frontStyle">
              <a-input-number
                v-model:value="formData.frontStyle"
                :min="1"
                :max="24"
                style="width: 100%"
                placeholder="请输入列数"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="按钮样式" name="buttonStyle">
              <hbt-select
                v-model:value="formData.btnStyle"
                dict-type="gen_button_style"
                type="radio"
                :show-all="false"
                placeholder="请选择按钮样式"
              />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="生成方式" name="genType">
              <hbt-select
                v-model:value="formData.genType"
                dict-type="gen_type"
                type="radio"
                :show-all="false"
                placeholder="请选择生成方式"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="生成功能" name="options">
              <hbt-select
                v-model:value="selectedOptions"
                dict-type="gen_function"
                type="checkbox"
                placeholder="请选择生成功能"
                @change="(val: SelectValue) => formData.options.crudGroup = val as number[]"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form-item>

      <!-- 树表配置 -->
      <a-form-item v-if="formData.tplCategory === 'tree'" label="树表配置" name="treeConfig">
        <a-row :gutter="16">
          <a-col :span="8">
            <a-form-item label="树编码字段" name="treeCode">
              <a-select
                v-model:value="formData.treeCode"
                placeholder="请选择树编码字段"
                :options="columnOptions"
              />
            </a-form-item>
          </a-col>
          <a-col :span="8">
            <a-form-item label="树父编码字段" name="treeParentCode">
              <a-select
                v-model:value="formData.treeParentCode"
                placeholder="请选择树父编码字段"
                :options="columnOptions"
              />
            </a-form-item>
          </a-col>
          <a-col :span="8">
            <a-form-item label="树名称字段" name="treeName">
              <a-select
                v-model:value="formData.treeName"
                placeholder="请选择树名称字段"
                :options="columnOptions"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form-item>

      <!-- 主子表配置 -->
      <a-form-item v-if="formData.tplCategory === 'sub'" label="主子表配置" name="subConfig">
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="关联子表" name="subTableName">
              <a-select
                v-model:value="formData.subTableName"
                placeholder="请选择关联子表"
                :options="tableOptions"
                @change="handleSubTableChange"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="子表关联外键" name="subTableFkName">
              <a-select
                v-model:value="formData.subTableFkName"
                placeholder="请选择子表关联外键"
                :options="subColumnOptions"
              />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form-item>

      <!-- 其他配置 -->
      <a-form-item label="其他配置" name="otherConfig">
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="生成菜单" name="generateMenu">
              <a-switch
                :checked="formData.generateMenu === 1"
                @change="(checked) => formData.generateMenu = checked ? 1 : 0"
              />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="排序" name="sortField">
              <a-row :gutter="16">
                <a-col :span="12">
                  <a-input-number
                    v-model:value="formData.sortField"
                    :min="0"
                    style="width: 100%"
                  />
                </a-col>
                <a-col :span="12">
                  <a-select
                    v-model:value="formData.sortType"
                    style="width: 100%"
                  >
                    <a-select-option value="asc">升序</a-select-option>
                    <a-select-option value="desc">降序</a-select-option>
                  </a-select>
                </a-col>
              </a-row>
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="权限前缀" name="permsPrefix">
              <a-input v-model:value="formData.permsPrefix" placeholder="请输入权限前缀" />
            </a-form-item>
          </a-col>
          <a-col :span="12">
            <a-form-item label="基础命名空间" name="baseNamespace">
              <a-input v-model:value="formData.baseNamespace" placeholder="请输入基础命名空间" />
            </a-form-item>
          </a-col>
        </a-row>
        <a-row :gutter="16">
          <a-col :span="12">
            <a-form-item label="生成路径" name="genPath" v-if="formData.genType === 'path'">
              <a-input v-model:value="formData.genPath" placeholder="请输入生成路径" />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form-item>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, watch, onMounted } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { CheckboxValueType } from 'ant-design-vue/es/checkbox/interface'
import type { SelectValue } from 'ant-design-vue/es/select'
import type { HbtGenTable, CodeOptions } from '@/types/generator/genTable'
import type { Menu } from '@/types/identity/menu'
import { getMenuList } from '@/api/identity/menu'
import { getTable, getTableList } from '@/api/generator/genTable'
import {
  SearchOutlined,
  EyeOutlined,
  PlusOutlined,
  EditOutlined,
  ImportOutlined,
  DeleteOutlined,
  ExportOutlined,
  PrinterOutlined
} from '@ant-design/icons-vue'

// 组件属性定义
interface Props {
  modelValue: HbtGenTable
  id?: number
}

// 组件事件定义
interface Emits {
  (e: 'update:modelValue', value: HbtGenTable): void
  (e: 'submit'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const formRef = ref<FormInstance>()

// 选中的选项
const selectedOptions = ref<number[]>([])

// 树表选项
const treeOptions = ref<number[]>([])

// 主子表选项
const subOptions = ref<number[]>([])

interface DictOption {
  label: string
  value: string
}

// 字典选项
// const templateOptions = ref<DictOption[]>([])
// const frontendOptions = ref<DictOption[]>([])
// const frontendStyleOptions = ref<DictOption[]>([])
// const buttonStyleOptions = ref<DictOption[]>([])
// const genTypeOptions = ref<DictOption[]>([])
// const genFunctionOptions = ref<DictOption[]>([])
// const treeConfigOptions = ref<DictOption[]>([])
// const subConfigOptions = ref<DictOption[]>([])

// 获取字典数据
// const getDictOptions = async () => {
//   try {
//     // 获取生成模板选项
//     const templateRes = await getDictData('gen_template_type')
//     templateOptions.value = templateRes.data.map((item: any) => ({
//       label: item.dictLabel,
//       value: item.dictValue
//     }))
//
//     // 获取前端模板选项
//     const frontendRes = await getDictData('gen_frontend_type')
//     frontendOptions.value = frontendRes.data.map((item: any) => ({
//       label: item.dictLabel,
//       value: item.dictValue
//     }))
//
//     // 获取前端布局选项
//     const frontendStyleRes = await getDictData('gen_frontend_style')
//     frontendStyleOptions.value = frontendStyleRes.data.map((item: any) => ({
//       label: item.dictLabel,
//       value: item.dictValue
//     }))
//
//     // 获取按钮样式选项
//     const buttonStyleRes = await getDictData('gen_button_style')
//     buttonStyleOptions.value = buttonStyleRes.data.map((item: any) => ({
//       label: item.dictLabel,
//       value: item.dictValue
//     }))
//
//     // 获取生成方式选项
//     const genTypeRes = await getDictData('gen_type')
//     genTypeOptions.value = genTypeRes.data.map((item: any) => ({
//       label: item.dictLabel,
//       value: item.dictValue
//     }))
//
//     // 获取生成功能选项
//     const genFunctionRes = await getDictData('gen_function')
//     genFunctionOptions.value = genFunctionRes.data.map((item: any) => ({
//       label: item.dictLabel,
//       value: item.dictValue
//     }))
//
//     // 获取树表配置选项
//     const treeConfigRes = await getDictData('gen_tree_config')
//     treeConfigOptions.value = treeConfigRes.data.map((item: any) => ({
//       label: item.dictLabel,
//       value: item.dictValue
//     }))
//
//     // 获取主子表配置选项
//     const subConfigRes = await getDictData('gen_sub_config')
//     subConfigOptions.value = subConfigRes.data.map((item: any) => ({
//       label: item.dictLabel,
//       value: item.dictValue
//     }))
//   } catch (error) {
//     console.error('获取字典数据失败:', error)
//   }
// }

// 表单数据
const formData = reactive<HbtGenTable>({
  id: 0,
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: '',
  deleteBy: '',
  deleteTime: '',
  isDeleted: 0,
  remark: '',
  databaseName: '',
  tableName: '',
  tableComment: '',
  baseNamespace: '',
  tplCategory: 'crud',
  subTableName: '',
  subTableFkName: '',
  treeCode: '',
  treeName: '',
  treeParentCode: '',
  moduleName: '',
  businessName: '',
  functionName: '',
  author: '',
  genType: '0',
  genPath: '/',
  parentMenuId: 0,
  sortField: '',
  sortType: 'asc',
  permsPrefix: '',
  generateMenu: 1,
  frontTpl: 1,
  btnStyle: 1,
  frontStyle: 24,
  status: 1,
  entityClassName: '',
  entityNamespace: '',
  dtoType: [],
  dtoNamespace: '',
  dtoClassName: '',
  serviceNamespace: '',
  iServiceClassName: '',
  serviceClassName: '',
  iRepositoryNamespace: '',
  iRepositoryClassName: '',
  repositoryNamespace: '',
  repositoryClassName: '',
  controllerNamespace: '',
  controllerClassName: '',
  options: {
    isSqlDiff: 0,
    isSnowflakeId: 0,
    isRepository: 0,
    crudGroup: [1, 2, 3, 4]
  },
  columns: [],
  subTable: undefined,
  tenantId: 0
})

// 菜单选项
const menuOptions = ref<Menu[]>([])

// 添加新的响应式变量
const columnOptions = ref<{ label: string; value: string }[]>([])
const tableOptions = ref<{ label: string; value: string }[]>([])
const subColumnOptions = ref<{ label: string; value: string }[]>([])

// 默认选项
const defaultOptions: CodeOptions = {
  isSqlDiff: 0,
  isSnowflakeId: 0,
  isRepository: 0,
  crudGroup: [1, 2, 3, 4]
}

// 表单校验规则
const rules: Record<string, Rule[]> = {
  baseNamespace: [{ required: true, message: '请输入命名空间前缀', trigger: 'blur' }],
  moduleName: [{ required: true, message: '请输入生成模块', trigger: 'blur' }],
  businessName: [{ required: true, message: '请输入生成业务名', trigger: 'blur' }],
  functionName: [{ required: true, message: '请输入生成功能名', trigger: 'blur' }],
  tplCategory: [{ required: true, message: '请选择生成类型', trigger: 'change' }]
}

// 处理生成方式变化
const handleGenModeChange = (e: any) => {
  if (e.target.value === 1) {
    // 全场景生成时，自动选中所有选项
    selectedOptions.value = [1, 2, 3, 4, 5, 6, 7, 8]
    formData.options = {
      ...defaultOptions,
      crudGroup: selectedOptions.value
    }
  } else {
    // 自定义生成时，清空选项
    selectedOptions.value = []
    formData.options = {
      ...defaultOptions,
      crudGroup: []
    }
  }
  emit('update:modelValue', formData)
}

// 监听表单数据变化
watch(
  () => props.modelValue,
  (newVal) => {
    if (newVal) {
      Object.assign(formData, newVal)
      // 设置选中的功能
      selectedOptions.value = formData.options?.crudGroup || []
    }
  },
  { deep: true, immediate: true }
)

// 监听本地数据变化
watch(
  () => formData,
  (newVal) => {
    // 确保 options 对象存在
    if (!newVal.options) {
      newVal.options = {
        ...defaultOptions,
        crudGroup: selectedOptions.value
      }
    } else {
      newVal.options = {
        ...defaultOptions,
        crudGroup: selectedOptions.value
      }
    }
    emit('update:modelValue', newVal)
  },
  { deep: true }
)

// 监听生成类型变化
watch(() => formData.tplCategory, (newVal) => {
  // 更新选中的选项
  if (newVal === 'tree') {
    selectedOptions.value = [5, 6, 7] // 树表操作
  } else if (newVal === 'sub') {
    selectedOptions.value = [8] // 主子表操作
  } else {
    selectedOptions.value = [1, 2, 3, 4] // 基础操作
  }
  // 更新表单选项
  formData.options = {
    ...defaultOptions,
    crudGroup: selectedOptions.value
  }
  emit('update:modelValue', formData)
})

// 获取菜单列表
const getMenus = async () => {
  try {
    const res = await getMenuList({
      pageIndex: 1,
      pageSize: 100
    })
    if (res.data?.data?.items) {
      menuOptions.value = res.data.data.items
    }
  } catch (error) {
    console.error('获取菜单列表失败:', error)
  }
}

// 处理子表变化
const handleSubTableChange = async (value: SelectValue) => {
  if (value) {
    try {
      const res = await getTable(Number(value))
      if (res.data?.data?.columns) {
        subColumnOptions.value = res.data.data.columns.map((col: any) => ({
          label: col.columnName,
          value: col.columnName
        }))
      }
    } catch (error) {
      console.error('获取子表信息失败:', error)
    }
  } else {
    subColumnOptions.value = []
  }
}

// 获取表列表
const getTables = async () => {
  try {
    const res = await getTableList({
      pageIndex: 1,
      pageSize: 100
    })
    if (res.data?.data?.items) {
      tableOptions.value = res.data.data.items.map((table: any) => ({
        label: table.tableName,
        value: table.tableName
      }))
    }
  } catch (error) {
    console.error('获取表列表失败:', error)
  }
}

// 获取列信息
const getColumns = async () => {
  if (props.id) {
    try {
      const res = await getTable(props.id)
      if (res.data?.data?.columns) {
        columnOptions.value = res.data.data.columns.map((col: any) => ({
          label: col.columnName,
          value: col.columnName
        }))
      }
    } catch (error) {
      console.error('获取列信息失败:', error)
    }
  }
}

// 初始化
onMounted(async () => {
  console.log('GenerateInfo - 组件挂载，当前数据:', formData)
  await getMenus()
  await getTables()
  await getColumns()
  
  // 如果有 id，获取详细信息
  if (props.id) {
    try {
      const res = await getTable(props.id)
      if (res.data?.data) {
        const tableData = res.data.data
        Object.assign(formData, {
          ...tableData,
          options: {
            ...defaultOptions,
            ...tableData.options
          }
        })
        // 更新选中的选项
        selectedOptions.value = tableData.options?.crudGroup || [1, 2, 3, 4]
        // 更新树表选项
        treeOptions.value = selectedOptions.value.filter(opt => 
          [5, 6, 7].includes(opt)
        )
        // 更新主子表选项
        subOptions.value = selectedOptions.value.filter(opt => 
          [8].includes(opt)
        )
      }
    } catch (error) {
      console.error('获取表信息失败:', error)
    }
  }
})

// 提交表单
const handleSubmit = () => {
  emit('submit')
}
</script>

<style lang="less" scoped>
.generate-info {
  padding: 24px;
  max-height: 600px;
  overflow-y: auto;

  :deep(.ant-form-item) {
    margin-bottom: 16px;
  }

  :deep(.ant-form-item-label) {
    font-weight: bold;
  }

  .options-container {
    background-color: #fafafa;
    padding: 16px;
    border-radius: 4px;
    border: 1px solid #f0f0f0;

    :deep(.ant-checkbox-wrapper) {
      margin-right: 0;
      width: 100%;
    }
  }
}
</style> 