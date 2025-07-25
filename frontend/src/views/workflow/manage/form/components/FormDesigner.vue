<template>
  <div class="form-designer-container">
    <!-- 顶部工具栏 -->
    <div class="designer-toolbar">
      <div class="toolbar-left">
        <a-button type="primary" @click="handleSave">
          <template #icon><SaveOutlined /></template>
          保存表单
        </a-button>
        <a-button @click="handlePreview">
          <template #icon><EyeOutlined /></template>
          预览表单
        </a-button>
        <a-button @click="handlePublish" :disabled="!canPublish">
          <template #icon><SendOutlined /></template>
          发布表单
        </a-button>
        <a-button @click="handleTest">
          <template #icon><ExperimentOutlined /></template>
          测试表单
        </a-button>
      </div>
      <div class="toolbar-right">
        <a-button @click="handleUndo" :disabled="!canUndo">
          <template #icon><UndoOutlined /></template>
          撤销
        </a-button>
        <a-button @click="handleRedo" :disabled="!canRedo">
          <template #icon><RedoOutlined /></template>
          重做
        </a-button>
        <a-button @click="handleClear">
          <template #icon><ClearOutlined /></template>
          清空
        </a-button>
        <a-button @click="handleImport">
          <template #icon><ImportOutlined /></template>
          导入
        </a-button>
        <a-button @click="handleExport">
          <template #icon><ExportOutlined /></template>
          导出
        </a-button>
        <a-button @click="handleTemplate">
          <template #icon><FileTextOutlined /></template>
          模板
        </a-button>
      </div>
    </div>

    <!-- 主要内容区域 -->
    <div class="designer-content">
      <!-- 左侧组件面板 -->
      <div class="component-panel">
        <div class="panel-header">
          <h3>表单组件</h3>
        </div>
        <div class="component-list">
          <div
            v-for="component in formComponents"
            :key="component.type"
            class="component-item"
            draggable="true"
            @dragstart="handleComponentDragStart(component)"
          >
            <div class="component-icon">
              <component :is="component.icon" />
            </div>
            <div class="component-label">{{ component.label }}</div>
          </div>
        </div>
        
        <!-- 字典组件 -->
        <div class="panel-header">
          <h3>字典组件</h3>
        </div>
        <div class="component-list">
          <div
            v-for="dict in dictComponents"
            :key="dict.type"
            class="component-item"
            draggable="true"
            @dragstart="handleDictComponentDragStart(dict)"
          >
            <div class="component-icon">
              <component :is="dict.icon" />
            </div>
            <div class="component-label">{{ dict.label }}</div>
          </div>
        </div>
      </div>

      <!-- 中间设计器区域 -->
      <div class="designer-main">
        <div class="designer-header">
          <div class="form-info">
            <a-input
              v-model:value="formName"
              placeholder="请输入表单名称"
              class="form-name-input"
            />
            <hbt-select
              v-model:value="formCategory"
              dict-type="workflow_form_category"
              placeholder="请选择表单分类"
              style="width: 150px"
            />
            <hbt-select
              v-model:value="formType"
              dict-type="workflow_form_type"
              placeholder="请选择表单类型"
              style="width: 150px"
            />
            <a-input
              v-model:value="formVersion"
              placeholder="版本号"
              style="width: 100px"
            />
          </div>
          <div class="form-actions">
            <a-button @click="handleFormSettings">
              <template #icon><SettingOutlined /></template>
              表单设置
            </a-button>
          </div>
        </div>
        
        <!-- 表单设计器 -->
        <div class="form-container">
          <hbt-form
            ref="formRef"
            v-model:value="formConfig"
            :readonly="false"
            :height="'600px'"
            @save="handleFormSave"
            @field-select="handleFieldSelect"
          />
        </div>
      </div>

      <!-- 右侧属性面板 -->
      <div class="property-panel">
        <div class="panel-header">
          <h3>属性配置</h3>
        </div>
        <div class="property-content">
          <!-- 表单属性 -->
          <div v-if="!selectedField" class="form-property">
            <h4>表单属性</h4>
            <a-form :model="formSettings" layout="vertical">
              <a-form-item label="表单名称">
                <a-input v-model:value="formSettings.formName" placeholder="请输入表单名称" />
              </a-form-item>
              <a-form-item label="表单键">
                <a-input v-model:value="formSettings.formKey" placeholder="请输入表单键" />
              </a-form-item>
              <a-form-item label="表单分类">
                <hbt-select
                  v-model:value="formSettings.formCategory"
                  dict-type="workflow_form_category"
                  placeholder="请选择表单分类"
                />
              </a-form-item>
              <a-form-item label="表单类型">
                <hbt-select
                  v-model:value="formSettings.formType"
                  dict-type="workflow_form_type"
                  placeholder="请选择表单类型"
                />
              </a-form-item>
              <a-form-item label="表单版本">
                <a-input v-model:value="formSettings.version" placeholder="请输入版本号" />
              </a-form-item>
              <a-form-item label="表单描述">
                <a-textarea v-model:value="formSettings.description" :rows="3" placeholder="请输入表单描述" />
              </a-form-item>
              <a-form-item label="表单布局">
                <a-select v-model:value="formSettings.layout" placeholder="请选择布局">
                  <a-select-option value="vertical">垂直布局</a-select-option>
                  <a-select-option value="horizontal">水平布局</a-select-option>
                  <a-select-option value="inline">行内布局</a-select-option>
                </a-select>
              </a-form-item>
              <a-form-item label="标签宽度">
                <a-input-number v-model:value="formSettings.labelWidth" :min="0" :max="300" style="width: 100%" />
              </a-form-item>
              <a-form-item label="是否显示标签">
                <a-switch v-model:checked="formSettings.showLabel" />
              </a-form-item>
              <a-form-item label="是否显示必填星号">
                <a-switch v-model:checked="formSettings.showRequiredMark" />
              </a-form-item>
              <a-form-item label="提交按钮文本">
                <a-input v-model:value="formSettings.submitText" placeholder="请输入提交按钮文本" />
              </a-form-item>
              <a-form-item label="重置按钮文本">
                <a-input v-model:value="formSettings.resetText" placeholder="请输入重置按钮文本" />
              </a-form-item>
            </a-form>
          </div>

          <!-- 字段属性 -->
          <div v-else class="field-property">
            <h4>字段属性</h4>
            <a-form :model="selectedField" layout="vertical">
              <a-form-item label="字段名称">
                <a-input v-model:value="selectedField.field" placeholder="请输入字段名称" />
              </a-form-item>
              <a-form-item label="字段标签">
                <a-input v-model:value="selectedField.title" placeholder="请输入字段标签" />
              </a-form-item>
              <a-form-item label="字段类型">
                <a-select v-model:value="selectedField.type" disabled>
                  <a-select-option value="input">输入框</a-select-option>
                  <a-select-option value="select">下拉选择</a-select-option>
                  <a-select-option value="radio">单选框</a-select-option>
                  <a-select-option value="checkbox">复选框</a-select-option>
                  <a-select-option value="textarea">文本域</a-select-option>
                  <a-select-option value="date">日期选择</a-select-option>
                  <a-select-option value="datetime">日期时间</a-select-option>
                  <a-select-option value="time">时间选择</a-select-option>
                  <a-select-option value="number">数字输入</a-select-option>
                  <a-select-option value="switch">开关</a-select-option>
                  <a-select-option value="slider">滑块</a-select-option>
                  <a-select-option value="rate">评分</a-select-option>
                  <a-select-option value="upload">文件上传</a-select-option>
                  <a-select-option value="cascader">级联选择</a-select-option>
                  <a-select-option value="tree">树形选择</a-select-option>
                  <a-select-option value="dict">字典选择</a-select-option>
                </a-select>
              </a-form-item>
              <a-form-item label="是否必填">
                <a-switch v-model:checked="selectedField.required" />
              </a-form-item>
              <a-form-item label="是否禁用">
                <a-switch v-model:checked="selectedField.disabled" />
              </a-form-item>
              <a-form-item label="是否只读">
                <a-switch v-model:checked="selectedField.readonly" />
              </a-form-item>
              <a-form-item label="占位符">
                <a-input v-model:value="selectedField.placeholder" placeholder="请输入占位符" />
              </a-form-item>
              <a-form-item label="默认值">
                <a-input v-model:value="selectedField.default" placeholder="请输入默认值" />
              </a-form-item>
              <a-form-item label="字段宽度">
                <a-input-number v-model:value="selectedField.width" :min="0" :max="100" style="width: 100%" />
              </a-form-item>
              
              <!-- 字典字段特有属性 -->
              <template v-if="selectedField.type === 'dict'">
                <a-divider>字典配置</a-divider>
                <a-form-item label="字典类型">
                  <a-input
                    v-model:value="selectedField.dictType"
                    placeholder="请输入字典类型，如：sys_normal_disable"
                  />
                </a-form-item>
                <a-form-item label="是否多选">
                  <a-switch v-model:checked="selectedField.multiple" />
                </a-form-item>
                <a-form-item label="是否可搜索">
                  <a-switch v-model:checked="selectedField.searchable" />
                </a-form-item>
              </template>

              <!-- 选择器字段特有属性 -->
              <template v-if="['select', 'radio', 'checkbox'].includes(selectedField.type)">
                <a-divider>选项配置</a-divider>
                <a-form-item label="选项数据">
                  <a-textarea
                    v-model:value="selectedField.options"
                    :rows="4"
                    placeholder="请输入选项数据，格式：label1:value1,label2:value2"
                  />
                </a-form-item>
                <a-form-item label="是否多选">
                  <a-switch v-model:checked="selectedField.multiple" />
                </a-form-item>
                <a-form-item label="是否可搜索">
                  <a-switch v-model:checked="selectedField.searchable" />
                </a-form-item>
              </template>

              <!-- 数字字段特有属性 -->
              <template v-if="selectedField.type === 'number'">
                <a-divider>数字配置</a-divider>
                <a-form-item label="最小值">
                  <a-input-number v-model:value="selectedField.min" style="width: 100%" />
                </a-form-item>
                <a-form-item label="最大值">
                  <a-input-number v-model:value="selectedField.max" style="width: 100%" />
                </a-form-item>
                <a-form-item label="步长">
                  <a-input-number v-model:value="selectedField.step" :min="0" style="width: 100%" />
                </a-form-item>
                <a-form-item label="精度">
                  <a-input-number v-model:value="selectedField.precision" :min="0" :max="10" style="width: 100%" />
                </a-form-item>
              </template>

              <!-- 文本域特有属性 -->
              <template v-if="selectedField.type === 'textarea'">
                <a-divider>文本域配置</a-divider>
                <a-form-item label="行数">
                  <a-input-number v-model:value="selectedField.rows" :min="1" :max="20" style="width: 100%" />
                </a-form-item>
                <a-form-item label="最大长度">
                  <a-input-number v-model:value="selectedField.maxLength" :min="0" style="width: 100%" />
                </a-form-item>
                <a-form-item label="是否显示字数统计">
                  <a-switch v-model:checked="selectedField.showCount" />
                </a-form-item>
              </template>

              <!-- 文件上传特有属性 -->
              <template v-if="selectedField.type === 'upload'">
                <a-divider>上传配置</a-divider>
                <a-form-item label="上传类型">
                  <a-select v-model:value="selectedField.uploadType" placeholder="请选择上传类型">
                    <a-select-option value="image">图片</a-select-option>
                    <a-select-option value="file">文件</a-select-option>
                    <a-select-option value="video">视频</a-select-option>
                    <a-select-option value="audio">音频</a-select-option>
                  </a-select>
                </a-form-item>
                <a-form-item label="最大文件数">
                  <a-input-number v-model:value="selectedField.maxCount" :min="1" style="width: 100%" />
                </a-form-item>
                <a-form-item label="文件大小限制(MB)">
                  <a-input-number v-model:value="selectedField.maxSize" :min="0" style="width: 100%" />
                </a-form-item>
                <a-form-item label="允许的文件类型">
                  <a-input v-model:value="selectedField.accept" placeholder="请输入允许的文件类型" />
                </a-form-item>
              </template>

              <a-divider>验证规则</a-divider>
              <a-form-item label="验证规则">
                <a-textarea
                  v-model:value="selectedField.validate"
                  :rows="4"
                  placeholder="请输入验证规则，JSON格式"
                />
              </a-form-item>
              <a-form-item label="自定义验证">
                <a-textarea
                  v-model:value="selectedField.customValidate"
                  :rows="3"
                  placeholder="请输入自定义验证函数"
                />
              </a-form-item>
            </a-form>
          </div>
        </div>
      </div>
    </div>

    <!-- 保存对话框 -->
    <a-modal
      v-model:open="saveModalVisible"
      title="保存表单"
      @ok="handleSaveConfirm"
      @cancel="saveModalVisible = false"
    >
      <a-form :model="saveForm" layout="vertical">
        <a-form-item label="表单名称" required>
          <a-input v-model:value="saveForm.formName" placeholder="请输入表单名称" />
        </a-form-item>
        <a-form-item label="表单键" required>
          <a-input v-model:value="saveForm.formKey" placeholder="请输入表单键" />
        </a-form-item>
        <a-form-item label="表单分类" required>
          <hbt-select
            v-model:value="saveForm.formCategory"
            dict-type="workflow_form_category"
            placeholder="请选择表单分类"
          />
        </a-form-item>
        <a-form-item label="表单类型" required>
          <hbt-select
            v-model:value="saveForm.formType"
            dict-type="workflow_form_type"
            placeholder="请选择表单类型"
          />
        </a-form-item>
        <a-form-item label="表单版本" required>
          <a-input v-model:value="saveForm.version" placeholder="请输入版本号" />
        </a-form-item>
        <a-form-item label="表单描述">
          <a-textarea v-model:value="saveForm.description" :rows="3" placeholder="请输入表单描述" />
        </a-form-item>
        <a-form-item label="备注">
          <a-textarea v-model:value="saveForm.remark" :rows="2" placeholder="请输入备注" />
        </a-form-item>
      </a-form>
    </a-modal>

    <!-- 预览对话框 -->
    <a-modal
      v-model:open="previewModalVisible"
      title="表单预览"
      width="80%"
      :footer="null"
    >
      <div class="preview-container">
        <hbt-form
          :model-value="formConfig"
          :readonly="true"
          :height="'500px'"
          @submit="handlePreviewSubmit"
        />
      </div>
    </a-modal>

    <!-- 测试对话框 -->
    <a-modal
      v-model:open="testModalVisible"
      title="表单测试"
      width="80%"
      :footer="null"
    >
      <div class="test-container">
        <hbt-form
          :model-value="formConfig"
          :readonly="false"
          :height="'500px'"
          @submit="handleTestSubmit"
        />
      </div>
    </a-modal>

    <!-- 导入对话框 -->
    <a-modal
      v-model:open="importModalVisible"
      title="导入表单"
      @ok="handleImportConfirm"
      @cancel="importModalVisible = false"
    >
      <a-upload
        v-model:file-list="importFileList"
        :before-upload="beforeImportUpload"
        :max-count="1"
        accept=".json"
      >
        <a-button>
          <template #icon><UploadOutlined /></template>
          选择文件
        </a-button>
      </a-upload>
      <div class="upload-tip">
        <p>支持导入JSON格式的表单配置文件</p>
      </div>
    </a-modal>

    <!-- 模板对话框 -->
    <a-modal
      v-model:open="templateModalVisible"
      title="表单模板"
      width="60%"
      :footer="null"
    >
      <div class="template-container">
        <div class="template-list">
          <div
            v-for="template in formTemplates"
            :key="template.key"
            class="template-item"
            @click="selectTemplate(template)"
          >
            <div class="template-icon">
              <component :is="template.icon" />
            </div>
            <div class="template-info">
              <div class="template-name">{{ template.name }}</div>
              <div class="template-desc">{{ template.description }}</div>
            </div>
          </div>
        </div>
      </div>
    </a-modal>

    <!-- 表单设置对话框 -->
    <a-modal
      v-model:open="settingsModalVisible"
      title="表单设置"
      width="600px"
      @ok="handleSettingsConfirm"
      @cancel="settingsModalVisible = false"
    >
      <a-form :model="formSettings" layout="vertical">
        <a-form-item label="表单名称">
          <a-input v-model:value="formSettings.formName" placeholder="请输入表单名称" />
        </a-form-item>
        <a-form-item label="表单键">
          <a-input v-model:value="formSettings.formKey" placeholder="请输入表单键" />
        </a-form-item>
        <a-form-item label="表单分类">
          <hbt-select
            v-model:value="formSettings.formCategory"
            dict-type="workflow_form_category"
            placeholder="请选择表单分类"
          />
        </a-form-item>
        <a-form-item label="表单类型">
          <hbt-select
            v-model:value="formSettings.formType"
            dict-type="workflow_form_type"
            placeholder="请选择表单类型"
          />
        </a-form-item>
        <a-form-item label="表单版本">
          <a-input v-model:value="formSettings.version" placeholder="请输入版本号" />
        </a-form-item>
        <a-form-item label="表单描述">
          <a-textarea v-model:value="formSettings.description" :rows="3" placeholder="请输入表单描述" />
        </a-form-item>
        <a-form-item label="表单布局">
          <a-select v-model:value="formSettings.layout" placeholder="请选择布局">
            <a-select-option value="vertical">垂直布局</a-select-option>
            <a-select-option value="horizontal">水平布局</a-select-option>
            <a-select-option value="inline">行内布局</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="标签宽度">
          <a-input-number v-model:value="formSettings.labelWidth" :min="0" :max="300" style="width: 100%" />
        </a-form-item>
        <a-form-item label="是否显示标签">
          <a-switch v-model:checked="formSettings.showLabel" />
        </a-form-item>
        <a-form-item label="是否显示必填星号">
          <a-switch v-model:checked="formSettings.showRequiredMark" />
        </a-form-item>
        <a-form-item label="提交按钮文本">
          <a-input v-model:value="formSettings.submitText" placeholder="请输入提交按钮文本" />
        </a-form-item>
        <a-form-item label="重置按钮文本">
          <a-input v-model:value="formSettings.resetText" placeholder="请输入重置按钮文本" />
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted, watch } from 'vue'
import { message } from 'ant-design-vue'
import {
  SaveOutlined,
  EyeOutlined,
  SendOutlined,
  UndoOutlined,
  RedoOutlined,
  ClearOutlined,
  ImportOutlined,
  ExportOutlined,
  FormOutlined,
  MinusOutlined,
  DownOutlined,
  CheckSquareOutlined,
  FileTextOutlined,
  CalendarOutlined,
  NumberOutlined,
  ExperimentOutlined,
  SettingOutlined,
  UploadOutlined,
  DatabaseOutlined,
  SelectOutlined,
  CheckCircleOutlined,
  ClockCircleOutlined,
  SwitcherOutlined,
  SlidersOutlined,
  StarOutlined,
  CloudUploadOutlined,
  BranchesOutlined,
  ApartmentOutlined
} from '@ant-design/icons-vue'
import { createForm, updateForm } from '@/api/workflow/form'
import { useDictStore } from '@/stores/dict'
import type { HbtFormCreate } from '@/types/workflow/form'

