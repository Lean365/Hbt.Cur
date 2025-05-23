//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : DictDataDetail.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典数据详情组件
//===================================================================

<template>
  <hbt-modal
    :open="open"
    :title="t('core.dict.dictData.detail')"
    :footer="null"
    width="600px"
    @cancel="handleCancel"
  >
    <a-descriptions :column="1" bordered>
      <a-descriptions-item :label="t('core.dict.dictDatas.fields.dictLabel.label')">
        {{ detailData.dictLabel }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('core.dict.dictDatas.fields.dictValue.label')">
        {{ detailData.dictValue }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('core.dict.dictDatas.fields.dictType.label')">
        {{ detailData.dictType }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.dict.dictDatas.fields.extLabel.label')">
        {{ detailData.extLabel }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.dict.dictDatas.fields.extValue.label')">
        {{ detailData.extValue }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.dict.dictDatas.fields.listClass.label')">
        {{ detailData.listClass }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.dict.dictDatas.fields.cssClass.label')">
        {{ detailData.cssClass }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('core.dict.dictDatas.fields.orderNum.label')">
        {{ detailData.orderNum }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('core.dict.dictDatas.fields.status.label')">
        <hbt-dict-tag dict-type="sys_normal_disable" :value="detailData.status" />
      </a-descriptions-item>

      <a-descriptions-item :label="t('core.dict.dictDatas.fields.remark.label')">
        {{ detailData.remark }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('common.createTime')">
        {{ detailData.createTime }}
      </a-descriptions-item>

      <a-descriptions-item :label="t('common.updateTime')">
        {{ detailData.updateTime }}
      </a-descriptions-item>
    </a-descriptions>

    <template #footer>
      <div class="dialog-footer">
        <a-button @click="handleCancel">{{ t('common.button.cancel') }}</a-button>
      </div>
    </template>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtDictData } from '@/types/core/dictData'
import { getHbtDictData } from '@/api/core/dictData'

const props = defineProps({
  open: {
    type: Boolean,
    default: false
  },
  dictDataId: {
    type: Number,
    default: undefined
  }
})

const emit = defineEmits(['update:open'])

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)

// === 详情数据 ===
const detailData = reactive<HbtDictData>({
  dictDataId: 0,
  dictType: '',
  dictLabel: '',
  dictValue: '',
  orderNum: 0,
  status: 0,
  isDefault: 0,
  tenantId: 0,
  remark: '',
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: '',
  isDeleted: 0
})

// === 方法定义 ===
// 获取字典数据详情
const getDictData = async (id: number) => {
  try {
    loading.value = true
    const res = await getHbtDictData(id)
    if (res.data.code === 200) {
      Object.assign(detailData, res.data.data)
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取字典数据详情失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 取消
const handleCancel = () => {
  emit('update:open', false)
}

// === 监听器 ===
// 监听对话框可见性变化
watch(() => props.open, (val) => {
  if (val && props.dictDataId) {
    getDictData(props.dictDataId)
  }
})

// === 生命周期 ===
onMounted(() => {
  if (props.open && props.dictDataId) {
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