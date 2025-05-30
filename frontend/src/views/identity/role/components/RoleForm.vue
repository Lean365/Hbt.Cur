<template>
  <hbt-modal
    :open="visible"
    :title="title"
    :loading="loading"
    :width="800"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-tabs>
      <!-- 基本信息标签页 -->
      <a-tab-pane :key="'basic'" :tab="t('identity.role.tabs.basic')">
        <a-form
          ref="formRef"
          :model="formState"
          :rules="rules"
          :label-col="{ span: 6 }"
          :wrapper-col="{ span: 16 }"
        >
          <a-form-item :label="t('identity.role.fields.roleName.label')" name="roleName">
            <a-input
              v-model:value="formState.roleName"
              :placeholder="t('identity.role.fields.roleName.placeholder')"
            />
          </a-form-item>
          <a-form-item :label="t('identity.role.fields.roleKey.label')" name="roleKey">
            <a-input
              v-model:value="formState.roleKey"
              :placeholder="t('identity.role.fields.roleKey.placeholder')"
            />
          </a-form-item>
          <a-form-item :label="t('identity.role.fields.roleSort.label')" name="orderNum">
            <a-input-number
              v-model:value="formState.orderNum"
              :min="0"
              style="width: 100%"
              :placeholder="t('identity.role.fields.roleSort.placeholder')"
            />
          </a-form-item>
          <a-form-item :label="t('identity.role.fields.dataScope.label')" name="dataScope">
            <a-select
              v-model:value="formState.dataScope"
              :placeholder="t('identity.role.fields.dataScope.placeholder')"
            >
              <a-select-option :value="1">{{ t('identity.role.fields.dataScope.options.all') }}</a-select-option>
              <a-select-option :value="2">{{ t('identity.role.fields.dataScope.options.custom') }}</a-select-option>
              <a-select-option :value="3">{{ t('identity.role.fields.dataScope.options.dept') }}</a-select-option>
              <a-select-option :value="4">{{ t('identity.role.fields.dataScope.options.deptAndChild') }}</a-select-option>
              <a-select-option :value="5">{{ t('identity.role.fields.dataScope.options.self') }}</a-select-option>
            </a-select>
          </a-form-item>
          <a-form-item :label="t('identity.role.fields.status.label')" name="status">
            <a-radio-group v-model:value="formState.status">
              <a-radio :value="0">{{ t('identity.role.fields.status.options.enabled') }}</a-radio>
              <a-radio :value="1">{{ t('identity.role.fields.status.options.disabled') }}</a-radio>
            </a-radio-group>
          </a-form-item>
          <a-form-item :label="t('identity.role.fields.description.label')" name="remark">
            <a-textarea
              v-model:value="formState.remark"
              :rows="4"
              :placeholder="t('identity.role.fields.description.placeholder')"
            />
          </a-form-item>
        </a-form>
      </a-tab-pane>

      <!-- 权限分配标签页 -->
      <a-tab-pane :key="'permission'" :tab="t('identity.role.tabs.permission')">
        <a-card :bordered="false">
          <template #extra>
            <a-checkbox
              :indeterminate="menuIndeterminate"
              :checked="menuCheckAll"
              @change="handleMenuCheckAllChange"
            >
              {{ t('identity.role.fields.menuPermission.selectAll') }}
            </a-checkbox>
          </template>
          <a-tree
            v-model:checkedKeys="formState.menuIds"
            :tree-data="menuTree"
            checkable
            :defaultExpandAll="true"
            @check="handleMenuCheck"
          />
        </a-card>
      </a-tab-pane>
    </a-tabs>
  </hbt-modal>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { TreeDataItem, DataNode } from 'ant-design-vue/es/tree'
import type { HbtRole, HbtRoleUpdate, HbtRoleCreate } from '@/types/identity/role'

import { getRole, createRole, updateRole } from '@/api/identity/role'
import { getCurrentUserMenus } from '@/api/identity/menu'
import type { HbtMenu } from '@/types/identity/menu'