// Props
interface Props {
  formId?: number
}

const props = withDefaults(defineProps<Props>(), {
  formId: undefined
})

// Emits
const emit = defineEmits<{
  'save': [formConfig: string]
  'cancel': []
}>()

// 字典store
const dictStore = useDictStore()

// 定义字段类型
interface FormField {
  field: string
  title: string
  type: string
  required: boolean
  disabled: boolean
  readonly: boolean
  placeholder: string
  default: string
  width: number
  validate: string
  customValidate: string
  // 字典字段特有
  dictType?: string
  multiple?: boolean
  searchable?: boolean
  // 选择器字段特有
  options?: string
  // 数字字段特有
  min?: number
  max?: number
  step?: number
  precision?: number
  // 文本域特有
  rows?: number
  maxLength?: number
  showCount?: boolean
  // 上传字段特有
  uploadType?: string
  maxCount?: number
  maxSize?: number
  accept?: string
}

// 响应式数据
const formRef = ref()
const formName = ref('')
const formCategory = ref(1)
const formType = ref(1)
const formVersion = ref('1.0')
const formConfig = ref('')
const selectedField = ref<FormField | null>(null)
const saveModalVisible = ref(false)
const previewModalVisible = ref(false)
const testModalVisible = ref(false)
const importModalVisible = ref(false)
const templateModalVisible = ref(false)
const settingsModalVisible = ref(false)
const importFileList = ref<any[]>([])

