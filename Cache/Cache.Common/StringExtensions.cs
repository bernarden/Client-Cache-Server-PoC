using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cache.Common
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Calculates the sha256 hash.
        /// </summary>
        public static string CalculateSha256Hash(this string value)
        {
            using (SHA256 hash = SHA256.Create())
            {
                return string.Join("", hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }
    }
}
