<template>
  <div class="hbt-image-upload">
    <div class="hbt-upload-list">
      <a-upload
        ref="uploadRef"
        list-type="picture-card"
        :action="uploadUrl"
        :before-upload="handleBeforeUpload"
        :custom-request="customRequest"
        :show-upload-list="true"
        :headers="headers"
        :file-list="fileList"
        @preview="handlePreview"
        @change="handleChange"
      >
        <div v-if="fileList.length < maxCount">
          <plus-outlined />
          <div style="margin-top: 8px">上传图片</div>
        </div>
      </a-upload>
    </div>

    <!-- 图片预览 -->
    <a-modal
      :open="previewVisible"
      :title="previewTitle"
      :footer="null"
      @cancel="handlePreviewCancel"
    >
      <img :src="previewImage" :alt="previewTitle" style="width: 100%" />
    </a-modal>

    <!-- 图片裁剪 -->
    <a-modal
      :open="cropperVisible"
      title="图片裁剪"
      :maskClosable="false"
      :width="800"
      @ok="handleCropperOk"
      @cancel="handleCropperCancel"
    >
      <div class="hbt-cropper-container">
        <div class="hbt-cropper-wrap">
          <cropper-canvas background>
            <cropper-image ref="cropperRef" :src="cropperImage"></cropper-image>
            <cropper-shade hidden></cropper-shade>
            <cropper-handle action="select" plain></cropper-handle>
            <cropper-selection :initial-coverage="0.5" movable resizable>
              <cropper-grid role="grid" bordered covered></cropper-grid>
              <cropper-crosshair centered></cropper-crosshair>
              <cropper-handle action="move" theme-color="rgba(255, 255, 255, 0.35)"></cropper-handle>
              <cropper-handle action="n-resize"></cropper-handle>
              <cropper-handle action="e-resize"></cropper-handle>
              <cropper-handle action="s-resize"></cropper-handle>
              <cropper-handle action="w-resize"></cropper-handle>
              <cropper-handle action="ne-resize"></cropper-handle>
              <cropper-handle action="nw-resize"></cropper-handle>
              <cropper-handle action="se-resize"></cropper-handle>
              <cropper-handle action="sw-resize"></cropper-handle>
            </cropper-selection>
          </cropper-canvas>
        </div>
        <div class="hbt-cropper-toolbar">
          <a-space>
            <a-button @click="rotateLeft">向左旋转</a-button>
            <a-button @click="rotateRight">向右旋转</a-button>
            <a-button @click="zoomIn">放大</a-button>
            <a-button @click="zoomOut">缩小</a-button>
            <a-button @click="resetCropper">重置</a-button>
          </a-space>
          <div class="size-settings">
            <a-space>
              <a-input-number
                v-model:value="cropperWidth"
                :min="10"
                addon-after="px"
                placeholder="宽度"
              />
              <span>x</span>
              <a-input-number
                v-model:value="cropperHeight"
                :min="10"
                addon-after="px"
                placeholder="高度"
              />
              <a-button type="primary" @click="applyCropperSize">应用尺寸</a-button>
            </a-space>
          </div>
        </div>
      </div>
    </a-modal>
    
    <!-- 上传进度 -->
    <a-progress
      v-if="uploadProgress > 0 && uploadProgress < 100"
      :percent="uploadProgress"
      :format="progressFormat"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onBeforeUnmount, nextTick } from 'vue'
import { message } from 'ant-design-vue'
import { PlusOutlined } from '@ant-design/icons-vue'
import {
  CropperCanvas,
  CropperImage,
  CropperShade,
  CropperHandle,
  CropperSelection,
  CropperGrid,
  CropperCrosshair,
} from 'cropperjs'
import type { UploadChangeParam, UploadProps, UploadFile } from 'ant-design-vue'
import type { UploadRequestOption } from 'ant-design-vue/es/vc-upload/interface'
import fileUploader, { ChunkInfo } from '@/utils/upload'
import imageProcessor from '@/utils/image'
import { PropType } from 'vue'
import { createRequestHeaders } from '@/utils/request'

interface FileItem {
  uid: string
  name: string
  status?: string
  url?: string
  thumbUrl?: string
  percent?: number
}

