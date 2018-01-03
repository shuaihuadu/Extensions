using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web.Mvc.Extensions;
using System.Web.Mvc.Extensions.Resources;
using System.Web.Script.Serialization;

namespace System.Web.Mvc
{
    /// <summary>
    /// The <see cref="HtmlHelper"/> Extensions.
    /// </summary>
    public static class HtmlHelperExtension
    {
        #region Image Helper        
        /// <summary>
        /// Generate an image to the specified action name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static string Image<T>(this HtmlHelper helper, string actionName, string controllerName, int width, int height) where T : Controller
        {
            return Image<T>(helper, actionName, controllerName, new object[] { }, width, height, string.Empty);
        }
        /// <summary>
        /// Generate an image to the specified action name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static string Image<T>(this HtmlHelper helper, string actionName, string controllerName, object routeValues, int width, int height) where T : Controller
        {
            return Image<T>(helper, actionName, controllerName, routeValues, width, height, string.Empty);
        }
        /// <summary>
        /// Generate an image to the specified action name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="alt">The alt.</param>
        /// <returns></returns>
        public static string Image<T>(this HtmlHelper helper, string actionName, string controllerName, object routeValues, int width, int height, string alt) where T : Controller
        {
            string url = new UrlHelper(helper.ViewContext.RequestContext).Action(actionName, controllerName, routeValues);
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
        public static string Gender(this HtmlHelper helper, bool? gender)
        {
            return !gender.HasValue ? Resource.Unknow : Gender(helper, gender.Value);
        }
        /// <summary>
        /// 获取性别的字符串表现形式
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="gender">性别</param>
        /// <returns>true:男(Male),false:女(Female)</returns>
        public static string Gender(this HtmlHelper helper, bool gender)
        {
            return gender ? Resource.Male : Resource.Female;
        }
        /// <summary>
        /// 获取性别的字符串表现形式
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="gender">性别</param>
        /// <returns>0:未知(Unknow),1:男(Male),2:女(Female)</returns>
        public static string Gender(this HtmlHelper helper, int gender)
        {
            return gender == 0 ? Resource.Unknow : gender == 1 ? Resource.Male : Resource.Female;
        }
        /// <summary>
        /// 获取布尔值的是否字符串表现形式
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="value">布尔值</param>
        /// <returns>true:是(Yes),fale:否(No),null:未知(Unknow)</returns>
        public static string Boolean(this HtmlHelper helper, bool? value)
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
        public static string Boolean(this HtmlHelper helper, bool value)
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
        public static string DateTime(this HtmlHelper helper, DateTime? dateTime)
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
        public static string DateTime(this HtmlHelper helper, DateTime? dateTime, string format)
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
        public static string Selected(this HtmlHelper helper, bool value1, bool value2)
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
        public static string Selected(this HtmlHelper helper, decimal value1, decimal value2)
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
        public static string Selected(this HtmlHelper helper, byte value1, byte value2)
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
        public static string Selected(this HtmlHelper helper, int value1, int value2)
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
        public static string Selected(this HtmlHelper helper, string value1, string value2)
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
        public static string Checked(this HtmlHelper helper, bool value1, bool value2)
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
        public static string Checked(this HtmlHelper helper, decimal value1, decimal value2)
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
        public static string Checked(this HtmlHelper helper, byte value1, byte value2)
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
        public static string Checked(this HtmlHelper helper, int value1, int value2)
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
        public static string Checked(this HtmlHelper helper, string value1, string value2)
        {
            return GetCheckedOrSelectedString(helper, value1, value2, CheckedOrSelected.Checked);
        }
        private static string GetCheckedOrSelectedString(this HtmlHelper helper, object value1, object value2, CheckedOrSelected cos)
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
        public static bool IsMicroMessageBrowser(this HtmlHelper helper)
        {
            return helper.ViewContext.HttpContext.Request.UserAgent.ToLower().IndexOf("micromessenger") > 0;
        }

        #endregion

        /// <summary>
        /// 获取一个值标识远程文件是否存在
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="url">远程路径</param>
        /// <returns>存在:true,否则:false</returns>
        public static bool RemoteFileExists(this HtmlHelper helper, string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 将指定的QueryString转换为Json字符串
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="queryString">QueryString</param>
        /// <returns>Json字符串</returns>
        public static string QueryStringToJson(this HtmlHelper helper, string queryString)
        {
            if (string.IsNullOrWhiteSpace(queryString))
            {
                return string.Empty;
            }
            return QueryStringToJson(helper, HttpUtility.ParseQueryString(queryString));
        }
        /// <summary>
        /// 将当前Request中的QueryString转换为Json字符串
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <returns>Json字符串</returns>
        public static string QueryStringToJson(this HtmlHelper helper)
        {
            return QueryStringToJson(helper, helper.ViewContext.HttpContext.Request.QueryString);
        }
        /// <summary>
        /// 将指定的键值集合转换为Json字符串
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="nvc">NameValueCollection</param>
        /// <returns>Json字符串</returns>
        public static string QueryStringToJson(this HtmlHelper helper, NameValueCollection nvc)
        {
            if (nvc != null && nvc.Count > 0)
            {
                return new JavaScriptSerializer().Serialize(nvc.AllKeys.ToDictionary(k => k, k => nvc[k]));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
