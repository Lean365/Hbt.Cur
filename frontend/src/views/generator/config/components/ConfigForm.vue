<template>
  <hbt-modal
    v-model:open="visible"
    :title="title"
    :width="800"
    :loading="loading"
    @cancel="handleCancel"
    @ok="handleOk"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 19 }"
    >
      <a-form-item label="配置名称" name="genConfigName">
        <a-input v-model:value="formData.genConfigName" placeholder="请输入配置名称" />
      </a-form-item>
      <a-form-item label="作者" name="author">
        <a-input v-model:value="formData.author" placeholder="请输入作者" />
      </a-form-item>
      <a-form-item label="模块名" name="moduleName">
        <a-input v-model:value="formData.moduleName" placeholder="请输入模块名" />
      </a-form-item>
      <a-form-item label="包名" name="packageName">
        <a-input v-model:value="formData.packageName" placeholder="请输入包名" />
      </a-form-item>
      <a-form-item label="业务名" name="businessName">
        <a-input v-model:value="formData.businessName" placeholder="请输入业务名" />
      </a-form-item>
      <a-form-item label="功能名" name="functionName">
        <a-input v-model:value="formData.functionName" placeholder="请输入功能名" />
      </a-form-item>
      <a-form-item label="生成类型" name="genType">
        <a-select v-model:value="formData.genType" placeholder="请选择生成类型">
          <a-select-option :value="0">生成类型1</a-select-option>
          <a-select-option :value="1">生成类型2</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="模板选用方式" name="genTemplateType">
        <a-select v-model:value="formData.genTemplateType" placeholder="请选择模板选用方式">
          <a-select-option :value="0">使用wwwroot/Generator/*.scriban模板</a-select-option>
          <a-select-option :value="1">使用HbtGenTemplate表中的模板</a-select-option>
        </a-select>
      </a-form-item>
      <a-form-item label="生成路径" name="genPath">
        <a-input v-model:value="formData.genPath" placeholder="请输入生成路径" />
      </a-form-item>
      <a-form-item label="选项配置" name="options">
        <a-textarea
          v-model:value="formData.options"
          :rows="4"
          placeholder="请输入选项配置"
        />
      </a-form-item>
      <a-form-item label="状态" name="status">
        <a-select v-model:value="formData.status" placeholder="请选择状态">
          <a-select-option :value="0">正常</a-select-option>
          <a-select-option :value="1">停用</a-select-option>
        </a-select>
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import { getGenConfig, createGenConfig, updateGenConfig } from '@/api/generator/genConfig'
import type { HbtGenConfig, HbtGenConfigCreate, HbtGenConfigUpdate } from '@/types/generator/genConfig'

const props = defineProps<{
  open: boolean
  title: string
  configId?: number
}>()

const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'success'): void
  (e: 'cancel'): void
}>()

const visible = computed({
  get: () => props.open,
  set: (value) => emit('update:open', value)
})

// 表单引用
const formRef = ref<FormInstance>()

// 加载状态
const loading = ref(false)

// 表单数据
const formData = reactive<HbtGenConfigCreate>({
  genConfigName: '',
  author: '',
  moduleName: '',
  packageName: '',
  businessName: '',
  functionName: '',
  genType: 0,
  genTemplateType: 0,
  genPath: '',
  status: 0,
  options: ''
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  genConfigName: [{ required: true, message: '请输入配置名称', trigger: 'blur' }],
  author: [{ required: true, message: '请输入作者', trigger: 'blur' }],
  moduleName: [{ required: true, message: '请输入模块名称', trigger: 'blur' }],
  packageName: [{ required: true, message: '请输入包名', trigger: 'blur' }],
  businessName: [{ required: true, message: '请输入业务名称', trigger: 'blur' }],
  functionName: [{ required: true, message: '请输入功能名称', trigger: 'blur' }],
  genType: [{ required: true, message: '请选择生成类型', trigger: 'change' }],
  genTemplateType: [{ required: true, message: '请选择模板类型', trigger: 'change' }],
  genPath: [{ required: true, message: '请输入生成路径', trigger: 'blur' }]
}

// 监听配置ID变化
watch(
  () => props.configId,
  async (newVal) => {
    if (newVal) {
      loading.value = true
      try {
        const res = await getGenConfig(newVal)
        if (res.data.code === 200 && res.data.data) {
          const configData = res.data.data
          Object.assign(formData, {
            genConfigName: configData.genConfigName,
            author: configData.author,
            moduleName: configData.moduleName,
            packageName: configData.packageName,
            businessName: configData.businessName,
            functionName: configData.functionName,
            genType: configData.genType,
            genTemplateType: configData.genTemplateType,
            genPath: configData.genPath,
            status: configData.status,
            options: configData.options
          })
        } else {
          message.error(res.data.msg || '获取数据失败')
        }
      } catch (error) {
        console.error('获取数据失败:', error)
        message.error('获取数据失败')
      } finally {
        loading.value = false
      }
    } else {
      Object.assign(formData, {
        genConfigName: '',
        author: '',
        moduleName: '',
        packageName: '',
        businessName: '',
        functionName: '',
        genType: 0,
        genTemplateType: 0,
        genPath: '',
        status: 0,
        options: ''
      })
    }
  },
  { immediate: true }
)

/** 确定按钮点击事件 */
const handleOk = async () => {
  if (!formRef.value) return
  await formRef.value.validate()
  
  try {
    loading.value = true
    if (props.configId) {
      const updateData: HbtGenConfigUpdate = {
        genConfigId: props.configId,
        genConfigName: formData.genConfigName,
        author: formData.author,
        moduleName: formData.moduleName,
        packageName: formData.packageName,
        businessName: formData.businessName,
        functionName: formData.functionName,
        genType: formData.genType,
        genTemplateType: formData.genTemplateType,
        genPath: formData.genPath,
        status: formData.status,
        options: formData.options
      }
      await updateGenConfig(props.configId, updateData)
      message.success('更新成功')
    } else {
      await createGenConfig(formData)
      message.success('创建成功')
    }
    emit('success')
    emit('update:open', false)
  } catch (error) {
    console.error(error)
  } finally {
    loading.value = false
  }
}

/** 取消按钮点击事件 */
const handleCancel = () => {
  emit('cancel')
  formRef.value?.resetFields()
}

const fetchData = async () => {
  if (!props.configId) return
  try {
    loading.value = true
    const { data } = await getGenConfig(props.configId)
    Object.assign(formData, data)
  } catch (error) {
    console.error(error)
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  fetchData()
})

defineExpose({
  formRef
})
</script> 