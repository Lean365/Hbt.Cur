# 评论配置说明

## 概述

评论配置用于管理新闻评论系统的各种行为和限制，包括字数限制、审核设置、频率控制等功能。

## 配置项说明

### 基础配置

#### `MaxCommentLength`
- **类型**: `int`
- **默认值**: `1000`
- **说明**: 评论内容最大字数限制
- **取值范围**: `1-10000`
- **示例**: `1000` 表示评论最多1000个字符

#### `MinCommentLength`
- **类型**: `int`
- **默认值**: `1`
- **说明**: 评论内容最小字数限制
- **取值范围**: `1-100`
- **示例**: `1` 表示评论至少1个字符

### 功能开关

#### `EnableSensitiveWordFilter`
- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否启用敏感词过滤
- **作用**: 启用后会自动检测和过滤评论中的敏感词

#### `EnableLengthLimit`
- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否启用字数限制
- **作用**: 启用后会验证评论字数是否符合要求

#### `EnableCommentAudit`
- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否启用评论审核
- **作用**: 启用后评论需要经过审核才能显示

#### `AllowAnonymousComment`
- **类型**: `bool`
- **默认值**: `false`
- **说明**: 是否允许匿名评论
- **作用**: 启用后允许未登录用户发表评论

### 审核配置

#### `DefaultAuditStatus`
- **类型**: `int`
- **默认值**: `0`
- **说明**: 评论默认审核状态
- **取值说明**:
  - `0`: 待审核
  - `1`: 已通过
  - `2`: 已拒绝

### 频率限制

#### `CommentRateLimit`
- **类型**: `int`
- **默认值**: `30`
- **说明**: 评论频率限制（秒）
- **作用**: 同一用户两次评论之间的最小时间间隔
- **示例**: `30` 表示用户需要等待30秒才能发表下一条评论

#### `EnableIpLimit`
- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否启用IP限制
- **作用**: 启用后会限制同一IP地址的评论数量

#### `DailyIpCommentLimit`
- **类型**: `int`
- **默认值**: `50`
- **说明**: 单个IP每日评论数量限制
- **作用**: 限制同一IP地址每天最多发表的评论数量

#### `EnableUserCommentLimit`
- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否启用用户评论数量限制
- **作用**: 启用后会限制单个用户的评论数量

#### `DailyUserCommentLimit`
- **类型**: `int`
- **默认值**: `20`
- **说明**: 单个用户每日评论数量限制
- **作用**: 限制单个用户每天最多发表的评论数量

### 内容检测

#### `EnableDuplicateCheck`
- **类型**: `bool`
- **默认值**: `true`
- **说明**: 是否启用评论内容重复检测
- **作用**: 启用后会检测用户是否发表了重复的评论内容

#### `DuplicateCheckHours`
- **类型**: `int`
- **默认值**: `24`
- **说明**: 重复检测时间范围（小时）
- **作用**: 在指定时间范围内检测重复评论
- **示例**: `24` 表示检测最近24小时内的重复评论

## 配置文件示例

### 开发环境配置 (appsettings.Development.json)

```json
{
  "Comment": {
    "_comment": "评论配置",
    "MaxCommentLength": 1000,
    "MinCommentLength": 1,
    "EnableSensitiveWordFilter": true,
    "EnableLengthLimit": true,
    "EnableCommentAudit": true,
    "DefaultAuditStatus": 0,
    "AllowAnonymousComment": false,
    "CommentRateLimit": 30,
    "EnableIpLimit": true,
    "DailyIpCommentLimit": 50,
    "EnableUserCommentLimit": true,
    "DailyUserCommentLimit": 20,
    "EnableDuplicateCheck": true,
    "DuplicateCheckHours": 24
  }
}
```

### 生产环境配置 (appsettings.json)

