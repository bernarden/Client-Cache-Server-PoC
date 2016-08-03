﻿using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Server.Common
{
    public static class StringExtensions
    {
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
