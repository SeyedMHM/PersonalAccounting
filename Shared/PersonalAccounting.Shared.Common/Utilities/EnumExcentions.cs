using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PersonalAccounting.Shared.Common.Utilities
{
    public static class EnumExcentions
    {
        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            return Enum.GetValues(input.GetType()).Cast<T>();
        }

        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            Assert.NotNull(value, nameof(value));

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }

        public static int ToValue(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {
            Assert.NotNull(value, nameof(value));
            
            return Convert.ToInt32(value);
        }
    }
    
    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
}
