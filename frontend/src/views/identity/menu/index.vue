<template>
  <div class="menu-container">
    <a-card :bordered="false">
      <!-- 工具栏 -->
      <div class="table-toolbar">
        <a-space>
          <a-button type="primary" @click="handleAdd">
            <template #icon><plus-outlined /></template>
            新增
          </a-button>
          <a-button @click="handleExpandAll">
            <template #icon><node-expand-outlined /></template>
            展开/折叠
          </a-button>
        </a-space>
      </div>

      <!-- 数据表格 -->
      <a-table
        :columns="columns"
        :data-source="tableData"
        :loading="loading"
        :pagination="false"
        row-key="id"
        :expandable="{
          defaultExpandAllRows: true
        }"
      >
        <template #bodyCell="{ column, record }">
          <!-- 图标列 -->
          <template v-if="column.key === 'icon'">
            <component :is="record.icon" v-if="record.icon" />
          </template>

          <!-- 类型列 -->
          <template v-if="column.key === 'type'">
            <a-tag :color="record.type === 'menu' ? 'success' : 'processing'">
              {{ record.type === 'menu' ? '菜单' : '目录' }}
            </a-tag>
          </template>

          <!-- 状态列 -->
          <template v-if="column.key === 'status'">
            <a-tag :color="(record as unknown as MenuInfo).status === 'normal' ? 'success' : 'error'">
              {{ (record as unknown as MenuInfo).status === 'normal' ? '正常' : '禁用' }}
            </a-tag>
          </template>
          
          <!-- 操作列 -->
          <template v-if="column.key === 'action'">
            <a-space>
              <a-button type="link" @click="handleAdd">新增</a-button>
              <a-button type="link" @click="handleEdit(record as unknown as MenuInfo)">编辑</a-button>
              <a-button type="link" danger>
                <a-popconfirm
                  title="确定要删除此菜单吗？"
                  @confirm="handleDelete(record as unknown as MenuInfo)"
                >
                  <template #default>
                    <span>删除</span>
                  </template>
                </a-popconfirm>
              </a-button>
            </a-space>
          </template>
        </template>
      </a-table>
    </a-card>

    <!-- 编辑对话框 -->
    <a-modal
      v-model:visible="modalVisible"
      :title="modalTitle"
      @ok="handleModalOk"
      @cancel="handleModalCancel"
    >
      <a-form
        ref="formRef"
        :model="formData"
        :rules="formRules"
        :label-col="{ span: 6 }"
        :wrapper-col="{ span: 16 }"
      >
        <a-form-item label="上级菜单" name="parentId">
          <a-tree-select
            v-model:value="formData.parentId"
            :tree-data="menuTree"
            :field-names="{ children: 'children', label: 'title', value: 'key' }"
            placeholder="请选择上级菜单"
            allow-clear
            tree-default-expand-all
          />
        </a-form-item>
        <a-form-item label="菜单名称" name="name">
          <a-input
            v-model:value="formData.name"
            placeholder="请输入菜单名称"
          />
        </a-form-item>
        <a-form-item label="菜单类型" name="type">
          <a-select
            v-model:value="formData.type"
            placeholder="请选择菜单类型"
          >
            <a-select-option value="directory">目录</a-select-option>
            <a-select-option value="menu">菜单</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="路由路径" name="path">
          <a-input
            v-model:value="formData.path"
            placeholder="请输入路由路径"
          />
        </a-form-item>
        <a-form-item label="组件路径" name="component" v-if="formData.type === 'menu'">
          <a-input
            v-model:value="formData.component"
            placeholder="请输入组件路径"
          />
        </a-form-item>
        <a-form-item label="图标" name="icon">
          <a-input
            v-model:value="formData.icon"
            placeholder="请输入图标"
          >
            <template #prefix>
              <component :is="formData.icon" v-if="formData.icon" />
            </template>
          </a-input>
        </a-form-item>
        <a-form-item label="显示顺序" name="orderNum">
          <a-input-number
            v-model:value="formData.orderNum"
            placeholder="请输入显示顺序"
            style="width: 100%"
            :min="0"
            :max="999"
          />
        </a-form-item>
        <a-form-item label="状态" name="status">
          <a-select
            v-model:value="formData.status"
            placeholder="请选择状态"
          >
            <a-select-option value="normal">正常</a-select-option>
            <a-select-option value="disabled">禁用</a-select-option>
          </a-select>
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import type { FormInstance, TableColumnsType } from 'ant-design-vue'
import type { DataNode } from 'ant-design-vue/es/tree'
import {
  PlusOutlined,
  NodeExpandOutlined
} from '@ant-design/icons-vue'

interface MenuInfo {
  id: number
  parentId: number | null
  name: string
  type: 'menu' | 'directory'
  path: string
  component?: string
  icon?: string
  orderNum: number
  status: string
  children?: MenuInfo[]
}

