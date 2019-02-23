namespace System.Reflection
{
    /// <summary>
    /// The <see cref="MemberInfo"/> extensions.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Gets the value of <paramref name="memberInfo"/>.
        /// </summary>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetValue<T>(this MemberInfo memberInfo, object obj = null) where T : class
        {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)memberInfo).GetValue(obj) as T;
                case MemberTypes.Property:
                    return ((PropertyInfo)memberInfo).GetValue(obj) as T;
                default:
                    return null;
            }
        }
        /// <summary>
        /// Convert the <see cref="MemberInfo"/> to <see cref="TypeInfo"/>.
        /// </summary>
        /// <param name="memberInfo">The <see cref="MemberInfo"/>.</param>
        /// <returns></returns>
        public static TypeInfo ToTypeInfo(this MemberInfo memberInfo)
        {
            return memberInfo as TypeInfo;
        }
    }
}
