namespace System.Collections.Generic
{
    /// <summary>
    /// Common extensions of <see cref="ICollection{T}"/>.
    /// </summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Adds the <paramref name="elements"/> to the end of the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="elements">The elements.</param>
        /// <exception cref="ArgumentNullException">
        /// collection
        /// or
        /// elements
        /// </exception>
        public static void AddRange<T>(this ICollection<T> collection, params T[] elements)
        {
            if (collection.IsNull())
            {
                throw new ArgumentNullException(nameof(collection));
            }
            if (elements.IsNull())
            {
                throw new ArgumentNullException(nameof(elements));
            }
            foreach (T value in elements)
            {
                collection.Add(value);
            }
        }
    }
}