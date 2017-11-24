using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TicketingSystem.Cryptography
{
    public static class EncryptionHelper
    {
        public static string EncryptPass(string clearText)
        {
            if (clearText == null || clearText == "")
                return null;

            SHA256 mySHA256 = SHA256Managed.Create();

            byte[] clearTextBytes = Encoding.Unicode.GetBytes(clearText);
            var hashValue = mySHA256.ComputeHash(clearTextBytes);
            var base64HashValue = Convert.ToBase64String(hashValue.ToArray());

            return base64HashValue;
        }
    }
}