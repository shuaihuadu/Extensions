using System.Linq;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// 常用的String扩展.
    /// </summary>
    public static class StringExtensions
    {
        // The Trim method only trims 0x0009, 0x000a, 0x000b, 0x000c, 0x000d, 0x0085, 0x2028, and 0x2029.
        // This array adds in control characters.
        static readonly char[] whiteSpaceChars = new char[]
        {
            (char)0x00, (char)0x01, (char)0x02, (char)0x03, (char)0x04, (char)0x05,
            (char)0x06, (char)0x07, (char)0x08, (char)0x09, (char)0x0a, (char)0x0b,
            (char)0x0c, (char)0x0d, (char)0x0e, (char)0x0f, (char)0x10, (char)0x11,
            (char)0x12, (char)0x13, (char)0x14, (char)0x15, (char)0x16, (char)0x17,
            (char)0x18, (char)0x19, (char)0x20, (char)0x1a, (char)0x1b, (char)0x1c,
            (char)0x1d, (char)0x1e, (char)0x1f, (char)0x7f, (char)0x85, (char)0x2028, (char)0x2029
        };
        /// <summary> 
        /// 获取一个值标识指定的字符串是否是空白的(包含所有隐藏的空白字符)
        /// <param name="value">指定的字符串</param>
        /// <returns>是否是空白的字符串</returns>
        /// </summary> 
        public static bool IsNullOrBlank(this string value)
        {
            if (value == null || string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            if (value.Trim(whiteSpaceChars).Length == 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 去除掉指定字符串中所有的空白字符(包含所有隐藏的空白字符)
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <returns>去除所有的空白字符后的字符串</returns>
        public static string TrimAll(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            else
            {
                string result = TrimBlank(value);
                foreach (var item in whiteSpaceChars)
                {
                    result = result.Replace(new string(new char[] { item }), string.Empty);
                }
                return result;
            }
        }
        /// <summary>
        /// 去除掉字符串两头的空白字符(包含所有隐藏的空白字符)
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <returns>去除两头的空白字符后的字符串</returns>
        public static string TrimBlank(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            else
            {
                return value.Trim(whiteSpaceChars);
            }
        }
        /// <summary>
        /// 获取一个值标识指定的字符串是否是合法的邮箱地址
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <returns>是否是合法的邮箱地址</returns>
        public static bool IsEmail(this string value)
        {
            if (value.IsNullOrBlank())
            {
                return false;
            }
            return new Regex(@"^[a-zA-Z0-9_+.-]+\@([a-zA-Z0-9-]+\.)+[a-zA-Z0-9]{2,4}$").Match(value).Success;
        }
        /// <summary>
        /// 获取一个值标识指定的字符串是否是合法的手机号码
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <returns>是否是合法的手机号码</returns>
        public static bool IsMobile(this string value)
        {
            if (value.IsNullOrBlank())
            {
                return false;
            }
            return new Regex(@"^1(3[0-9]|4[57]|5[0-35-9]|7[0678]|8[0-9])\d{8}$").Match(value).Success;
        }
        /// <summary>
        /// 获取一个值标识指定的字符串是否是汉字
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <returns>是否是汉字</returns>
        public static bool IsHans(this string value)
        {
            if (value.IsNullOrBlank())
            {
                return false;
            }
            return Regex.Match(value, @"[\u4e00-\u9fa5]").Success;
        }
        /// <summary>
        /// 从当前 System.String 对象移除所有前导空白字符和尾部空白字符,如果对应的值为Null的话不会造成异常
        /// </summary>
        /// <returns>从当前字符串的开头和结尾删除所有空白字符后剩余的字符串</returns>
        public static string SafeTrim(this string value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                return value.Trim();
            }
        }
        /// <summary>
        /// 获取一个指标识指定的字符串是否在指定的长度区间内
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="trim">是否去掉两头的不可见字符</param>
        /// <returns>是否在指定的长度区间</returns>
        public static bool IsValidLength(this string value, int minLength, int maxLength, bool trim = false)
        {
            if (minLength < 0)
            {
                throw new ArgumentException("minLength must be greater than 0.");
            }
            if (maxLength > int.MaxValue)
            {
                throw new ArgumentException("minLength must be less than Int32.MaxValue.");
            }
            if (value == null)
            {
                throw new ArgumentException("value can not be null.");
            }
            if (trim)
            {
                value = value.Trim().TrimBlank();
            }
            return value.Length >= minLength && value.Length <= maxLength;
        }
        /// <summary>
        /// 获取一个指标识指定的字符串是否在指定的长度区间内
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <param name="lenght">指定的字符串的长度</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="trim">是否去掉两头的不可见字符</param>
        /// <returns>是否在指定的长度区间</returns>
        public static bool IsValidLength(this string value, out int lenght, int minLength, int maxLength, bool trim = false)
        {
            if (minLength < 0)
            {
                throw new ArgumentException("minLength must be greater than 0.");
            }
            if (maxLength > int.MaxValue)
            {
                throw new ArgumentException("minLength must be less than Int32.MaxValue.");
            }
            if (value == null)
            {
                throw new ArgumentException("value can not be null.");
            }
            if (trim)
            {
                value = value.Trim().TrimBlank();
            }

            lenght = value.Length;

            return value.Length >= minLength && value.Length <= maxLength;
        }
        /// <summary>
        /// 获取一个指标识指定的字符串是否在指定的字节区间
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="trim">是否去掉两头的不可见字符</param>
        /// <returns>是否在指定的字节区间</returns>
        public static bool IsValidBytes(this string value, int minLength, int maxLength, bool trim = false)
        {
            if (minLength < 0)
            {
                throw new ArgumentException("minLength must be greater than 0.");
            }
            if (value == null)
            {
                throw new ArgumentException("value can not be null.");
            }
            if (trim)
            {
                value = value.Trim().TrimBlank();
            }
            int length = System.Text.Encoding.Default.GetByteCount(value);
            return length >= minLength && length <= maxLength;
        }
        /// <summary>
        /// 获取一个指标识指定的字符串是否在指定的字节区间
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <param name="byteLength">指定的字符串的字节数</param>
        /// <param name="minLength">最小长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="trim">是否去掉两头的不可见字符</param>
        /// <returns>是否在指定的字节区间</returns>
        public static bool IsValidBytes(this string value, out int byteLength, int minLength, int maxLength, bool trim = false)
        {
            if (minLength < 0)
            {
                throw new ArgumentException("minLength must be greater than 0.");
            }
            if (value == null)
            {
                throw new ArgumentException("value can not be null.");
            }
            if (trim)
            {
                value = value.Trim().TrimBlank();
            }
            int length = System.Text.Encoding.Default.GetByteCount(value);
            byteLength = length;
            return length >= minLength && length <= maxLength;
        }
        /// <summary>
        /// 将指定的字符串转换为short值
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <returns>转换后的short值</returns>
        public static short ToInt16(this string value)
        {
            short result = 0;
            if (!value.IsNullOrBlank())
            {
                short.TryParse(value, out result);
            }
            return result;
        }
        /// <summary>
        /// 将指定的字符串转换为int值
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <returns>转换后的int值</returns>
        public static int ToInt32(this string value)
        {
            int result = 0;
            if (!value.IsNullOrBlank())
            {
                int.TryParse(value, out result);
            }
            return result;
        }
        /// <summary>
        /// 将指定的字符串转换为long值
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <returns>转换后的long值</returns>
        public static long ToInt64(this string value)
        {
            long result = 0;
            if (!value.IsNullOrBlank())
            {
                long.TryParse(value, out result);
            }
            return result;
        }
        /// <summary>
        /// 获取指定的字符串的反转结果
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <returns>反转后的字符串结果</returns>
        public static string Reverse(this string value)
        {
            if (value.IsNullOrBlank())
            {
                return string.Empty;
            }
            char[] array = value.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
        /// <summary>
        /// 获取制定字符串的缩短格式
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <param name="toLength">缩短后的长度</param>
        /// <param name="cutOffReplacement">截取内容的表示</param>
        /// <returns>字符串的缩短格式</returns>
        public static string Shorten(this string value, int toLength, string cutOffReplacement = " ...")
        {
            if (string.IsNullOrEmpty(value) || value.Length <= toLength)
            {
                return value;
            }
            else
            {
                return value.Remove(toLength) + cutOffReplacement;
            }
        }
        /// <summary>
        /// 将指定的字符串转换为指定的枚举类型
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">指定的字符串</param>
        /// <param name="ignorecase">是否忽略大小写</param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, bool ignorecase = true)
        {
            if (value.IsNullOrBlank())
            {
                throw new ArgumentException("Must specify valid information for parsing in the string.", "value");
            }
            Type t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException("Type provided must be an Enum.", "T");
            }
            return (T)Enum.Parse(t, value, ignorecase);
        }
        /// <summary>
        /// 将指定的字符串转换为DateTime格式
        /// </summary>
        /// <param name="value">指定的字符串</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value)
        {
            DateTime dt;
            var result = DateTime.TryParse(value, out dt);
            return (result) ? dt : DateTime.MinValue;
        }
        /// <summary>
        /// 获取一个值标识对应的字符串是否是合法的GUID
        /// </summary>
        /// <param name="input">对应的字符串</param>
        /// <param name="format">GUID的分隔格式化字符,默认为"D"</param>
        /// <returns>是否是合法的GUID</returns>
        public static bool IsGuid(this string input, string format = "D")
        {
            string[] formats = new[] { "D", "d", "N", "n", "P", "p", "B", "b", "X", "x" };
            try
            {
                if (!formats.Contains(format))
                {
                    format = formats[0];
                }
                Guid.ParseExact(input, format);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 将指定的字符串转化为GUID
        /// </summary>
        /// <param name="input"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string input, string format = "D")
        {
            string[] formats = new[] { "D", "d", "N", "n", "P", "p", "B", "b", "X", "x" };
            if (!formats.Contains(format))
            {
                format = formats[0];
            }
            if (IsGuid(input, format))
            {
                return Guid.ParseExact(input, format);
            }
            else
            {
                throw new ArgumentException("Input string is not a valid GUID format.");
            }
        }
    }
}