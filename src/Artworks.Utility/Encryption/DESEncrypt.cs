using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Artworks.Utility.Encryption
{
    /// <summary>
    /// DES加密类。
    /// </summary>
    public class DESEncrypt
    {
        // Fields
        private static byte[] iv = Encoding.UTF8.GetBytes("12345678");

        // Methods
        public static string Decrypt(string data, string key)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            int num = data.Length / 2;
            byte[] buffer = new byte[num];
            for (int i = 0; i < num; i++)
            {
                int num3 = Convert.ToInt32(data.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte)num3;
            }
            try
            {
                provider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
                provider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                return Encoding.Default.GetString(stream.ToArray());
            }
            catch
            {
                return data;
            }
        }


        public static string Encrypt(string data, string key)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(data);
            provider.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            provider.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            return builder.ToString();
        }

        public static string Decrypt3DES(string data, string key)
        {
            string str;
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key));
            provider.Mode = CipherMode.ECB;
            ICryptoTransform transform = provider.CreateDecryptor();
            try
            {
                byte[] inputBuffer = Convert.FromBase64String(data);
                str = Encoding.UTF8.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch (Exception exception)
            {
                throw new Exception("无效的密钥或解密串不是有效的base64串", exception);
            }
            return str;
        }
        public static string Encrypt3DES(string data, string key)
        {
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(key));
            provider.Mode = CipherMode.ECB;
            ICryptoTransform transform = provider.CreateEncryptor();
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(transform.TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public static string Des3DecodeCBC(string data, string key)
        {
            try
            {
                byte[] buffer = Convert.FromBase64String(data);
                MemoryStream stream = new MemoryStream(buffer);
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;
                provider.Key = Encoding.UTF8.GetBytes(key);
                provider.IV = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] bytes = Encoding.UTF8.GetBytes(key);
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Read);
                byte[] buffer3 = new byte[buffer.Length];
                stream2.Read(buffer3, 0, buffer3.Length);
                return Encoding.UTF8.GetString(buffer3);
            }
            catch (CryptographicException exception)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", exception.Message);
                return null;
            }
        }

        public static string Des3EncodeCBC(string data, string key)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;
                provider.Key = Encoding.UTF8.GetBytes(key);
                provider.IV = Encoding.UTF8.GetBytes(key.Substring(0, 8));
                byte[] bytes = Encoding.UTF8.GetBytes(key);
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                byte[] inArray = stream.ToArray();
                stream2.Close();
                stream.Close();
                return Convert.ToBase64String(inArray);
            }
            catch (CryptographicException exception)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", exception.Message);
                return null;
            }
        }

    }
}
