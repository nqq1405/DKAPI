using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace  DK_API 
{
    public static class UtilString
    {
        /// <summary>
        /// Chuyên đổi chuỗi hexa sang byte[]
        /// </summary>
        /// <param name="hex">chuỗi hexa</param>
        /// <returns>mảng byte[]</returns>
        public static byte[] HexaToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}