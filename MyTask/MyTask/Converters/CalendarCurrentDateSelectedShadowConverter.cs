using System;
using System.Globalization;
using Xamarin.Forms;

namespace MyTask.Converters
{
    public class CalendarCurrentDateSelectedShadowConverter:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var date =  values[0] == null ?DateTime.Now: (DateTime) values[0];
            var currentDate = (DateTime) values[1];
            if (date.ToShortDateString() == currentDate.ToShortDateString())
                return Color.FromHex("#AFB8EA");
            else
                return Color.FromHex("#C1C8D1");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}