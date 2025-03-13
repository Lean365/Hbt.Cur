<!-- 
===================================================================
项目名称: Lean.Hbt
文件名称: dictType.form.vue
创建日期: 2024-03-20
描述: 字典类型表单对话框组件
=================================================================== 
-->

<template>
  <hbt-modal
    :open="open"
    :title="title"
    :loading="loading"
    :width="800"
    @update:open="(val: boolean) => emit('update:open', val)"
    @ok="handleOk"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formState"
      :rules="rules"
      :label-col="{ span: 8 }"
      :wrapper-col="{ span: 14 }"
    >
      <a-tabs v-model:activeKey="activeTab">
        <a-tab-pane key="basic" :tab="t('common.tab.basicInfo')">
          <a-row :gutter="24">
            <a-col :span="12">
              <a-form-item :label="t('admin.dicttype.form.tenantId')" name="tenantId">
                <a-input-number
                  v-model:value="formState.tenantId"
                  :min="0"
                  :max="9999"
                  :precision="0"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('admin.dicttype.form.name')" name="dictName">
                <a-input
                  v-model:value="formState.dictName"
                  :placeholder="t('admin.dicttype.form.namePlaceholder')"
                  :show-count="true"
                  :max-length="100"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('admin.dicttype.form.type')" name="dictType">
                <a-input
                  v-model:value="formState.dictType"
                  :placeholder="t('admin.dicttype.form.typePlaceholder')"
                  :show-count="true"
                  :max-length="100"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('admin.dicttype.form.category')" name="dictCategory">
                <hbt-select
                  v-model:value="formState.dictCategory"
                  dict-type="sys_dict_category"
                  type="radio"
                  :placeholder="t('admin.dicttype.form.categoryPlaceholder')"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('admin.dicttype.form.builtin')" name="dictBuiltin">
                <hbt-select
                  v-model:value="formState.dictBuiltin"
                  dict-type="sys_yes_no"
                  type="radio"
                  :placeholder="t('admin.dicttype.form.builtinPlaceholder')"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('admin.dicttype.form.orderNum')" name="orderNum">
                <a-input-number
                  v-model:value="formState.orderNum"
                  :min="0"
                  :max="9999"
                  style="width: 100%"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('admin.dicttype.form.status')" name="status">
                <hbt-select
                  v-model:value="formState.status"
                  dict-type="sys_normal_disable"
                  type="radio"
                  :placeholder="t('admin.dicttype.form.statusPlaceholder')"
                  allow-clear
                />
              </a-form-item>
            </a-col>
            <a-col :span="24">
              <a-form-item :label="t('admin.dicttype.form.sqlScript')" name="sqlScript" :label-col="{ span: 4 }" :wrapper-col="{ span: 19 }">
                <div class="textarea-wrapper">
                  <a-textarea
                    v-model:value="formState.sqlScript"
                    :placeholder="t('admin.dicttype.form.sqlScriptPlaceholder')"
                    :show-count="true"
                    :max-length="4000"
                    :rows="8"
                    allow-clear
                  />
                  <span class="suffix">
                    <a-popover
                      trigger="hover"
                      placement="topRight"
                      :overlay-style="{ maxWidth: '600px' }"
                    >
                      <template #content>
                        <div class="sql-script-help">
                          <div class="sql-example">
                            <div class="example-title">{{ t('admin.dicttype.sqlHelp.title') }}</div>
                            <pre class="example-code">{{ t('admin.dicttype.sqlHelp.example') }}</pre>
                          </div>
                          <div class="field-desc">
                            <div class="desc-title">{{ t('admin.dicttype.sqlHelp.description') }}</div>
                            <ul class="desc-list">
                              <li v-for="field in sqlHelpFields" :key="field">
                                {{ t(`admin.dicttype.sqlHelp.fields.${field}`) }}
                              </li>
                            </ul>
                          </div>
                        </div>
                      </template>
                      <info-circle-outlined :style="{ color: 'var(--ant-color-text-quaternary)' }" />
                    </a-popover>
                  </span>
                </div>
              </a-form-item>
            </a-col>
            <a-col :span="24">
              <a-form-item :label="t('admin.dicttype.form.remark')" name="remark" :label-col="{ span: 4 }" :wrapper-col="{ span: 19 }">
                <a-textarea
                  v-model:value="formState.remark"
                  :placeholder="t('admin.dicttype.form.remarkPlaceholder')"
                  :show-count="true"
                  :max-length="500"
                  :rows="3"
                  allow-clear
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>
      </a-tabs>
    </a-form>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, watch, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { InfoCircleOutlined } from '@ant-design/icons-vue'
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form'
import type { HbtDictType } from '@/types/admin/hbtDictType'
import type { HbtStatus } from '@/types/common'
import { useDictStore } from '@/stores/dict'
import { useUserStore } from '@/stores/user'

