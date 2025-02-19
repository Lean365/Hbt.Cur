export default {
  admin: {
    language: {
      title: '语言管理',
      id: '语言ID',
      code: {
        label: '语言编码',
        placeholder: '请输入语言编码',
        validation: {
          required: '语言编码不能为空',
          length: '语言编码长度必须在2-20个字符之间'
        }
      },
      name: {
        label: '语言名称',
        placeholder: '请输入语言名称',
        validation: {
          required: '语言名称不能为空',
          length: '语言名称长度必须在2-30个字符之间'
        }
      },
      icon: {
        label: '语言图标'
      }
    }
  }
} 