// 字典相关 - 直接使用字典store，不需要额外的字典类型列表

// 保存表单
const saveForm = reactive<HbtFormCreate>({
  formKey: '',
  formName: '',
  formCategory: 1,
  formType: 1,
  version: '1.0',
  formConfig: '',
  description: '',
  remark: ''
})

// 表单设置
const formSettings = reactive({
  formName: '',
  formKey: '',
  formCategory: 1,
  formType: 1,
  version: '1.0',
  description: '',
  layout: 'vertical',
  labelWidth: 100,
  showLabel: true,
  showRequiredMark: true,
  submitText: '提交',
  resetText: '重置'
})

// 表单组件定义
const formComponents = [
  { type: 'input', label: '输入框', icon: MinusOutlined },
  { type: 'select', label: '下拉选择', icon: SelectOutlined },
  { type: 'radio', label: '单选框', icon: CheckCircleOutlined },
  { type: 'checkbox', label: '复选框', icon: CheckSquareOutlined },
  { type: 'textarea', label: '文本域', icon: FileTextOutlined },
  { type: 'date', label: '日期选择', icon: CalendarOutlined },
  { type: 'datetime', label: '日期时间', icon: CalendarOutlined },
  { type: 'time', label: '时间选择', icon: ClockCircleOutlined },
  { type: 'number', label: '数字输入', icon: NumberOutlined },
  { type: 'switch', label: '开关', icon: SwitcherOutlined },
  { type: 'slider', label: '滑块', icon: SlidersOutlined },
  { type: 'rate', label: '评分', icon: StarOutlined },
  { type: 'upload', label: '文件上传', icon: CloudUploadOutlined },
  { type: 'cascader', label: '级联选择', icon: BranchesOutlined },
  { type: 'tree', label: '树形选择', icon: ApartmentOutlined }
]

