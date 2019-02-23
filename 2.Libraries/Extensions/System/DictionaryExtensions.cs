using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// The <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"/> extensions.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Safes the get value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
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
