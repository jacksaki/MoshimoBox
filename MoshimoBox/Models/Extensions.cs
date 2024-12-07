using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MoshimoBox.Models
{
    public static class Extensions
    {
        public static string ToDataMember(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            var lines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var sb = new System.Text.StringBuilder();
            foreach (var line in lines)
            {
                sb.AppendLine($"[DataMember(Name = \"{line.ToSnakeCase()}\")]");
                sb.AppendLine($"public string {line.ToPascalCase()} {{get; set;}}");
            }
            return sb.ToString();
        }
        public static string ToJsonMember(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            var lines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var sb = new System.Text.StringBuilder();
            foreach (var line in lines)
            {
                sb.AppendLine($"\"{line.ToSnakeCase()}\": \"\"\",");
            }
            return sb.ToString();
        }
        public static string ToSnakeCase(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (text.Length < 2)
            {
                return text;
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static string ToSha256(this string value)
        {
            return System.Text.Encoding.UTF8.GetBytes(value).ToSha256();
        }

        public static string ToSha256(this byte[] value)
        {
            var sha256 = SHA256.Create();

            var bytes = sha256.ComputeHash(value);
            sha256.Clear();
            return string.Join("", bytes.Select(x => $"{x:x2}"));
        }

        public static string UrlEncode(this string value)
        {
            return System.Web.HttpUtility.UrlEncode(value);
        }

        public static string UrlDeocde(this string value)
        {
            return System.Web.HttpUtility.UrlDecode(value);
        }

        public static string Base64Encode(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(value));
        }

        public static byte[] Base64Decode(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new byte[] { };
            }
            return Convert.FromBase64String(value);
        }

        public static string ToPascalCase(this string original)
        {
            var invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
            var whiteSpace = new Regex(@"(?<=\s)");
            var startsWithLowerCaseChar = new Regex("^[a-z]");
            var firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
            var lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
            var upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");

            // replace white spaces with undescore, then replace all invalid chars with empty string
            var pascalCase = invalidCharsRgx.Replace(whiteSpace.Replace(original, "_"), string.Empty)
                .Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper()))
                .Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower()))
                .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper()))
                .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));

            return string.Concat(pascalCase);
        }
    }
}
