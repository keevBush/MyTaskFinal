using MyTask.Repositories;
using MyTask.Services;
using MyTask.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyTask.Core.Data.Interfaces;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MyTask.ViewModels
{
    public class RegisterViewModel: ViewModelBase
    {
        private static List<Color> _colors = new List<Color>()
        {
            Color.FromHex("ff0000"),Color.FromHex("4169e1"),Color.FromHex("20BE28"),Color.FromHex("8921b3"), Color.FromHex("136b4e")
        };
        #region Properties
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value, "IsLoading");
        }

        private string _firstLetter;
        public string FirstLetter
        {
            get => _firstLetter;
            set => SetProperty<string>(ref _firstLetter, value, "FirstLetter");
        }
     
        private Color _currentColor;
        public Color CurrentColor
        {
            get => _currentColor;
            set => SetProperty<Color>(ref _currentColor, value, "CurrentColor");
        }
        
        private string _username;
        public string Username
        {
            get => _username;
            set {
                FirstLetter = string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) ? "A" : value[0].ToString().ToUpper();
                CurrentColor = RandomColor();
                SetProperty<string>(ref _username, value, "Username");

            }
        }

        private string _fullname;
        public string Fullname
        {
            get => _fullname;
            set => SetProperty<string>(ref _fullname, value, "Fullname");
        }
        #endregion

        #region Commands
        public AsyncCommand RegisterCommad { get; set; }
        #endregion

        #region Injection
        private readonly IUserRepository _userRepository;
        private readonly INavigationService _navigationService;
        #endregion

        public RegisterViewModel(IUserRepository userRepository, INavigationService navigationService) : base(navigationService)
        {
            _userRepository = userRepository;
            _navigationService = navigationService;
            RegisterCommad = new AsyncCommand(ExecuteRegisterCommad);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            CurrentColor = RandomColor();
        }

        private async System.Threading.Tasks.Task ExecuteRegisterCommad()
        {
            try
            {
                if (string.IsNullOrEmpty(Username))
                    throw new ApplicationException("Error on username");
                if (string.IsNullOrEmpty(Fullname))
                    throw new ApplicationException("Error on fullname");
                var user = new User
                {
                    Color = CurrentColor.ToHex(),
                    Fullname = Fullname.Trim(),
                    Id = Guid.NewGuid().ToString(),
                    Username = Username.Trim()
                };
                await _userRepository.CreateUser(user);
                await _navigationService.NavigateAsync("/scheduler-view");
            }
            catch (Exception e)
            {
                MessagingCenter.Send(this, "ExceptionRise", (e, SnackBarType.Warning));
            }
        }

        private Color RandomColor()
        {
            var random = new Random();
            var index = random.Next(0, _colors.Count - 1);
            return _colors[index];
        }
    }
}
