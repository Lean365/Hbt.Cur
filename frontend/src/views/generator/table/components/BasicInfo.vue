<template>
  <div class="basic-info">
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="表名" name="tableName">
            <a-input v-model:value="formData.tableName" placeholder="请输入表名" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="表注释" name="tableComment">
            <a-input v-model:value="formData.tableComment" placeholder="请输入表注释" />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="24">
          <a-form-item label="基础命名空间" name="baseNamespace" :label-col="{ span: 3 }" :wrapper-col="{ span: 20 }">
            <a-input v-model:value="formData.baseNamespace" placeholder="请输入基础命名空间" />
          </a-form-item>
        </a-col>
      </a-row>

      <a-divider orientation="left">实体</a-divider>
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="实体命名空间" name="entityNamespace" >
            <a-input v-model:value="formData.entityNamespace" placeholder="请输入实体命名空间" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="实体类名" name="entityClassName">
            <a-input v-model:value="formData.entityClassName" placeholder="请输入实体类名" />
          </a-form-item>
        </a-col>
      </a-row>

      <a-divider orientation="left">对象</a-divider>
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="DTO命名空间" name="dtoNamespace">
            <a-input v-model:value="formData.dtoNamespace" placeholder="请输入DTO命名空间" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="DTO类名" name="dtoClassName">
            <a-input v-model:value="formData.dtoClassName" placeholder="请输入DTO类名" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="16">
        <a-col :span="24">
          <a-form-item label="DTO类型" name="dtoType" :label-col="{ span: 3 }" :wrapper-col="{ span: 20 }">
            <a-checkbox-group
              v-model:value="formData.dtoType"
              :options="[
                { label: 'Dto', value: 'Dto' },
                { label: 'QueryDto', value: 'QueryDto' },
                { label: 'CreateDto', value: 'CreateDto' },
                { label: 'UpdateDto', value: 'UpdateDto' },
                { label: 'DeleteDto', value: 'DeleteDto' },
                { label: 'TplDto', value: 'TplDto' },
                { label: 'ImportDto', value: 'ImportDto' },
                { label: 'ExportDto', value: 'ExportDto' },
                { label: 'StatusDto', value: 'StatusDto' },
                { label: 'ResetPwdDto', value: 'ResetPwdDto' },
                { label: 'ChangePwdDto', value: 'ChangePwdDto' },
                { label: 'OrderDto', value: 'OrderDto' }
              ]"
            />
          </a-form-item>
        </a-col>
      </a-row>

      <a-divider orientation="left">服务</a-divider>
      <a-row :gutter="16">
        <a-col :span="24">
          <a-form-item label="服务命名空间" name="serviceNamespace" :label-col="{ span: 3 }" :wrapper-col="{ span: 20 }">
            <a-input v-model:value="formData.serviceNamespace" placeholder="请输入服务命名空间" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="服务接口类名" name="iServiceClassName">
            <a-input v-model:value="formData.iServiceClassName" placeholder="请输入服务接口类名" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="服务实现类名" name="serviceClassName">
            <a-input v-model:value="formData.serviceClassName" placeholder="请输入服务实现类名" />
          </a-form-item>
        </a-col>
      </a-row>

      <a-divider orientation="left">仓储</a-divider>
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="仓储接口命名空间" name="iRepositoryNamespace">
            <a-input v-model:value="formData.iRepositoryNamespace" placeholder="请输入仓储接口命名空间" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="仓储接口类名" name="iRepositoryClassName">
            <a-input v-model:value="formData.iRepositoryClassName" placeholder="请输入仓储接口类名" />
          </a-form-item>
        </a-col>
      </a-row>
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="仓储实现命名空间" name="repositoryNamespace">
            <a-input v-model:value="formData.repositoryNamespace" placeholder="请输入仓储实现命名空间" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="仓储实现类名" name="repositoryClassName">
            <a-input v-model:value="formData.repositoryClassName" placeholder="请输入仓储实现类名" />
          </a-form-item>
        </a-col>
      </a-row>

      <a-divider orientation="left">控制器</a-divider>
      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="控制器命名空间" name="controllerNamespace">
            <a-input v-model:value="formData.controllerNamespace" placeholder="请输入控制器命名空间" />
          </a-form-item>
        </a-col>
        <a-col :span="12">
          <a-form-item label="控制器类名" name="controllerClassName">
            <a-input v-model:value="formData.controllerClassName" placeholder="请输入控制器类名" />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="12">
          <a-form-item label="作者" name="author">
            <a-input v-model:value="formData.author" placeholder="请输入作者" />
          </a-form-item>
        </a-col>
      </a-row>

      <a-row :gutter="16">
        <a-col :span="24">
          <a-form-item label="备注" name="remark" :label-col="{ span: 3 }" :wrapper-col="{ span: 20 }">
            <a-textarea v-model:value="formData.remark" placeholder="请输入备注" :rows="4" />
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtGenTable } from '@/types/generator/genTable'

