import CryptoJS from 'crypto-js'
import { maskPassword, maskCustom } from './mask'

/**
 * 密码加密工具
 * 使用PBKDF2(Password-Based Key Derivation Function 2)算法
 * 实现与后端 Rfc2898DeriveBytes 完全匹配
 */
export class PasswordEncryptor {
  private static readonly SALT_SIZE = 32        // 盐值长度(字节)
  private static readonly HASH_SIZE = 32        // 哈希长度(字节)
  private static readonly DEFAULT_ITERATIONS = 100000   // PBKDF2迭代次数

  /**
   * 生成随机盐值
   * @returns 32字节的随机盐值(Base64编码)
   */
  static generateSalt(): string {
    const salt = CryptoJS.lib.WordArray.random(this.SALT_SIZE)
    return CryptoJS.enc.Base64.stringify(salt)
  }

  /**
   * 使用PBKDF2加密密码
   * 注意：此实现需要与后端的Rfc2898DeriveBytes完全匹配
   */
  static hashPassword(password: string, salt: string, iterations: number = this.DEFAULT_ITERATIONS): string {
    try {
      // console.log('[PBKDF2] 开始加密')
      // console.log('[PBKDF2] 输入密码:', maskPassword(password))
      // console.log('[PBKDF2] 输入盐值:', maskCustom(salt, 8, 8))
      // console.log('[PBKDF2] 迭代次数:', iterations)

      // 1. 将密码转换为UTF8字节数组
      const passwordUtf8 = CryptoJS.enc.Utf8.parse(password)
      // console.log('[PBKDF2] 密码UTF8编码:', maskCustom(passwordUtf8.toString(), 4, 4))

      // 2. 解析Base64盐值
      const saltBytes = CryptoJS.enc.Base64.parse(salt)
      // console.log('[PBKDF2] 盐值解码:', maskCustom(saltBytes.toString(), 4, 4))
      
      // 3. 使用PBKDF2加密
      const key = CryptoJS.PBKDF2(
        passwordUtf8,  // 使用UTF8编码的密码
        saltBytes,     // 使用解码后的盐值
        {
          keySize: this.HASH_SIZE / 4,  // 256位密钥
          iterations: iterations,        // 迭代次数
          hasher: CryptoJS.algo.SHA256  // 使用SHA256
        }
      )
      
      // 4. 转换为Base64
      const result = CryptoJS.enc.Base64.stringify(key)
      // console.log('[PBKDF2] 加密结果:', maskCustom(result, 8, 8))
      
      return result
    } catch (error) {
      // console.error('[PBKDF2] 加密失败:', error)
      throw error
    }
  }

  /**
   * 验证密码
   */
  static verifyPassword(password: string, hashedPassword: string, salt: string, iterations: number = this.DEFAULT_ITERATIONS): boolean {
    try {
      const computedHash = this.hashPassword(password, salt, iterations)
      return computedHash === hashedPassword
    } catch (error) {
      // console.error('[密码验证] 失败:', error)
      return false
    }
  }

  /**
   * 测试前后端加密一致性
   */
  static testEncryption() {
    const testCases = [
      {
        password: '123456',
        salt: '7dkqWD3PCkHX1W9MHvHuDaUDPuFLa5MaHfdH7615tM4=',
        iterations: 100000,
        expectedHash: 'BqIUxc22D7AcIbaPBUr4EHC4zFWzAIhIXjCV0RDetW4='
      }
    ]

    // console.group('=== 前后端加密一致性测试 ===')
    testCases.forEach((testCase, index) => {
      // console.group(`测试用例 ${index + 1}:`)
      // console.log('输入:', {
      //   密码: maskPassword(testCase.password),
      //   盐值: maskCustom(testCase.salt, 8, 8),
      //   迭代次数: testCase.iterations,
      //   期望哈希: maskCustom(testCase.expectedHash, 8, 8)
      // })

      const actualHash = this.hashPassword(testCase.password, testCase.salt, testCase.iterations)
      const isMatch = actualHash === testCase.expectedHash

      // console.log('结果:', {
      //   实际哈希: maskCustom(actualHash, 8, 8),
      //   是否匹配: isMatch
      // })
      // console.groupEnd()
    })
    // console.groupEnd()
  }
}

// 添加全局测试方法
declare global {
  interface Window {
    testPasswordEncryption: () => void;
  }
}

// 暴露测试方法到全局
window.testPasswordEncryption = () => {
  // console.group('=== 密码加密测试 ===')
  
  // 测试1：使用后端的盐值加密
  // console.group('测试1：使用后端的盐值加密')
  const password = '123456'
  const backendSalt = '7dkqWD3PCkHX1W9MHvHuDaUDPuFLa5MaHfdH7615tM4='
  const backendHash = 'BqIUxc22D7AcIbaPBUr4EHC4zFWzAIhIXjCV0RDetW4='

  // 使用后端的盐值进行加密
  const frontendHash = PasswordEncryptor.hashPassword(password, backendSalt)

  // console.log('比对结果：', {
  //   输入密码: maskPassword(password),
  //   后端盐值: maskCustom(backendSalt, 8, 8),
  //   前端计算结果: maskCustom(frontendHash, 8, 8),
  //   后端期望结果: maskCustom(backendHash, 8, 8),
  //   是否匹配: frontendHash === backendHash
  // })
  // console.groupEnd()

  // 测试2：完整加密流程
  // console.group('测试2：完整加密流程')
  // 生成新的盐值
  const newSalt = PasswordEncryptor.generateSalt()
  // 使用新盐值加密
  const newHash = PasswordEncryptor.hashPassword(password, newSalt)
  // console.log('新加密结果：', {
  //   输入密码: maskPassword(password),
  //   生成盐值: maskCustom(newSalt, 8, 8),
  //   加密结果: maskCustom(newHash, 8, 8)
  // })
  // console.groupEnd()

  // 测试3：详细检查中间过程
  // console.group('测试3：详细检查中间过程')
  // 检查密码编码
  const passwordUtf8 = CryptoJS.enc.Utf8.parse(password)
  // console.log('密码UTF8编码：', {
  //   原始密码: maskPassword(password),
  //   UTF8字节: maskCustom(passwordUtf8.toString(), 4, 4),
  //   字节长度: passwordUtf8.sigBytes
  // })
   
  // 检查盐值解码
  const saltBytes = CryptoJS.enc.Base64.parse(backendSalt)
  // console.log('盐值解码：', {
  //   原始盐值: maskCustom(backendSalt, 8, 8),
  //   解码字节: maskCustom(saltBytes.toString(), 4, 4),
  //   字节长度: saltBytes.sigBytes
  // })
  // console.groupEnd()

  // console.groupEnd()
}