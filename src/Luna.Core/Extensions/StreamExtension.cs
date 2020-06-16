using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Luna.Extensions
{
    public static class StreamExtension
    {
        public static byte[] ToByteArray(this Stream @this)
        {
            using var ms = new MemoryStream();
            @this.CopyTo(ms);
            return ms.ToArray();
        }

        public static string ToMd5Hash(this Stream @this)
        {
            using var md5 = MD5.Create();
            var hashBytes = md5.ComputeHash(@this);
            var sb = new StringBuilder();
            foreach (var bytes in hashBytes)
            {
                sb.Append(bytes.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}