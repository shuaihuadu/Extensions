using System.IO;
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
        public static bool IsNot<T>(this object item) where T : class
        {
            return !(item.Is<T>());
        }
        /// <summary>
        /// Convert the specified object to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of expected.</typeparam>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The converted <typeparamref name="T"/></returns>
        public static T As<T>(this object item) where T : class
        {
            return item as T;
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
                    StringBuilder builder = new StringBuilder();
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    XmlWriter writer = XmlWriter.Create(builder);
                    serializer.Serialize(writer, obj);
                    result = builder.ToString();
                    writer.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }
    }
}