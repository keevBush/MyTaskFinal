using System;
using System.Threading.Tasks;
using MyTask.Repositories;
using MyTask.Core.Data.Interfaces;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace MyTask.ViewModels
{
    public class SchedulerViewModel:ViewModelBase
    {
        private Color _color;
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value, "Color");
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value, "Username");
        }

        private string _firstLetter = "-";
        public string FirstLetter
        {
            get => _firstLetter;
            set => SetProperty(ref _firstLetter, value, "FirstLetter");
        }

        private readonly IUserRepository _userRepository;

        public SchedulerViewModel(IUserRepository userRepository,INavigationService navigationService):base(navigationService)
        {
            _userRepository = userRepository;
        }

        public async override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            await LoadData();

        }

        private async Task LoadData()
        {
            try
            {
                var currentUser = await _userRepository.GetCurrentUser();
                Username = currentUser.Username;
                FirstLetter = Username.ToUpper()[0].ToString();
                Color = Xamarin.Forms.Color.FromHex(currentUser.Color);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
