using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMSSMI.Extensions.Hashing
{
    public static class Cryptography
    {
        #region Base64Encoding

        /// <summary>
        /// Converts a string of bytes into a string of ASCII characters.
        /// </summary>
        /// <param name="source">The source to encode</param>
        /// <returns></returns>
        public static string Base64Encode(string source)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(source);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Decode a string of ASCII character into text.
        /// </summary>
        /// <param name="source">The source to encode</param>
        /// <returns></returns>
        public static string Base64Decode(string source)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(source);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion
    }
}
