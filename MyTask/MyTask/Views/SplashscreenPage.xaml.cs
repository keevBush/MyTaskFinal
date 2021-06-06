using MyTask.Repositories;
using MyTask.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyTask.Views
{
    public partial class SplashscreenPage : ContentPage
    {
        public SplashscreenPage()
        {
            InitializeComponent();
        }
        
        
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            await this.ShowSuccessSnackBarAsync("cooool", SnackbarDuration.Long);
        }
    }
}
