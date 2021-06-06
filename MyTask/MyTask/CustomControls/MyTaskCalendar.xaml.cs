using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTask.CustomControls
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTaskCalendar : Grid
    {

        public ICommand CurrentDateChangedCommand { get; set; }

        public static readonly BindableProperty CurrentDateChangedCommandProperty =
            BindableProperty.Create(nameof(CurrentDateChangedCommand), typeof(ICommand),
                typeof(MyTaskCalendar), null,
                propertyChanged: (bindable, oldValue, newValue) =>
                    ((MyTaskCalendar) bindable).ChangeCurrentDateChangedCommand((ICommand) newValue));
        public DateTime CurrentDate { get; set; } = DateTime.Now;

        public static readonly BindableProperty CurrentDateProperty =
            BindableProperty.Create(nameof(CurrentDate), typeof(DateTime),
                typeof(MyTaskCalendar), DateTime.Now,
                propertyChanged: (bindable, oldVal, newVal) =>
                    ((MyTaskCalendar) bindable).SetCurrentDate((DateTime) newVal));
        public ObservableCollection<DateTime> Dates { get; set; }
        
        public MyTaskCalendar()
        {
            InitializeComponent();
            Dates = new ObservableCollection<DateTime>();
            days.ItemsSource = Dates;
            days.SelectedItem = CurrentDate;
            InitializeDays();
            MessagingCenter.Instance.Subscribe<CalendarPopup,(int,int)>(this, "updateMonthYear", 
                (s, data) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (CurrentDate.Year + CurrentDate.Month != data.Item1 + data.Item2)
                        {
                            CurrentDate = new DateTime(data.Item1, data.Item2, 1);
                            LoadDays(Convert.ToInt32(data.Item1),Convert.ToInt32(data.Item2));
                            days.SelectedItem = CurrentDate;
                            days.ScrollTo(1);
                            labelMonth.Text = Enum.GetName(typeof(Months), (Months)(CurrentDate.Month));
                            labelYear.Text = CurrentDate.Year.ToString();
                            if(CurrentDateChangedCommand != null)
                                CurrentDateChangedCommand.Execute(CurrentDate);
                        }
                    });
                });
        }

        private void InitializeDays()
        {
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
            var frame = (Frame)sender;
            var currentDate = frame.BindingContext;
            days.SelectedItem =(DateTime)currentDate;
            
            SetCurrentDate((DateTime)currentDate);

            days.ScrollTo(CurrentDate.Day - 1,position:ScrollToPosition.Center);
            if(CurrentDateChangedCommand != null)
                CurrentDateChangedCommand.Execute((DateTime)currentDate);
        }

        private void MonthYearTapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new CalendarPopup(CurrentDate.Month,CurrentDate.Year));
        }

        private void SetCurrentDate(DateTime date)
        {
            CurrentDate = date;
        }

        private void ChangeCurrentDateChangedCommand(ICommand command)
        {
            CurrentDateChangedCommand = command;
        }
    }
}