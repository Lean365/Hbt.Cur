# 代码风格规范

## 1. 文件组织

- 每个文件只包含一个主要的类/接口
- 文件顺序:
  1. using 指令
  2. 命名空间声明
  3. 类型声明
- using 指令按以下顺序排列:
  1. .NET Framework命名空间
  2. 第三方命名空间
  3. 项目命名空间
  4. 静态using

## 2. 命名空间组织

- 按照项目结构组织命名空间
- 遵循以下模式:
  ```csharp
  namespace Lean.Hbt.{Layer}.{Module}[.{SubModule}]
  ```
- 示例:
  ```csharp
  namespace Lean.Hbt.Domain.Entities.Identity
  namespace Lean.Hbt.Application.Services.System
  namespace Lean.Hbt.Infrastructure.Repositories
  ```

## 3. 类组织

- 按以下顺序组织类成员:
  1. 常量
  2. 字段
  3. 构造函数
  4. 属性
  5. 方法
  6. 嵌套类型
- 按访问级别分组:
  1. public
  2. internal
  3. protected
  4. private
- 相关的成员应该放在一起

## 4. 代码格式化

- 使用4个空格进行缩进
- 大括号单独成行
- 每行代码不超过120个字符
- 方法之间空一行
- 相关的代码块之间空一行
- 运算符两边加空格
- 逗号后面加空格
- 括号内部不加空格

## 5. 注释规范

- 所有公共成员必须有XML注释
- 复杂的私有方法应该有注释
- 注释应该说明"为什么"而不是"是什么"
- 避免无意义的注释
- 保持注释的及时更新
- 使用//进行单行注释
- 使用/* */进行多行注释

## 6. 编码实践

- 使用var时机:
  - 当类型明显时使用var
  - 当类型复杂时使用var
  - 当使用new关键字时使用var
- 字符串处理:
  - 使用string.Empty而不是""
  - 使用字符串内插而不是+连接
  - 大量字符串拼接使用StringBuilder
- 异常处理:
  - 只捕获特定异常
  - 总是重新抛出或记录异常
  - 避免空catch块
- 资源处理:
  - 使用using语句处理IDisposable
  - 显式调用Dispose方法
- 异步编程:
  - 使用async/await而不是Task.Wait
  - 方法名以Async结尾
  - 避免async void
  - 正确处理异常

## 7. LINQ使用规范

- 优先使用方法语法而不是查询语法
- 避免在循环中使用LINQ
- 合理使用延迟执行
- 需要多次使用结果时调用ToList/ToArray
- 按照可读性选择使用方法链或多行

## 8. 性能考虑

- 合理使用字符串操作
- 避免装箱和拆箱
- 合理使用集合类型
- 避免不必要的对象创建
- 正确使用异步操作
- 合理使用缓存

## 9. 安全性

- 正确处理敏感数据
- 使用安全的字符串比较
- 避免SQL注入
- 避免跨站脚本攻击
- 正确处理用户输入
- 使用安全的加密方法 