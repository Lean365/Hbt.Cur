<template>
  <div class="generate-info">
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item label="生成模块" name="moduleName">
        <a-input v-model:value="formData.moduleName" placeholder="请输入生成模块" />
      </a-form-item>
      <a-form-item label="生成包路径" name="packageName">
        <a-input v-model:value="formData.packageName" placeholder="请输入生成包路径" />
      </a-form-item>
      <a-form-item label="生成业务名" name="businessName">
        <a-input v-model:value="formData.businessName" placeholder="请输入生成业务名" />
      </a-form-item>
      <a-form-item label="生成功能名" name="functionName">
        <a-input v-model:value="formData.functionName" placeholder="请输入生成功能名" />
      </a-form-item>
      <a-form-item label="上级菜单" name="parentMenuId">
        <a-tree-select
          v-model:value="formData.parentMenuId"
          :tree-data="menuOptions"
          placeholder="请选择上级菜单"
          allow-clear
          tree-default-expand-all
        />
      </a-form-item>
      <a-form-item label="生成类型" name="tplCategory">
        <a-radio-group v-model:value="formData.tplCategory">
          <a-radio value="crud">单表（增删改查）</a-radio>
          <a-radio value="tree">树表（增删改查）</a-radio>
          <a-radio value="sub">主子表（增删改查）</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item label="生成路径" name="genPath">
        <a-input v-model:value="formData.genPath" placeholder="请输入生成路径" />
      </a-form-item>
      <a-form-item label="生成选项" name="options">
        <a-checkbox-group v-model:value="formData.options">
          <a-checkbox value="treeCode">树编码字段</a-checkbox>
          <a-checkbox value="treeParentCode">树父编码字段</a-checkbox>
          <a-checkbox value="treeName">树名称字段</a-checkbox>
          <a-checkbox value="parentMenuId">上级菜单</a-checkbox>
          <a-checkbox value="query">查询</a-checkbox>
          <a-checkbox value="add">新增</a-checkbox>
          <a-checkbox value="edit">修改</a-checkbox>
          <a-checkbox value="delete">删除</a-checkbox>
          <a-checkbox value="import">导入</a-checkbox>
          <a-checkbox value="export">导出</a-checkbox>
        </a-checkbox-group>
      </a-form-item>
    </a-form>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtGenTable } from '@/types/generator/genTable'
import type { Menu } from '@/types/identity/menu'
import { getMenuList } from '@/api/identity/menu'

const props = defineProps<{
  modelValue: HbtGenTable
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value: HbtGenTable): void
}>()

const formRef = ref<FormInstance>()

// 表单数据
const formData = reactive<HbtGenTable & { 
  options?: string[],
  packageName: string,
  parentMenuId?: number,
  tplCategory?: string
}>({
  ...props.modelValue,
  options: props.modelValue.options ? JSON.parse(props.modelValue.options) : [],
  packageName: '',
  parentMenuId: undefined,
  tplCategory: 'crud'
})

// 菜单选项
const menuOptions = ref<Menu[]>([])

// 表单校验规则
const rules: Record<string, Rule[]> = {
  moduleName: [{ required: true, message: '请输入生成模块', trigger: 'blur' }],
  packageName: [{ required: true, message: '请输入生成包路径', trigger: 'blur' }],
  businessName: [{ required: true, message: '请输入生成业务名', trigger: 'blur' }],
  functionName: [{ required: true, message: '请输入生成功能名', trigger: 'blur' }],
  tplCategory: [{ required: true, message: '请选择生成类型', trigger: 'change' }]
}

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
  getMenus()
})
</script>

<style lang="less" scoped>
.generate-info {
  padding: 24px;
}
</style> 