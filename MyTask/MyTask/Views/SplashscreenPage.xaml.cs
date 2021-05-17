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
        private readonly UserRepository _userRepository;
        private readonly INavigationService _navigationService;
        public SplashscreenPage()
        {
            
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //BindingContext = new ViewModels.SplashscreenViewModel(_userRepository, _navigationService);
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
        }
    }
}
