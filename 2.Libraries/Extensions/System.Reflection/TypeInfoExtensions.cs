using System.Collections.Generic;

namespace System.Reflection
{
    /// <summary>
    /// The <see cref="TypeInfo"/> extensions.
    /// </summary>
    public static class TypeInfoExtensions
    {
        /// <summary>
        /// Convert the fields of the <paramref name="typeInfo"/> to dictionary.
        /// </summary>
        /// <param name="typeInfo">The member information.</param>
        public static Dictionary<string, string> ToDictionary(this TypeInfo typeInfo)
        {
            var result = new Dictionary<string, string>();
            if (typeInfo.IsNotNull() && typeInfo.DeclaredMembers.IsNotNullOrEmpty())
            {
                foreach (var item in typeInfo.DeclaredMembers)
                {
                    if (item.MemberType == MemberTypes.Field)
                    {
                        result.Add(item.Name, item.GetValue<string>());
                    }
                }
            }
            return result;
        }
    }
}