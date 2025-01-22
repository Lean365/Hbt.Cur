#nullable enable

//===================================================================
// 项目名 : Lean.Hbt
// 文件名 : HbtEncryptUtils.cs
// 创建者 : Lean365
// 创建时间: 2024-01-19 11:00
// 版本号 : V.0.0.1
// 描述    : 加密工具类
//===================================================================

using System.Security.Cryptography;
using System.Text;

namespace Lean.Hbt.Common.Utils
{
    /// <summary>
    /// 加密工具类
    /// </summary>
    public static class HbtEncryptUtils
    {
        private static readonly string Key = "Lean.Hbt.2024.01"; // 16字节的密钥
        private static readonly string IV = "Lean.Hbt.Encrypt"; // 16字节的初始化向量

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns>密文(Base64编码)</returns>
        public static string AesEncrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = Encoding.UTF8.GetBytes(IV);

            using var encryptor = aes.CreateEncryptor();
            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">密文(Base64编码)</param>
        /// <returns>明文</returns>
        public static string AesDecrypt(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = Encoding.UTF8.GetBytes(IV);

            using var decryptor = aes.CreateDecryptor();
            using var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            
            return srDecrypt.ReadToEnd();
        }
    }
} 