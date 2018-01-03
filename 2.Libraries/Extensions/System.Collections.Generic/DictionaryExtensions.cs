namespace System.Collections.Generic
{
    /// <summary>
    /// The dictionary extensions.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Safe gets the value associated with the specified key, without throw <see cref="KeyNotFoundException"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>
        /// The value associated with the specified key. If the specified key is not found, return default of <typeparamref name="TValue"/>. 
        /// </returns>
        public static TValue SafeGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary.IsNull() || key.IsNull())
            {
                return default(TValue);
            }
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                return default(TValue);
            }
        }
    }
}