// 字典组件定义
const dictComponents = [
  { type: 'dict', label: '字典选择', icon: DatabaseOutlined }
]

// 表单模板
const formTemplates = [
  {
    key: 'basic',
    name: '基础表单',
    description: '包含常用字段的基础表单模板',
    icon: FormOutlined,
    config: {
      rule: [
        {
          type: 'input',
          field: 'title',
          title: '标题',
          props: { placeholder: '请输入标题' },
          validate: [{ required: true, message: '请输入标题' }]
        },
        {
          type: 'textarea',
          field: 'description',
          title: '描述',
          props: { placeholder: '请输入描述' }
        }
      ]
    }
  },
  {
    key: 'personnel',
    name: '人事表单',
    description: '适用于人事相关业务',
    icon: FormOutlined,
    config: {
      rule: [
        {
          type: 'input',
          field: 'employeeName',
          title: '员工姓名',
          props: { placeholder: '请输入员工姓名' },
          validate: [{ required: true, message: '请输入员工姓名' }]
        },
        {
          type: 'dict',
          field: 'department',
          title: '部门',
          props: { placeholder: '请选择部门' },
          dictType: 'sys_dept'
        },
        {
          type: 'date',
          field: 'entryDate',
          title: '入职日期',
          props: { placeholder: '请选择入职日期' }
        }
      ]
    }
  },
  {
    key: 'finance',
    name: '财务表单',
    description: '适用于财务相关业务',
    icon: FormOutlined,
    config: {
      rule: [
        {
          type: 'input',
          field: 'expenseTitle',
          title: '费用标题',
          props: { placeholder: '请输入费用标题' },
          validate: [{ required: true, message: '请输入费用标题' }]
        },
        {
          type: 'number',
          field: 'amount',
          title: '金额',
          props: { placeholder: '请输入金额' },
          validate: [{ required: true, message: '请输入金额' }]
        },
        {
          type: 'dict',
          field: 'expenseType',
          title: '费用类型',
          props: { placeholder: '请选择费用类型' },
          dictType: 'expense_type'
        }
      ]
    }
  }
]

