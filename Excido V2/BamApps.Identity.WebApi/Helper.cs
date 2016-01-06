using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace BamApps.Identity.WebApi {
    public class Helper {
        public static string GetHash(string input) {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static string CreateStringId() {
            return Guid.NewGuid().ToString("N");
        }

        public static string CreateNewSecret(int length = 32) {
            var key = new byte[length];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);
            return base64Secret;
        }
    }
}