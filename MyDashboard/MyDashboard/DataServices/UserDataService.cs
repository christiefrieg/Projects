using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Dashboard.DataServices
{
    public class UserDataService
    {
        public static User CurrentUser
        {
            get
            {
                var eUsername = HttpContext.Current.Session["Username"].ToString();
                var ePassword = HttpContext.Current.Session["Password"].ToString();
                if (eUsername != "" && ePassword != "")
                {
                    var username = DecryptStringAES(eUsername);
                    var password = DecryptStringAES(ePassword);
                    using (var db = new PortfolioEntities())
                    {
                        var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
                        return new User { 
                            Username = user.Username,
                            FirstName = user.FirstName,
                            UserID = user.UserID,
                            LastName = user.LastName,
                            FullName = user.FullName
                        };
                    } 
                }
                else
                    return null;
            }
        }

        public static bool LogInUser(string username, string password)
        {
            var dUsername = DecryptStringAES(username);
            var dPassword = DecryptStringAES(password);
            using(var db = new PortfolioEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == dUsername && u.Password == dPassword);
                if (user != null)
                {
                    HttpContext.Current.Session["Username"] = username.Replace(' ', '+');
                    HttpContext.Current.Session["Password"] = password.Replace(' ', '+');
                    return true;
                }
                else
                    return false;
            }
        }

        private static string DecryptStringAES(string cipherText)
        {
            var keybytes = Encoding.UTF8.GetBytes("4851745953265874");
            var iv = Encoding.UTF8.GetBytes("4851745953265874");
            cipherText = cipherText.Replace(' ', '+');
            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
            return string.Format(decriptedFromJavascript);
        }
        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.  
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }  
    }
}