using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTask.ViewModels;
using Prism.Navigation;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTask.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTaskPopupPage 
    {
        public NewTaskPopupPage()
        {
            InitializeComponent();
        }
    }
}