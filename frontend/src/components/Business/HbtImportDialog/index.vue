//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : ImportDialog/index.vue
// 创建者 : Claude
// 创建时间: 2024-03-27
// 版本号 : v1.0.0
// 描述    : 通用导入对话框组件
//===================================================================

<template>
  <a-modal
    :title="t('common.import.title')"
    :open="open"
    :confirm-loading="loading"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-form :label-col="{ span: 4 }" :wrapper-col="{ span: 19 }">
      <!-- 文件上传 -->
      <a-form-item :label="t('common.import.file')">
        <a-upload
          :accept="accept"
          :show-upload-list="true"
          :before-upload="handleBeforeUpload"
          :customRequest="handleCustomRequest"
          :name="'file'"
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
      <a-form-item :label="t('common.import.template')" v-if="templateFileName">
        <a @click="handleDownloadTemplate">
          <download-outlined />
          {{ t('common.import.download') }}
        </a>
      </a-form-item>

      <!-- 导入说明 -->
      <a-form-item :label="t('common.import.note')" v-if="tips || sheetName">
        <a-alert
          v-if="tips"
          :message="tips"
          type="info"
          show-icon
        />
        <div class="mt-2" v-if="sheetName">
          <a-alert
            :message="`请确保 Excel 文件的 sheet 名称为：${sheetName}`"
            type="warning"
            show-icon
          />
        </div>
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
import type { UploadChangeParam } from 'ant-design-vue'
import request from '@/utils/request'

interface ImportResult {
  total: number
  success: number
  failed: number
  message?: string
}

interface Props {
  open: boolean
  /** 上传方法 */
  uploadMethod: (file: File, onUploadProgress?: (progressEvent: any) => void) => Promise<any>
  /** 模板下载方法 */
  templateMethod: () => Promise<Blob>
  /** 模板文件名 */
  templateFileName?: string
  /** 接受的文件类型 */
  accept?: string
  /** Excel工作表名称 */
  sheetName?: string
  /** 文件大小限制(MB) */
  maxSize?: number
  /** 导入说明 */
  tips?: string
  /** 是否自动关闭 */
  autoClose?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  accept: '.xlsx,.xls',
  maxSize: 2,
  autoClose: true
})

const emit = defineEmits<{
  (e: 'update:open', open: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 加载状态
const loading = ref(false)

// 导入结果
const importResult = ref<ImportResult>()

// 上传文件
const uploadFile = ref<File>()

// 处理对话框显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:open', val)
  if (!val) {
    uploadFile.value = undefined
    importResult.value = undefined
  }
}

// 上传状态改变
const handleChange = (info: UploadChangeParam) => {
  if (info.file.status === 'uploading') {
    loading.value = true
  } else if (info.file.status === 'done') {
    loading.value = false
    const response = info.file.response
    if (response.code === 200) {
      importResult.value = {
        total: (response.data.success || 0) + (response.data.fail || 0),
        success: response.data.success || 0,
        failed: response.data.fail || 0,
        message: response.msg
      }
      message.success(t('common.import.success'))
      emit('success')
      if (props.autoClose) {
        handleVisibleChange(false)
      }
    } else {
      message.error(response.msg || t('common.import.failed'))
    }
  } else if (info.file.status === 'error') {
    loading.value = false
    message.error(info.file.response?.msg || t('common.import.failed'))
  }
}

// 自定义上传请求
const handleCustomRequest = async (options: any) => {
  const { file, onProgress, onSuccess, onError } = options
  
  try {
    // 设置上传进度
    const onUploadProgress = (progressEvent: any) => {
      if (progressEvent.total) {
        const percent = Math.round((progressEvent.loaded * 100) / progressEvent.total)
        onProgress({ percent })
      }
    }

    console.log('[上传文件] 开始上传:', {
      name: file.name,
      size: file.size,
      type: file.type
    })

    // 使用props传入的uploadMethod处理文件上传
    const response = await props.uploadMethod(file, onUploadProgress)
    
    console.log('[上传文件] 上传成功:', response)
    
    // 处理响应
    if (response?.code === 200) {
      onSuccess(response)
    } else {
      onError(new Error(response?.msg || t('common.import.failed')))
    }
  } catch (error: any) {
    console.error('[上传文件] 上传失败:', error)
    onError(error)
  }
}

// 上传前校验
const handleBeforeUpload = (file: File) => {
  // 检查文件对象和内容
  if (!file || file.size === 0) {
    message.error(t('common.import.empty'))
    return false
  }

  // 检查文件类型
  const isExcel = file.type === 'application/vnd.ms-excel' || 
    file.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
  if (!isExcel) {
    message.error(t('common.import.format'))
    return false
  }

  // 检查文件大小
  const isLtMaxSize = file.size / 1024 / 1024 < props.maxSize
  if (!isLtMaxSize) {
    message.error(t('common.import.size', { size: props.maxSize }))
    return false
  }

  return true
}

// 下载模板
const handleDownloadTemplate = async () => {
  try {
    loading.value = true
    const blob = await props.templateMethod()
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = props.templateFileName || `导入模板_${new Date().getTime()}.xlsx`
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
    window.URL.revokeObjectURL(url)
    message.success(t('common.message.downloadSuccess'))
  } catch (error) {
    console.error('下载模板失败:', error)
    message.error(t('common.message.downloadFailed'))
  } finally {
    loading.value = false
  }
}

// 提交
const handleSubmit = async () => {
  if (!uploadFile.value) {
    message.warning(t('common.import.select'))
    return
  }
  
  try {
    loading.value = true
    console.log('开始提交文件:', {
      name: uploadFile.value.name,
      size: uploadFile.value.size,
      type: uploadFile.value.type
    })
    
    // 使用props传入的uploadMethod处理文件上传
    const response = await props.uploadMethod(uploadFile.value)
    console.log('提交成功:', response)
    
    // 处理响应
    if (response.code === 200) {
      importResult.value = {
        total: (response.data.success || 0) + (response.data.fail || 0),
        success: response.data.success || 0,
        failed: response.data.fail || 0,
        message: response.msg
      }
      message.success(t('common.import.success'))
      emit('success')
      if (props.autoClose) {
        handleVisibleChange(false)
      }
    } else {
      message.error(response.msg || t('common.import.failed'))
    }
  } catch (error: any) {
    console.error('提交失败:', error)
    message.error(error.response?.data?.msg || error.message || t('common.import.failed'))
  } finally {
    loading.value = false
  }
}
</script>

<style lang="less" scoped>
:deep(.ant-upload-list) {
  max-height: 300px;
  overflow-y: auto;
}

:deep(.ant-descriptions-item-label) {
  width: 100px;
}
</style> 