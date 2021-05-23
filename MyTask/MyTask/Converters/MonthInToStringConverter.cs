using System;
using System.Globalization;
using MyTask.CustomControls;
using Xamarin.Forms;

namespace MyTask.Converters
{
    public class MonthInToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.GetName(typeof(Months), (Months) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}