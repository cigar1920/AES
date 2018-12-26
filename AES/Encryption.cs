using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AES
{
     public class Encryption
    {
         public static string Encrypt(String Plaintext, string key, String IV)
        {
            return EncrypText(GetPlainArray(Plaintext), GetKeyArray(key), GetIvByte(IV));
        }
 
        public static string Decrypt(string Ciphertext, string key, String IV)
        {
            return Decryptext(GetCipherArray(Ciphertext), GetKeyArray(key), GetIvByte(IV));
        }
 
 
        //Iv从string类型转化为byte[]类型
        //初始化向量无疑就和口令加密过程中使用的掩值道理类同。
        private static Byte[] GetIvByte(String iv)
        {
            // 16*8 = 128 
            byte[] IVArray = new byte[16];
            //建议不用default
            Array.Copy(Encoding.UTF32.GetBytes(iv.PadRight(IVArray.Length, 'a')), IVArray, IVArray.Length);
            return IVArray;
        }
 
        //K 从string类型转化为byte[]类型
        private static Byte[] GetKeyArray(String key)
        {
            //24*8 = 192  16*8 = 128  32*8 = 256 
            byte[] KeyArray = new byte[32];
            Array.Copy(Encoding.UTF32.GetBytes(key.PadRight(KeyArray.Length, 'a')), KeyArray, KeyArray.Length);
            return KeyArray;
        }
 
        //把要加密的内容 转化为byte类型数据
        private static byte[] GetPlainArray(String Plaintext)
        {
            Byte[] TextArray = Encoding.UTF32.GetBytes(Plaintext);
            return TextArray;
        }
        //把要解密的内容 转化为byte类型数据
        private static byte[] GetCipherArray(String Ciphertext)
        {
            Byte[] Cipher = Convert.FromBase64String(Ciphertext);
            return Cipher;
        }
 
        //加密
        private static string EncrypText(byte[] Plaintext, byte[] key, byte[] IV)
        {
            try
            {
                // RijndaelManaged rijndael = new RijndaelManaged(); 
                Aes aes = new AesManaged();
                //获取或设置对称算法的密钥
                aes.Key = key;
                //获取或设置对称算法的初始化向量 
                aes.IV = IV;
                aes.BlockSize = 128; //运算模式
                aes.Mode = CipherMode.ECB;
                //块补充：要填充的数量= 128 - （数据Plaintext长度 mod 块长度）
                aes.Padding = PaddingMode.ANSIX923;
                // 用当前的 Key 属性和初始化向量 (IV) 创建对称加密器对象。
                // iCryptoTransform 定义加密转换的基本操作。
                ICryptoTransform iCryptoTransform = aes.CreateEncryptor(key, IV);
                //转换指定的字节数组的指定的区域。
                byte[] resultArray = iCryptoTransform.TransformFinalBlock(Plaintext, 0, Plaintext.Length);
                return Convert.ToBase64String (resultArray);
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
 
        //解密
        private static string Decryptext(byte[] Ciphertext, byte[] key, byte[] IV)
        {
            try
            {
                Aes aes = new AesManaged();
                aes.Key = key;
                aes.IV = IV;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.ANSIX923;
                ICryptoTransform cTransform = aes.CreateDecryptor(key, IV);
                byte[] resultArray = cTransform.TransformFinalBlock(Ciphertext, 0, Ciphertext.Length);
                return Encoding.UTF32.GetString(resultArray);
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

    }
}
