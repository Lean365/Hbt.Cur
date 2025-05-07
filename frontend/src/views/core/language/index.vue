//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : index.vue
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 语言管理页面
//===================================================================

<template>
  <div class="language-container">
    <!-- 查询区域 -->
    <hbt-query-form
      :loading="loading"
      :fields="queryFields"
      v-model:queryParams="queryParams"
      @search="handleSearch"
      @reset="handleReset"
    />

    <!-- 工具栏 -->
    <hbt-toolbar
      :show-add="true"
      :add-permission="['admin:language:create']"
      :show-edit="true"
      :edit-permission="['admin:language:update']"
      :show-delete="true"
      :delete-permission="['admin:language:delete']"
      :disabled-edit="selectedRowKeys.length !== 1"
      :disabled-delete="selectedRowKeys.length === 0"
      @add="handleAdd"
      @edit="handleEditSelected"
      @delete="handleBatchDelete"
      @refresh="fetchData"
    />

    <!-- 数据表格 -->
    <hbt-table
      :loading="loading"
      :data-source="tableData"
      :columns="columns"
      :pagination="false"
      :scroll="{ x: 'max-content' }"
      :row-key="(record: HbtLanguage) => String(record.languageId)"
      v-model:selectedRowKeys="selectedRowKeys"
      :row-selection="{
        type: 'checkbox',
        columnWidth: 60
      }"
    >
      <!-- 状态列 -->
      <template #bodyCell="{ column, record }">
        <template v-if="column.dataIndex === 'status'">
          <hbt-dict-tag dict-type="sys_normal_disable" :value="record.status" />
        </template>

        <!-- 是否默认列 -->
        <template v-if="column.dataIndex === 'isDefault'">
          <hbt-dict-tag dict-type="sys_yes_no" :value="record.isDefault ? 1 : 0" />
        </template>

        <!-- 操作列 -->
        <template v-if="column.key === 'action'">
          <hbt-operation
            :record="record"
            :show-view="true"
            :view-permission="['admin:language:query']"
            :show-edit="true"
            :edit-permission="['admin:language:update']"
            :show-delete="true"
            :delete-permission="['admin:language:delete']"
            size="small"
            @view="handleView"
            @edit="handleEdit"
            @delete="handleDelete"
          />
          <a-button
            type="link"
            size="small"
            @click="handleTranslation(record)"
          >
            {{ t('admin.language.translation') }}
          </a-button>
        </template>
      </template>
    </hbt-table>

    <!-- 分页组件 -->
    <hbt-pagination
      v-model:current="queryParams.pageIndex"
      v-model:pageSize="queryParams.pageSize"
      :total="total"
      :show-size-changer="true"
      :show-quick-jumper="true"
      :show-total="(total: number, range: [number, number]) => h('span', null, `共 ${total} 条`)"
      @change="handlePageChange"
      @showSizeChange="handleSizeChange"
    />

    <!-- 语言表单对话框 -->
    <language-form
      v-model:visible="formVisible"
      :title="formTitle"
      :language-id="selectedLanguageId"
      @success="handleSuccess"
    />

    <!-- 语言详情对话框 -->
    <language-detail
      v-model:dialog-visible="detailVisible"
      :language-id="selectedLanguageId!"
    />

    <!-- 翻译管理对话框 -->
    <translation
      v-if="selectedLanguage"
      v-model:visible="translationVisible"
      :lang-code="selectedLanguage.langCode"
    />
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { HbtLanguage, HbtLanguageQuery } from '@/types/core/language'
import {
  getHbtLanguageList,
  getHbtLanguage,
  createHbtLanguage,
  updateHbtLanguage,
  deleteHbtLanguage,
  batchDeleteHbtLanguage
} from '@/api/core/language'
import LanguageForm from './components/LanguageForm.vue'
import LanguageDetail from './components/LanguageDetail.vue'
import Translation from './components/Translation.vue'

const { t } = useI18n()

// === 状态定义 ===
const loading = ref(false)
const tableData = ref<HbtLanguage[]>([])
const selectedRowKeys = ref<string[]>([])
const selectedLanguageId = ref<number>()
const selectedLanguage = ref<HbtLanguage>()
const formVisible = ref(false)
const formTitle = ref('')
const detailVisible = ref(false)
const translationVisible = ref(false)
const total = ref(0)

// === 查询参数 ===
const queryParams = ref<HbtLanguageQuery>({
  pageIndex: 1,
  pageSize: 10,
  langCode: '',
  langName: '',
  status: undefined
})

