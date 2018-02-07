using System.ComponentModel;
using System.Data;

namespace System
{
    /// <summary>
    /// Common extensions of <see cref="object"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets schema of the specified object.
        /// </summary>
        /// <param name="type">The entity type.</param>
        /// <returns>A datatable of specified object with no records.</returns>
        public static DataTable GetSchema(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            DataTable table = new DataTable(type.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
            foreach (PropertyDescriptor prop in properties)
            {
                Type t = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                table.Columns.Add(prop.Name, t);
            }
            return table;
        }
    }
}