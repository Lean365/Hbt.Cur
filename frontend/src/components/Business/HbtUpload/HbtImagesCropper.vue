<template>
  <div class="hbt-images-cropper">
    <a-modal
      :open="visible"
      @update:open="(val) => emit('update:visible', val)"
      :title="title"
      :maskClosable="false"
      :width="800"
      :footer="null"
      @cancel="handleCancel"
    >
      <div class="cropper-container">
        <!-- 约束选择 -->
        <form class="constraint-form">
          <fieldset>
            <legend>选区约束：</legend>
            <input
              id="inputWithinCanvas"
              v-model="within"
              type="radio"
              name="within"
              value="canvas"
            >
            <label for="inputWithinCanvas">画布范围</label>
            <input
              id="inputWithinImage"
              v-model="within"
              type="radio"
              name="within"
              value="image"
            >
            <label for="inputWithinImage">图片范围</label>
            <input
              id="inputWithinNone"
              v-model="within"
              type="radio"
              name="within"
              value="none"
            >
            <label for="inputWithinNone">无约束</label>
          </fieldset>
        </form>

        <!-- 裁剪区域 -->
        <div class="cropper-wrap" ref="containerRef">
          <cropper-canvas
            ref="cropperCanvas"
            background
            :scale-step="0.1"
            @actionstart="onActionStart"
            @actionmove="onActionMove"
            @actionend="onActionEnd"
            @action="onAction"
          >
            <cropper-image
              ref="cropperImage"
              :src="imageUrl"
              :alt="title"
              crossorigin="anonymous"
              :translatable="movable"
              :rotatable="rotatable"
              :scalable="scalable"
              @ready="onImageReady"
            />
            <cropper-selection
              ref="cropperSelection"
              :aspect-ratio="aspectRatio"
              :initial-aspect-ratio="initialAspectRatio"
              :initial-coverage="initialCoverage"
              :movable="cropBoxMovable"
              :resizable="cropBoxResizable"
              :min-width="minCropBoxWidth"
              :min-height="minCropBoxHeight"
            >
              <cropper-grid></cropper-grid>
              <cropper-crosshair></cropper-crosshair>
              <cropper-handle action="move"></cropper-handle>
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

        <!-- 工具栏 -->
        <div class="cropper-toolbar">
          <a-space>
            <a-button-group>
              <a-button @click="() => handleRotate(-15)">
                <template #icon><RotateLeftOutlined /></template>
                向左旋转
              </a-button>
              <a-button @click="() => handleRotate(15)">
                <template #icon><RotateRightOutlined /></template>
                向右旋转
              </a-button>
            </a-button-group>

            <a-button-group>
              <a-button @click="() => handleScale(1.1)">
                <template #icon><ZoomInOutlined /></template>
                放大
              </a-button>
              <a-button @click="() => handleScale(0.9)">
                <template #icon><ZoomOutOutlined /></template>
                缩小
              </a-button>
            </a-button-group>

            <a-button @click="handleReset">
              <template #icon><UndoOutlined /></template>
              重置
            </a-button>
          </a-space>
        </div>

        <!-- 底部按钮 -->
        <div class="cropper-footer">
          <a-space>
            <a-button @click="handleCancel">取消</a-button>
            <a-button type="primary" @click="handleConfirm">确定</a-button>
          </a-space>
        </div>
      </div>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, nextTick, onMounted, onBeforeUnmount, watch } from 'vue'
import { message } from 'ant-design-vue'
import {
  RotateLeftOutlined,
  RotateRightOutlined,
  ZoomInOutlined,
  ZoomOutOutlined,
  UndoOutlined
} from '@ant-design/icons-vue'
import {
  CropperCanvas,
  CropperImage,
  CropperSelection,
  CropperGrid,
  CropperCrosshair,
  CropperHandle
} from 'cropperjs'

// 定义组件属性
const props = defineProps<{
  visible: boolean
  title: string
  imageUrl: string
}>()

// 定义组件事件
const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success', result: { blob: Blob; data: any; dataUrl: string }): void
  (e: 'error', error: any): void
  (e: 'cancel'): void
}>()

// 定义响应式变量
const within = ref<'canvas' | 'image' | 'none'>('none')
const containerRef = ref<HTMLElement | null>(null)
const cropperCanvas = ref<CropperCanvas | null>(null)
const cropperImage = ref<CropperImage | null>(null)
const cropperSelection = ref<CropperSelection | null>(null)

// Cropper 2.0 相关参数
const aspectRatio = ref(1)
const initialAspectRatio = ref(1)
const initialCoverage = ref(1)
const cropBoxMovable = ref(true)
const cropBoxResizable = ref(true)
const minCropBoxWidth = ref(200)
const minCropBoxHeight = ref(200)
const movable = ref(true)
const rotatable = ref(true)
const scalable = ref(true)

// 事件
const onImageReady = () => {}
const onActionStart = (e: any) => {}
const onActionMove = (e: any) => {}
const onActionEnd = (e: any) => {}
const onAction = (e: any) => {}

// 旋转
const handleRotate = (degree: number) => {
  cropperImage.value?.$rotate(degree)
}

// 缩放
const handleScale = (ratio: number) => {
  cropperImage.value?.$scale(ratio)
}

// 重置
const handleReset = () => {
  cropperImage.value?.$resetTransform()
  cropperSelection.value?.$reset()
}

// 确认裁剪
const handleConfirm = async () => {
  const canvas = await cropperSelection.value?.$toCanvas()
  if (canvas) {
    canvas.toBlob((blob) => {
      if (blob) {
        emit('success', {
          blob,
          data: null,
          dataUrl: canvas.toDataURL()
        })
        emit('update:visible', false)
      }
    })
  }
}

// 取消
const handleCancel = () => {
  emit('cancel')
  emit('update:visible', false)
}

// 监听visible变化
watch(() => props.visible, (newVal) => {
  if (newVal) {
    nextTick(() => {})
  }
}, { immediate: true })

// 监听imageUrl变化
watch(() => props.imageUrl, (newUrl) => {
  if (!newUrl) {
    message.error('请先选择图片')
    return
  }
  if (props.visible) {
    nextTick(() => {})
  }
}, { immediate: true })

// 组件挂载时初始化
onMounted(() => {})

// 组件卸载时清理
onBeforeUnmount(() => {})
</script>

<style scoped>
.hbt-images-cropper {
  position: relative;
}

.cropper-container {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.cropper-wrap {
  position: relative;
  width: 100%;
  height: 400px;
  overflow: hidden;
}

.cropper-toolbar {
  display: flex;
  justify-content: center;
  gap: 8px;
}

.cropper-footer {
  display: flex;
  justify-content: flex-end;
  gap: 8px;
  margin-top: 16px;
}

:deep(.cropper-image) {
  max-width: 100%;
  max-height: 100%;
  object-fit: contain;
}

:deep(.cropper-canvas) {
  background-color: #f0f0f0;
}
</style>