using System;
using System.Globalization;
using System.Windows.Data;
using Client.ViewModels;

namespace Client.Helpers
{
    public class StatusTypeConverterToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is FileStatusType))
                return string.Empty;

            FileStatusType objValue = (FileStatusType)value;
            return objValue.ToDescriptionString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}