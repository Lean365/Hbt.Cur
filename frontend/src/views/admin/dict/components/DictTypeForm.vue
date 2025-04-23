//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : DictTypeForm.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典类型表单组件
//===================================================================

<template>
  <hbt-modal
    :title="title"
    :open="visible"
    width="600px"
    append-to-body
    destroy-on-close
    @cancel="handleCancel"
  >
    <hbt-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <hbt-form-item
        :label="t('admin.dict.dictName.label')"
        name="dictName"
      >
        <hbt-input
          v-model:value="formData.dictName"
          :placeholder="t('admin.dict.dictName.placeholder')"
          :disabled="!!dictTypeId"
        />
      </hbt-form-item>

      <hbt-form-item
        :label="t('admin.dict.dictType.label')"
        name="dictType"
      >
        <hbt-input
          v-model:value="formData.dictType"
          :placeholder="t('admin.dict.dictType.placeholder')"
          :disabled="!!dictTypeId"
        />
      </hbt-form-item>

      <hbt-form-item
        :label="t('admin.dict.status.label')"
        name="status"
      >
        <hbt-radio-group v-model:value="formData.status">
          <hbt-radio :value="0">{{ t('admin.dict.status.normal') }}</hbt-radio>
          <hbt-radio :value="1">{{ t('admin.dict.status.disable') }}</hbt-radio>
        </hbt-radio-group>
      </hbt-form-item>

      <hbt-form-item
        :label="t('admin.dict.remark.label')"
        name="remark"
      >
        <hbt-textarea
          v-model:value="formData.remark"
          :placeholder="t('admin.dict.remark.placeholder')"
          :rows="4"
        />
      </hbt-form-item>
    </hbt-form>

    <template #footer>
      <div class="dialog-footer">
        <hbt-button @click="handleCancel">{{ t('common.cancel') }}</hbt-button>
        <hbt-button type="primary" :loading="loading" @click="handleSubmit">
          {{ t('common.confirm') }}
        </hbt-button>
      </div>
    </template>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { HbtDictType, HbtDictTypeCreate, HbtDictTypeUpdate } from '@/types/admin/dictType'
import { getHbtDictType, createHbtDictType, updateHbtDictType } from '@/api/admin/dictType'

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  title: {
    type: String,
    default: ''
  },
  dictTypeId: {
    type: Number,
    default: undefined
  }
})

const emit = defineEmits(['update:visible', 'success'])

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)
const formRef = ref<FormInstance>()

// === 表单数据 ===
const formData = reactive<HbtDictTypeCreate>({
  dictName: '',
  dictType: '',
  dictCategory: '',
  dictBuiltin: 0,
  orderNum: 0,
  tenantId: 0,
  status: 0,
  remark: ''
})

// === 表单验证规则 ===
const rules = {
  dictName: [
    { required: true, message: t('admin.dict.dictName.required'), trigger: 'blur' },
    { min: 2, max: 50, message: t('admin.dict.dictName.length'), trigger: 'blur' }
  ],
  dictType: [
    { required: true, message: t('admin.dict.dictType.required'), trigger: 'blur' },
    { min: 2, max: 50, message: t('admin.dict.dictType.length'), trigger: 'blur' }
  ],
  status: [
    { required: true, message: t('admin.dict.status.required'), trigger: 'change' }
  ]
}

// === 方法定义 ===
// 获取字典类型详情
const getDictType = async (id: number) => {
  try {
    loading.value = true
    const res = await getHbtDictType(id)
    if (res.code === 200) {
      Object.assign(formData, res.data)
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取字典类型详情失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true

    let res
    if (props.dictTypeId) {
      // 更新
      const data: HbtDictTypeUpdate = {
        ...formData,
        dictTypeId: props.dictTypeId
      }
      res = await updateHbtDictType(data)
    } else {
      // 新增
      res = await createHbtDictType(formData)
    }

    if (res.code === 200) {
      message.success(t('common.message.saveSuccess'))
      emit('success')
    } else {
      message.error(res.msg || t('common.message.saveFailed'))
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
  emit('update:visible', false)
}

// 重置表单
const resetForm = () => {
  formRef.value?.resetFields()
  Object.assign(formData, {
    dictName: '',
    dictType: '',
    dictCategory: '',
    dictBuiltin: 0,
    orderNum: 0,
    tenantId: 0,
    status: 0,
    remark: ''
  })
}

// === 监听器 ===
// 监听对话框可见性变化
watch(() => props.visible, (val) => {
  if (val) {
    if (props.dictTypeId) {
      getDictType(props.dictTypeId)
    } else {
      resetForm()
    }
  }
})

// === 生命周期 ===
onMounted(() => {
  if (props.visible && props.dictTypeId) {
    getDictType(props.dictTypeId)
  }
})
</script>

<style lang="less" scoped>
.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}
</style> 