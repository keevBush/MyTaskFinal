using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyTask.Models;
using Prism.Navigation;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace MyTask.ViewModels
{
    public class NewTaskViewModel:ViewModelBase
    {
        
        public ObservableCollection<Step> Steps { get; set; }
        
        public Command AddStepCommand { get; set; }
        
        private readonly INavigationService _navigationService;
        public NewTaskViewModel(INavigationService navigationService):base(navigationService)
        {
            Steps = new ObservableCollection<Step>();
            AddStepCommand = new Command(ExecuteAddStepCommand);
            _navigationService = navigationService;
            
        }

        private void ExecuteAddStepCommand(object parameter)
        {
            Steps.Add(new Step());
        }


        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            //var test = parameters["xks"];
        }
    }
}