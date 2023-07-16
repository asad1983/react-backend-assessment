using System.ComponentModel;

namespace React_Backend.Application.Enums
{
    public static class EnumHelper
    {
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string GetDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : string.Empty; // value.ToString();
        }

        /// <summary>
        /// Gets the enum by description value.
        /// Example usage:  (LogType.MessageType)NOKEnum.GetEnumByDescriptionValue(typeof(LogType.MessageType), "Description text");
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static Enum GetEnumByDescriptionValue(Type enumType, string description)
        {
            var fis = enumType.GetFields();

            foreach (var fi in fis)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length <= 0) continue;
                if (attributes[0].Description == description)
                {
                    return (Enum)Enum.Parse(enumType, fi.Name);
                }
            }

            throw new Exception(String.Format("Couldn't find enum member of type '{0}' with description '{1}'.", enumType, description));
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }

        public static Enum GetEnumByName(Type enumType, string name)
        {
            return (Enum)Enum.Parse(enumType, name, true);
        }

       
    }
}

