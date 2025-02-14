# 静态资源目录

此目录用于存放项目的静态资源文件。

## 目录结构
- icons/：图标文件目录
  - SVG图标
  - 字体图标
- images/：图片资源目录
  - 背景图片
  - 业务图片
  - Logo等
- styles/：样式文件目录
  - 全局样式
  - 主题样式
  - 变量定义
  - 混入(mixins)

## 命名规范
- 图标文件：使用kebab-case，例如：`user-avatar.svg`
- 图片文件：使用kebab-case，例如：`login-background.png`
- 样式文件：使用kebab-case，例如：`main-theme.less`

## 注意事项
1. 图片资源应进行适当压缩
2. SVG图标应优化以减小文件体积
3. 样式文件应遵循BEM命名规范
4. 避免存放过大的媒体文件 