const { t } = useI18n()

// === 属性定义 ===
interface Props {
  open: boolean
  title?: string
  loading?: boolean
  model?: Partial<HbtDictType>
}

const props = withDefaults(defineProps<Props>(), {
  open: false,
  title: '',
  loading: false,
  model: () => ({})
})

// === Emits 定义 ===
const emit = defineEmits<{
  (e: 'update:open', value: boolean): void
  (e: 'ok', values: Partial<HbtDictType>): void
  (e: 'cancel'): void
  (e: 'auth-error'): void
}>()

// === 状态定义 ===
const formRef = ref<FormInstance>()
const activeTab = ref('basic')

// === 字典数据 ===
const dictStore = useDictStore()

// 计算属性：字典类别选项
const dictCategoryOptions = computed(() => {
  return dictStore.getDictOptions('sys_dict_category')
})

// 计算属性：是否内置选项
const yesNoOptions = computed(() => {
  return dictStore.getDictOptions('sys_yes_no')
})

// 计算属性：状态选项
const normalDisableOptions = computed(() => {
  return dictStore.getDictOptions('sys_normal_disable')
})

const formState = ref<Partial<HbtDictType>>({
  dictName: '',
  dictType: '',
  dictCategory: undefined,
  dictBuiltin: undefined,
  orderNum: 0,
  status: 0,
  remark: '',
  tenantId: 0,
  sqlScript: ''
})

// === 表单校验规则 ===
const rules: Record<string, RuleObject[]> = {
  tenantId: [
    { required: true, message: t('admin.dicttype.rules.tenantIdRequired'), trigger: 'blur' },
    { type: 'number', min: 0, max: 9999, message: t('admin.dicttype.rules.tenantIdRange'), trigger: 'blur' }
  ],
  dictName: [
    { required: true, message: t('admin.dicttype.rules.nameRequired'), trigger: 'blur' },
    { min: 2, max: 100, message: t('admin.dicttype.rules.namePattern'), trigger: 'blur' }
  ],
  dictType: [
    { required: true, message: t('admin.dicttype.rules.typeRequired'), trigger: 'blur' },
    { pattern: /^[a-zA-Z][a-zA-Z0-9_]{2,100}$/, message: t('admin.dicttype.rules.typePattern'), trigger: 'blur' },
    {
      validator: async (_rule: RuleObject, value: string) => {
        if (formState.value.dictCategory === 1 && !value.startsWith('sql_')) {
          throw new Error(t('admin.dicttype.rules.typeSqlPrefix'))
        }
      },
      trigger: 'change'
    }
  ],
  dictCategory: [
    { required: true, message: t('admin.dicttype.rules.categoryRequired'), trigger: 'change' }
  ],
  dictBuiltin: [
    { required: true, message: t('admin.dicttype.rules.builtinRequired'), trigger: 'change' }
  ],
  orderNum: [
    { required: true, message: t('admin.dicttype.rules.orderNumRequired'), trigger: 'blur' },
    { type: 'number', min: 0, max: 9999, message: t('admin.dicttype.rules.orderNumRange'), trigger: 'blur' }
  ],
  status: [
    { required: true, message: t('admin.dicttype.rules.statusRequired'), trigger: 'change' }
  ],
  sqlScript: [
    { pattern: /^[\s\S]*SELECT[\s\S]*FROM[\s\S]*$/i, message: t('admin.dicttype.rules.sqlPattern'), trigger: 'blur' }
  ]
}

// === 监听数据变化 ===
watch(
  () => props.model,
  (val) => {
    if (val) {
      formState.value = {
        dictName: val.dictName || '',
        dictType: val.dictType || '',
        dictCategory: val.dictCategory,
        dictBuiltin: val.dictBuiltin,
        orderNum: val.orderNum ?? 0,
        status: val.status ?? 0,
        remark: val.remark || '',
        tenantId: val.tenantId ?? 0,
        sqlScript: val.sqlScript || ''
      }
    }
  },
  { immediate: true }
)

