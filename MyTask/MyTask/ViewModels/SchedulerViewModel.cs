using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyTask.Repositories;
using MyTask.Core.Data.Interfaces;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
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
        public AsyncCommand NewTaskPopupCommand { get; set; }

        public ObservableCollection<Models.Task> Tasks { get; set; }
        private readonly IUserRepository _userRepository;
        private readonly TaskRepository _taskRepository;
        private readonly INavigationService _navigationService;
        
        public SchedulerViewModel(IUserRepository userRepository,INavigationService navigationService, TaskRepository taskRepository):base(navigationService)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _navigationService = navigationService;

            Tasks = new ObservableCollection<Models.Task>();
            NewTaskPopupCommand = new AsyncCommand(ExecuteNewTaskPopupCommand);
        }

        public async override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            await LoadData();

        }

        private async Task LoadData()
        {
            var currentUser = await _userRepository.GetCurrentUser();
            Username = currentUser.Username;
            FirstLetter = Username.ToUpper()[0].ToString();
            Color = Xamarin.Forms.Color.FromHex(currentUser.Color);
                
            Tasks.Clear();

            var tasks = await _taskRepository.GetAsync(t => t.CreatedAt == DateTime.Now);
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }
        
        private async Task ExecuteNewTaskPopupCommand()
        {
            await _navigationService.NavigateAsync("new-task-popup");
        }
    }
}
