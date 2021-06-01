using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTask.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrameDateTimePicker
    {
        private DatePicker _datePicker;
        private TimePicker _timePicker;
        public FrameDateTimePicker()
        {
            InitializeComponent();
            _datePicker = new DatePicker();
            _timePicker = new TimePicker();
            
            _datePicker.DateSelected +=DatePickerOnDateSelected;
            
        }

        private void DatePickerOnDateSelected(object sender, DateChangedEventArgs e)
        {
            
        }
    }
}