// === 查询字段定义 ===
const queryFields = [
  {
    label: t('admin.language.langCode'),
    name: 'langCode',
    component: 'Input',
    componentProps: {
      placeholder: t('admin.language.langCodePlaceholder')
    }
  },
  {
    label: t('admin.language.langName'),
    name: 'langName',
    component: 'Input',
    componentProps: {
      placeholder: t('admin.language.langNamePlaceholder')
    }
  },
  {
    label: t('admin.language.status'),
    name: 'status',
    component: 'Select',
    componentProps: {
      placeholder: t('admin.language.statusPlaceholder'),
      options: [
        { label: t('common.status.normal'), value: 0 },
        { label: t('common.status.disable'), value: 1 }
      ]
    }
  }
]

// === 表格列定义 ===
const columns = [
  {
    title: t('admin.language.langCode'),
    dataIndex: 'langCode',
    key: 'langCode',
    width: 150
  },
  {
    title: t('admin.language.langName'),
    dataIndex: 'langName',
    key: 'langName',
    width: 150
  },
  {
    title: t('admin.language.langIcon'),
    dataIndex: 'langIcon',
    key: 'langIcon',
    width: 100
  },
  {
    title: t('admin.language.orderNum'),
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100,
    sorter: true
  },
  {
    title: t('admin.language.isDefault'),
    dataIndex: 'isDefault',
    key: 'isDefault',
    width: 100
  },
  {
    title: t('admin.language.status'),
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: t('admin.language.remark'),
    dataIndex: 'remark',
    key: 'remark',
    width: 200,
    ellipsis: true
  },
  {
    title: t('common.createTime'),
    dataIndex: 'createTime',
    key: 'createTime',
    width: 180,
    sorter: true
  },
  {
    title: t('common.table.header.operation'),
    key: 'action',
    width: 180,
    fixed: 'right'
  }
]

// === 方法定义 ===
// 获取数据
const fetchData = async () => {
  try {
    loading.value = true
    const res = await getHbtLanguageList(queryParams.value)
    if (res.code === 200) {
      tableData.value = res.data.rows
      total.value = res.data.totalNum
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('加载语言列表失败:', error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 查询
const handleSearch = () => {
  queryParams.value.pageIndex = 1
  fetchData()
}

// 重置
const handleReset = () => {
  queryParams.value = {
    pageIndex: 1,
    pageSize: 10,
    langCode: '',
    langName: '',
    status: undefined
  }
  fetchData()
}

// 新增
const handleAdd = () => {
  selectedLanguageId.value = undefined
  formTitle.value = t('common.title.create')
  formVisible.value = true
}

// 编辑选中
const handleEditSelected = () => {
  if (selectedRowKeys.value.length !== 1) {
    message.warning(t('common.message.selectOneRecord'))
    return
  }
  const record = tableData.value.find(item => String(item.languageId) === selectedRowKeys.value[0])
  if (record) {
    handleEdit(record)
  }
}

// 编辑
const handleEdit = (record: HbtLanguage) => {
  selectedLanguageId.value = record.languageId
  formTitle.value = t('common.title.edit')
  formVisible.value = true
}

// 查看
const handleView = (record: HbtLanguage) => {
  selectedLanguageId.value = record.languageId
  detailVisible.value = true
}

// 删除
const handleDelete = async (record: HbtLanguage) => {
  try {
    const res = await deleteHbtLanguage(record.languageId)
    if (res.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      fetchData()
    } else {
      message.error(res.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 批量删除
const handleBatchDelete = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning(t('common.message.selectRecord'))
    return
  }
  try {
    const res = await batchDeleteHbtLanguage(selectedRowKeys.value.map(key => Number(key)))
    if (res.code === 200) {
      message.success(t('common.message.deleteSuccess'))
      selectedRowKeys.value = []
      fetchData()
    } else {
      message.error(res.msg || t('common.message.deleteFailed'))
    }
  } catch (error) {
    console.error('批量删除失败:', error)
    message.error(t('common.message.deleteFailed'))
  }
}

// 翻译管理
const handleTranslation = (record: HbtLanguage) => {
  selectedLanguage.value = record
  translationVisible.value = true
}

// 页码变化
const handlePageChange = (page: number) => {
  queryParams.value.pageIndex = page
  fetchData()
}

// 每页条数变化
const handleSizeChange = (current: number, size: number) => {
  queryParams.value.pageIndex = current
  queryParams.value.pageSize = size
  fetchData()
}

// 表单提交成功
const handleSuccess = () => {
  formVisible.value = false
  fetchData()
}

// === 生命周期 ===
onMounted(() => {
  fetchData()
})
</script>

<style lang="less" scoped>
.language-container {
  padding: 16px;
  background-color: #fff;
  border-radius: 4px;
}
</style> 