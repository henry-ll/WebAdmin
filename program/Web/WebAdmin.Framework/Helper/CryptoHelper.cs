using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using WebAdmin.Framework.Util;

namespace WebAdmin.Framework.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class CryptoHelper : SingleInstance<CryptoHelper>
    {
        #region AES
        //默认AES密钥向量   
        static byte[] _AES_IV = { 0x12, 0x3c, 0xcc, 0x78, 0x90, 0x99, 0xCD, 0x1F, 0x12, 0x34, 0x56, 0xcb, 0x46, 0x10, 0xf1, 0xEF };
        static string _AES_SIV = "ASoib345n1f767gI";
        public static readonly string AesKey = "Jc_t.z9#3mF_2023";

        byte[] CipherData(PaddedBufferedBlockCipher cipher, byte[] data)
        {
            int minSize = cipher.GetOutputSize(data.Length);
            byte[] outBuf = new byte[minSize];
            int length1 = cipher.ProcessBytes(data, 0, data.Length, outBuf, 0);
            int length2 = cipher.DoFinal(outBuf, length1);
            int actualLength = length1 + length2;
            byte[] result = new byte[actualLength];
            Array.Copy(outBuf, 0, result, 0, result.Length);
            return result;
        }

        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="cipher"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public byte[] AesDecrypt(byte[] cipher, string strKey)
        {
            try
            {
                PaddedBufferedBlockCipher aes = new PaddedBufferedBlockCipher(new CfbBlockCipher(new AesEngine(), 128), new Pkcs7Padding());
                var ivAndKey = new ParametersWithIV(new KeyParameter(Encoding.UTF8.GetBytes(strKey)), Encoding.UTF8.GetBytes(_AES_SIV));
                aes.Init(false, ivAndKey);
                return CipherData(aes, cipher);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //微信Unionid解密专用
        public byte[] AesDecrypt(byte[] cipher, byte[] strKey, byte[] iv)
        {
            try
            {
                PaddedBufferedBlockCipher aes = new PaddedBufferedBlockCipher(new CbcBlockCipher(new AesEngine()), new Pkcs7Padding());
                var ivAndKey = new ParametersWithIV(new KeyParameter(strKey), iv);
                aes.Init(false, ivAndKey);
                return CipherData(aes, cipher);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string AesDecrypt(string cipher, string strkey)
        {
            try
            {
                return Encoding.UTF8.GetString(AesDecrypt(Convert.FromBase64String(cipher), strkey));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="plain"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public byte[] AesEncrypt(byte[] plain, string strKey)
        {
            try
            {
                PaddedBufferedBlockCipher aes = new PaddedBufferedBlockCipher(new CfbBlockCipher(new AesEngine(), 128), new Pkcs7Padding());
                var ivAndKey = new ParametersWithIV(new KeyParameter(Encoding.UTF8.GetBytes(strKey)), Encoding.UTF8.GetBytes(_AES_SIV));
                aes.Init(true, ivAndKey);
                return CipherData(aes, plain);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string AesEncrypt(string cipher, string strkey)
        {
            try
            {
                return Convert.ToBase64String(AesEncrypt(Encoding.UTF8.GetBytes(cipher), strkey));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #endregion

        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="plain"></param>
        /// <returns></returns>
        public string Md5Encrypt(string plain)
        {
            var data = Encoding.UTF8.GetBytes(plain);
            return Md5Encrypt(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Md5Encrypt(byte[] data)
        {
            try
            {
                MD5Digest digest = new MD5Digest();
                digest.BlockUpdate(data, 0, data.Length);
                byte[] md5Buf = new byte[digest.GetDigestSize()];
                digest.DoFinal(md5Buf, 0);
                return BitConverter.ToString(md5Buf).Replace("-", "").ToLower();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        #region RSA
        /// <summary>
        /// 生成公钥私钥
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="privatekey"></param>
        public void GetRsaKey(ref string publickey, ref string privatekey)
        {
            //生成密钥对
            var rsaKeyPairGenerator = new RsaKeyPairGenerator();
            var rsaKeyGenerationParameters = new RsaKeyGenerationParameters(BigInteger.ValueOf(3), new SecureRandom(), 512, 25);
            rsaKeyPairGenerator.Init(rsaKeyGenerationParameters);//初始化参数
            var keyPair = rsaKeyPairGenerator.GenerateKeyPair();

            var publicKey = keyPair.Public;//公钥
            var privateKey = keyPair.Private;//私钥
            var subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
            var privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);

            //变字符串
            var asn1ObjectPublic = subjectPublicKeyInfo.ToAsn1Object();
            byte[] publicInfoByte = asn1ObjectPublic.GetEncoded();
            var asn1ObjectPrivate = privateKeyInfo.ToAsn1Object();
            byte[] privateInfoByte = asn1ObjectPrivate.GetEncoded();
            publickey = Convert.ToBase64String(publicInfoByte);
            privatekey = Convert.ToBase64String(privateInfoByte);
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="byteData"></param>
        /// <param name="publickey"></param>
        /// <returns></returns>
        public byte[] RsaEncrypt(byte[] byteData, string publickey)
        {
            var engine = new Pkcs1Encoding(new RsaEngine());
            try
            {
                var pubKey = PublicKeyFactory.CreateKey(Convert.FromBase64String(publickey));
                engine.Init(true, pubKey);
                var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                return ResultData;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="publickey"></param>
        /// <returns></returns>
        public string RsaEncrypt(string strData, string publickey)
        {
            try
            {
                return Convert.ToBase64String(RsaEncrypt(Encoding.UTF8.GetBytes(strData), publickey));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Rsa解密
        /// </summary>
        /// <param name="byteData"></param>
        /// <param name="privatekey"></param>
        /// <returns></returns>
        public byte[] RsaDecrypt(byte[] byteData, string privatekey)
        {
            var engine = new Pkcs1Encoding(new RsaEngine());
            try
            {
                var priKey = PrivateKeyFactory.CreateKey(Convert.FromBase64String(privatekey));
                engine.Init(false, priKey);
                var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                return ResultData;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Rsa解密
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="privatekey"></param>
        /// <returns></returns>
        public string RsaDecrypt(string strData, string privatekey)
        {
            try
            {
                return Encoding.UTF8.GetString(RsaDecrypt(Convert.FromBase64String(strData), privatekey));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #endregion

        /// <summary>
        /// gizp压缩
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public byte[] GetGzipInfo(object info)
        {
            using (var compressStream = new MemoryStream())
            {
                var srcbytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(info));
                using (var zipArchive = new GZipStream(compressStream, CompressionMode.Compress, true))
                {
                    zipArchive.Write(srcbytes, 0, srcbytes.Length);   //压缩     
                }
                return compressStream.ToArray();
            }
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public string UnGzipInfo(byte[] info)
        {
            using (var compressStream = new MemoryStream(info))
            {
                using (var decompressed = new MemoryStream())
                {
                    using (var zipArchive = new GZipStream(compressStream, CompressionMode.Decompress, true))
                    {
                        zipArchive.CopyTo(decompressed);
                    }
                    return Encoding.UTF8.GetString(decompressed.ToArray());
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EncryptText"></param>
        /// <param name="EncryptKey"></param>
        /// <returns></returns>
        public static string HMACSHA1Text(string EncryptText, string EncryptKey)
        {
            //HMACSHA1加密
            System.Security.Cryptography.HMACSHA1 hmacsha1 = new System.Security.Cryptography.HMACSHA1();
            //HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = System.Text.Encoding.UTF8.GetBytes(EncryptKey);
            byte[] dataBuffer = System.Text.Encoding.UTF8.GetBytes(EncryptText);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GenerateRandomSeed()
        {
            byte[] bytes = new byte[4];
            RNGCryptoServiceProvider rngCSP = new RNGCryptoServiceProvider();
            rngCSP.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
