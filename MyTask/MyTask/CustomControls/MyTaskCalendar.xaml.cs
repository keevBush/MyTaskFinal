using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Navigation;
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
            MessagingCenter.Instance.Subscribe<CalendarPopup,(int,int)>(this, "updateMonthYear", 
                (s, data) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        CurrentDate = new DateTime(data.Item1, data.Item2, 1);
                        LoadDays(Convert.ToInt32(data.Item1),Convert.ToInt32(data.Item2));
                        days.ScrollTo(1);
                        labelMonth.Text = Enum.GetName(typeof(Months), (Months)(CurrentDate.Month));
                        labelYear.Text = CurrentDate.Year.ToString();
                    });
                });
        }

        private void InitializeDays()
        {
            //selector.CurrentDateTime = CurrentDate;
            Dates.Clear();
            foreach (var day in Enumerable.Range(1, DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month)))
                Dates.Add(new DateTime(CurrentDate.Year, CurrentDate.Month, day));
        }

        private void LoadDays(int year, int month)
        {
            Dates.Clear();
            foreach (var day in Enumerable.Range(1,DateTime.DaysInMonth(year,month)))
                Dates.Add(new DateTime(CurrentDate.Year, CurrentDate.Month, day));
        }
        
        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var frame = (Frame) sender;

            var currentDate = frame.BindingContext;
            
            CurrentDate = (DateTime) currentDate;
            days.ScrollTo(CurrentDate.Day - 1,position:ScrollToPosition.Center);

            selector.CurrentDateTime = CurrentDate;
            days.BatchCommit();
        }

        private void MonthYearTapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new CalendarPopup(CurrentDate.Month,CurrentDate.Year));
        }
    }
}