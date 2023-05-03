using System.Text;

namespace MZ_WorkerService.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetByteArray(this string str)
        {
            return Encoding.GetEncoding("iso-8859-1").GetBytes(str);
        }

        public static string GetString(this byte[] bytes)
        {
            return Encoding.GetEncoding("iso-8859-1").GetString(bytes);
        }
    }
}
