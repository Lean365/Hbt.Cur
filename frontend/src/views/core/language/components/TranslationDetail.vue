//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : TranslationDetail.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 翻译详情组件
//===================================================================

<template>
  <hbt-modal
    :title="t('common.title.detail')"
    :open="dialogVisible"
    width="600px"
    append-to-body
    destroy-on-close
  >
    <a-descriptions :column="1" bordered>
      <a-descriptions-item :label="t('admin.language.translation.moduleName')">
        {{ form.moduleName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.language.translation.transKey')">
        {{ form.transKey }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.language.translation.transValue')">
        {{ form.transValue }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.language.translation.orderNum')">
        {{ form.orderNum }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.language.translation.status')">
        <hbt-dict-tag dict-type="sys_normal_disable" :value="form.status" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('admin.language.translation.remark')">
        {{ form.remark }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('common.createTime')">
        {{ form.createTime }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('common.updateTime')">
        {{ form.updateTime }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('common.createBy')">
        {{ form.createBy }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('common.updateBy')">
        {{ form.updateBy }}
      </a-descriptions-item>
    </a-descriptions>
    <template #footer>
      <div class="dialog-footer">
        <a-button @click="handleCancel">{{ t('common.button.close') }}</a-button>
      </div>
    </template>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtTranslation } from '@/types/core/translation'
import { getHbtTranslation } from '@/api/core/translation'

const props = defineProps<{
  translationId?: number
}>()

const emit = defineEmits(['update:visible'])

const { t } = useI18n()
const loading = ref(false)
const dialogVisible = ref(true)

const form = reactive<HbtTranslation>({
  id: 0,
  translationId: 0,
  langCode: '',
  moduleName: '',
  transKey: '',
  transValue: '',
  orderNum: 0,
  status: 0,
  remark: '',
  createTime: '',
  updateTime: '',
  createBy: '',
  updateBy: '',
  isDeleted: 0
})

// 获取翻译详情
const getDetail = async () => {
  if (!props.translationId) return
  
  try {
    loading.value = true
    const res = await getHbtTranslation(props.translationId)
    if (res.code === 200) {
      Object.assign(form, res.data)
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取翻译详情失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 取消
const handleCancel = () => {
  dialogVisible.value = false
  emit('update:visible', false)
}

// 监听对话框可见性变化
watch(() => dialogVisible.value, (val) => {
  emit('update:visible', val)
})

// 初始化
getDetail()
</script>

<style lang="less" scoped>
.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
}
</style> 