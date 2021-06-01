using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MyTask.Models;
using MyTask.Repositories;
using MyTask.Services;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace MyTask.ViewModels
{
    public class NewTaskViewModel:ViewModelBase
    {

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, "Name");
        }
        
        public ObservableCollection<Step> Steps { get; set; }
        
        public Command AddStepCommand { get; set; }
        
        public Command SaveTaskCommand { get; set; }
        public Command<string> DeleteCurrentStepCommand { get; set; }
        
        private readonly INavigationService _navigationService;
        private readonly TaskRepository _taskRepository;
        private readonly StepRepository _stepRepository;
        public NewTaskViewModel(INavigationService navigationService, TaskRepository taskRepository, StepRepository stepRepository):base(navigationService)
        {
            
            Steps = new ObservableCollection<Step>();
            AddStepCommand = new Command(ExecuteAddStepCommand);
            SaveTaskCommand = new Command(ExecuteSaveCommand);
            DeleteCurrentStepCommand = new Command<string>(ExecuteDeleteCurrentStepCommand);

            _navigationService = navigationService;
            _taskRepository = taskRepository;
            _stepRepository = stepRepository;
        }

        private async void ExecuteSaveCommand(object obj)
        {
            try
            {
                IsRunning = true;            
                var task = new Models.Task()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Name,
                    Deadline = DateTime.MaxValue,
                    Description = "",
                    Labels = new List<string>(),
                    Steps = Steps.ToList(),
                };
                for (int i = 0; i < Steps.Count; i++)
                {
                    Steps[i].Task = task;
                }
                await _stepRepository.CreateAsync(Steps.ToArray());
                MessagingCenter.Instance.Send(this,"update-tasks");
                //await _taskRepository.CreateAsync(task);
                await _navigationService.GoBackAsync();
            }
            catch (Exception e)
            {
                await Application.Current.
                                MainPage.
                                ShowErrorSnackBarAsync("One error occured", 
                                    SnackbarDuration.Long);
            }
        }

        private void ExecuteAddStepCommand(object parameter)
        {
            Steps.Add(new Step()
            {
                Id = Guid.NewGuid().ToString(),
                Name = $"Name {Steps.Count - 1}"
            });
        }

        private  void ExecuteDeleteCurrentStepCommand(string parameter)
        {
         
            var index = Steps.Select(i => i.Id).ToList().IndexOf(parameter);
            Steps.RemoveAt(index);
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            //var test = parameters["xks"];
        }
    }
}