using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication_stored_procedure
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["spdb"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    string query = "SELECT EMAIL, PASSWORD FROM REG_TABLE WHERE EMAIL = @EMAIL";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EMAIL", email);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            dr.Read();
                            string eid = dr["EMAIL"].ToString();
                            string encryptedpwd = dr["PASSWORD"].ToString();
                            string decryptedpwd = Decrypted(encryptedpwd);

                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("EMAIL:-- " + eid);
                            sb.AppendLine("PASSWORD:- " + decryptedpwd);
                            

                            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                            {
                                EnableSsl = true,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential("theme0786@gmail.com", "uppt amwg fuka tqtz")
                            };

                            MailMessage msg = new MailMessage
                            {
                                From = new MailAddress("theme0786@gmail.com", "Forgot Password"),
                                Subject = "Your Password",
                                Body = sb.ToString(),
                                IsBodyHtml = false
                            };
                            msg.To.Add(txtEmail.Text);

                            client.Send(msg);
                            Lblmsg.Text = "Your password has been sent to the registered email ID.";
                        }
                        else
                        {
                            Lblmsg.Text = "Invalid Email ID.";
                        }
                    }
                }
            }
        
            catch (Exception ex)
            {
                Lblmsg.Text = "General Error: " + ex.Message;
            }
        }
        private string Decrypted(string encryptedText)
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
                byte[] iv = new byte[aes.BlockSize / 8];
                byte[] ActualEncryptedByte = new byte[encryptedBytes.Length - iv.Length];

                Array.Copy(encryptedBytes, iv, iv.Length);
                Array.Copy(encryptedBytes, iv.Length, ActualEncryptedByte, 0, ActualEncryptedByte.Length);
                aes.IV = iv;
                byte[] decryptedBytes = null;
                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    decryptedBytes = decryptor.TransformFinalBlock(ActualEncryptedByte, 0, ActualEncryptedByte.Length);
                }

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
        protected void btngoback_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
