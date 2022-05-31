using System.IO.Compression;
using System.Security.Cryptography;

namespace Communication
{
    public class DiffieHellman
    {
        public struct AesKeys
        {
            public byte[] Key { get; set; }

            public byte[] IV { get; set; }

            public AesKeys(byte[] key, byte[] iv)
            {
                Key = key;
                IV = iv;
            }
        }
        
        static readonly byte[] salt = new byte[] { 0xf5, 0xf9, 0x5f, 0xc1, 0x41, 0x83, 0xeb, 0xde, 0xdc, 0x3e, 0x50, 0x5d, 0x36, 0x49, 0xe3, 0x4e };

        ECDiffieHellman diffie;

        public byte[] PublicKey { get => diffie.ExportSubjectPublicKeyInfo(); }

        public static byte[] Handshake
        {
            get => new byte[] { 0x05, 0x07, 0x07, 0x07, 0x07, 0x16, 0x16, 0x06 };
        }

        public static int PublicKeyLength
        {
            get => 158;
        }

        public DiffieHellman()
        {
            diffie = ECDiffieHellman.Create();
        }

        public byte[] GetSharedSecret(byte[] publicKey)
        {
            using (ECDiffieHellman hellman = ECDiffieHellman.Create())
            {
                hellman.ImportSubjectPublicKeyInfo(publicKey, out _);
                return diffie.DeriveKeyMaterial(hellman.PublicKey);
            }
        }

        public byte[] Encrypt(byte[] data, AesKeys keys)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = keys.Key;
                aes.IV = keys.IV;

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                    }

                    return ms.ToArray();
                }
            }
        }

        public byte[] Decrypt(byte[] data, AesKeys keys)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = keys.Key;
                aes.IV = keys.IV;

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                         cs.Write(data, 0, data.Length);
                    }

                    return ms.ToArray();
                }
            }
        }

        public AesKeys GenerateKeys(byte[] publicKey)
        {
            var keyBase = new Rfc2898DeriveBytes(GetSharedSecret(publicKey), salt, 65536);
            return new AesKeys(keyBase.GetBytes(32), keyBase.GetBytes(16));
        }
    }
}