const props = defineProps<{
  visible: boolean
  title: string
  roleId?: number
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 提交按钮loading
const submitLoading = ref(false)

// 表单校验规则
const rules: Record<string, Rule[]> = {
  roleName: [
    { required: true, message: t('identity.role.fields.roleName.validation.required') },
    { max: 30, message: t('identity.role.fields.roleName.validation.maxLength') }
  ],
  roleKey: [
    { required: true, message: t('identity.role.fields.roleKey.validation.required') },
    { max: 100, message: t('identity.role.fields.roleKey.validation.maxLength') }
  ],
  roleSort: [
    { required: true, message: t('identity.role.fields.roleSort.validation.required') },
    { type: 'number', message: t('identity.role.fields.roleSort.validation.type') }
  ],
  status: [{ required: true, message: t('identity.role.fields.status.validation.required') }],
  remark: [{ max: 500, message: t('identity.role.fields.remark.validation.maxLength') }]
}

// 表单数据
const formState = ref<Partial<HbtRole>>({
  roleId: undefined,
  roleName: '',
  roleKey: '',
  orderNum: 0,
  dataScope: 1,
  status: 0,
  tenantId: 0,
  userCount: 0,
  remark: '',
  menuIds: [],
  deptIds: [],
  createBy: '',
  createTime: '',
  isDeleted: 0
})

// 加载状态
const loading = ref(false)

// 菜单树相关
const menuTree = ref<DataNode[]>([])
const menuCheckAll = ref(false)
const menuIndeterminate = ref(false)

// 获取角色信息
const getInfo = async (roleId: number) => {
  try {
    const res = await getRole(roleId)
    if (res.data.code === 200) {
      Object.assign(formState.value, res.data.data)
    } else {
      message.error(res.data.msg || t('common.failed'))
    }
  } catch (error) {
    console.error('获取角色信息失败:', error)
    message.error(t('common.failed'))
  }
}

// 将菜单数据转换为树形控件数据
const transformMenuToTreeData = (menus: HbtMenu[]): DataNode[] => {
  return menus.map(menu => ({
    key: menu.menuId,
    title: menu.menuName,
    children: menu.children ? transformMenuToTreeData(menu.children) : undefined
  }))
}

const getMenuTree = async () => {
  try {
    const res = await getCurrentUserMenus()
    if (res.code === 200) {
      menuTree.value = transformMenuToTreeData(res.data)
    } else {
      console.error('获取菜单树失败:', res)
      message.error(res.msg || '获取菜单树失败')
    }
  } catch (error) {
    console.error('获取菜单树出错:', error)
    message.error('获取菜单树失败')
  }
}

// 处理全选菜单
const handleMenuCheckAllChange = (e: { target: { checked: boolean } }) => {
  const allKeys = getAllMenuKeys(menuTree.value)
  formState.value.menuIds = e.target.checked ? allKeys : []
  menuIndeterminate.value = false
  menuCheckAll.value = e.target.checked
}

// 处理菜单选择
const handleMenuCheck = (_checkedKeys: any, e: { checked: boolean; checkedNodes: TreeDataItem[] }) => {
  const allKeys = getAllMenuKeys(menuTree.value)
  const checkedKeys = e.checkedNodes.map(node => node.key as number)
  menuCheckAll.value = checkedKeys.length === allKeys.length
  menuIndeterminate.value = checkedKeys.length > 0 && checkedKeys.length < allKeys.length
}

// 获取所有菜单key
const getAllMenuKeys = (menus: TreeDataItem[]): number[] => {
  const keys: number[] = []
  const traverse = (nodes: TreeDataItem[]) => {
    nodes.forEach(node => {
      if (typeof node.key === 'number') {
        keys.push(node.key)
      }
      if (node.children?.length) {
        traverse(node.children)
      }
    })
  }
  traverse(menus)
  return keys
}

// 监听角色ID变化
watch(
  () => props.roleId,
  async (newVal: number | undefined) => {
    if (newVal) {
      try {
        const res = await getRole(newVal)
        if (res.data.code === 200) {
          const { roleId, ...rest } = res.data.data
          formState.value = rest
        } else {
          message.error(res.data.msg || t('common.failed'))
        }
      } catch (error) {
        console.error('[角色管理] 获取角色详情出错:', error)
        message.error(t('common.failed'))
      }
    } else {
      resetForm()
    }
  }
)

// 重置表单
const resetForm = () => {
  formState.value = {
    roleId: undefined,
    roleName: '',
    roleKey: '',
    orderNum: 0,
    dataScope: 1,
    status: 0,
    tenantId: 0,
    userCount: 0,
    remark: '',
    menuIds: [],
    deptIds: [],
    createBy: '',
    createTime: '',
    isDeleted: 0
  }
  formRef.value?.resetFields()
}

// 处理弹窗显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:visible', val)
  if (!val) {
    resetForm()
  }
}

// 处理取消
const handleCancel = () => {
  emit('update:visible', false)
  resetForm()
}

// 提交表单
const handleSubmit = () => {
  formRef.value?.validate().then(async () => {
    try {
      submitLoading.value = true
      if (props.roleId) {
        // 构造更新数据
        const updateData: HbtRoleUpdate = {
          roleId: props.roleId,
          roleName: formState.value.roleName || '',
          roleKey: formState.value.roleKey || '',
          orderNum: formState.value.orderNum || 0,
          dataScope: formState.value.dataScope || 1,
          status: formState.value.status || 0,
          tenantId: formState.value.tenantId || 0,
          userCount: formState.value.userCount || 0,
          remark: formState.value.remark,
          menuIds: formState.value.menuIds || [],
          deptIds: formState.value.deptIds || []
        }
        const res = await updateRole(updateData)
        if (res.data.code === 200) {
          message.success(t('common.update.success'))
          emit('update:visible', false)
          emit('success')
        } else {
          message.error(res.data.msg || t('common.update.failed'))
        }
      } else {
        // 构造创建数据
        const createData: HbtRoleCreate = {
          roleName: formState.value.roleName || '',
          roleKey: formState.value.roleKey || '',
          orderNum: formState.value.orderNum || 0,
          dataScope: formState.value.dataScope || 1,
          status: formState.value.status || 0,
          tenantId: formState.value.tenantId || 0,
          userCount: formState.value.userCount || 0,
          remark: formState.value.remark,
          menuIds: formState.value.menuIds || [],
          deptIds: formState.value.deptIds || []
        }
        const res = await createRole(createData)
        if (res.data.code === 200) {
          message.success(t('common.create.success'))
          emit('update:visible', false)
          emit('success')
        } else {
          message.error(res.data.msg || t('common.create.failed'))
        }
      }
    } catch (error) {
      console.error('[角色管理] 提交表单出错:', error)
      message.error(t('common.failed'))
    } finally {
      submitLoading.value = false
    }
  })
}

onMounted(() => {
  const initData = async () => {
    if (props.roleId) {
      const res = await getRole(props.roleId)
      if (res.data.code === 200) {
        formState.value = res.data.data
      }
    }
  }
  initData()
  getMenuTree()
})

defineExpose({
  resetForm
})
</script>

<style lang="less" scoped>
.ant-card {
  margin-top: 16px;
}
</style>
