# OAuth第三方登录配置指南

## 概述

本系统支持多种第三方登录提供商，包括GitHub、Google、Facebook、Twitter、QQ、微信、支付宝、Gitee等。每个提供商都可以独立配置和启用。

## 配置文件结构

### appsettings.json 配置示例

```json
{
  "HbtOAuth": {
    "Enabled": true,
    "DefaultRedirectUri": "https://your-app.com/oauth/callback",
    "SessionTimeoutMinutes": 30,
    "GitHub": {
      "Enabled": true,
      "ClientId": "your-github-client-id",
      "ClientSecret": "your-github-client-secret",
      "RedirectUri": "https://your-app.com/oauth/github/callback",
      "Scope": "read:user,user:email"
    },
    "Google": {
      "Enabled": true,
      "ClientId": "your-google-client-id",
      "ClientSecret": "your-google-client-secret",
      "RedirectUri": "https://your-app.com/oauth/google/callback",
      "Scope": "openid email profile"
    },
    "Facebook": {
      "Enabled": false,
      "AppId": "your-facebook-app-id",
      "AppSecret": "your-facebook-app-secret",
      "RedirectUri": "https://your-app.com/oauth/facebook/callback",
      "Scope": "email,public_profile"
    },
    "Twitter": {
      "Enabled": false,
      "ApiKey": "your-twitter-api-key",
      "ApiKeySecret": "your-twitter-api-secret",
      "RedirectUri": "https://your-app.com/oauth/twitter/callback",
      "Scope": "tweet.read users.read offline.access"
    },
    "QQ": {
      "Enabled": false,
      "AppId": "your-qq-app-id",
      "AppKey": "your-qq-app-key",
      "RedirectUri": "https://your-app.com/oauth/qq/callback",
      "Scope": "get_user_info"
    },
    "WeChat": {
      "Enabled": true,
      "AppId": "your-wechat-app-id",
      "AppSecret": "your-wechat-app-secret",
      "RedirectUri": "https://your-app.com/oauth/wechat/callback",
      "Scope": "snsapi_login"
    },
    "Alipay": {
      "Enabled": true,
      "AppId": "your-alipay-app-id",
      "PrivateKey": "your-alipay-private-key",
      "PublicKey": "your-alipay-public-key",
      "RedirectUri": "https://your-app.com/oauth/alipay/callback",
      "Scope": "auth_user"
    },
    "Gitee": {
      "Enabled": false,
      "ClientId": "your-gitee-client-id",
      "ClientSecret": "your-gitee-client-secret",
      "RedirectUri": "https://your-app.com/oauth/gitee/callback",
      "Scope": "user_info emails"
    }
  },
  "HbtQrCode": {
    "ExpirationMinutes": 5,
    "PixelsPerModule": 20,
    "EccLevel": "M",
    "BaseUrl": "https://your-app.com/qr",
    "EnableThirdPartyLogin": true,
    "EnableWeChatLogin": true,
    "EnableAlipayLogin": true
  }
}
```

## 各提供商配置说明

### GitHub登录

1. 在GitHub开发者设置中创建OAuth应用
2. 设置回调地址为：`https://your-app.com/oauth/github/callback`
3. 获取Client ID和Client Secret
4. 配置授权范围：`read:user,user:email`

### Google登录

1. 在Google Cloud Console中创建OAuth 2.0客户端
2. 设置授权重定向URI为：`https://your-app.com/oauth/google/callback`
3. 获取Client ID和Client Secret
4. 配置授权范围：`openid email profile`

### Facebook登录

1. 在Facebook开发者平台创建应用
2. 设置OAuth重定向URI为：`https://your-app.com/oauth/facebook/callback`
3. 获取App ID和App Secret
4. 配置授权范围：`email,public_profile`

### Twitter登录

1. 在Twitter开发者平台创建应用
2. 设置回调URL为：`https://your-app.com/oauth/twitter/callback`
3. 获取API Key和API Key Secret
4. 配置授权范围：`tweet.read users.read offline.access`

### QQ登录

1. 在QQ互联平台创建应用
2. 设置回调地址为：`https://your-app.com/oauth/qq/callback`
3. 获取App ID和App Key
4. 配置授权范围：`get_user_info`

### 微信登录

1. 在微信开放平台创建网站应用
2. 设置授权回调域为：`your-app.com`
3. 获取App ID和App Secret
4. 配置授权范围：`snsapi_login`

### 支付宝登录

1. 在支付宝开放平台创建应用
2. 设置授权回调地址为：`https://your-app.com/oauth/alipay/callback`
3. 获取App ID、应用私钥和支付宝公钥
4. 配置授权范围：`auth_user`

### Gitee登录

1. 在Gitee开发者设置中创建应用
2. 设置回调地址为：`https://your-app.com/oauth/gitee/callback`
3. 获取Client ID和Client Secret
4. 配置授权范围：`user_info emails`

## 二维码登录配置

二维码登录支持微信和支付宝扫码登录，需要同时配置OAuth选项和二维码选项：

```json
{
  "HbtQrCode": {
    "EnableWeChatLogin": true,
    "EnableAlipayLogin": true
  }
}
```

## 安全注意事项

1. **密钥安全**：所有Client Secret、App Secret、Private Key等敏感信息应存储在环境变量或密钥管理系统中
2. **HTTPS**：生产环境必须使用HTTPS协议
3. **回调地址验证**：确保回调地址的域名与配置一致
4. **授权范围**：只请求必要的授权范围，避免过度授权
5. **状态参数**：使用state参数防止CSRF攻击

## 环境变量配置

为了安全起见，建议将敏感信息存储在环境变量中：

```bash
# GitHub
export GITHUB_CLIENT_ID="your-github-client-id"
export GITHUB_CLIENT_SECRET="your-github-client-secret"

# Google
export GOOGLE_CLIENT_ID="your-google-client-id"
export GOOGLE_CLIENT_SECRET="your-google-client-secret"

# 微信
export WECHAT_APP_ID="your-wechat-app-id"
export WECHAT_APP_SECRET="your-wechat-app-secret"

# 支付宝
export ALIPAY_APP_ID="your-alipay-app-id"
export ALIPAY_PRIVATE_KEY="your-alipay-private-key"
export ALIPAY_PUBLIC_KEY="your-alipay-public-key"
```

然后在配置文件中引用：

```json
{
  "HbtOAuth": {
    "GitHub": {
      "ClientId": "${GITHUB_CLIENT_ID}",
      "ClientSecret": "${GITHUB_CLIENT_SECRET}"
    }
  }
}
```

## 测试配置

1. 确保所有启用的提供商配置正确
2. 测试授权流程是否正常
3. 验证回调处理是否正确
4. 检查用户信息获取是否成功
5. 测试二维码登录功能（如果启用）

## 故障排除

### 常见问题

1. **回调地址不匹配**：确保回调地址与第三方平台配置一致
2. **授权范围错误**：检查授权范围是否与第三方平台要求一致
3. **密钥错误**：验证Client Secret、App Secret等是否正确
4. **HTTPS要求**：某些提供商要求必须使用HTTPS
5. **域名验证**：某些提供商需要验证域名所有权

### 日志调试

启用详细日志来调试OAuth流程：

```json
{
  "Logging": {
    "LogLevel": {
      "Lean.Hbt.Application.Services.Identity": "Debug"
    }
  }
}
``` 