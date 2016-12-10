using System.ComponentModel;
using Client.ViewModels;

namespace Client.Helpers
{
    public static class Extensions
    {
        public static string ToDescriptionString(this FileStatusType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
