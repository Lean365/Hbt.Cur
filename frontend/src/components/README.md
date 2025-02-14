# 公共组件目录

此目录用于存放可复用的Vue组件。

## 组件分类
- 基础组件（base/）：最基础的组件，如按钮、输入框等
- 业务组件（business/）：特定业务相关的可复用组件
- 布局组件（layout/）：页面布局相关的组件

## 命名规范
- 组件文件：使用PascalCase
  - `UserAvatar.vue`
  - `DataTable.vue`
  - `SearchForm.vue`
- 组件名称：使用PascalCase
```typescript
export default {
  name: 'UserAvatar',
  // ...
}
```

## 组件规范
1. 每个组件应该有完整的TypeScript类型定义
2. Props必须定义类型和默认值
3. 组件应该有完整的注释说明
4. 复杂组件应该有使用示例

## 示例结构
```
components/
├── base/
│   ├── BaseButton.vue
│   ├── BaseInput.vue
│   └── BaseSelect.vue
├── business/
│   ├── UserList.vue
│   └── OrderForm.vue
└── layout/
    ├── PageHeader.vue
    └── SideMenu.vue
``` 