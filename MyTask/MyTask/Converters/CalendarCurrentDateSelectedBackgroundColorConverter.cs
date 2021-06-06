using System;
using System.Globalization;
using Xamarin.Forms;

namespace MyTask.Converters
{
    public class CalendarCurrentDateSelectedBackgroundColorConverter:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var date =  values[0] == null ?DateTime.Now: (DateTime) values[0];
            var currentDate = (DateTime) values[1];
            if (date.ToShortDateString() == currentDate.ToShortDateString())
                return Color.RoyalBlue;
            else
                return Color.FromHex("#FAFBFF");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}