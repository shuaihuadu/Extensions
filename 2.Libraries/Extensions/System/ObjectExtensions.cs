using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace System
{
    /// <summary>
    /// Common extensions of <see cref="object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Indicates whether the specified object is null.
        /// </summary>
        /// <param name="obj">The object to test.</param>
        /// <returns>true if the object is null;otherwise, false.</returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Indicates whether the specified object is not null.
        /// </summary>
        /// <param name="obj">The object to test.</param>
        /// <returns>true if the object is not null;otherwise, false.</returns>
        public static bool IsNotNull(this object obj)
        {
            return !IsNull(obj);
        }
        /// <summary>
        /// Indicates whether the specified object is <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of expected.</typeparam>
        /// <param name="obj">The object to test.</param>
        /// <returns>true if the object is <typeparamref name="T"/>;otherwise, false.</returns>
        public static bool Is<T>(this object obj) where T : class
        {
            return obj is T;
        }
        /// <summary>
        /// Indicates whether the specified object is not <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of expected.</typeparam>
        /// <param name="obj">The object to test.</param>
        /// <returns>true if the object is not <typeparamref name="T"/>;otherwise, false.</returns>
        public static bool IsNot<T>(this object obj) where T : class
        {
            return !(obj.Is<T>());
        }
        /// <summary>
        /// Convert the specified object to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of expected.</typeparam>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The converted <typeparamref name="T"/></returns>
        public static T As<T>(this object obj) where T : class
        {
            return obj as T;
        }
        /// <summary>
        /// Convert the <paramref name="obj"/> to byte[].
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <exception cref="Exception"></exception>
        /// <returns>The byte[] of <paramref name="obj"/></returns>
        public static byte[] ToBytes(this object obj)
        {
            byte[] result = null;
            if (obj != null)
            {
                try
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        new BinaryFormatter().Serialize(stream, obj);
                        result = stream.ToArray();
                        stream.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// Convert the <paramref name="obj"/> to xml string.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <exception cref="Exception"></exception>
        /// <returns>The xml string of the <paramref name="obj"/>.</returns>
        public static string ToXml(this object obj)
        {
            string result = null;
            if (obj != null)
            {
                try
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    using (var writer = new StringWriter())
                    {
                        serializer.Serialize(writer, obj);
                        return writer.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
        /// <summary>
        /// Determines whether the <paramref name="item"/> in the specified <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="item"/>.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="list">The list.</param>
        /// <returns>
        ///   <c>true</c> if the <paramref name="item"/> is in the <paramref name="list"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">list</exception>
        public static bool In<T>(this T item, params T[] list)
        {
            if (list.IsNull())
            {
                throw new ArgumentNullException(nameof(list));
            }
            return list.Contains(item);
        }
        /// <summary>
        /// Determines whether the <paramref name="actual"/> is between <paramref name="lower"/> and <paramref name="upper"/>.
        /// </summary>
        /// <typeparam name="T">The type where inherts <see cref="IComparable{T}"/></typeparam>
        /// <param name="actual">The actual.</param>
        /// <param name="lower">The lower.</param>
        /// <param name="upper">The upper.</param>
        /// <returns>
        ///   <c>true</c> <paramref name="actual"/> is between <paramref name="lower"/> and <paramref name="upper"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool Between<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
            return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) <= 0;
        }
        /// <summary>
        /// Withes the specified action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        public static void With<T>(this T obj, Action<T> action)
        {
            action(obj);
        }
        /// <summary>
        /// Throws if argument is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowIfArgumentIsNull<T>(this T obj, string parameterName) where T : class
        {
            if (obj == null) throw new ArgumentNullException(parameterName + " not allowed to be null");
        }
    }
}