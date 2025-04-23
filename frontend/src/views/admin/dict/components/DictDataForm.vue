//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : DictDataForm.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据表单组件
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
        :label="t('admin.dict.dictLabel.label')"
        name="dictLabel"
      >
        <hbt-input
          v-model:value="formData.dictLabel"
          :placeholder="t('admin.dict.dictLabel.placeholder')"
        />
      </hbt-form-item>

      <hbt-form-item
        :label="t('admin.dict.dictValue.label')"
        name="dictValue"
      >
        <hbt-input
          v-model:value="formData.dictValue"
          :placeholder="t('admin.dict.dictValue.placeholder')"
        />
      </hbt-form-item>

      <hbt-form-item
        :label="t('admin.dict.orderNum.label')"
        name="orderNum"
      >
        <hbt-input-number
          v-model:value="formData.orderNum"
          :placeholder="t('admin.dict.orderNum.placeholder')"
          :min="0"
          :max="999"
          style="width: 100%"
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
import type { HbtDictData, HbtDictDataCreate, HbtDictDataUpdate } from '@/types/admin/dictData'
import { getHbtDictData, createHbtDictData, updateHbtDictData } from '@/api/admin/dictData'

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  title: {
    type: String,
    default: ''
  },
  dictType: {
    type: String,
    required: true
  },
  dictDataId: {
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
const formData = reactive<HbtDictDataCreate>({
  dictType: props.dictType,
  dictLabel: '',
  dictValue: '',
  orderNum: 0,
  status: 0,
  isDefault: 0,
  tenantId: 0,
  remark: ''
})

// === 表单验证规则 ===
const rules = {
  dictLabel: [
    { required: true, message: t('admin.dict.dictLabel.required'), trigger: 'blur' },
    { min: 2, max: 50, message: t('admin.dict.dictLabel.length'), trigger: 'blur' }
  ],
  dictValue: [
    { required: true, message: t('admin.dict.dictValue.required'), trigger: 'blur' },
    { min: 2, max: 50, message: t('admin.dict.dictValue.length'), trigger: 'blur' }
  ],
  orderNum: [
    { required: true, message: t('admin.dict.orderNum.required'), trigger: 'change' }
  ],
  status: [
    { required: true, message: t('admin.dict.status.required'), trigger: 'change' }
  ]
}

// === 方法定义 ===
// 获取字典数据详情
const getDictData = async (id: number) => {
  try {
    loading.value = true
    const res = await getHbtDictData(id)
    if (res.code === 200) {
      Object.assign(formData, res.data)
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取字典数据详情失败:', error)
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
    if (props.dictDataId) {
      // 更新
      const data: HbtDictDataUpdate = {
        ...formData,
        dictDataId: props.dictDataId
      }
      res = await updateHbtDictData(data)
    } else {
      // 新增
      res = await createHbtDictData(formData)
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
    dictType: props.dictType,
    dictLabel: '',
    dictValue: '',
    orderNum: 0,
    status: 0,
    isDefault: 0,
    tenantId: 0,
    remark: ''
  })
}

// === 监听器 ===
// 监听对话框可见性变化
watch(() => props.visible, (val) => {
  if (val) {
    if (props.dictDataId) {
      getDictData(props.dictDataId)
    } else {
      resetForm()
    }
  }
})

// === 生命周期 ===
onMounted(() => {
  if (props.visible && props.dictDataId) {
    getDictData(props.dictDataId)
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