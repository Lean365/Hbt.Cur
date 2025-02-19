<template>
  <a-modal
    :title="title"
    :open="visible"
    :confirm-loading="loading"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item :label="t('identity.menu.form.base.parentMenu.label')" name="parentId">
        <menu-tree-select v-model:value="form.parentId" />
      </a-form-item>
      <a-form-item :label="t('identity.menu.form.display.type.label')" name="menuType">
        <a-radio-group v-model:value="form.menuType">
          <a-radio :value="HbtMenuType.Directory">{{ t('identity.menu.form.display.type.directory') }}</a-radio>
          <a-radio :value="HbtMenuType.Menu">{{ t('identity.menu.form.display.type.menu') }}</a-radio>
          <a-radio :value="HbtMenuType.Button">{{ t('identity.menu.form.display.type.button') }}</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item :label="t('identity.menu.form.base.name.label')" name="menuName">
        <a-input v-model:value="form.menuName" :placeholder="t('identity.menu.form.base.name.placeholder')" />
      </a-form-item>
      <a-form-item :label="t('identity.menu.form.base.transKey.label')" name="transKey">
        <a-input v-model:value="form.transKey" :placeholder="t('identity.menu.form.base.transKey.placeholder')" />
        <template #help>
          <span v-if="form.transKey">
            {{ t('identity.menu.form.base.transKey.preview') }}: 
            <span style="color: #1890ff">{{ t(form.transKey) === form.transKey ? t('identity.menu.form.base.transKey.notFound') : t(form.transKey) }}</span>
          </span>
        </template>
      </a-form-item>
      <a-form-item :label="t('identity.menu.form.base.orderNum.label')" name="orderNum">
        <a-input-number v-model:value="form.orderNum" :min="0" :max="999" style="width: 100%" />
      </a-form-item>
      <a-form-item 
        :label="t('identity.menu.form.route.path.label')" 
        name="path"
        v-if="form.menuType !== HbtMenuType.Button"
      >
        <a-input v-model:value="form.path" :placeholder="t('identity.menu.form.route.path.placeholder')" />
      </a-form-item>
      <a-form-item 
        :label="t('identity.menu.form.route.component.label')" 
        name="component"
        v-if="form.menuType === HbtMenuType.Menu"
      >
        <a-input v-model:value="form.component" :placeholder="t('identity.menu.form.route.component.placeholder')" />
      </a-form-item>
      <a-form-item 
        :label="t('identity.menu.form.route.query.label')" 
        name="queryParams"
        v-if="form.menuType === HbtMenuType.Menu"
      >
        <a-input v-model:value="form.queryParams" :placeholder="t('identity.menu.form.route.query.placeholder')" />
      </a-form-item>
      <a-form-item 
        :label="t('identity.menu.form.permission.perms.label')" 
        name="perms"
        v-if="form.menuType !== HbtMenuType.Directory"
      >
        <a-input v-model:value="form.perms" :placeholder="t('identity.menu.form.permission.perms.placeholder')" />
      </a-form-item>
      <a-form-item 
        :label="t('identity.menu.form.display.icon.label')" 
        name="icon"
        v-if="form.menuType !== HbtMenuType.Button"
      >
        <a-input v-model:value="form.icon" :placeholder="t('identity.menu.form.display.icon.placeholder')" />
      </a-form-item>
      <a-form-item 
        :label="t('identity.menu.form.display.isFrame.label')" 
        name="isFrame"
        v-if="form.menuType === HbtMenuType.Menu"
      >
        <a-radio-group v-model:value="form.isFrame">
          <a-radio :value="HbtYesNo.No">{{ t('identity.menu.form.display.isFrame.no') }}</a-radio>
          <a-radio :value="HbtYesNo.Yes">{{ t('identity.menu.form.display.isFrame.yes') }}</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item 
        :label="t('identity.menu.form.display.isCache.label')" 
        name="isCache"
        v-if="form.menuType === HbtMenuType.Menu"
      >
        <a-radio-group v-model:value="form.isCache">
          <a-radio :value="HbtYesNo.No">{{ t('identity.menu.form.display.isCache.no') }}</a-radio>
          <a-radio :value="HbtYesNo.Yes">{{ t('identity.menu.form.display.isCache.yes') }}</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item :label="t('identity.menu.form.display.visible.label')" name="visible">
        <a-radio-group v-model:value="form.visible">
          <a-radio :value="HbtVisible.Show">{{ t('identity.menu.form.display.visible.show') }}</a-radio>
          <a-radio :value="HbtVisible.Hide">{{ t('identity.menu.form.display.visible.hide') }}</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item :label="t('identity.menu.form.display.status.label')" name="status">
        <a-radio-group v-model:value="form.status">
          <a-radio :value="HbtStatus.Normal">{{ t('identity.menu.form.display.status.normal') }}</a-radio>
          <a-radio :value="HbtStatus.Disabled">{{ t('identity.menu.form.display.status.disabled') }}</a-radio>
        </a-radio-group>
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { RuleObject } from 'ant-design-vue/es/form'
import type { Menu, MenuCreate, MenuUpdate } from '@/types/identity/menu'
import { createMenu, updateMenu } from '@/api/identity/menu'
import { HbtStatus, HbtYesNo, HbtMenuType, HbtVisible } from '@/types/enums'
import MenuTreeSelect from './MenuTreeSelect.vue'