// 表格列定义
const columns: TableColumnsType<MenuInfo> = [
  {
    title: '菜单名称',
    dataIndex: 'name',
    key: 'name',
    width: 200
  },
  {
    title: '图标',
    dataIndex: 'icon',
    key: 'icon',
    width: 80,
    align: 'center'
  },
  {
    title: '菜单类型',
    dataIndex: 'type',
    key: 'type',
    width: 100
  },
  {
    title: '路由路径',
    dataIndex: 'path',
    key: 'path',
    width: 200
  },
  {
    title: '组件路径',
    dataIndex: 'component',
    key: 'component',
    width: 200
  },
  {
    title: '显示顺序',
    dataIndex: 'orderNum',
    key: 'orderNum',
    width: 100
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    width: 100
  },
  {
    title: '操作',
    key: 'action',
    width: 200,
    fixed: 'right' as const
  }
]

// 表格数据
const loading = ref(false)
const tableData = ref<MenuInfo[]>([])

// 编辑表单数据
const modalVisible = ref(false)
const modalTitle = ref('')
const formRef = ref<FormInstance>()
const formData = reactive<MenuInfo>({
  id: 0,
  parentId: null,
  name: '',
  type: 'menu',
  path: '',
  component: '',
  icon: '',
  orderNum: 0,
  status: 'normal'
})

// 表单验证规则
const formRules = {
  name: [
    { required: true, message: '请输入菜单名称' },
    { min: 2, max: 20, message: '菜单名称长度必须在2-20个字符之间' }
  ],
  type: [
    { required: true, message: '请选择菜单类型' }
  ],
  path: [
    { required: true, message: '请输入路由路径' }
  ],
  component: [
    { required: true, message: '请输入组件路径' }
  ],
  orderNum: [
    { required: true, message: '请输入显示顺序' }
  ],
  status: [
    { required: true, message: '请选择状态' }
  ]
}

// 菜单树数据
const menuTree = ref<DataNode[]>([])

// 处理新增
const handleAdd = (e: MouseEvent | number) => {
  const parentId = typeof e === 'number' ? e : null
  modalTitle.value = '新增菜单'
  Object.assign(formData, {
    id: 0,
    parentId,
    name: '',
    type: 'menu',
    path: '',
    component: '',
    icon: '',
    orderNum: 0,
    status: 'normal'
  })
  modalVisible.value = true
}

// 处理编辑
const handleEdit = (record: MenuInfo) => {
  modalTitle.value = '编辑菜单'
  const { id, parentId, name, type, path, component, icon, orderNum, status } = record
  Object.assign(formData, { id, parentId, name, type, path, component, icon, orderNum, status })
  modalVisible.value = true
}

// 处理删除
const handleDelete = async (record: MenuInfo) => {
  try {
    // 调用删除API
    console.log('删除菜单:', record.id)
    // 重新加载数据
    fetchData()
  } catch (error) {
    console.error('删除菜单失败:', error)
  }
}

// 处理展开/折叠
const handleExpandAll = () => {
  // 实现展开/折叠逻辑
}

// 处理模态框确认
const handleModalOk = () => {
  formRef.value?.validate().then(() => {
    // 调用保存API
    console.log('保存菜单:', formData)
    modalVisible.value = false
    // 重新加载数据
    fetchData()
  })
}

// 处理模态框取消
const handleModalCancel = () => {
  modalVisible.value = false
}

// 获取表格数据
const fetchData = () => {
  loading.value = true
  // 模拟API调用
  setTimeout(() => {
    tableData.value = [
      {
        id: 1,
        parentId: null,
        name: '系统管理',
        type: 'directory',
        path: '/system',
        icon: 'SettingOutlined',
        orderNum: 1,
        status: 'normal',
        children: [
          {
            id: 2,
            parentId: 1,
            name: '用户管理',
            type: 'menu',
            path: 'user',
            component: 'system/user/index',
            icon: 'UserOutlined',
            orderNum: 1,
            status: 'normal'
          },
          {
            id: 3,
            parentId: 1,
            name: '角色管理',
            type: 'menu',
            path: 'role',
            component: 'system/role/index',
            icon: 'TeamOutlined',
            orderNum: 2,
            status: 'normal'
          }
        ]
      }
    ]
    // 构建菜单树
    menuTree.value = buildMenuTree(tableData.value)
    loading.value = false
  }, 500)
}

// 构建菜单树
const buildMenuTree = (menus: MenuInfo[]): DataNode[] => {
  return menus.map(item => ({
    key: item.id,
    title: item.name,
    children: item.children ? buildMenuTree(item.children) : undefined
  }))
}

// 初始化
fetchData()
</script>

<style lang="less" scoped>
.menu-container {
  .table-toolbar {
    margin-bottom: 16px;
  }

  .text-danger {
    color: #ff4d4f;
  }
}
</style> 