// 计算属性
const canPublish = computed(() => {
  return formConfig.value && formName.value
})

const canUndo = computed(() => {
  return formRef.value?.canUndo() || false
})

const canRedo = computed(() => {
  return formRef.value?.canRedo() || false
})

// 方法




const handleComponentDragStart = (component: any) => {
  console.log('拖拽组件:', component)
}

const handleDictComponentDragStart = (dictComponent: any) => {
  console.log('拖拽字典组件:', dictComponent)
}

const handleFormSave = (config: string) => {
  formConfig.value = config
  message.success('表单配置已保存')
}

const handleFieldSelect = (field: FormField) => {
  selectedField.value = field
}

const handleSave = () => {
  if (!formName.value) {
    message.warning('请输入表单名称')
    return
  }
  if (!formConfig.value) {
    message.warning('请先设计表单')
    return
  }
  
  saveForm.formName = formName.value
  saveForm.formCategory = formCategory.value
  saveForm.formType = formType.value
  saveForm.version = formVersion.value
  saveForm.formConfig = formConfig.value
  saveModalVisible.value = true
}

const handleSaveConfirm = async () => {
  try {
    // 如果有表单ID，则更新；否则创建
    let result
    if (props.formId) {
      result = await updateForm({
        ...saveForm,
        formId: props.formId
      })
    } else {
      result = await createForm(saveForm)
    }
    
    if (result.data.code === 200) {
      message.success('表单保存成功')
      saveModalVisible.value = false
      emit('save', saveForm.formConfig)
    } else {
      message.error(result.data.msg || '保存失败')
    }
  } catch (error) {
    console.error('保存失败:', error)
    message.error('保存失败')
  }
}

