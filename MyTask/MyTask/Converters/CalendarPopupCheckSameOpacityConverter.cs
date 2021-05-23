using System;
using System.Globalization;
using Xamarin.Forms;

namespace MyTask.Converters
{
    public class CalendarPopupCheckSameOpacityConverter:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var current = values[0];
            var selected = values[1];
            if (current == selected) 
                return 1;
            else 
                return 0.6;

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}