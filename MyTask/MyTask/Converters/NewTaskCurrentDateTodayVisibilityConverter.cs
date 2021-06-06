using System;
using System.Globalization;
using Xamarin.Forms;

namespace MyTask.Converters
{
    public class NewTaskCurrentDateTodayVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (DateTime) value >= DateTime.Today;
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}