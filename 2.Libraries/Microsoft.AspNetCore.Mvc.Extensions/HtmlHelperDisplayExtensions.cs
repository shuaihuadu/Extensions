using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Extensions.Resources;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Primitives;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    /// <summary>
    /// Display-related extensions for <see cref="IHtmlHelper"/> and <see cref="IHtmlHelper{TModel}"/>.
    /// </summary>
    public static class HtmlHelperDisplayExtensions
    {

        #region Image Helper        
        /// <summary>
        /// Generate an image to the specified action name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="action">Name of the action.</param>
        /// <param name="controller">Name of the controller.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static string Image<T>(this IHtmlHelper helper, string action, string controller, int width, int height) where T : Controller
        {
            return Image<T>(helper, action, controller, new object[] { }, width, height, string.Empty);
        }
        /// <summary>
        /// Generate an image to the specified action name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="action">Name of the action.</param>
        /// <param name="controller">Name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static string Image<T>(this IHtmlHelper helper, string action, string controller, object routeValues, int width, int height) where T : Controller
        {
            return Image<T>(helper, action, controller, routeValues, width, height, string.Empty);
        }
        /// <summary>
        /// Generate an image to the specified action name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="action">Name of the action.</param>
        /// <param name="controller">Name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="alt">The alt.</param>
        /// <returns></returns>
        public static string Image<T>(this IHtmlHelper helper, string action, string controller, object routeValues, int width, int height, string alt) where T : Controller
        {
            string url = new UrlHelper(helper.ViewContext).Action(action, controller, routeValues);
            return string.Format("<img src=\"{0}\" width = \"{1}\" height = \"{2}\" alt = \"{3}\" />", url, width, height, alt);
        }

        #endregion

        #region Nullable Value Helper

        /// <summary>
        /// 获取性别的字符串表现形式
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="gender">性别</param>
        /// <returns>true:男(Male),false:女(Female),null:未知(Unknow)</returns>
        public static string Gender(this IHtmlHelper helper, bool? gender)
        {
            return !gender.HasValue ? Resource.Unknow : Gender(helper, gender.Value);
        }
        /// <summary>
        /// 获取性别的字符串表现形式
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="gender">性别</param>
        /// <returns>true:男(Male),false:女(Female)</returns>
        public static string Gender(this IHtmlHelper helper, bool gender)
        {
            return gender ? Resource.Male : Resource.Female;
        }
        /// <summary>
        /// 获取性别的字符串表现形式
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="gender">性别</param>
        /// <returns>0:未知(Unknow),1:男(Male),2:女(Female)</returns>
        public static string Gender(this IHtmlHelper helper, int gender)
        {
            return gender == 0 ? Resource.Unknow : gender == 1 ? Resource.Male : Resource.Female;
        }
        /// <summary>
        /// 获取布尔值的是否字符串表现形式
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value">布尔值</param>
        /// <returns>true:是(Yes),fale:否(No),null:未知(Unknow)</returns>
        public static string Boolean(this IHtmlHelper helper, bool? value)
        {
            if (value.HasValue)
            {
                return Boolean(helper, value.Value);
            }
            return Resource.Unknow;
        }
        /// <summary>
        /// 获取布尔值的是否字符串表现形式
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value">布尔值</param>
        /// <returns>true:是(Yes),fale:否(No)</returns>
        public static string Boolean(this IHtmlHelper helper, bool value)
        {
            if (value)
            {
                return Resource.Yes;
            }
            else
            {
                return Resource.No;
            }
        }
        /// <summary>
        /// 获取指定可为空的DateTime类型的字符串表现形式
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="dateTime">需要显示的DateTime?类型的值</param>
        /// <returns>按照yyyy-MM-dd格式化后的日期字符串</returns>
        public static string DateTime(this IHtmlHelper helper, DateTime? dateTime)
        {
            return DateTime(helper, dateTime, "yyyy-MM-dd");
        }
        /// <summary>
        /// 获取指定可为空的DateTime类型的字符串表现形式
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="dateTime">需要显示的DateTime?类型的值</param>
        /// <param name="format">日期格式化选项</param>
        /// <returns>按照格式化选项格式化后的日期字符串</returns>
        public static string DateTime(this IHtmlHelper helper, DateTime? dateTime, string format)
        {
            if (dateTime.HasValue)
            {
                try
                {
                    return dateTime.Value.ToString(format);
                }
                catch (ArgumentException ex)
                {
                    throw ex;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region checked & selected

        /// <summary>
        /// 返回一个字符串标识select元素的选中项
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value1">比较中的第一个值</param>
        /// <param name="value2">比较中的第二个值</param>
        /// <returns>selected="selected"</returns>
        public static string Selected(this IHtmlHelper helper, bool value1, bool value2)
        {
            return GetCheckedOrSelectedString(helper, value1, value2, CheckedOrSelected.Selected);
        }
        /// <summary>
        /// 返回一个字符串标识select元素的选中项
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value1">比较中的第一个值</param>
        /// <param name="value2">比较中的第二个值</param>
        /// <returns>selected="selected"</returns>
        public static string Selected(this IHtmlHelper helper, decimal value1, decimal value2)
        {
            return GetCheckedOrSelectedString(helper, value1, value2, CheckedOrSelected.Selected);
        }
        /// <summary>
        /// 返回一个字符串标识select元素的选中项
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value1">比较中的第一个值</param>
        /// <param name="value2">比较中的第二个值</param>
        /// <returns>selected="selected"</returns>
        public static string Selected(this IHtmlHelper helper, byte value1, byte value2)
        {
            return GetCheckedOrSelectedString(helper, value1, value2, CheckedOrSelected.Selected);
        }
        /// <summary>
        /// 返回一个字符串标识select元素的选中项
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value1">比较中的第一个值</param>
        /// <param name="value2">比较中的第二个值</param>
        /// <returns>selected="selected"</returns>
        public static string Selected(this IHtmlHelper helper, int value1, int value2)
        {
            return GetCheckedOrSelectedString(helper, value1, value2, CheckedOrSelected.Selected);
        }
        /// <summary>
        /// 返回一个字符串标识select元素的选中项
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value1">比较中的第一个值</param>
        /// <param name="value2">比较中的第二个值</param>
        /// <returns>selected="selected"</returns>
        public static string Selected(this IHtmlHelper helper, string value1, string value2)
        {
            return GetCheckedOrSelectedString(helper, value1, value2, CheckedOrSelected.Selected);
        }
        /// <summary>
        /// 返回一个字符串标识radio、checkbox元素的选中项
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value1">比较中的第一个值</param>
        /// <param name="value2">比较中的第二个值</param>
        /// <returns>checked="checked"</returns>
        public static string Checked(this IHtmlHelper helper, bool value1, bool value2)
        {
            return GetCheckedOrSelectedString(helper, value1, value2, CheckedOrSelected.Checked);
        }
        /// <summary>
        /// 返回一个字符串标识radio、checkbox元素的选中项
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value1">比较中的第一个值</param>
        /// <param name="value2">比较中的第二个值</param>
        /// <returns>checked="checked"</returns>
        public static string Checked(this IHtmlHelper helper, decimal value1, decimal value2)
        {
            return GetCheckedOrSelectedString(helper, value1, value2, CheckedOrSelected.Checked);
        }
        /// <summary>
        /// 返回一个字符串标识radio、checkbox元素的选中项
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value1">比较中的第一个值</param>
        /// <param name="value2">比较中的第二个值</param>
        /// <returns>checked="checked"</returns>
        public static string Checked(this IHtmlHelper helper, byte value1, byte value2)
        {
            return GetCheckedOrSelectedString(helper, value1, value2, CheckedOrSelected.Checked);
        }
        /// <summary>
        /// 返回一个字符串标识radio、checkbox元素的选中项
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value1">比较中的第一个值</param>
        /// <param name="value2">比较中的第二个值</param>
        /// <returns>checked="checked"</returns>
        public static string Checked(this IHtmlHelper helper, int value1, int value2)
        {
            return GetCheckedOrSelectedString(helper, value1, value2, CheckedOrSelected.Checked);
        }
        /// <summary>
        /// 返回一个字符串标识radio、checkbox元素的选中项
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value1">比较中的第一个值</param>
        /// <param name="value2">比较中的第二个值</param>
        /// <returns>checked="checked"</returns>
        public static string Checked(this IHtmlHelper helper, string value1, string value2)
        {
            return GetCheckedOrSelectedString(helper, value1, value2, CheckedOrSelected.Checked);
        }
        /// <summary>
        /// Gets the checked or selected string.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="cos">The cos.</param>
        /// <returns></returns>
        private static string GetCheckedOrSelectedString(this IHtmlHelper helper, object value1, object value2, CheckedOrSelected cos)
        {
            if (value1 == null || value2 == null)
            {
                return string.Empty;
            }
            else
            {
                if (value1.ToString().ToLower() == value2.ToString().ToLower())
                {
                    return GetCheckedOrSelectedString(cos);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        private static string GetCheckedOrSelectedString(CheckedOrSelected cos)
        {
            string result = string.Empty;
            if (cos == CheckedOrSelected.Checked)
            {
                result = "checked=\"checked\"";
            }
            else if (cos == CheckedOrSelected.Selected)
            {
                result = "selected=\"selected\"";
            }
            else
            {
                result = string.Empty;
            }

            return result;
        }

        #endregion

        #region Browser
        /// <summary>
        /// 是否是微信浏览器
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <returns>是微信浏览器:true,否则:false</returns>
        public static bool IsMicroMessageBrowser(this IHtmlHelper helper)
        {
            bool result = false;
            var headers = helper.ViewContext.HttpContext.Request.Headers;
            if (headers != null && headers.ContainsKey("User-Agent"))
            {
                var userAgent = headers["User-Agent"];
                if (!StringValues.IsNullOrEmpty(userAgent))
                {
                    result = userAgent.Any(x => x.ToLower().IndexOf("micromessenger") > 0);
                }
            }
            return result;
        }

        #endregion

        #region Accent
        /// <summary>
        /// Gets the accent class name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="accentValue">The accent value.</param>
        /// <param name="accentClass">The accent class name.</param>
        /// <returns>The accent class name.</returns>
        public static string AccentClass(string value, string accentValue, string accentClass)
        {
            return GetAccentString(value, accentValue, accentClass);
        }
        /// <summary>
        /// Gets the accent class name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="accentValue">The accent value.</param>
        /// <param name="accentClass">The accent class name.</param>
        /// <returns>The accent class name.</returns>
        public static string AccentClass(bool value, bool accentValue, string accentClass)
        {
            return GetAccentString(value, accentValue, accentClass);
        }
        /// <summary>
        /// Gets the accent class name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="accentValue">The accent value.</param>
        /// <param name="accentClass">The accent class name.</param>
        /// <returns>The accent class name.</returns>
        public static string AccentClass(int value, int accentValue, string accentClass)
        {
            return GetAccentString(value, accentValue, accentClass);
        }
        /// <summary>
        /// Gets the accent class name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="accentValue">The accent value.</param>
        /// <param name="accentClass">The accent class name.</param>
        /// <returns>The accent class name.</returns>
        public static string AccentClass(byte value, byte accentValue, string accentClass)
        {
            return GetAccentString(value, accentValue, accentClass);
        }
        /// <summary>
        /// Gets the accent class name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="accentValue">The accent value.</param>
        /// <param name="accentClass">The accent class name.</param>
        /// <returns>The accent class name.</returns>
        public static string AccentClass(double value, double accentValue, string accentClass)
        {
            return GetAccentString(value, accentValue, accentClass);
        }
        /// <summary>
        /// Gets the accent class name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="accentValue">The accent value.</param>
        /// <param name="accentClass">The accent class name.</param>
        /// <returns>The accent class name.</returns>
        public static string AccentClass(decimal value, decimal accentValue, string accentClass)
        {
            return GetAccentString(value, accentValue, accentClass);
        }
        /// <summary>
        /// Gets the accent string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="accentValue">The accent value.</param>
        /// <param name="accentString">The accent string.</param>
        /// <returns></returns>
        private static string GetAccentString<T>(T value, T accentValue, string accentString)
        {
            if (value.Equals(accentValue))
            {
                return accentString;
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}