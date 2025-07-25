<template>
  <hbt-modal
    :title="translationId ? t('common.title.edit') : t('common.title.create')"
    :open="open"
    width="800px"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 20 }"
    >
      <a-form-item :label="t('core.translation.fields.moduleName.label')" name="moduleName">
        <hbt-select
          v-model:value="form.moduleName"
          type="radio"
          :show-all="false"
          :placeholder="t('core.translation.fields.moduleName.placeholder')"
          :disabled="!!props.transKey"
          dict-type="sys_translation_module"
        />
      </a-form-item>
      <a-form-item :label="t('core.translation.fields.transKey.label')" name="transKey">
        <a-input 
          v-model:value="form.transKey" 
          :placeholder="t('core.translation.fields.transKey.placeholder')"
          :disabled="!!props.transKey"
        />
      </a-form-item>
      <a-form-item :label="t('core.translation.fields.status.label')" name="status">
        <hbt-select
          v-model:value="form.status"
          type="radio"
          :show-all="false"
          :placeholder="t('core.translation.fields.status.placeholder')"
          dict-type="sys_yes_no"
        />
      </a-form-item>

      <!-- 语言翻译列表 -->
      <div class="translation-list">
        <h3 class="section-title">{{ t('core.translation.title') }}</h3>
        <div v-for="lang in languageList" :key="lang.langCode" class="translation-item">
          <a-form-item
            :label="lang.langName + ' (' + lang.langCode + ')'"
            :name="['translations', lang.langCode, 'transValue']"
            :rules="[
              { 
                required: ['en-US', 'zh-CN'].includes(lang.langCode), 
                message: t('core.translation.fields.transValue.validation.required') 
              }
            ]"
          >
            <a-textarea
              v-model:value="form.translations[lang.langCode].transValue"
              :rows="2"
              :placeholder="t('core.translation.fields.transValue.placeholder')"
            />
          </a-form-item>
        </div>
      </div>
    </a-form>
    <template #footer>
      <div class="dialog-footer">
        <a-button @click="handleCancel">{{ t('common.button.cancel') }}</a-button>
        <a-button type="primary" :loading="loading" @click="handleSubmit">
          {{ t('common.button.confirm') }}
        </a-button>
      </div>
    </template>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import type { HbtTransposedData, HbtTranslationLang, HbtTranslationCreate, HbtTranslationUpdate } from '@/types/routine/core/translation'
import { 
  getHbtTransposedDetail, 
  createHbtTransposed, 
  updateHbtTransposed, 
  getHbtTranslationList,
  createHbtTranslation,
  updateHbtTranslation
} from '@/api/routine/core/translation'
import { getLanguageOptions } from '@/api/routine/core/language'

const props = defineProps<{
  open: boolean
  translationId?: number
  transKey?: string
}>()

const emit = defineEmits(['update:open', 'success'])

const { t } = useI18n()
const formRef = ref<FormInstance>()
const loading = ref(false)
const languageList = ref<{ langCode: string, langName: string }[]>([])

const form = reactive<HbtTransposedData>({
  transKey: '',
  moduleName: 'frontend',
  status: 0,
  remark: '',
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: '',
  translations: {}
})

const rules: Record<string, Rule[]> = {
  moduleName: [
    { required: true, message: t('core.translation.fields.moduleName.validation.required'), trigger: 'blur' },
    { min: 2, max: 50, message: t('core.translation.fields.moduleName.validation.length'), trigger: 'blur' }
  ],
  transKey: [
    { required: true, message: t('core.translation.fields.transKey.validation.required'), trigger: 'blur' },
    { min: 2, max: 100, message: t('core.translation.fields.transKey.validation.length'), trigger: 'blur' }
  ],
  status: [
    { required: true, message: t('core.translation.fields.status.validation.required'), trigger: 'change' }
  ]
}