const props = withDefaults(defineProps<{
  visible: boolean
  title: string
  formData?: Menu | undefined
}>(), {
  visible: false,
  title: '',
  formData: undefined
})

const emit = defineEmits<{
  'update:visible': [value: boolean]
  'success': []
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 默认表单数据
const defaultForm: MenuCreate = {
  menuName: '',
  transKey: '',
  parentId: undefined,
  orderNum: 0,
  path: '',
  component: '',
  queryParams: '',
  isFrame: HbtYesNo.No,
  isCache: HbtYesNo.No,
  menuType: HbtMenuType.Menu,
  visible: HbtVisible.Show,
  status: HbtStatus.Normal,
  perms: '',
  icon: '',
  tenantId: 0
}

// 表单数据
const form = reactive<MenuCreate>({ ...defaultForm })

// 表单校验规则
const rules: Record<string, RuleObject[]> = {
  menuName: [
    { required: true, message: t('identity.menu.validation.name.required') },
    { min: 2, max: 50, message: t('identity.menu.validation.name.length') }
  ],
  orderNum: [
    { required: true, message: t('identity.menu.validation.orderNum.required') }
  ],
  path: [
    { 
      required: true, 
      message: t('identity.menu.validation.path.required'),
      trigger: 'blur',
      validator: (_rule: RuleObject, value: string) => {
        if (form.menuType === HbtMenuType.Button) {
          return Promise.resolve()
        }
        if (!value) {
          return Promise.reject(t('identity.menu.validation.path.required'))
        }
        return Promise.resolve()
      }
    }
  ],
  menuType: [
    { required: true, message: t('identity.menu.validation.type.required') }
  ]
}

// 加载状态
const loading = ref(false)

// 监听表单数据变化
watch(
  () => props.formData,
  (val) => {
    if (val) {
      Object.assign(form, val)
    } else {
      formRef.value?.resetFields()
      Object.assign(form, defaultForm)
    }
  },
  { immediate: true }
)

// 提交表单
const handleSubmit = () => {
  if (!formRef.value) return

  formRef.value.validate().then(async () => {
    loading.value = true
    try {
      if (props.formData?.menuId) {
        const res = await updateMenu({
          ...form,
          menuId: props.formData.menuId
        })
        if(res.code === 200) {
          message.success(t('identity.menu.messages.update.success'))
          emit('success')
          handleVisibleChange(false)
        } else {
          console.error('更新菜单失败:', res.msg)
          message.error(res.msg || t('identity.menu.messages.update.failed'))
        }
      } else {
        const res = await createMenu(form)
        if(res.code === 200) {
          message.success(t('identity.menu.messages.create.success'))
          emit('success')
          handleVisibleChange(false)
        } else {
          console.error('创建菜单失败:', res.msg)
          message.error(res.msg || t('identity.menu.messages.create.failed'))
        }
      }
    } catch (error) {
      console.error('菜单操作失败:', error)
      message.error(t('common.failed'))
    } finally {
      loading.value = false
    }
  }).catch(error => {
    console.error('表单验证失败:', error)
    message.error(t('common.form.validation.failed'))
  })
}

// 处理对话框显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:visible', val)
  if (!val) {
    formRef.value?.resetFields()
    Object.assign(form, defaultForm)
  }
}
</script> 