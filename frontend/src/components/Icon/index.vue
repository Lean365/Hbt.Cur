<template>
  <div class="icon-container">
    <!-- 使用说明 -->
    <div class="usage-section">
      <h2>图标使用说明</h2>
      <div class="usage-list">
        <div v-for="(code, title) in usage" :key="title" class="usage-item">
          <div class="usage-title">{{ title }}</div>
          <a-typography-paragraph copyable>
            <code>{{ code }}</code>
          </a-typography-paragraph>
        </div>
      </div>
    </div>

    <!-- 搜索栏 -->
    <div class="search-bar">
      <a-input-search
        v-model:value="searchText"
        placeholder="搜索图标，例如：edit"
        style="width: 300px"
        @search="handleSearch"
      />
    </div>

    <!-- 图标列表 -->
    <div class="icon-list">
      <template v-for="(icons, category) in groupedIcons" :key="category">
        <div class="category-title">{{ category }}</div>
        <div class="icon-grid">
          <div
            v-for="icon in filterIcons(icons)"
            :key="icon.name"
            class="icon-item"
            @click="handleCopy(icon.name)"
          >
            <component :is="icon.name" class="icon" />
            <div class="icon-name">{{ formatIconName(icon.name) }}</div>
          </div>
        </div>
      </template>
    </div>
  </div>
</template>

<script lang="ts">
// 导出所有图标，方便其他组件按需导入
export { default as Icons } from '@ant-design/icons-vue'
</script>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { message } from 'ant-design-vue'
import * as Icons from '@ant-design/icons-vue'

// 使用示例文档
const usage = {
  '按需导入': `import { EditOutlined } from '@ant-design/icons-vue'`,
  '模板使用': `<edit-outlined />`,
  '动态组件': `<component :is="'EditOutlined'" />`
}

// 图标分类
const iconCategories = {
  '方向类图标': [
    'LeftOutlined', 'RightOutlined', 'UpOutlined', 'DownOutlined',
    'ArrowLeftOutlined', 'ArrowRightOutlined', 'ArrowUpOutlined', 'ArrowDownOutlined'
  ],
  '提示类图标': [
    'QuestionOutlined', 'QuestionCircleOutlined', 'PlusOutlined', 'PlusCircleOutlined',
    'PlusSquareOutlined', 'MinusOutlined', 'MinusCircleOutlined', 'MinusSquareOutlined',
    'InfoOutlined', 'InfoCircleOutlined', 'ExclamationOutlined', 'ExclamationCircleOutlined',
    'CloseOutlined', 'CloseCircleOutlined', 'CloseSquareOutlined', 'CheckOutlined',
    'CheckCircleOutlined', 'CheckSquareOutlined'
  ],
  '编辑类图标': [
    'EditOutlined', 'FormOutlined', 'CopyOutlined', 'ScissorOutlined', 'DeleteOutlined',
    'SnippetsOutlined', 'DiffOutlined', 'HighlightOutlined'
  ],
  '数据类图标': [
    'SearchOutlined', 'FilterOutlined', 'TableOutlined', 'BarsOutlined',
    'StarOutlined', 'HeartOutlined'
  ],
  '文件类图标': [
    'FileOutlined', 'FileTextOutlined', 'FileAddOutlined', 'FileExcelOutlined',
    'FileWordOutlined', 'FilePdfOutlined', 'FileImageOutlined', 'FileZipOutlined',
    'FileUnknownOutlined'
  ],
  '其他常用图标': [
    'UserOutlined', 'TeamOutlined', 'SettingOutlined', 'ToolOutlined', 'AppstoreOutlined',
    'CloudOutlined', 'CloudUploadOutlined', 'CloudDownloadOutlined', 'CloudSyncOutlined',
    'ReloadOutlined', 'RedoOutlined', 'UndoOutlined', 'LoginOutlined', 'LogoutOutlined',
    'PoweroffOutlined', 'MenuFoldOutlined', 'MenuUnfoldOutlined', 'FullscreenOutlined',
    'FullscreenExitOutlined', 'EyeOutlined', 'EyeInvisibleOutlined', 'LockOutlined',
    'UnlockOutlined', 'ExportOutlined', 'ImportOutlined', 'SaveOutlined', 'PrinterOutlined',
    'ShareAltOutlined', 'DownloadOutlined', 'UploadOutlined', 'SyncOutlined', 'HomeOutlined',
    'FolderOutlined', 'FolderOpenOutlined', 'CalendarOutlined', 'BellOutlined',
    'MailOutlined', 'PhoneOutlined', 'GlobalOutlined', 'LoadingOutlined'
  ]
}

