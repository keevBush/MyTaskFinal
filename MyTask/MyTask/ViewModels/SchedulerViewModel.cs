using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyTask.Repositories;
using MyTask.Core.Data.Interfaces;
using MyTask.Models;
using MyTask.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace MyTask.ViewModels
{
    public class SchedulerViewModel:ViewModelBase,Prism.AppModel.IPageLifecycleAware
    {
        private DateTime _currentDate = DateTime.Today;

        public DateTime CurrentDate
        {
            get => _currentDate;
            set => SetProperty(ref _currentDate, value, "CurrentDate");
        }
        
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
        public Command RefreshCommand { get; set; }
        public Command CurrentDateChangedCommand { get; set; }
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
            CurrentDateChangedCommand = new Command(ExecuteCurrentDateCommandChanged);
            RefreshCommand = new Command(ExecuteRefreshCommand);
            MessagingCenter.Instance.Subscribe<ViewModels.NewTaskViewModel>(this,"update-tasks",ExecuteUpdateTasks);
        }

        private async void ExecuteRefreshCommand(object obj)
        {
            try
            {
                var tasks = await _taskRepository.
                GetAsync(t => t.CreatedAt >= CurrentDate && t.CreatedAt < CurrentDate.AddDays(1));

                Tasks.Clear();

                foreach (var task in tasks)
                {
                    Tasks.Add(task);
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.ShowErrorSnackBarAsync(e.Message);
            }
            
        }

        private async void ExecuteCurrentDateCommandChanged(object obj)
        {
            try
            {
                var currentDate = (DateTime)obj;
                CurrentDate = currentDate;

                var tasks = await _taskRepository.
                    GetAsync(t => t.CreatedAt >= CurrentDate && t.CreatedAt < CurrentDate.AddDays(1));

                Tasks.Clear();

                foreach (var task in tasks)
                {
                    Tasks.Add(task);
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.ShowErrorSnackBarAsync(e.Message);
            }

        }

        private async void ExecuteUpdateTasks(NewTaskViewModel obj)
        {
            await LoadData();
        }

        public  override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            await LoadData();

        }

        private async Task LoadData()
        {
            IsRunning = true;
            var currentUser = await _userRepository.GetCurrentUser();
            Username = currentUser.Username;
            FirstLetter = Username.ToUpper()[0].ToString();
            Color = Xamarin.Forms.Color.FromHex(currentUser.Color);
                
            try
            {
                var tasks = await _taskRepository.
                    //GetAllAync();
                    GetAsync(t => t.CreatedAt >= CurrentDate && t.CreatedAt < CurrentDate.AddDays(1));
                IsRunning = false;

                Tasks.Clear();
                foreach (var task in tasks)
                {
                    Tasks.Add(task);
                }
                
            }
            catch (Exception e)
            {
                IsRunning = false;
                await Application.Current.
                                MainPage.
                                ShowErrorSnackBarAsync("One error occured", 
                                    SnackbarDuration.Long);
            }
            
        }


        
        private async Task ExecuteNewTaskPopupCommand()
        {
            await _navigationService.NavigateAsync("new-task-popup", ("currentDate",CurrentDate));
        }

        public void OnAppearing()
        {
            
        }

        public void OnDisappearing()
        {
            
        }
    }
}
