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
    :title="t('common.title.detail')"
    :open="visible"
    width="600px"
    append-to-body
    destroy-on-close
    @cancel="handleCancel"
  >
    <hbt-descriptions :column="1" bordered>
      <hbt-descriptions-item :label="t('admin.dict.dictLabel.label')">
        {{ detailData.dictLabel }}
      </hbt-descriptions-item>

      <hbt-descriptions-item :label="t('admin.dict.dictValue.label')">
        {{ detailData.dictValue }}
      </hbt-descriptions-item>

      <hbt-descriptions-item :label="t('admin.dict.orderNum.label')">
        {{ detailData.orderNum }}
      </hbt-descriptions-item>

      <hbt-descriptions-item :label="t('admin.dict.status.label')">
        <hbt-dict-tag dict-type="sys_normal_disable" :value="detailData.status" />
      </hbt-descriptions-item>

      <hbt-descriptions-item :label="t('admin.dict.remark.label')">
        {{ detailData.remark }}
      </hbt-descriptions-item>

      <hbt-descriptions-item :label="t('common.createTime')">
        {{ detailData.createTime }}
      </hbt-descriptions-item>

      <hbt-descriptions-item :label="t('common.updateTime')">
        {{ detailData.updateTime }}
      </hbt-descriptions-item>
    </hbt-descriptions>

    <template #footer>
      <div class="dialog-footer">
        <hbt-button @click="handleCancel">{{ t('common.close') }}</hbt-button>
      </div>
    </template>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtDictData } from '@/types/admin/dictData'
import { getHbtDictData } from '@/api/admin/dictData'

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  dictDataId: {
    type: Number,
    default: undefined
  }
})

const emit = defineEmits(['update:visible'])

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)

// === 详情数据 ===
const detailData = reactive<HbtDictData>({
  id: 0,
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
    if (res.code === 200) {
      Object.assign(detailData, res.data)
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

// 取消
const handleCancel = () => {
  emit('update:visible', false)
}

// === 监听器 ===
// 监听对话框可见性变化
watch(() => props.visible, (val) => {
  if (val && props.dictDataId) {
    getDictData(props.dictDataId)
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