```json
{
  "Comment": {
    "_comment": "评论配置",
    "MaxCommentLength": 500,
    "MinCommentLength": 5,
    "EnableSensitiveWordFilter": true,
    "EnableLengthLimit": true,
    "EnableCommentAudit": true,
    "DefaultAuditStatus": 0,
    "AllowAnonymousComment": false,
    "CommentRateLimit": 60,
    "EnableIpLimit": true,
    "DailyIpCommentLimit": 30,
    "EnableUserCommentLimit": true,
    "DailyUserCommentLimit": 10,
    "EnableDuplicateCheck": true,
    "DuplicateCheckHours": 48
  }
}
```

## 不同场景的配置建议

### 宽松模式（开发/测试环境）
```json
{
  "Comment": {
    "MaxCommentLength": 2000,
    "MinCommentLength": 1,
    "EnableSensitiveWordFilter": false,
    "EnableLengthLimit": false,
    "EnableCommentAudit": false,
    "DefaultAuditStatus": 1,
    "AllowAnonymousComment": true,
    "CommentRateLimit": 5,
    "EnableIpLimit": false,
    "DailyIpCommentLimit": 100,
    "EnableUserCommentLimit": false,
    "DailyUserCommentLimit": 50,
    "EnableDuplicateCheck": false,
    "DuplicateCheckHours": 1
  }
}
```

### 严格模式（生产环境）
```json
{
  "Comment": {
    "MaxCommentLength": 500,
    "MinCommentLength": 10,
    "EnableSensitiveWordFilter": true,
    "EnableLengthLimit": true,
    "EnableCommentAudit": true,
    "DefaultAuditStatus": 0,
    "AllowAnonymousComment": false,
    "CommentRateLimit": 120,
    "EnableIpLimit": true,
    "DailyIpCommentLimit": 20,
    "EnableUserCommentLimit": true,
    "DailyUserCommentLimit": 5,
    "EnableDuplicateCheck": true,
    "DuplicateCheckHours": 72
  }
}
```

### 中等模式（一般生产环境）
```json
{
  "Comment": {
    "MaxCommentLength": 1000,
    "MinCommentLength": 5,
    "EnableSensitiveWordFilter": true,
    "EnableLengthLimit": true,
    "EnableCommentAudit": true,
    "DefaultAuditStatus": 0,
    "AllowAnonymousComment": false,
    "CommentRateLimit": 60,
    "EnableIpLimit": true,
    "DailyIpCommentLimit": 30,
    "EnableUserCommentLimit": true,
    "DailyUserCommentLimit": 15,
    "EnableDuplicateCheck": true,
    "DuplicateCheckHours": 24
  }
}
```

## 配置验证

系统会在启动时验证配置的有效性：

1. **字数限制验证**: `MinCommentLength` 必须小于等于 `MaxCommentLength`
2. **时间限制验证**: `CommentRateLimit` 必须大于0
3. **数量限制验证**: 各种限制数量必须大于0
4. **时间范围验证**: `DuplicateCheckHours` 必须在合理范围内

## 动态配置

评论配置支持运行时动态修改，修改后需要重启应用程序才能生效。

## 注意事项

1. **性能考虑**: 启用过多的检测功能可能会影响系统性能
2. **用户体验**: 过于严格的限制可能会影响用户体验
3. **安全平衡**: 需要在安全性和便利性之间找到平衡
4. **监控建议**: 建议监控评论相关的系统指标，及时调整配置

## 相关API

评论配置相关的API接口：

- `GET /api/routine/document/news-comment-audit/statistics` - 获取审核统计
- `GET /api/routine/document/news-comment-audit/pending` - 获取待审核列表
- `POST /api/routine/document/news-comment-audit/audit` - 审核评论
- `POST /api/routine/document/news-comment-audit/batch-audit` - 批量审核

## 故障排除

### 常见问题

1. **评论无法发表**: 检查字数限制和频率限制配置
2. **审核不生效**: 确认 `EnableCommentAudit` 设置为 `true`
3. **敏感词过滤不工作**: 确认 `EnableSensitiveWordFilter` 设置为 `true`
4. **重复检测过于严格**: 调整 `DuplicateCheckHours` 参数

### 日志查看

评论相关的日志会记录在以下位置：
- 审核操作日志
- 敏感词过滤日志
- 频率限制日志
- 错误日志 