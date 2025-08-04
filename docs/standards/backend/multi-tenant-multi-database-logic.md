# 多租户与多数据库逻辑约束

## 概述

在 Hbt.Cur 系统中，多租户功能和多数据库功能之间存在严格的逻辑约束关系，确保系统配置的正确性和一致性。

## 逻辑约束规则

### 1. 基本约束
- **多租户未启用** → 多数据库必须未启用（使用默认数据库）
- **多租户启用** → 多数据库可以启用或未启用
- **多数据库启用** → 多租户必须启用

### 2. 配置验证
系统在启动时会自动验证配置的正确性：

```csharp
// 多租户和多数据库的逻辑约束
var tenantEnabled = _dbOptions.Tenant.Enabled;
var multiDatabaseEnabled = _dbOptions.Multi;

if (!tenantEnabled && multiDatabaseEnabled)
{
    _logger.Warn("多租户未启用但多数据库已启用，这是不正确的配置。强制禁用多数据库模式。");
    _multiDatabaseEnabled = false;
}
else
{
    _multiDatabaseEnabled = tenantEnabled && multiDatabaseEnabled;
}
```

## 配置示例

### 正确配置示例

#### 1. 多租户启用 + 多数据库启用
```json
{
  "Tenant": {
    "Enabled": true
  },
  "Database": {
    "Multi": true
  },
  "ConnectionStrings": {
    "Default": "Server=...;Database=Hbt_Cur_Dev;...",
    "Tenant": [
      {
        "ConfigId": "tcj",
        "ConnectionString": "Server=...;Database=Hbt_Tcj_Dev;..."
      },
      {
        "ConfigId": "tca", 
        "ConnectionString": "Server=...;Database=Hbt_Tca_Dev;..."
      }
    ]
  }
}
```

#### 2. 多租户启用 + 多数据库未启用
```json
{
  "Tenant": {
    "Enabled": true
  },
  "Database": {
    "Multi": false
  },
  "ConnectionStrings": {
    "Default": "Server=...;Database=Hbt_Cur_Dev;..."
  }
}
```

#### 3. 多租户未启用 + 多数据库未启用
```json
{
  "Tenant": {
    "Enabled": false
  },
  "Database": {
    "Multi": false
  },
  "ConnectionStrings": {
    "Default": "Server=...;Database=Hbt_Cur_Dev;..."
  }
}
```

### 错误配置示例

#### ❌ 多租户未启用 + 多数据库启用（会被自动修正）
```json
{
  "Tenant": {
    "Enabled": false
  },
  "Database": {
    "Multi": true  // 这会被强制设置为 false
  }
}
```

## 运行时行为

### 1. 数据库切换逻辑
所有数据库切换相关的方法都会检查多租户和多数据库的启用状态：

- `SwitchToTenantDatabaseAsync()`
- `SwitchToCurrentTenantDatabaseAsync()`
- `GetTenantConnection()`
- `GetCurrentTenantConnection()`

### 2. 初始化逻辑
- `InitializeTenantDatabasesAsync()` - 只有在多租户和多数据库都启用时才执行
- 种子数据初始化 - 只有在多租户启用时才执行

### 3. 中间件行为
租户中间件会根据配置状态决定是否进行数据库切换：

```csharp
// 检查多租户是否启用
if (!_dbOptions.Tenant.Enabled)
{
    _logger.Debug("多租户功能未启用，使用默认数据库");
    return;
}

if (!_multiDatabaseEnabled)
{
    _logger.Debug("多数据库模式未启用，使用默认数据库");
    return;
}
```

## 最佳实践

### 1. 配置管理
- 始终确保配置的一致性
- 使用配置验证工具检查配置正确性
- 在部署前验证多租户和多数据库的配置

### 2. 开发建议
- 在开发环境中可以禁用多租户功能，简化开发流程
- 在生产环境中根据实际需求启用相应的功能
- 定期检查配置文件的正确性

### 3. 监控和日志
- 关注系统启动时的配置验证日志
- 监控数据库切换的成功率
- 记录多租户相关的操作日志

## 故障排除

### 1. 常见问题
- **问题**: 多数据库功能不生效
- **原因**: 多租户功能未启用
- **解决**: 启用多租户功能或禁用多数据库功能

- **问题**: 租户数据库切换失败
- **原因**: 配置的 ConfigId 与租户代码不匹配
- **解决**: 检查租户种子数据和连接配置的一致性

### 2. 调试方法
1. 检查启动日志中的配置验证信息
2. 验证 `appsettings.json` 中的配置
3. 确认租户种子数据中的 `ConfigId` 设置
4. 检查数据库连接字符串的有效性

## 总结

多租户和多数据库的逻辑约束确保了系统配置的一致性和正确性。系统会自动验证和修正不正确的配置，避免运行时出现意外行为。开发人员应该遵循这些约束规则，确保系统的稳定运行。 