const props = defineProps<{
  modelValue: HbtGenTable
  tableId?: number
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value: HbtGenTable): void
}>()

const formRef = ref<FormInstance>()

// 表单数据
const formData = reactive<HbtGenTable>({
  ...props.modelValue,
  dtoClassName: typeof props.modelValue.dtoClassName === 'string' ? props.modelValue.dtoClassName : '',
  dtoType: Array.isArray(props.modelValue.dtoType) ? props.modelValue.dtoType : []
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  tableName: [{ required: true, message: '请输入表名', trigger: 'blur' }],
  tableComment: [{ required: true, message: '请输入表注释', trigger: 'blur' }],
  baseNamespace: [{ required: true, message: '请输入基础命名空间', trigger: 'blur' }],
  entityNamespace: [{ required: true, message: '请输入实体命名空间', trigger: 'blur' }],
  entityClassName: [{ required: true, message: '请输入实体类名', trigger: 'blur' }],
  dtoNamespace: [{ required: true, message: '请输入DTO命名空间', trigger: 'blur' }],
  dtoType: [{ required: true, message: '请输入DTO类型', trigger: 'blur' }],
  serviceNamespace: [{ required: true, message: '请输入服务命名空间', trigger: 'blur' }],
  iServiceClassName: [{ required: true, message: '请输入服务接口类名', trigger: 'blur' }],
  serviceClassName: [{ required: true, message: '请输入服务实现类名', trigger: 'blur' }],
  iRepositoryNamespace: [{ required: true, message: '请输入仓储接口命名空间', trigger: 'blur' }],
  iRepositoryClassName: [{ required: true, message: '请输入仓储接口类名', trigger: 'blur' }],
  repositoryNamespace: [{ required: true, message: '请输入仓储实现命名空间', trigger: 'blur' }],
  repositoryClassName: [{ required: true, message: '请输入仓储实现类名', trigger: 'blur' }],
  controllerNamespace: [{ required: true, message: '请输入控制器命名空间', trigger: 'blur' }],
  controllerClassName: [{ required: true, message: '请输入控制器类名', trigger: 'blur' }],
  author: [{ required: true, message: '请输入作者', trigger: 'blur' }]
}

// 监听表单数据变化
watch(
  () => props.modelValue,
  (newVal) => {
    console.log('BasicInfo - 接收到新的表单数据:', newVal)
    if (newVal) {
      Object.assign(formData, newVal)
    }
  },
  { deep: true, immediate: true }
)

// 监听本地数据变化
watch(
  () => formData,
  (newVal) => {
    console.log('BasicInfo - 本地数据变化:', newVal)
    emit('update:modelValue', newVal)
  },
  { deep: true }
)

// 初始化
onMounted(() => {
  console.log('BasicInfo - 组件挂载，当前数据:', formData)
})
</script>

<style lang="less" scoped>
.basic-info {
  padding: 24px;
}
</style> 