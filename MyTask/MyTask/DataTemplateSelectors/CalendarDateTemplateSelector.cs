using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyTask.Annotations;
using Xamarin.Forms;

namespace MyTask.DataTemplateSelectors
{
    public class CalendarDateTemplateSelector:DataTemplateSelector,INotifyPropertyChanged
    {

        private DateTime _currentDateTime;
        
        public DateTime CurrentDateTime
        {
            get => _currentDateTime;
            set
            {
                _currentDateTime = value;
                OnPropertyChanged("CurrentDateTime");
            }
        }
        public DataTemplate CurrentDate{ get; set; }
        public DataTemplate DateNotSelected{ get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
            => ((DateTime) item).Day != CurrentDateTime.Day || ((DateTime) item).Month != CurrentDateTime.Month || ((DateTime) item).Year != CurrentDateTime.Year
                ? DateNotSelected
                : CurrentDate;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}