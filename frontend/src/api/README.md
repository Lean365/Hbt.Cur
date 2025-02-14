# API接口目录

此目录用于存放所有的API接口定义文件。

## 文件命名规范
- 按照业务模块进行分类，例如：`user.ts`, `product.ts`
- 使用camelCase命名方式

## 接口命名规范
- 获取数据：getXxx
- 创建数据：createXxx
- 更新数据：updateXxx
- 删除数据：deleteXxx
- 批量操作：batchXxx

## 示例
```typescript
// user.ts
export const userApi = {
  login: (data: LoginParams) => request.post('/auth/login', data),
  getUserInfo: () => request.get('/user/info'),
  updateUser: (data: UserInfo) => request.put('/user/update', data)
}
``` 