// 配置参数
const props = defineProps({
  uploadUrl: {
    type: String,
    required: true
  },
  // 保存路径配置
  savePath: {
    type: String,
    default: 'uploads/images' // 默认图片保存目录
  },
  // 文件名称
  fileName: {
    type: String,
    default: ''
  },
  // 文件数量限制
  maxCount: {
    type: Number,
    default: 8 // 默认最多8张图片
  },
  // 文件大小限制
  maxSize: {
    type: Number,
    default: 50 // 默认最大50MB
  },
  // 文件类型限制
  accept: {
    type: String,
    default: '.jpg,.jpeg,.png,.gif' // 默认接受的图片类型
  },
  fileTypes: {
    type: Array as PropType<string[]>,
    default: () => ['image/jpeg', 'image/png', 'image/gif'] // 允许的MIME类型
  },
  // 文件名称处理方式
  nameStrategy: {
    type: String as PropType<'original' | 'random' | 'custom'>,
    default: 'random' // 图片默认使用随机文件名
  },
  // 自定义文件名模板
  nameTemplate: {
    type: String,
    default: '{random}{ext}' // 支持 {filename} {ext} {timestamp} {random} 变量
  },
  // 分片上传配置
  chunkSize: {
    type: Number,
    default: 2 * 1024 * 1024 // 默认分块大小2MB
  },
  // 图片压缩配置
  compress: {
    type: Object,
    default: () => ({
      quality: 0.8, // 压缩质量
      maxWidth: 1920, // 最大宽度
      maxHeight: 1080 // 最大高度
    })
  },
  // 默认裁剪配置
  crop: {
    type: Object,
    default: () => ({
      aspect: 16 / 9, // 默认宽高比
      width: 800, // 默认宽度
      height: 450 // 默认高度
    })
  }
})

// 上传状态
const uploadRef = ref()
const fileList = ref<UploadFile[]>([])
const uploadProgress = ref(0)
const currentFile = ref<File | null>(null)
const chunks = ref<ChunkInfo[]>([])
const uploadedChunks = ref(new Set<string>())

// 预览状态
const previewVisible = ref(false)
const previewImage = ref('')
const previewTitle = ref('')

// 裁剪状态
const cropperRef = ref<CropperImage>()
const cropperVisible = ref(false)
const cropperImage = ref('')
const cropperWidth = ref(props.crop.width)
const cropperHeight = ref(props.crop.height)
const cropperFile = ref<File>()

// 计算属性
const headers = computed(() => createRequestHeaders())

// 格式化进度
const progressFormat = (percent?: number) => {
  if (percent === undefined) return ''
  return percent === 100 ? '上传完成' : `${percent}%`
}

// 初始化裁剪器
const initCropper = () => {
  if (!cropperRef.value) return
  cropperVisible.value = true
}

// 裁剪图片
const cropImage = (): Promise<File> => {
  return new Promise((resolve, reject) => {
    if (!cropperRef.value || !cropperFile.value) {
      reject(new Error('裁剪器未初始化'))
      return
    }

    const canvas = cropperRef.value.querySelector('canvas')
    if (canvas) {
      canvas.toBlob((blob: Blob | null) => {
        if (blob) {
          const file = new File([blob], cropperFile.value!.name, {
            type: cropperFile.value!.type,
            lastModified: Date.now()
          })
          resolve(file)
        } else {
          reject(new Error('图片裁剪失败'))
        }
      }, cropperFile.value.type, 1)
    } else {
      reject(new Error('无法获取裁剪画布'))
    }
  })
}

// 裁剪器操作
const rotateLeft = () => {
  const selection = cropperRef.value?.querySelector('cropper-selection')
  selection?.setAttribute('rotate', '-90')
}
const rotateRight = () => {
  const selection = cropperRef.value?.querySelector('cropper-selection')
  selection?.setAttribute('rotate', '90')
}
const zoomIn = () => {
  const selection = cropperRef.value?.querySelector('cropper-selection')
  selection?.setAttribute('scale', '1.1')
}
const zoomOut = () => {
  const selection = cropperRef.value?.querySelector('cropper-selection')
  selection?.setAttribute('scale', '0.9')
}
const resetCropper = () => {
  const selection = cropperRef.value?.querySelector('cropper-selection')
  if (selection) {
    selection.removeAttribute('rotate')
    selection.removeAttribute('scale')
  }
}

const applyCropperSize = () => {
  const selection = cropperRef.value?.querySelector?.('cropper-selection') as HTMLElement | null
  if (selection) {
    selection.style.width = `${cropperWidth.value}px`
    selection.style.height = `${cropperHeight.value}px`
  }
}

// 处理预览
const handlePreview = async (file: UploadFile) => {
  if (!file.url && !file.preview) {
    file.preview = await getBase64(file.originFileObj as File)
  }
  previewImage.value = file.url || file.preview || ''
  previewVisible.value = true
  previewTitle.value = file.name || ''
}

const handlePreviewCancel = () => {
  previewVisible.value = false
  previewTitle.value = ''
}

// 处理裁剪
const handleCropperOk = async () => {
  try {
    const croppedFile = await cropImage()
    const compressedFile = await imageProcessor.compress(croppedFile, props.compress)
    await uploadFile(compressedFile)
    cropperVisible.value = false
  } catch (error) {
    message.error('图片处理失败')
  }
}

const handleCropperCancel = () => {
  cropperVisible.value = false
  const selection = cropperRef.value?.querySelector('cropper-selection')
  if (selection) {
    selection.removeAttribute('rotate')
    selection.removeAttribute('scale')
  }
}

