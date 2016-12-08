using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Calculates the sha256 hash.
        /// </summary>
        public static string CalculateSha256Hash(this byte[] value)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] computeHash = hash.ComputeHash(value);
                return string.Join("", computeHash.Select(item => item.ToString("x2")));
            }
        }
    }
}
