using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using iTextSharp;
using iTextSharp.text.pdf;

namespace WebApplication_stored_procedure
{
    public partial class password : System.Web.UI.Page
    {
        protected void OnEncrypt(object sender, EventArgs e)
        {
            lblEncrypted.Text = this.Encrypt(txtPlain.Text.Trim());
        }

        private string Encrypt(string plainText)
        {
            //Secret Key.
            string secretKey = "$ASPcAwSNIgcPPEoTS78ODw#";

            //Secret Bytes.
            byte[] secretBytes = Encoding.UTF8.GetBytes(secretKey);

            //Plain Text Bytes.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes;

            //Encrypt with AES Alogorithm using Secret Key.
            using (Aes aes = Aes.Create())
            {
                aes.Key = secretBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                //Genrate IV
                aes.GenerateIV();
                byte[] iv = aes.IV;

                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key,iv))
                {
                    encryptedBytes = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
                }
                byte[] CombinedBytes = new byte[iv.Length +encryptedBytes.Length];
                Array.Copy(iv, 0, CombinedBytes, 0, iv.Length);
                Array.Copy(encryptedBytes, 0, CombinedBytes, iv.Length,encryptedBytes.Length);
                
                return Convert.ToBase64String(CombinedBytes);
            }
        }
        protected void OnDecrypt(object sender, EventArgs e)
        {
            lblDecrypted.Text = this.Decrypt(lblEncrypted.Text.Trim());
        }

        private string Decrypt(string encryptedText)
        {
            //Secret Key.
            string secretKey = "$ASPcAwSNIgcPPEoTS78ODw#";

            //Secret Bytes.
            byte[] secretBytes = Encoding.UTF8.GetBytes(secretKey);

            //Encrypted Bytes.
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            //Decrypt with AES Alogorithm using Secret Key.
            using (Aes aes = Aes.Create())
            {
                aes.Key = secretBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                byte[] iv = new byte[aes.BlockSize/8];
                byte[] ActualEncryptedByte = new byte[encryptedBytes.Length - iv.Length];

                Array.Copy(encryptedBytes, iv, iv.Length);
                Array.Copy(encryptedBytes, iv.Length,ActualEncryptedByte,0,ActualEncryptedByte.Length);
                aes.IV = iv;
                byte[] decryptedBytes = null;
                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    decryptedBytes = decryptor.TransformFinalBlock(ActualEncryptedByte,0,ActualEncryptedByte.Length);
                }

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}