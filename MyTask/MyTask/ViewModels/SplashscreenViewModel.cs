using MyTask.Repositories;
using MyTask.Services;
using MyTasks.Core.Data.Interfaces;
using Prism.AppModel;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyTask.ViewModels
{
    public class SplashscreenViewModel:ViewModelBase
    {
        private readonly UserRepository _userRepository;
        private readonly INavigationService _navigationService;

        private bool _isRuning;
        public bool IsRunning
        {
            get => _isRuning;
            set => SetProperty(ref _isRuning, value, "IsRunning");
        }
        public SplashscreenViewModel(UserRepository userRepository, INavigationService navigationService):base(navigationService)
        {
            _userRepository = userRepository;
            _navigationService = navigationService;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            CheckUserExistAsync();

        }

        private async void CheckUserExistAsync()
        {
            IsRunning = true;
            try
            {
                await Task.Delay(3500);
                var currentUser = await _userRepository.GetCurrentUser();
                IsRunning = false;
                if (currentUser == null)
                {
                    
                    var result  =await _navigationService.NavigateAsync("/register-view");
                    return;
                }

                await _navigationService.NavigateAsync("/scheduler-view");
            }
            catch (Exception e)
            {
                IsRunning = false;
                MessagingCenter.Send<SplashscreenViewModel, (SnackBarType, Exception)>
                    (this, "error-splash", (SnackBarType.Error, e));
            }
        }

    }
}