const handlePreview = () => {
  if (!formConfig.value) {
    message.warning('请先设计表单')
    return
  }
  previewModalVisible.value = true
}

const handlePreviewSubmit = (formData: any) => {
  console.log('预览表单提交:', formData)
  message.success('表单提交成功')
}

const handleTest = () => {
  if (!formConfig.value) {
    message.warning('请先设计表单')
    return
  }
  testModalVisible.value = true
}

const handleTestSubmit = (formData: any) => {
  console.log('测试表单提交:', formData)
  message.success('表单测试成功')
}

const handlePublish = async () => {
  try {
    if (!props.formId) {
      message.error('请先保存表单')
      return
    }
    
    // 更新表单状态为已发布
    const result = await updateForm({
      formId: props.formId,
      status: 1 // 已发布状态
    } as any)
    
    if (result.data.code === 200) {
      message.success('表单发布成功')
    } else {
      message.error(result.data.msg || '发布失败')
    }
  } catch (error) {
    console.error('发布失败:', error)
    message.error('发布失败')
  }
}

const handleUndo = () => {
  formRef.value?.undo()
}

const handleRedo = () => {
  formRef.value?.redo()
}

const handleClear = () => {
  formConfig.value = ''
  selectedField.value = null
  message.success('表单已清空')
}

