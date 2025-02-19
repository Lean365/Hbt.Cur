<template>
  <a-modal
    :title="t('common.import.title')"
    :visible="visible"
    :confirm-loading="loading"
    @update:visible="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-form :label-col="{ span: 4 }" :wrapper-col="{ span: 19 }">
      <!-- 文件上传 -->
      <a-form-item :label="t('common.import.file')">
        <a-upload
          :accept="accept"
          :action="uploadUrl"
          :headers="headers"
          :show-upload-list="true"
          :before-upload="handleBeforeUpload"
          :customRequest="handleCustomRequest"
          :maxCount="1"
          @change="handleChange"
        >
          <a-button>
            <upload-outlined />
            {{ t('common.import.select') }}
          </a-button>
        </a-upload>
      </a-form-item>

      <!-- 下载模板 -->
      <a-form-item :label="t('common.import.template')" v-if="templateName">
        <a @click="handleDownloadTemplate">
          <download-outlined />
          {{ t('common.import.download') }}
        </a>
      </a-form-item>

      <!-- 导入说明 -->
      <a-form-item :label="t('common.import.note')">
        <a-alert
          :message="t('common.import.tips')"
          type="info"
          show-icon
        />
      </a-form-item>
    </a-form>

    <!-- 导入结果 -->
    <template v-if="importResult">
      <a-divider />
      <a-descriptions :column="1">
        <a-descriptions-item :label="t('common.import.total')">
          {{ importResult.total }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('common.import.success')">
          {{ importResult.success }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('common.import.failed')">
          {{ importResult.failed }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('common.import.message')" v-if="importResult.message">
          {{ importResult.message }}
        </a-descriptions-item>
      </a-descriptions>
    </template>
  </a-modal>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { UploadOutlined, DownloadOutlined } from '@ant-design/icons-vue'
import { getToken } from '@/utils/auth'
import type { UploadChangeParam, UploadProps } from 'ant-design-vue'

interface ImportResult {
  total: number
  success: number
  failed: number
  message?: string
}

interface Props {
  visible: boolean
  uploadUrl: string
  templateName?: string
  accept?: string
}

const props = withDefaults(defineProps<Props>(), {
  accept: '.xlsx,.xls'
})

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 加载状态
const loading = ref(false)

// 导入结果
const importResult = ref<ImportResult>()

// 上传文件
const uploadFile = ref<File>()

// 请求头
const headers = {
  Authorization: 'Bearer ' + getToken()
}

// 处理对话框显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:visible', val)
  if (!val) {
    uploadFile.value = undefined
    importResult.value = undefined
  }
}

// 上传前校验
const handleBeforeUpload = (file: File) => {
  const isExcel = file.type === 'application/vnd.ms-excel' || 
    file.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
  if (!isExcel) {
    message.error(t('common.import.format'))
    return false
  }
  const isLt2M = file.size / 1024 / 1024 < 2
  if (!isLt2M) {
    message.error(t('common.import.size'))
    return false
  }
  return true
}

// 自定义上传
const handleCustomRequest = async (options: any) => {
  const { file, onSuccess, onError } = options
  uploadFile.value = file
  try {
    const formData = new FormData()
    formData.append('file', file)
    const response = await fetch(props.uploadUrl, {
      method: 'POST',
      headers: {
        Authorization: 'Bearer ' + getToken()
      },
      body: formData
    })
    const result = await response.json()
    if (result.code === 200) {
      onSuccess(result)
      importResult.value = result.data
      message.success(t('common.import.success'))
      emit('success')
    } else {
      onError(result)
      message.error(result.msg || t('common.import.failed'))
    }
  } catch (error) {
    onError(error)
    message.error(t('common.import.failed'))
  }
}

// 上传状态改变
const handleChange = (info: UploadChangeParam) => {
  if (info.file.status === 'uploading') {
    loading.value = true
  } else {
    loading.value = false
  }
}

// 下载模板
const handleDownloadTemplate = () => {
  if (!props.templateName) return
  const link = document.createElement('a')
  link.href = `/api/common/template/${props.templateName}`
  link.download = props.templateName
  document.body.appendChild(link)
  link.click()
  document.body.removeChild(link)
}

// 提交
const handleSubmit = () => {
  if (!uploadFile.value) {
    message.warning(t('common.import.select'))
    return
  }
  // 触发自定义上传
  handleCustomRequest({
    file: uploadFile.value,
    onSuccess: () => {},
    onError: () => {}
  })
}
</script>

<style lang="less" scoped>
.ant-upload-list {
  margin-top: 8px;
}
</style>
  <a-modal
    :title="t('common.import.title')"
    :visible="visible"
    :confirm-loading="loading"
    @update:visible="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-form :label-col="{ span: 4 }" :wrapper-col="{ span: 19 }">
      <!-- 文件上传 -->
      <a-form-item :label="t('common.import.file')">
        <a-upload
          :accept="accept"
          :action="uploadUrl"
          :headers="headers"
          :show-upload-list="true"
          :before-upload="handleBeforeUpload"
          :customRequest="handleCustomRequest"
          :maxCount="1"
          @change="handleChange"
        >
          <a-button>
            <upload-outlined />
            {{ t('common.import.select') }}
          </a-button>
        </a-upload>
      </a-form-item>

      <!-- 下载模板 -->
      <a-form-item :label="t('common.import.template')" v-if="templateName">
        <a @click="handleDownloadTemplate">
          <download-outlined />
          {{ t('common.import.download') }}
        </a>
      </a-form-item>

      <!-- 导入说明 -->
      <a-form-item :label="t('common.import.note')">
        <a-alert
          :message="t('common.import.tips')"
          type="info"
          show-icon
        />
      </a-form-item>
    </a-form>

    <!-- 导入结果 -->
    <template v-if="importResult">
      <a-divider />
      <a-descriptions :column="1">
        <a-descriptions-item :label="t('common.import.total')">
          {{ importResult.total }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('common.import.success')">
          {{ importResult.success }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('common.import.failed')">
          {{ importResult.failed }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('common.import.message')" v-if="importResult.message">
          {{ importResult.message }}
        </a-descriptions-item>
      </a-descriptions>
    </template>
  </a-modal>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { UploadOutlined, DownloadOutlined } from '@ant-design/icons-vue'
import { getToken } from '@/utils/auth'
import type { UploadChangeParam, UploadProps } from 'ant-design-vue'

interface ImportResult {
  total: number
  success: number
  failed: number
  message?: string
}

interface Props {
  visible: boolean
  uploadUrl: string
  templateName?: string
  accept?: string
}

const props = withDefaults(defineProps<Props>(), {
  accept: '.xlsx,.xls'
})

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 加载状态
const loading = ref(false)

// 导入结果
const importResult = ref<ImportResult>()

// 上传文件
const uploadFile = ref<File>()

// 请求头
const headers = {
  Authorization: 'Bearer ' + getToken()
}

// 处理对话框显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:visible', val)
  if (!val) {
    uploadFile.value = undefined
    importResult.value = undefined
  }
}

// 上传前校验
const handleBeforeUpload = (file: File) => {
  const isExcel = file.type === 'application/vnd.ms-excel' || 
    file.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
  if (!isExcel) {
    message.error(t('common.import.format'))
    return false
  }
  const isLt2M = file.size / 1024 / 1024 < 2
  if (!isLt2M) {
    message.error(t('common.import.size'))
    return false
  }
  return true
}

// 自定义上传
const handleCustomRequest = async (options: any) => {
  const { file, onSuccess, onError } = options
  uploadFile.value = file
  try {
    const formData = new FormData()
    formData.append('file', file)
    const response = await fetch(props.uploadUrl, {
      method: 'POST',
      headers: {
        Authorization: 'Bearer ' + getToken()
      },
      body: formData
    })
    const result = await response.json()
    if (result.code === 200) {
      onSuccess(result)
      importResult.value = result.data
      message.success(t('common.import.success'))
      emit('success')
    } else {
      onError(result)
      message.error(result.msg || t('common.import.failed'))
    }
  } catch (error) {
    onError(error)
    message.error(t('common.import.failed'))
  }
}

// 上传状态改变
const handleChange = (info: UploadChangeParam) => {
  if (info.file.status === 'uploading') {
    loading.value = true
  } else {
    loading.value = false
  }
}

// 下载模板
const handleDownloadTemplate = () => {
  if (!props.templateName) return
  const link = document.createElement('a')
  link.href = `/api/common/template/${props.templateName}`
  link.download = props.templateName
  document.body.appendChild(link)
  link.click()
  document.body.removeChild(link)
}

// 提交
const handleSubmit = () => {
  if (!uploadFile.value) {
    message.warning(t('common.import.select'))
    return
  }
  // 触发自定义上传
  handleCustomRequest({
    file: uploadFile.value,
    onSuccess: () => {},
    onError: () => {}
  })
}
</script>

<style lang="less" scoped>
.ant-upload-list {
  margin-top: 8px;
}
</style>