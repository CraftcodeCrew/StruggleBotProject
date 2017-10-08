using System;
using System.Security.Cryptography;
using System.Text;

namespace StruggleApplication.framework
{
    public class GuidConverter
    {
        public static Guid CreateGuidFromString(String s)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(s));
            Guid result = new Guid(hash);
            return result;
        }
    }
}