const searchText = ref('')

// 将图标按分类组织
const groupedIcons = computed(() => {
  const result: Record<string, { name: string; component: any }[]> = {}
  
  Object.entries(iconCategories).forEach(([category, iconNames]) => {
    result[category] = iconNames.map(name => ({
      name,
      component: (Icons as any)[name]
    }))
  })
  
  return result
})

// 过滤图标
const filterIcons = (icons: { name: string; component: any }[]) => {
  if (!searchText.value) return icons
  return icons.filter(icon => 
    icon.name.toLowerCase().includes(searchText.value.toLowerCase())
  )
}

// 格式化图标名称
const formatIconName = (name: string) => {
  return name.replace('Outlined', '')
}

// 搜索图标
const handleSearch = (value: string) => {
  searchText.value = value
}

// 复制图标名称
const handleCopy = (iconName: string) => {
  // 提供两种复制格式：组件标签和导入语句
  const options = [
    { text: `<${iconName} />`, desc: '组件标签' },
    { text: `import { ${iconName} } from '@ant-design/icons-vue'`, desc: '导入语句' }
  ]
  
  const menu = document.createElement('div')
  menu.style.position = 'fixed'
  menu.style.zIndex = '1000'
  menu.style.backgroundColor = '#fff'
  menu.style.boxShadow = '0 2px 8px rgba(0,0,0,0.15)'
  menu.style.borderRadius = '4px'
  menu.style.padding = '4px 0'
  
  options.forEach(({ text, desc }) => {
    const item = document.createElement('div')
    item.style.padding = '8px 12px'
    item.style.cursor = 'pointer'
    item.style.transition = 'all 0.3s'
    item.innerText = desc
    
    item.addEventListener('mouseover', () => {
      item.style.backgroundColor = '#f5f5f5'
    })
    
    item.addEventListener('mouseout', () => {
      item.style.backgroundColor = '#fff'
    })
    
    item.addEventListener('click', () => {
      navigator.clipboard.writeText(text).then(() => {
        message.success(`已复制: ${text}`)
        document.body.removeChild(menu)
      }).catch(() => {
        message.error('复制失败')
      })
    })
    
    menu.appendChild(item)
  })
  
  // 点击其他区域关闭菜单
  const handleClickOutside = (e: MouseEvent) => {
    if (!menu.contains(e.target as Node)) {
      document.body.removeChild(menu)
      document.removeEventListener('click', handleClickOutside)
    }
  }
  
  document.addEventListener('click', handleClickOutside)
  document.body.appendChild(menu)
  
  // 定位菜单
  const event = window.event as MouseEvent
  menu.style.left = `${event.clientX}px`
  menu.style.top = `${event.clientY}px`
}
</script>

<style lang="less" scoped>
.icon-container {
  padding: 24px;
  
  .usage-section {
    margin-bottom: 32px;
    
    h2 {
      margin-bottom: 16px;
      font-size: 20px;
      font-weight: 500;
    }
    
    .usage-list {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 16px;
      
      .usage-item {
        padding: 16px;
        background-color: #fafafa;
        border-radius: 4px;
        
        .usage-title {
          font-weight: 500;
          margin-bottom: 8px;
        }
        
        code {
          display: block;
          padding: 8px;
          background-color: #f5f5f5;
          border-radius: 4px;
          font-family: monospace;
        }
      }
    }
  }
  
  .search-bar {
    margin-bottom: 24px;
    display: flex;
    justify-content: center;
  }
  
  .icon-list {
    .category-title {
      font-size: 18px;
      font-weight: 500;
      margin: 16px 0;
      padding-left: 8px;
      border-left: 4px solid var(--ant-primary-color);
    }
    
    .icon-grid {
      display: grid;
      grid-template-columns: repeat(auto-fill, minmax(120px, 1fr));
      gap: 16px;
      margin-bottom: 32px;
      
      .icon-item {
        display: flex;
        flex-direction: column;
        align-items: center;
        padding: 16px 8px;
        border-radius: 4px;
        cursor: pointer;
        transition: all 0.3s;
        
        &:hover {
          background-color: var(--ant-primary-1);
          transform: translateY(-2px);
        }
        
        .icon {
          font-size: 24px;
          margin-bottom: 8px;
        }
        
        .icon-name {
          font-size: 12px;
          color: rgba(0, 0, 0, 0.65);
          text-align: center;
        }
      }
    }
  }
}
</style> 