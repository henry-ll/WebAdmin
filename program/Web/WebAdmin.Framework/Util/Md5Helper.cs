using System;
using System.Security.Cryptography;
using System.Text;

namespace WebAdmin.Framework.Util
{
    /// <summary>
    /// MD5加密帮助类
    /// </summary>
    public class Md5Helper
    {
        /// <summary>
        /// MD5加密大写
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string MD5(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
                strEncrypt = GetMd5Hash(str).Substring(8, 16);
            if (code == 32)
                strEncrypt = GetMd5Hash(str);
            return strEncrypt;
        }
        /// <summary>
        /// MD5加密小写
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string MD5Lower(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
                strEncrypt = GetMd5Hash(str).Substring(8, 16).ToLower();
            if (code == 32)
                strEncrypt = GetMd5Hash(str).ToLower();
            return strEncrypt;
        }
        /// <summary>
        /// MD5小写加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string MD5SmallLower(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
                strEncrypt = GetMd5Hash(str.ToLower()).Substring(8, 16);
            if (code == 32)
                strEncrypt = GetMd5Hash(str.ToLower());
            return strEncrypt;
        }
        /// <summary>
        /// 加密MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            MD5 m = new MD5CryptoServiceProvider();
            byte[] s = m.ComputeHash(Encoding.UTF8.GetBytes(str));
            string md5Str = BitConverter.ToString(s).Replace("-", "");
            return md5Str;
        }
        public static string GetMd5Hash(string input)
        {
            MD5 md5Hash = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }
    }
}
