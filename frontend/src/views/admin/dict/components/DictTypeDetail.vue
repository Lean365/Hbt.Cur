//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : DictTypeDetail.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 字典类型详情组件
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
      <hbt-descriptions-item :label="t('admin.dict.dictName.label')">
        {{ detailData.dictName }}
      </hbt-descriptions-item>

      <hbt-descriptions-item :label="t('admin.dict.dictType.label')">
        {{ detailData.dictType }}
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
import type { HbtDictType } from '@/types/admin/dictType'
import { getHbtDictType } from '@/api/admin/dictType'

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  dictTypeId: {
    type: Number,
    default: undefined
  }
})

const emit = defineEmits(['update:visible'])

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)

// === 详情数据 ===
const detailData = reactive<HbtDictType>({
  id: 0,
  dictTypeId: 0,
  dictName: '',
  dictType: '',
  dictCategory: '',
  dictBuiltin: 0,
  orderNum: 0,
  tenantId: 0,
  status: 0,
  remark: '',
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: '',
  isDeleted: 0
})

// === 方法定义 ===
// 获取字典类型详情
const getDictType = async (id: number) => {
  try {
    loading.value = true
    const res = await getHbtDictType(id)
    if (res.code === 200) {
      Object.assign(detailData, res.data)
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

// 取消
const handleCancel = () => {
  emit('update:visible', false)
}

// === 监听器 ===
// 监听对话框可见性变化
watch(() => props.visible, (val) => {
  if (val && props.dictTypeId) {
    getDictType(props.dictTypeId)
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