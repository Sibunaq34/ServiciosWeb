using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Servicios_Medicos.Services
{
    public class EncriptadorAESServices
    {
        private readonly byte[] clave =
            Encoding.UTF8.GetBytes("71962840184936251827825463019826");

        public string Encriptar(string textoPlano)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = clave;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // Genera un IV aleatorio
                aes.GenerateIV();

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                using (MemoryStream ms = new MemoryStream())
                {
                    // Guardar el IV al inicio
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(textoPlano);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public string Desencriptar(string textoCifradoBase64)
        {
            try
            {
                byte[] datos = Convert.FromBase64String(textoCifradoBase64);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = clave;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    // Extraer el IV (16 bytes)
                    byte[] iv = new byte[16];
                    Array.Copy(datos, 0, iv, 0, iv.Length);
                    aes.IV = iv;

                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    using (MemoryStream ms = new MemoryStream(datos, 16, datos.Length - 16))
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            catch
            {
                throw new ArgumentException("Usuario y/o contraseña incorrectos.");
            }
        }

        public bool CompararPassword(string passwordIngresada, string passwordCifradaBD)
        {
            string passwordBD = Desencriptar(passwordCifradaBD);

            return passwordIngresada == passwordBD;
        }
    }
}