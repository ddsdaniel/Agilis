using Agilis.Core.Domain.Abstractions.Services;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Agilis.Core.Domain.Services
{
    public class AdvancedEncryptionStandardService: Service, ICriptografiaSimetrica
    {
        private const string SEGREDO = "2DB0DDC3-2254-4011-8B73-DF2BB3EA53C4";

        public string Cifrar(string mensagem, string chaveSecreta)
        {
            if (string.IsNullOrEmpty(mensagem))
            {
                Criticar("Mensagem não deve ser nula");
                return null;
            }

            var aesAlg = NewRijndaelManaged(chaveSecreta);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(mensagem);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        private bool IsBase64String(string base64String)
        {
            base64String = base64String.Trim();
            return (base64String.Length % 4 == 0) &&
                   Regex.IsMatch(base64String, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }

        public string Decifrar(string mensagemCifrada, string chaveSecreta)
        {
            if (string.IsNullOrEmpty(mensagemCifrada))
            {
                Criticar("Mensagem cifrada não deve ser nula");
                return null;
            }

            if (!IsBase64String(mensagemCifrada))
            {
                Criticar("Mensagem cifrada deve ser um texto do tipo Base64");
                return null;
            }

            string text;

            var aesAlg = NewRijndaelManaged(chaveSecreta);
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            var cipher = Convert.FromBase64String(mensagemCifrada);

            try
            {
                using (var msDecrypt = new MemoryStream(cipher))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            text = srDecrypt.ReadToEnd();
                        }
                    }
                }

                return text;
            }
            catch (Exception)
            {
                Criticar("Erro ao decifrar a mensagem cifrada");
                return null;
            }            
        }

        private RijndaelManaged NewRijndaelManaged(string salt)
        {
            if (salt == null)
            {
                Criticar("Salt não deve ser nulo");
                return null;
            }

            var saltBytes = Encoding.ASCII.GetBytes(salt);
            var key = new Rfc2898DeriveBytes(SEGREDO, saltBytes);

            var aesAlg = new RijndaelManaged();
            aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
            aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

            return aesAlg;
        }
    }
}