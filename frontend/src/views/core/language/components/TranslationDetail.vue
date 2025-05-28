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
    :title="t('core.translation.actions.detail')"
    :open="open"
    :footer="null"
    width="800px"
    @cancel="handleCancel"
  >
    <a-spin :spinning="loading">
      <a-descriptions
        :column="1"
        bordered
        class="translation-detail"
        size="small"
      >
        <a-descriptions-item :label="t('core.translation.fields.transKey.label')">
          {{ detail.transKey }}
        </a-descriptions-item>

        <a-descriptions-item :label="t('core.translation.fields.moduleName.label')">
          {{ detail.moduleName }}
        </a-descriptions-item>

        <a-descriptions-item :label="t('core.translation.fields.status.label')">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="detail.status" />
        </a-descriptions-item>

        <a-descriptions-item :label="t('core.translation.fields.remark.label')">
          {{ detail.remark }}
        </a-descriptions-item>

        <a-descriptions-item :label="t('table.columns.createBy')">
          {{ detail.createBy }}
        </a-descriptions-item>

        <a-descriptions-item :label="t('table.columns.createTime')">
          {{ detail.createTime }}
        </a-descriptions-item>

        <a-descriptions-item :label="t('table.columns.updateBy')">
          {{ detail.updateBy }}
        </a-descriptions-item>

        <a-descriptions-item :label="t('table.columns.updateTime')">
          {{ detail.updateTime }}
        </a-descriptions-item>
      </a-descriptions>

      <!-- 翻译列表 -->
      <div class="translation-list">
        <h3 class="section-title">{{ t('core.translation.fields.translations.label') }}</h3>
        <a-table
          :columns="columns"
          :data-source="translationList"
          :pagination="false"
          size="small"
          bordered
        >
          <template #bodyCell="{ column, record }">
            <template v-if="column.dataIndex === 'status'">
              <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
            </template>
          </template>
        </a-table>
      </div>
    </a-spin>
    <template #footer>
      <div class="dialog-footer">
        <a-button @click="handleCancel">{{ t('common.button.cancel') }}</a-button>
      </div>
    </template>
  </hbt-modal>
</template>

<script lang="ts" setup>
import { ref, onMounted, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtTransposedData, HbtTranslationLang } from '@/types/core/translation'
import { getHbtTransposedDetail } from '@/api/core/translation'

const { t } = useI18n()

const props = defineProps({
  open: {
    type: Boolean,
    default: false
  },
  transKey: {
    type: String,
    required: true
  }
})

const emit = defineEmits(['update:open'])

const loading = ref(false)
const detail = ref<HbtTransposedData>({
  transKey: '',
  moduleName: '',
  status: 0,
  remark: '',
  createBy: '',
  createTime: '',
  updateBy: '',
  updateTime: '',
  translations: {}
})

// 表格列定义
const columns = [
  {
    title: t('core.translation.fields.langCode.label'),
    dataIndex: 'langCode',
    key: 'langCode',
    width: 120
  },
  {
    title: t('core.translation.fields.transValue.label'),
    dataIndex: 'transValue',
    key: 'transValue',
    ellipsis: true
  },
  {
    title: t('core.translation.fields.status.label'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  }
]

// 转换翻译数据为表格数据
const translationList = computed(() => {
  return Object.entries(detail.value.translations).map(([langCode, translation]) => ({
    key: langCode,
    ...translation
  }))
})

const loadTranslationDetail = async () => {
  if (!props.transKey) {
    message.error(t('common.message.paramError'))
    return
  }
  
  try {
    loading.value = true
    const res = await getHbtTransposedDetail(props.transKey)
    if (res.data.code === 200) {
      detail.value = res.data.data
    } else {
      message.error(res.data.msg || t('common.message.queryFailed'))
    }
  } catch (error) {
    console.error('加载翻译详情失败:', error)
    message.error(t('common.message.queryFailed'))
  } finally {
    loading.value = false
  }
}

const handleCancel = () => {
  emit('update:open', false)
}

// 监听visible和transKey的变化
watch(
  () => [props.open, props.transKey],
  ([newOpen, newTransKey]) => {
    if (newOpen && newTransKey) {
      loadTranslationDetail()
    }
  },
  { immediate: true }
)

onMounted(() => {
  if (props.open && props.transKey) {
    loadTranslationDetail()
  }
})
</script>

<style lang="less" scoped>
.translation-detail {
  padding: 24px;

  :deep(.ant-descriptions-item-label) {
    width: 120px;
    font-weight: 500;
  }
}

.translation-list {
  margin-top: 24px;
  padding: 0 24px 24px;

  .section-title {
    margin-bottom: 16px;
    font-size: 16px;
    font-weight: 500;
  }
}

.dialog-footer {
  text-align: right;
  padding: 8px 24px;
  border-top: 1px solid var(--ant-color-split);
}
</style> 