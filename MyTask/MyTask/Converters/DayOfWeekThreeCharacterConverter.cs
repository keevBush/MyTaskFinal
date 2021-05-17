using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MyTask.Converters
{
    public class DayOfWeekThreeCharacterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var content = (DayOfWeek)value;
            var returnValue = "-";
            switch (content)
            {
                case DayOfWeek.Friday:
                    returnValue = "Fri";
                    break;
                case DayOfWeek.Monday:
                    returnValue = "Mon";
                    break;
                case DayOfWeek.Saturday:
                    returnValue = "Sat";
                    break;
                case DayOfWeek.Sunday:
                    returnValue = "Sun";
                    break;
                case DayOfWeek.Thursday:
                    returnValue = "Thu";
                    break;
                case DayOfWeek.Tuesday:
                    returnValue = "Tue";
                    break;
                case DayOfWeek.Wednesday:
                    returnValue = "Wed";
                    break;
                default:
                    returnValue = "-";
                    break;
            }

            return returnValue;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