const handleImport = () => {
  importModalVisible.value = true
}

const beforeImportUpload = (file: File) => {
  const isJson = file.type === 'application/json' || file.name.endsWith('.json')
  if (!isJson) {
    message.error('只能上传JSON文件!')
    return false
  }
  return false
}

const handleImportConfirm = () => {
  if (importFileList.value.length === 0) {
    message.warning('请选择要导入的文件')
    return
  }
  
  const file = importFileList.value[0].originFileObj
  const reader = new FileReader()
  reader.onload = (e) => {
    try {
      const config = JSON.parse(e.target?.result as string)
      formConfig.value = JSON.stringify(config, null, 2)
      message.success('导入成功')
      importModalVisible.value = false
      importFileList.value = []
    } catch (error) {
      message.error('文件格式错误')
    }
  }
  reader.readAsText(file)
}

const handleExport = () => {
  if (!formConfig.value) {
    message.warning('请先设计表单')
    return
  }
  
  const dataStr = formConfig.value
  const dataBlob = new Blob([dataStr], { type: 'application/json' })
  const url = URL.createObjectURL(dataBlob)
  const link = document.createElement('a')
  link.href = url
  link.download = `${formName.value || 'form'}.json`
  link.click()
  URL.revokeObjectURL(url)
  message.success('导出成功')
}

const handleTemplate = () => {
  templateModalVisible.value = true
}

const selectTemplate = (template: any) => {
  formConfig.value = JSON.stringify(template.config, null, 2)
  templateModalVisible.value = false
  message.success(`已应用模板：${template.name}`)
}

const handleFormSettings = () => {
  // 同步当前表单信息到设置
  Object.assign(formSettings, {
    formName: formName.value,
    formCategory: formCategory.value,
    formType: formType.value,
    version: formVersion.value
  })
  settingsModalVisible.value = true
}

const handleSettingsConfirm = () => {
  // 同步设置到表单信息
  formName.value = formSettings.formName
  formCategory.value = formSettings.formCategory
  formType.value = formSettings.formType
  formVersion.value = formSettings.version
  settingsModalVisible.value = false
  message.success('表单设置已保存')
}

// 监听表单信息变化
watch(formName, (newVal) => {
  formSettings.formName = newVal
})

