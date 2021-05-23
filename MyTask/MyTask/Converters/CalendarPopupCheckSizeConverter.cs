using System;
using System.Globalization;
using Xamarin.Forms;

namespace MyTask.Converters
{
    public class CalendarPopupCheckSizeConverter:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var current = values[0];
            var selected = values[1];
            if (current == selected)
                return  Device.GetNamedSize(NamedSize.Title, typeof(Label));
            else
                return  Device.GetNamedSize(NamedSize.Subtitle, typeof(Label));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}