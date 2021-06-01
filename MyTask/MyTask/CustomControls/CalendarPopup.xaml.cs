using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTask.ViewModels;
using Prism.Mvvm;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTask.CustomControls
{
    
    public enum Months
    {
        January=1,February=2,March=3,April=4,May=5,June=6,July=7,August=8,September=9,October=10,November=11,December=12
    }
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPopup : PopupPage
    {
        
        
        private string[] _months ;

        private string _currentMonth;
        public CalendarPopup(int month, int year)
        {
            InitializeComponent();
            _months = Enum.GetNames(typeof(Months));
            monthView.ItemsSource = _months;
            _currentMonth = Enum.GetName(typeof(Months),month);
            monthView.CurrentItem = _currentMonth;
            txtbxYear.Text = year.ToString();
        }

        private void MonthView_OnCurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            Xamarin.Essentials.Vibration.Vibrate(150);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var content = (string)((Label) sender).BindingContext;
            monthView.CurrentItem = content;
        }

        protected override void OnDisappearing()
        {
            var secondData = (Months)(Enum.Parse(typeof(Months), (string) (monthView.CurrentItem)));
            
            var data = (
                Convert.ToInt32(txtbxYear.Text),
                Convert.ToInt32(secondData)
                );
            
            MessagingCenter.Instance.Send(this,"updateMonthYear",data);

            base.OnDisappearing();
        }
    }
}