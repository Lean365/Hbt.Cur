<template>
  <hbt-modal
    :open="visible"
    :title="title"
    :loading="loading"
    :width="800"
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
      <a-form-item :label="t('identity.menu.fields.parentMenu.label')" name="parentId">
        <menu-tree-select v-model:value="form.parentId" />
      </a-form-item>
      <a-form-item :label="t('identity.menu.fields.menuName.label')" name="menuName">
        <a-input
          v-model:value="form.menuName"
          :placeholder="t('identity.menu.fields.menuName.placeholder')"
        />
      </a-form-item>
      <a-form-item :label="t('identity.menu.fields.menuType.label')" name="menuType">
        <a-radio-group v-model:value="form.menuType">
          <a-radio :value="0">{{ t('identity.menu.fields.menuType.options.directory') }}</a-radio>
          <a-radio :value="1">{{ t('identity.menu.fields.menuType.options.menu') }}</a-radio>
          <a-radio :value="2">{{ t('identity.menu.fields.menuType.options.button') }}</a-radio>
        </a-radio-group>
      </a-form-item>
      <a-form-item :label="t('identity.menu.fields.orderNum.label')" name="orderNum">
        <a-input-number
          v-model:value="form.orderNum"
          :min="0"
          style="width: 100%"
          :placeholder="t('identity.menu.fields.orderNum.placeholder')"
        />
      </a-form-item>
      <a-form-item
        v-if="form.menuType !== 2"
        :label="t('identity.menu.fields.icon.label')"
        name="icon"
      >
        <a-input
          v-model:value="form.icon"
          :placeholder="t('identity.menu.fields.icon.placeholder')"
        />
      </a-form-item>
      <a-form-item
        v-if="form.menuType === 1"
        :label="t('identity.menu.fields.component.label')"
        name="component"
      >
        <a-input
          v-model:value="form.component"
          :placeholder="t('identity.menu.fields.component.placeholder')"
        />
      </a-form-item>
      <a-form-item
        v-if="form.menuType !== 0"
        :label="t('identity.menu.fields.permission.label')"
        name="perms"
      >
        <a-input
          v-model:value="form.perms"
          :placeholder="t('identity.menu.fields.permission.placeholder')"
        />
      </a-form-item>
      <a-form-item :label="t('identity.menu.fields.status.label')" name="status">
        <hbt-select
          v-model:value="form.status"
          dict-type="sys_normal_disable"
          type="radio"
          :placeholder="t('identity.menu.fields.status.placeholder')"
          allow-clear
        />
      </a-form-item>
    </a-form>
  </hbt-modal>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { Menu, MenuCreate } from '@/types/identity/menu'
import { getMenu, createMenu, updateMenu } from '@/api/identity/menu'
import MenuTreeSelect from '../components/MenuTreeSelect.vue'

const props = defineProps<{
  visible: boolean
  title: string
  menuId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const form = reactive<MenuCreate>({
  menuName: '',
  parentId: undefined,
  orderNum: 0,
  menuType: 0,
  path: '',
  component: '',
  perms: '',
  status: 0,
  icon: '',
  isFrame: 0,
  isCache: 0,
  visible: 0,
  queryParams: '',
  transKey: ''
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  menuName: [
    { required: true, message: t('identity.menu.fields.menuName.validation.required') },
    { min: 2, max: 50, message: t('identity.menu.fields.menuName.validation.length') }
  ],
  orderNum: [
    { required: true, message: t('identity.menu.fields.orderNum.validation.required') }
  ],
  menuType: [
    { required: true, message: t('identity.menu.fields.menuType.validation.required') }
  ]
}

// 加载状态
const loading = ref(false)

// 获取菜单信息
const getInfo = async (menuId: number) => {
  try {
    loading.value = true
    const res = await getMenu(menuId)
    if (res.code === 200) {
      Object.assign(form, res.data)
    } else {
      message.error(res.msg || t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 处理弹窗显示状态变化
const handleVisibleChange = (visible: boolean) => {
  emit('update:visible', visible)
  if (!visible) {
    formRef.value?.resetFields()
  }
}

// 处理表单提交
const handleSubmit = () => {
  formRef.value?.validate().then(async () => {
    try {
      loading.value = true
      let res
      if (props.menuId) {
        res = await updateMenu({
          ...form,
          menuId: props.menuId
        })
      } else {
        res = await createMenu(form)
      }
      if (res.code === 200) {
        message.success(t('common.save.success'))
        emit('success')
        handleVisibleChange(false)
      } else {
        message.error(res.msg || t('common.save.failed'))
      }
    } catch (error) {
      console.error(error)
      message.error(t('common.save.failed'))
    } finally {
      loading.value = false
    }
  })
}

// 初始化
onMounted(() => {
  if (props.menuId) {
    getInfo(props.menuId)
  }
})
</script>
