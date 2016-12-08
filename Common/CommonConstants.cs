using System;

namespace Common
{
    /// <summary>
    /// Common Constants 
    /// </summary>
    public static class CommonConstants
    {
        public static readonly string ServerFilesLocation = AppDomain.CurrentDomain.BaseDirectory + @"ServerFiles";
        public static readonly string CacheFilesLocation = AppDomain.CurrentDomain.BaseDirectory + @"CacheFiles";
        public static readonly string ClientFilesLocation = AppDomain.CurrentDomain.BaseDirectory + @"ClientFiles";
    }
}