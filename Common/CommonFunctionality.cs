using System;
using System.IO;

namespace Common
{
    /// <summary>
    /// Common functionality
    /// </summary>
    public static class CommonFunctionality
    {
        /// <summary>
        /// Checks if file exists and then returns memory stream of the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        public static Stream GetMemoryStreamOfTheFile(string filePath)
        {
            if (!DoesFileExist(filePath)) throw new FileNotFoundException();

            using (FileStream fileStream = File.OpenRead(filePath))
            {
                MemoryStream memStream = new MemoryStream();
                memStream.SetLength(fileStream.Length);

                fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);
                return memStream;
            }
        }

        /// <summary>
        /// Checks if file exist.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public static bool DoesFileExist(string filePath)
        {
            return File.Exists(filePath);
        }


        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }


        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