const loadLanguages = async () => {
  try {
    const res = await getLanguageOptions()
    console.log('支持的语言列表响应:', res)
    if (res.data.code === 200) {
      if (!res.data.data?.length) {
        console.warn('支持的语言列表为空')
        message.warning(t('core.language.message.noLanguages'))
        return
      }
      languageList.value = res.data.data
      console.log('设置的语言列表:', languageList.value)
      // 初始化 translations
      form.translations = {}
      languageList.value.forEach(lang => {
        form.translations[lang.langCode] = {
          translationId: 0,
          langCode: lang.langCode,
          transValue: '',
          status: 0
        }
      })
      console.log('初始化的翻译数据:', form.translations)
    } else {
      console.error('获取支持的语言列表失败:', res.data.msg)
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载支持的语言列表出错:', error)
    message.error(t('common.failed'))
  }
}

// 监听表单字段变化，自动更新备注
watch(
  [
    () => form.moduleName,
    () => form.transKey,
    () => form.translations
  ],
  () => {
    const translations = Object.values(form.translations)
      .map(t => t.transValue)
      .filter(v => v)
      .map(v => `(${v})`)
      .join(', ')
    
    form.remark = [
      form.moduleName,
      form.transKey,
      translations
    ].filter(Boolean).join(', ')
  },
  { deep: true }
)

// 获取翻译详情
const getDetail = async (transKey: string) => {
  if (!transKey) return
  
  try {
    loading.value = true
    const res = await getHbtTransposedDetail(transKey)
    if (res.data.code === 200) {
      Object.assign(form, res.data.data)
      // 初始化时也更新一次备注
      const translations = Object.values(form.translations)
        .map(t => t.transValue)
        .filter(v => v)
        .map(v => `(${v})`)
        .join(', ')
      
      form.remark = [
        form.moduleName,
        form.transKey,
        translations
      ].filter(Boolean).join(', ')
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取翻译详情失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 重置表单
const resetForm = () => {
  form.transKey = ''
  form.moduleName = 'frontend'
  form.status = 0
  form.remark = ''
  form.createBy = ''
  form.createTime = ''
  form.updateBy = ''
  form.updateTime = ''
  form.translations = {}
  formRef.value?.resetFields()
}

// 提交表单
const handleSubmit = async () => {
  if (!formRef.value) return
  
  try {
    await formRef.value.validate()
    loading.value = true
    
    // 生成备注内容
    const translationValues = Object.values(form.translations)
      .map(t => t.transValue)
      .filter(v => v)
      .map(v => `(${v})`)
      .join(', ')
    
    form.remark = [
      form.moduleName,
      form.transKey,
      translationValues
    ].filter(Boolean).join(', ')
    
    // 直接使用完整的 transKey
    const submitData = {
      ...form
    }
    
    // 使用转置翻译 API
    const response = props.transKey
      ? await updateHbtTransposed(submitData)
      : await createHbtTransposed(submitData)
    
    if (response.data.code === 200) {
      message.success(t('common.message.saveSuccess'))
      emit('success')
      resetForm()
      handleCancel()
    } else {
      message.error(response.data.msg || t('common.message.saveFailed'))
    }
  } catch (error) {
    console.error('保存失败:', error)
    message.error(t('common.message.saveFailed'))
  } finally {
    loading.value = false
  }
}

// 取消
const handleCancel = () => {
  resetForm()
  emit('update:open', false)
}

// 监听对话框可见性变化
watch(() => props.open, (val) => {
  if (val) {
    loadLanguages()
    if (props.transKey) {
      console.log('对话框打开，transKey:', props.transKey)
      getDetail(props.transKey)
    }
  }
})

// 监听 transKey 变化
watch(() => props.transKey, (val) => {
  if (val && props.open) {
    console.log('transKey 变化:', val)
    getDetail(val)
  }
})
</script>

<style lang="less" scoped>
.translation-list {
  margin-top: 24px;
  padding: 0 24px;

  .section-title {
    margin-bottom: 16px;
    font-size: 16px;
    font-weight: 500;
  }

  .translation-item {
    margin-bottom: 16px;
  }
}

.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}
</style> 