using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        /// To the specified value of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static T To<T>(this object obj)
        {
            T result = default(T);
            result = (T)Convert.ChangeType(obj, typeof(T));
            return result;
        }
        /// <summary>
        /// To the specified value of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="default">The default.</param>
        /// <returns></returns>
        public static T To<T>(this object obj, T @default)
        {
            T result = @default;
            try
            {
                result = (T)Convert.ChangeType(obj, typeof(T));
                return result;
            }
            catch (Exception)
            {
                return @default;
            }
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
        /// <summary>
        /// To the data table with type name of <paramref name="entity"/>.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>An empty data table with type name of <paramref name="entity"/>.</returns>
        public static DataTable ToDataTable<T>(this T entity)
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (PropertyDescriptor prop in properties)
            {
                Type type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                table.Columns.Add(prop.Name, type);
            }
            return table;
        }
        /// <summary>
        /// Convertables the specified type.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static bool Convertable(this object obj, ObjectConvertbleSupportedType type)
        {
            if (obj.IsNull())
            {
                return false;
            }
            try
            {
                if (type == ObjectConvertbleSupportedType.Int)
                {
                    return int.TryParse(obj.ToString(), out int result);
                }
                else if (type == ObjectConvertbleSupportedType.Decimal)
                {
                    return decimal.TryParse(obj.ToString(), out decimal result);
                }
                else if (type == ObjectConvertbleSupportedType.DateTime)
                {
                    return DateTime.TryParse(obj.ToString(), out DateTime result);
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Hiddens the field value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="isReverse">If true reverse the fileds in <typeparamref name="T"/>.</param>
        /// <param name="fileds">The fileds.</param>
        /// <returns></returns>
        public static T HiddenFieldValue<T>(this T obj, bool isReverse = false, params string[] fileds) where T : class
        {
            if (obj.IsNull())
            {
                return obj;
            }
            var properties = obj.GetType().GetProperties();
            foreach (var property in properties)
            {
                bool showOrHidden = isReverse ? (!fileds.Contains(property.Name)) : fileds.Contains(property.Name);
                if (property.IsNotNull() && property.CanWrite && showOrHidden)
                {
                    property.SetValue(obj, null, null);
                }
            }
            return obj;
        }
    }

    /// <summary>
    /// The object convertable supported type.
    /// </summary>
    [Serializable]
    public enum ObjectConvertbleSupportedType
    {
        /// <summary>
        /// The int
        /// </summary>
        Int = 0,
        /// <summary>
        /// The decimal
        /// </summary>
        Decimal = 1,
        /// <summary>
        /// The date time
        /// </summary>
        DateTime = 2
    }
    /// <summary>
    /// The Object Convertble Supported Type Mapping
    /// </summary>
    [Serializable]
    public class ObjectConvertbleSupportedTypeMapping
    {
        /// <summary>
        /// Gets the int.
        /// </summary>
        /// <value>
        /// The int.
        /// </value>
        public static KeyValuePair<ObjectConvertbleSupportedType, Type> Int => new KeyValuePair<ObjectConvertbleSupportedType, Type>(ObjectConvertbleSupportedType.Int, typeof(int));
        /// <summary>
        /// Gets the decimal.
        /// </summary>
        /// <value>
        /// The decimal.
        /// </value>
        public static KeyValuePair<ObjectConvertbleSupportedType, Type> Decimal => new KeyValuePair<ObjectConvertbleSupportedType, Type>(ObjectConvertbleSupportedType.Decimal, typeof(decimal));
        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public static KeyValuePair<ObjectConvertbleSupportedType, Type> DateTime => new KeyValuePair<ObjectConvertbleSupportedType, Type>(ObjectConvertbleSupportedType.DateTime, typeof(DateTime));
    }
}