// 生成文件名
const generateFileName = (file: File): string => {
  const ext = file.name.substring(file.name.lastIndexOf('.'))
  const filename = props.fileName || file.name.substring(0, file.name.lastIndexOf('.'))
  const timestamp = Date.now()
  const random = Math.random().toString(36).substring(2, 8)

  switch (props.nameStrategy) {
    case 'original':
      return file.name
    case 'random':
      return `${random}${ext}`
    case 'custom':
      return props.nameTemplate
        .replace('{filename}', filename)
        .replace('{ext}', ext)
        .replace('{timestamp}', timestamp.toString())
        .replace('{random}', random)
    default:
      return file.name
  }
}

// 上传前处理
const handleBeforeUpload = async (file: File) => {
  // 检查文件大小
  if (file.size > props.maxSize * 1024 * 1024) {
    message.error(`文件大小不能超过${props.maxSize}MB`)
    return false
  }

  // 检查文件类型
  if (!props.fileTypes.includes(file.type)) {
    message.error(`不支持的图片格式，请上传${props.accept}格式的图片`)
    return false
  }

  // 检查数量限制
  if (fileList.value.length >= props.maxCount) {
    message.error(`最多只能上传${props.maxCount}张图片`)
    return false
  }

  // 显示裁剪对话框
  cropperFile.value = file
  cropperImage.value = await getBase64(file)
  cropperVisible.value = true
  await nextTick()
  initCropper()

  return false // 阻止默认上传
}

// 自定义上传
const customRequest = async (options: UploadRequestOption) => {
  const { file, onProgress, onSuccess, onError } = options
  try {
    await uploadFile(file as File)
    onSuccess?.({})
  } catch (error) {
    onError?.(error as Error)
  }
}

// 上传文件
const uploadFile = async (file: File) => {
  try {
    currentFile.value = file
    const fileMd5 = await fileUploader.calculateFileMD5(file)
    const fileName = generateFileName(file)
    const fileChunks = fileUploader.createFileChunks(file)
    chunks.value = fileChunks.map(chunk => ({
      ...chunk,
      hash: `${fileMd5}-${chunk.index}`
    }))

    // 检查已上传的块
    const response = await fetch(`${props.uploadUrl}/check`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...headers.value
      },
      body: JSON.stringify({
        filename: fileName,
        fileMd5,
        savePath: props.savePath
      })
    })

    const { uploaded } = await response.json()
    uploadedChunks.value = new Set(uploaded)

    // 开始上传未完成的块
    await fileUploader.uploadChunks(
      chunks.value,
      props.uploadUrl,
      headers.value,
      (progress) => {
        uploadProgress.value = progress
      },
      uploadedChunks.value
    )

    // 所有分块上传完成，通知服务器合并
    if (uploadedChunks.value.size === chunks.value.length) {
      const mergeResponse = await fileUploader.mergeChunks(
        props.uploadUrl,
        headers.value,
        {
          filename: fileName,
          size: file.size,
          chunks: chunks.value.length
        }
      )

      const result = await mergeResponse.json()
      if (result.code === 200) {
        message.success('上传成功')
        emit('success', result)
      } else {
        throw new Error('文件合并失败')
      }
    }
  } catch (error) {
    message.error('上传失败')
    emit('error', error)
  }
}

// 生命周期
onBeforeUnmount(() => {
  if (cropperRef.value) {
    cropperRef.value.remove()
  }
})

// 工具函数
const getBase64 = imageProcessor.getBase64

// 处理上传状态改变
const handleChange = (info: UploadChangeParam) => {
  const file = info.file
  fileList.value = info.fileList

  if (file.status === 'done') {
    uploadProgress.value = 100
  } else if (file.status === 'error') {
    uploadProgress.value = 0
  }
}

// 定义事件
const emit = defineEmits<{
  (e: 'success', result: any): void
  (e: 'error', error: any): void
}>()

// 注册自定义元素
if (!customElements.get('cropper-canvas')) {
  customElements.define('cropper-canvas', CropperCanvas)
  customElements.define('cropper-image', CropperImage)
  customElements.define('cropper-shade', CropperShade)
  customElements.define('cropper-handle', CropperHandle)
  customElements.define('cropper-selection', CropperSelection)
  customElements.define('cropper-grid', CropperGrid)
  customElements.define('cropper-crosshair', CropperCrosshair)
}

// 生成日期路径
const getDatePath = () => {
  const date = new Date()
  const year = date.getFullYear()
  const month = String(date.getMonth() + 1).padStart(2, '0')
  const day = String(date.getDate()).padStart(2, '0')
  return `${year}${month}${day}`
}

// 获取完整保存路径
const getSavePath = computed(() => {
  return `${props.savePath}/${getDatePath()}`
})
</script>

<style lang="less" scoped>
.hbt-image-upload {
  .hbt-upload-list {
    :deep(.ant-upload-list-picture-card) {
      .ant-upload-list-item-info {
        &::before {
          left: 0;
        }
      }
    }
  }

  .hbt-cropper-container {
    .hbt-cropper-wrap {
      height: 400px;
      background-color: #f0f0f0;
    }

    .hbt-cropper-toolbar {
      margin-top: 16px;
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
  }

  .hbt-upload-tip {
    margin-top: 8px;
    color: rgba(0, 0, 0, 0.45);
  }
}
</style> 