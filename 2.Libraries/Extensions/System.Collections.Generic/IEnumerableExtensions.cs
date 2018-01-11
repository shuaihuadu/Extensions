using System.Data;
using System.Linq;
using System.Reflection;

namespace System.Collections.Generic
{
    /// <summary>
    /// Common extensions of <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Creates a new <see cref="IEnumerable{T}"/> that is a copy of the current instance.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="collection">The collection to clone.</param>
        /// <returns>A new object that is a copy of <see cref="IEnumerable{T}"/>.</returns>
        public static IEnumerable<T> Clone<T>(this IEnumerable<T> collection) where T : ICloneable
        {
            return collection.Select(item => (T)item.Clone());
        }
        /// <summary>
        /// Indicates whether the specified collection is null or an empty collection.
        /// </summary>
        /// <param name="collection">The collection to test.</param>
        /// <returns>true if the collection is null or an empty collection; otherwise, false.</returns>
        public static bool IsNullOrEmpty(this IEnumerable collection)
        {
            if (collection == null)
            {
                return true;
            }
            return !collection.GetEnumerator().MoveNext();
        }

        /// <summary>
        /// Indicates whether the specified collection is not null or not an empty collection.
        /// </summary>
        /// <param name="collection">The collection to test.</param>
        /// <returns>true if the collection is not null or not an empty collection; otherwise, false.</returns>
        public static bool IsNotNullOrEmpty(this IEnumerable collection)
        {
            return !collection.IsNullOrEmpty();
        }

        /// <summary>
        /// Indicates whether the specified collection is null or an empty collection.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="collection">The collection to test.</param>
        /// <returns>true if the collection is null or an empty collection; otherwise, false.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || (collection != null && collection.Count() == 0);
        }

        /// <summary>
        /// Indicates whether the specified collection is not null or not an empty collection.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="collection">The collection to test.</param>
        /// <returns>true if the collection is not null or not an empty collection; otherwise, false.</returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return !collection.IsNullOrEmpty();
        }

        /// <summary>
        /// Get an empty collection If <paramref name="collection"/> is null.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="collection">The collection to test.</param>
        /// <returns>The <paramref name="collection"/>,if <paramref name="collection"/>is null return an empty collection.</returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> collection)
        {
            return collection ?? Enumerable.Empty<T>();
        }
        /// <summary>
        /// Convert the <see cref="IEnumerable{T}"/> to a DataTable.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="collection">The collection to convert.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>The converted datatable.</returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            List<string> propertyNameList = new List<string>();
            DataTable dataTable = new DataTable();
            if (collection.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(collection), "The collection to convert can not be null or empty.");
            }
            PropertyInfo[] propertys = collection.FirstOrDefault().GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                dataTable.Columns.Add(pi.Name, pi.PropertyType);
            }
            for (int i = 0; i < collection.Count(); i++)
            {
                ArrayList arrayList = new ArrayList();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        object obj = pi.GetValue(collection.ToList()[i], null);
                        arrayList.Add(obj);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                        {
                            object obj = pi.GetValue(collection.ToList()[i], null);
                            arrayList.Add(obj);
                        }
                    }
                }
                object[] array = arrayList.ToArray();
                dataTable.LoadDataRow(array, true);
            }
            return dataTable;
        }
    }
}