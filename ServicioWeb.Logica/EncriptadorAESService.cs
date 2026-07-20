using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Servicios_Medicos.Services
{
    public class EncriptadorAESServices
    {
        private const string PrefijoGcm = "GCM:";
        private const int TamanoNonce = 12;
        private const int TamanoTag = 16;
        private const int TamanoTagBits = 128;
        private readonly byte[] _clave;

        public EncriptadorAESServices()
        {
            var valor = ConfigurationManager.AppSettings["ClaveAES"];
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new InvalidOperationException("No se encontró la configuración de la clave AES.");
            }

            _clave = Encoding.UTF8.GetBytes(valor);
            if (_clave.Length != 32)
            {
                throw new InvalidOperationException("La clave AES debe tener exactamente 32 bytes.");
            }
        }

        public string Encriptar(string textoPlano)
        {
            if (string.IsNullOrEmpty(textoPlano))
            {
                return string.Empty;
            }

            var nonce = new byte[TamanoNonce];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(nonce);
            }

            var plaintextBytes = Encoding.UTF8.GetBytes(textoPlano);
            var cipher = new GcmBlockCipher(new AesEngine());
            var aeadParameters = new AeadParameters(new KeyParameter(_clave), TamanoTagBits, nonce, null);
            cipher.Init(true, aeadParameters);

            var output = new byte[cipher.GetOutputSize(plaintextBytes.Length)];
            var len = cipher.ProcessBytes(plaintextBytes, 0, plaintextBytes.Length, output, 0);
            len += cipher.DoFinal(output, len);

            var bytes = new byte[TamanoNonce + len];
            Buffer.BlockCopy(nonce, 0, bytes, 0, TamanoNonce);
            Buffer.BlockCopy(output, 0, bytes, TamanoNonce, len);

            return PrefijoGcm + Convert.ToBase64String(bytes);
        }

        public string Desencriptar(string textoCifrado)
        {
            if (string.IsNullOrWhiteSpace(textoCifrado))
            {
                return string.Empty;
            }

            if (EsFormatoGcm(textoCifrado))
            {
                var payload = Convert.FromBase64String(textoCifrado.Substring(PrefijoGcm.Length));
                if (payload.Length < TamanoNonce + TamanoTag)
                {
                    throw new CryptographicException("El texto cifrado GCM es inválido.");
                }

                var nonce = new byte[TamanoNonce];
                Buffer.BlockCopy(payload, 0, nonce, 0, nonce.Length);

                var cifradoConTag = new byte[payload.Length - TamanoNonce];
                Buffer.BlockCopy(payload, TamanoNonce, cifradoConTag, 0, cifradoConTag.Length);

                var cipher = new GcmBlockCipher(new AesEngine());
                var aeadParameters = new AeadParameters(new KeyParameter(_clave), TamanoTagBits, nonce, null);
                cipher.Init(false, aeadParameters);

                var output = new byte[cipher.GetOutputSize(cifradoConTag.Length)];
                var len = cipher.ProcessBytes(cifradoConTag, 0, cifradoConTag.Length, output, 0);
                len += cipher.DoFinal(output, len);
                return Encoding.UTF8.GetString(output, 0, len);
            }

            return DesencriptarLegacyCbc(textoCifrado);
        }

        public bool CompararPassword(string passwordIngresada, string passwordCifradaBD)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(passwordCifradaBD))
                {
                    return false;
                }

                var passwordBD = Desencriptar(passwordCifradaBD);
                return string.Equals(passwordIngresada, passwordBD, StringComparison.Ordinal);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EsFormatoGcm(string textoCifrado)
        {
            return !string.IsNullOrWhiteSpace(textoCifrado) && textoCifrado.StartsWith(PrefijoGcm, StringComparison.OrdinalIgnoreCase);
        }

        private string DesencriptarLegacyCbc(string textoCifradoBase64)
        {
            var datos = Convert.FromBase64String(textoCifradoBase64);
            using (var aes = Aes.Create())
            {
                aes.Key = _clave;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                var iv = new byte[16];
                Array.Copy(datos, 0, iv, 0, iv.Length);
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                using (var ms = new MemoryStream(datos, 16, datos.Length - 16))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}