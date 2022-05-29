using System.Security.Cryptography;

namespace Communication
{
    public class DiffieHellman
    {
        static readonly byte[] salt = new byte[] { 0xf5, 0xf9, 0x5f, 0xc1, 0x41, 0x83, 0xeb, 0xde, 0xdc, 0x3e, 0x50, 0x5d, 0x36, 0x49, 0xe3, 0x4e };

        ECDiffieHellman diffie;

        public byte[] PublicKey { get => diffie.ExportSubjectPublicKeyInfo(); }

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

        public byte[] Encrypt(byte[] data, byte[] publicKey)
        {
            using (Aes aes = Aes.Create())
            {
                var keyBase = new Rfc2898DeriveBytes(GetSharedSecret(publicKey), salt, 65536);
                aes.Key = keyBase.GetBytes(32);
                aes.IV = keyBase.GetBytes(16);

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

        public byte[] Decrypt(byte[] data, byte[] publicKey)
        {
            using (Aes aes = Aes.Create())
            {
                var keyBase = new Rfc2898DeriveBytes(GetSharedSecret(publicKey), salt, 65536);
                aes.Key = keyBase.GetBytes(32);
                aes.IV = keyBase.GetBytes(16);

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
    }
}
