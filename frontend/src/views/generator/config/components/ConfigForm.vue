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
      <a-form-item :label="t('generator.config.fields.genConfigName')" name="genConfigName">
        <a-input v-model:value="formData.genConfigName" :placeholder="t('generator.config.placeholder.genConfigName')" />
      </a-form-item>
      <a-form-item :label="t('generator.config.fields.author')" name="author">
        <a-input v-model:value="formData.author" :placeholder="t('generator.config.placeholder.author')" disabled />
      </a-form-item>
      <a-form-item :label="t('generator.config.fields.projectName')" name="projectName">
        <hbt-select v-model:value="formData.projectName" dict-type="gen_project_name" type="radio" 
        :show-all="false" :placeholder="t('generator.config.placeholder.projectName')" style="width: 100%" />
      </a-form-item>
      <a-form-item :label="t('generator.config.fields.moduleName')" name="moduleName">
        <hbt-select v-model:value="formData.moduleName" dict-type="gen_module_name" type="radio" 
        :show-all="false" :placeholder="t('generator.config.placeholder.moduleName')" style="width: 100%" />
      </a-form-item>
      <a-form-item :label="t('generator.config.fields.businessName')" name="businessName">
        <a-input 
          v-model:value="formData.businessName" 
          :placeholder="t('generator.config.placeholder.businessName')"
          @input="handleBusinessNameInput"
          :maxLength="20"
        />
      </a-form-item>
      <a-form-item :label="t('generator.config.fields.functionName')" name="functionName">
        <a-input 
          v-model:value="formData.functionName" 
          :placeholder="t('generator.config.placeholder.functionName')"
          @input="handleFunctionNameInput"
          :maxLength="20"
        />
      </a-form-item>
      <a-form-item :label="t('generator.config.fields.genMethod')" name="genMethod">
        <hbt-select v-model:value="formData.genMethod" dict-type="gen_method" type="radio" 
        :show-all="false" :placeholder="t('generator.config.placeholder.genMethod')" style="width: 100%" />
      </a-form-item>
      <a-form-item :label="t('generator.config.fields.genTplType')" name="genTplType">
        <hbt-select v-model:value="formData.genTplType" dict-type="gen_tpl_type" type="radio" 
        :show-all="false" :placeholder="t('generator.config.placeholder.genTplType')" style="width: 100%" />
      </a-form-item>
      <a-form-item v-if="formData.genMethod === 0" :label="t('generator.config.fields.genPath')" name="genPath">
        <a-input v-model:value="formData.genPath" :placeholder="t('generator.config.placeholder.genPath')" />
      </a-form-item>
      <a-form-item :label="t('generator.config.fields.options')" name="options">
        <a-textarea
          v-model:value="formData.options"
          :rows="4"
          :placeholder="t('generator.config.placeholder.options')"
        />
      </a-form-item>
      <a-form-item :label="t('generator.config.fields.status')" name="status">
        <hbt-select v-model:value="formData.status" dict-type="sys_normal_disable" type="radio" 
        :show-all="false" :placeholder="t('generator.config.placeholder.status')" style="width: 100%" />
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
import { useI18n } from 'vue-i18n'
import { useUserStore } from '@/stores/user'
import { useDictStore } from '@/stores/dict'
const { t } = useI18n()
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

const userStore = useUserStore()
const dictStore = useDictStore()
console.log(userStore.userInfo)
const formData = reactive<HbtGenConfigCreate>({
  genConfigName: '',
  author: '',
  projectName: 'Lean.Hbt',
  moduleName: 'Core',
  businessName: '',
  functionName: '',
  genMethod: 0,
  genTplType: 0,
  genPath: '',
  options: '',
  status: 0
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  genConfigName: [{ required: true, message: t('generator.config.placeholder.genConfigName'), trigger: 'blur' }],
  author: [{ required: true, message: t('generator.config.placeholder.author'), trigger: 'blur' }],
  moduleName: [{ required: true, message: t('generator.config.placeholder.moduleName'), trigger: 'blur' }],
  projectName: [{ required: true, message: t('generator.config.placeholder.projectName'), trigger: 'blur' }],
  businessName: [
    { required: true, message: t('generator.config.placeholder.businessName'), trigger: 'blur' },
    { 
      pattern: /^[A-Z][a-zA-Z]{3,19}$/, 
      message: t('generator.config.validation.pascalCase'), 
      trigger: 'blur' 
    }
  ],
  functionName: [
    { required: true, message: t('generator.config.placeholder.functionName'), trigger: 'blur' },
    { 
      pattern: /^[A-Z][a-zA-Z]{3,19}$/, 
      message: t('generator.config.validation.pascalCase'), 
      trigger: 'blur' 
    }
  ],
  genMethod: [{ required: true, message: t('generator.config.placeholder.genMethod'), trigger: 'change' }],
  genTplType: [{ required: true, message: t('generator.config.placeholder.genTplType'), trigger: 'change' }],
  genPath: [{ required: true, message: t('generator.config.placeholder.genPath'), trigger: 'blur' }]
}

// 处理业务名称输入
const handleBusinessNameInput = (e: Event) => {
  const input = e.target as HTMLInputElement
  let value = input.value.replace(/[^a-zA-Z]/g, '') // 只保留字母
  if (value.length > 0) {
    value = value.charAt(0).toUpperCase() + value.slice(1) // 首字母转大写
  }
  formData.businessName = value
}

// 处理功能名称输入
const handleFunctionNameInput = (e: Event) => {
  const input = e.target as HTMLInputElement
  let value = input.value.replace(/[^a-zA-Z]/g, '') // 只保留字母
  if (value.length > 0) {
    value = value.charAt(0).toUpperCase() + value.slice(1) // 首字母转大写
  }
  formData.functionName = value
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
            projectName: configData.projectName,
            businessName: configData.businessName,
            functionName: configData.functionName,
            genMethod: configData.genMethod,
            genTplType: configData.genTplType,
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
        moduleName: 'Core',
        projectName: 'Lean.Hbt',
        businessName: '',
        functionName: '',
        genMethod: 0,
        genTplType: 0,
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
      await updateGenConfig(props.configId, formData as any)
      message.success('更新成功')
    } else {
      await createGenConfig(formData as any)
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
  if (props.configId) {
    fetchData()
  } else {
    formData.author = userStore.userInfo?.userName || ''
  }
})

defineExpose({
  formRef
})
</script> 