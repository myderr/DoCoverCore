using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace DoCover.Common
{
    public class RsaForJava
    {

        public RsaForJava()
        {

        }
        /// <summary>
        /// KEY 结构体
        /// </summary>
        public struct Rsakey
        {
            /// <summary>
            /// 公钥
            /// </summary>
            public string PublicKey
            {
                get;
                set;
            }
            /// <summary>
            /// 私钥
            /// </summary>
            public string PrivateKey
            {
                get;
                set;
            }
        }
        
        /// <summary>
        /// 获取密钥对
        /// </summary>
        /// <returns></returns>
        public Rsakey GetKey()
        {
            //RSA密钥对的构造器  
            RsaKeyPairGenerator keyGenerator = new RsaKeyPairGenerator();

            //RSA密钥构造器的参数  
            RsaKeyGenerationParameters param = new RsaKeyGenerationParameters(
                Org.BouncyCastle.Math.BigInteger.ValueOf(3),
                new Org.BouncyCastle.Security.SecureRandom(),
                1024,   //密钥长度  
                25);
            //用参数初始化密钥构造器  
            keyGenerator.Init(param);
            //产生密钥对  
            AsymmetricCipherKeyPair keyPair = keyGenerator.GenerateKeyPair();
            //获取公钥和密钥  
            AsymmetricKeyParameter publicKey = keyPair.Public;
            AsymmetricKeyParameter privateKey = keyPair.Private;

            SubjectPublicKeyInfo subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
            PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);


            Asn1Object asn1ObjectPublic = subjectPublicKeyInfo.ToAsn1Object();

            byte[] publicInfoByte = asn1ObjectPublic.GetEncoded("UTF-8");
            Asn1Object asn1ObjectPrivate = privateKeyInfo.ToAsn1Object();
            byte[] privateInfoByte = asn1ObjectPrivate.GetEncoded("UTF-8");

            Rsakey item = new Rsakey()
            {
                PublicKey = Convert.ToBase64String(publicInfoByte),
                PrivateKey = Convert.ToBase64String(privateInfoByte)
            };
            return item;
        }

        /// <summary>
        /// 公钥参数
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private AsymmetricKeyParameter GetPublicKeyParameter(string s)
        {
            s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("-----BEGINPUBLICKEY-----", "").Replace("-----ENDPUBLICKEY-----", "");
            byte[] publicInfoByte = Convert.FromBase64String(s);
            Asn1Object pubKeyObj = Asn1Object.FromByteArray(publicInfoByte);//这里也可以从流中读取，从本地导入   
            AsymmetricKeyParameter pubKey = PublicKeyFactory.CreateKey(publicInfoByte);
            return pubKey;
        }
        /// <summary>
        /// 私钥参数
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private AsymmetricKeyParameter GetPrivateKeyParameter(string s)
        {
            s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "").Replace("-----BEGINRSAPRIVATEKEY-----", "").Replace("-----ENDRSAPRIVATEKEY-----", "");
            byte[] privateInfoByte = Convert.FromBase64String(s);
            // Asn1Object priKeyObj = Asn1Object.FromByteArray(privateInfoByte);//这里也可以从流中读取，从本地导入   
            // PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKey);
            AsymmetricKeyParameter priKey = PrivateKeyFactory.CreateKey(privateInfoByte);
            return priKey;
        }

        /// <summary>
        /// 私钥加密
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string EncryptByPrivateKey(string s, string key)
        {
            //非对称加密算法，加解密用  
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());
            //加密
            try
            {
                engine.Init(true, GetPrivateKeyParameter(key));
                byte[] byteData = System.Text.Encoding.UTF8.GetBytes(s);
                var resultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                return Convert.ToBase64String(resultData);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        /// <summary>
        /// 公钥加密
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string EncryptByPublicKey(string s, string key)
        {
            //非对称加密算法，加解密用  
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());
            //加密
            try
            {
                engine.Init(true, GetPublicKeyParameter(key));
                byte[] byteData = System.Text.Encoding.UTF8.GetBytes(s);
                var resultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                return Convert.ToBase64String(resultData);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 私钥解密
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string DecryptByPrivateKey(string s, string key)
        {
            s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            //非对称加密算法，加解密用  
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());
            //解密  
            try
            {
                engine.Init(false, GetPrivateKeyParameter(key));
                byte[] byteData = Convert.FromBase64String(s);
                var resultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                return System.Text.Encoding.UTF8.GetString(resultData);
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        /// <summary>
        /// 公钥解密
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string DecryptByPublicKey(string s, string key)
        {
            s = s.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            //非对称加密算法，加解密用  
            IAsymmetricBlockCipher engine = new Pkcs1Encoding(new RsaEngine());
            //解密  
            try
            {
                engine.Init(false, GetPublicKeyParameter(key));
                byte[] byteData = Convert.FromBase64String(s);
                var ResultData = engine.ProcessBlock(byteData, 0, byteData.Length);
                return System.Text.Encoding.UTF8.GetString(ResultData);
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}