watch(formCategory, (newVal) => {
  formSettings.formCategory = newVal
})

watch(formType, (newVal) => {
  formSettings.formType = newVal
})

watch(formVersion, (newVal) => {
  formSettings.version = newVal
})

onMounted(async () => {
  // 加载字典数据
  await dictStore.loadDicts([
    'workflow_form_category',
    'workflow_form_type'
  ])
  
  // 初始化默认表单配置
  if (!formConfig.value) {
    formConfig.value = JSON.stringify({
      rule: [
        {
          type: 'input',
          field: 'title',
          title: '标题',
          props: {
            placeholder: '请输入标题'
          },
          validate: [
            { required: true, message: '请输入标题' }
          ]
        }
      ]
    }, null, 2)
  }
})
</script>

<style lang="less" scoped>
.form-designer-container {
  height: 100vh;
  display: flex;
  flex-direction: column;
  background-color: #f5f5f5;
}

.designer-toolbar {
  height: 50px;
  background-color: #fff;
  border-bottom: 1px solid #d9d9d9;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 16px;

  .toolbar-left,
  .toolbar-right {
    display: flex;
    gap: 8px;
  }
}

.designer-content {
  flex: 1;
  display: flex;
  overflow: hidden;
}

.component-panel {
  width: 220px;
  background-color: #fff;
  border-right: 1px solid #d9d9d9;
  display: flex;
  flex-direction: column;

  .panel-header {
    padding: 12px 16px;
    border-bottom: 1px solid #d9d9d9;
    
    h3 {
      margin: 0;
      font-size: 14px;
      font-weight: 500;
    }
  }

  .component-list {
    flex: 1;
    padding: 16px;

    .component-item {
      display: flex;
      align-items: center;
      padding: 8px 12px;
      margin-bottom: 8px;
      border: 1px solid #d9d9d9;
      border-radius: 4px;
      cursor: grab;
      transition: all 0.3s;

      &:hover {
        border-color: #1890ff;
        background-color: #f0f8ff;
      }

      .component-icon {
        margin-right: 8px;
        font-size: 16px;
        color: #666;
      }

      .component-label {
        font-size: 12px;
        color: #333;
      }
    }
  }
}

.designer-main {
  flex: 1;
  display: flex;
  flex-direction: column;
  background-color: #fff;
}

.designer-header {
  height: 60px;
  border-bottom: 1px solid #d9d9d9;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 16px;

  .form-info {
    display: flex;
    align-items: center;
    gap: 16px;

    .form-name-input {
      width: 200px;
    }
  }

  .form-actions {
    display: flex;
    gap: 8px;
  }
}

.form-container {
  flex: 1;
  padding: 16px;
  overflow: hidden;
}

.property-panel {
  width: 380px;
  background-color: #fff;
  border-left: 1px solid #d9d9d9;
  display: flex;
  flex-direction: column;

  .panel-header {
    padding: 12px 16px;
    border-bottom: 1px solid #d9d9d9;
    
    h3 {
      margin: 0;
      font-size: 14px;
      font-weight: 500;
    }
  }

  .property-content {
    flex: 1;
    padding: 16px;
    overflow-y: auto;

    .form-property,
    .field-property {
      h4 {
        margin-bottom: 16px;
        font-size: 14px;
        font-weight: 500;
      }
    }
  }
}

.preview-container,
.test-container {
  .hbt-form {
    border: 1px solid #d9d9d9;
    border-radius: 4px;
  }
}

.upload-tip {
  margin-top: 16px;
  color: #666;
  font-size: 12px;
}

.template-container {
  .template-list {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
    gap: 16px;

    .template-item {
      border: 1px solid #d9d9d9;
      border-radius: 8px;
      padding: 16px;
      cursor: pointer;
      transition: all 0.3s;

      &:hover {
        border-color: #1890ff;
        box-shadow: 0 2px 8px rgba(24, 144, 255, 0.2);
      }

      .template-icon {
        font-size: 24px;
        color: #1890ff;
        margin-bottom: 12px;
      }

      .template-info {
        .template-name {
          font-size: 16px;
          font-weight: 500;
          color: #333;
          margin-bottom: 8px;
        }

        .template-desc {
          font-size: 14px;
          color: #666;
          line-height: 1.4;
        }
      }
    }
  }
}
</style> 