// 监听字典类别变化
watch(
  () => formState.value.dictCategory,
  (newCategory) => {
    // 如果是新建（即没有dictBuiltin值）时，根据类别设置默认值
    if (formState.value.dictBuiltin === undefined) {
      // 系统类型(0)默认为内置(1)，业务类型(1)默认为非内置(0)
      formState.value.dictBuiltin = newCategory === 0 ? 1 : 0
    }
  }
)

// 监听dictName和dictType变化，自动生成备注
watch(
  () => formState.value.dictName,
  (newName) => {
    if (newName && formState.value.dictType) {
      formState.value.remark = `${newName}字典(${formState.value.dictType})`
    }
  }
)

watch(
  () => formState.value.dictType,
  (newType) => {
    if (formState.value.dictName && newType) {
      formState.value.remark = `${formState.value.dictName}字典(${newType})`
    }
  }
)

// === 方法定义 ===
const handleOk = async () => {
  try {
    // 表单验证
    await formRef.value?.validate()
    
    // 权限检查
    if (!hasPermission('admin:dicttype:create')) {
      message.error(t('common.auth.noPermission', { action: t('common.action.create'), resource: t('admin.dicttype.name') }))
      emit('auth-error')
      return
    }

    // 构建提交数据
    const submitData = {
      ...formState.value,
      dictCategory: formState.value.dictCategory ?? 0,
      dictBuiltin: formState.value.dictBuiltin ?? 0,
      status: formState.value.status ?? 0,
      remark: formState.value.remark || `${formState.value.dictName}字典(${formState.value.dictType})`
    }

    // 发送数据
    emit('ok', submitData)
  } catch (error: any) {
    console.error('[DictTypeForm] 表单提交失败:', error)
    
    if (error.response) {
      const { status, data } = error.response
      switch (status) {
        case 403:
          message.error(data?.msg || t('common.auth.accessDenied'))
          emit('auth-error')
          break
        case 401:
          message.error(t('common.auth.sessionExpired'))
          emit('auth-error')
          break
        default:
          message.error(data?.msg || t('common.error.submitFailed'))
      }
    } else if (error.message?.includes('validation')) {
      // 表单验证错误
      message.error(t('common.error.validationFailed'))
    } else {
      message.error(t('common.error.submitFailed'))
    }
  }
}

// 权限检查函数
const hasPermission = (permission: string): boolean => {
  const userStore = useUserStore()
  return userStore.permissions?.includes(permission) ?? false
}

const handleCancel = () => {
  formRef.value?.resetFields()
  emit('update:open', false)
  emit('cancel')
}

// SQL帮助字段列表
const sqlHelpFields = [
  'label',
  'value',
  'cssClass',
  'listClass',
  'status',
  'extLabel',
  'extValue',
  'transKey',
  'orderNum',
  'remark'
]

// === 生命周期钩子 ===
onMounted(() => {
  dictStore.loadDicts([
    'sys_dict_category',
    'sys_yes_no',
    'sys_normal_disable'
  ])
})
</script>

<style lang="less" scoped>
.ant-form-item {
  margin-bottom: 24px;
}

.textarea-wrapper {
  position: relative;
  width: 100%;

  :deep(.ant-input) {
    background-color: var(--ant-color-bg-container);
  }

  .suffix {
    position: absolute;
    top: 8px;
    right: 12px;
    z-index: 1;
    display: flex;
    align-items: center;
    cursor: pointer;
  }
}

.sql-script-help {
  font-size: 13px;
  color: var(--ant-color-text-secondary);
  
  .sql-example {
    margin-bottom: 16px;

    .example-title {
      font-weight: 500;
      margin-bottom: 8px;
      color: var(--ant-color-primary);
    }

    .example-code {
      margin: 0;
      padding: 12px;
      background: var(--ant-color-bg-container-disabled);
      border-radius: var(--ant-border-radius-base);
      font-family: 'Courier New', Courier, monospace;
      font-size: 12px;
      line-height: 1.6;
      white-space: pre-wrap;
      word-break: break-all;
    }
  }

  .field-desc {
    .desc-title {
      font-weight: 500;
      margin-bottom: 8px;
      color: var(--ant-color-primary);
    }

    .desc-list {
      list-style: none;
      padding: 0;
      margin: 0;

      li {
        margin-bottom: 4px;
        
        &:last-child {
          margin-bottom: 0;
        }
      }
    }
  }
}
</style> 