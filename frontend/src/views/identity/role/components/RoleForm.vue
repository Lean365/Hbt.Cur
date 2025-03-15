<template>
  <hbt-modal
    :open="visible"
    :title="title"
    :loading="loading"
    :width="800"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
  >
    <a-tabs>
      <!-- 基本信息标签页 -->
      <a-tab-pane :key="'basic'" :tab="t('identity.role.tabs.basic')">
        <a-form
          ref="formRef"
          :model="form"
          :rules="rules"
          :label-col="{ span: 6 }"
          :wrapper-col="{ span: 16 }"
        >
          <a-form-item :label="t('identity.role.fields.roleName.label')" name="roleName">
            <a-input
              v-model:value="form.roleName"
              :placeholder="t('identity.role.fields.roleName.placeholder')"
            />
          </a-form-item>
          <a-form-item :label="t('identity.role.fields.roleKey.label')" name="roleKey">
            <a-input
              v-model:value="form.roleKey"
              :placeholder="t('identity.role.fields.roleKey.placeholder')"
            />
          </a-form-item>
          <a-form-item :label="t('identity.role.fields.roleSort.label')" name="orderNum">
            <a-input-number
              v-model:value="form.orderNum"
              :min="0"
              style="width: 100%"
              :placeholder="t('identity.role.fields.roleSort.placeholder')"
            />
          </a-form-item>
          <a-form-item :label="t('identity.role.fields.dataScope.label')" name="dataScope">
            <a-select
              v-model:value="form.dataScope"
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
            <a-radio-group v-model:value="form.status">
              <a-radio :value="0">{{ t('identity.role.fields.status.options.enabled') }}</a-radio>
              <a-radio :value="1">{{ t('identity.role.fields.status.options.disabled') }}</a-radio>
            </a-radio-group>
          </a-form-item>
          <a-form-item :label="t('identity.role.fields.description.label')" name="remark">
            <a-textarea
              v-model:value="form.remark"
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
            v-model:checkedKeys="form.menuIds"
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
import { ref, reactive, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'
import type { TreeDataItem, DataNode } from 'ant-design-vue/es/tree'
import type { Role } from '@/types/identity/role'
import { getRole, createRole, updateRole } from '@/api/identity/role'
import { getCurrentUserMenus } from '@/api/identity/menu'
import type { Menu } from '@/types/identity/menu'

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

// 表单数据
const form = reactive<Role>({
  roleId: 0,
  roleName: '',
  roleKey: '',
  orderNum: 0,
  dataScope: 1,
  status: 0,
  tenantId: 0,
  remark: '',
  menuIds: [],
  deptIds: [],
  id: 0,
  createBy: '',
  createTime: '',
  isDeleted: 0
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  roleName: [
    { required: true, message: t('identity.role.fields.roleName.validation.required'), type: 'string' as const },
    { max: 50, message: t('identity.role.fields.roleName.validation.length'), type: 'string' as const }
  ],
  roleKey: [
    { required: true, message: t('identity.role.fields.roleKey.validation.required'), type: 'string' as const },
    { max: 100, message: t('identity.role.fields.roleKey.validation.length'), type: 'string' as const }
  ],
  orderNum: [
    { required: true, message: t('identity.role.fields.roleSort.validation.required'), type: 'number' as const }
  ],
  dataScope: [
    { required: true, message: t('identity.role.fields.dataScope.validation.required'), type: 'number' as const }
  ]
}

// 加载状态
const loading = ref(false)

// 菜单树相关
const menuTree = ref<DataNode[]>([])
const menuCheckAll = ref(false)
const menuIndeterminate = ref(false)

// 获取角色信息
const getInfo = async (roleId: number) => {
  try {
    loading.value = true
    const res = await getRole(roleId)
    Object.assign(form, res.data)
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 将菜单数据转换为树形控件数据
const transformMenuToTreeData = (menus: Menu[]): DataNode[] => {
  return menus.map(menu => ({
    key: menu.menuId,
    title: menu.menuName,
    children: menu.children ? transformMenuToTreeData(menu.children) : undefined
  }))
}

const getMenuTree = async () => {
  try {
    const res = await getCurrentUserMenus()
    if (res?.data?.code === 200 && Array.isArray(res.data.data)) {
      menuTree.value = transformMenuToTreeData(res.data.data)
    } else {
      console.error('获取菜单树失败:', res)
      message.error('获取菜单树失败')
    }
  } catch (error) {
    console.error('获取菜单树出错:', error)
    message.error('获取菜单树失败')
  }
}

// 处理全选菜单
const handleMenuCheckAllChange = (e: { target: { checked: boolean } }) => {
  const allKeys = getAllMenuKeys(menuTree.value)
  form.menuIds = e.target.checked ? allKeys : []
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
      if (props.roleId) {
        await updateRole(form)
      } else {
        await createRole(form)
      }
      message.success(t('common.save.success'))
      emit('success')
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
  if (props.roleId) {
    getInfo(props.roleId)
  }
  getMenuTree()
})
</script>

<style lang="less" scoped>
.ant-card {
  margin-top: 16px;
}
</style>
