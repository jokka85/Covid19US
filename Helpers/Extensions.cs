using System.IO;
using System.Text.RegularExpressions;

namespace Covid19Texas.Helpers
{
    public static class Extensions
    {

        /// <summary>
        /// Returns the string file name with application path
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ToApplicationPath(this string fileName)
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return Path.Combine(appRoot, fileName);
        }

    }
}
