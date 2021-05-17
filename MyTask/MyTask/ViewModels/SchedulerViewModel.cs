using System;
using System.Threading.Tasks;
using MyTask.Repositories;
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

        public string FirstLetter
        {
            get => string.IsNullOrWhiteSpace(Username) || string.IsNullOrEmpty(Username) ? "-" : Username[0].ToString().ToUpper();
        }

        private readonly UserRepository _userRepository;

        public SchedulerViewModel(UserRepository userRepository,INavigationService navigationService):base(navigationService)
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
                Color = Xamarin.Forms.Color.FromHex(currentUser.Color);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
