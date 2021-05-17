using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTask.CustomControls
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTaskCalendar : Grid
    {

        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public ObservableCollection<DateTime> Dates { get; set; }
        public MyTaskCalendar()
        {
            InitializeComponent();
            Dates = new ObservableCollection<DateTime>();
            days.ItemsSource = Dates;
            InitializeDays();
        }

        private void InitializeDays()
        {
            Dates.Clear();
            foreach (var day in Enumerable.Range(1, DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month)))
            {
                Dates.Add(new DateTime(CurrentDate.Year, CurrentDate.Month, day));
            }
        }
    }
}