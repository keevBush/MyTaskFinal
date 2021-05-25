using MyTask.Services;
using MyTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyTask.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<RegisterViewModel, (Exception, Services.SnackBarType)>(this, "ExceptionRise", ShowSnackbar);
        }

        private async void ShowSnackbar(RegisterViewModel sender, (Exception, SnackBarType) args)
        {
            if (args.Item2 == SnackBarType.Error)
                await this.ShowErrorSnackBarAsync(args.Item1.Message);
            if (args.Item2 == SnackBarType.Warning)
                await this.ShowWarningSnackBarAsync(args.Item1.Message);
        }

        private void MyTaskEntry_TextChanged(object sender, CustomControls.MyTaskEntryEventArgs e)
        {
            //var text = e.Text;
        }
    }
}