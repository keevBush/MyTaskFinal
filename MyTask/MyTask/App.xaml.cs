using MyTask.Repositories;
using MyTask.Services;
using MyTask.ViewModels;
using MyTask.Views;
using MyTasks.Core.Data.Interfaces;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MyTask
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
#if DEBUG
            HotReloader.Current.Run(this); 
#endif
            await NavigationService.NavigateAsync("splashscreen-view");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SchedulerPage, SchedulerViewModel>("scheduler-view");
            containerRegistry.RegisterForNavigation<SplashscreenPage, SplashscreenViewModel>("splashscreen-view");
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterViewModel>("register-view");

            containerRegistry.RegisterInstance(new DatabaseService());
            containerRegistry.RegisterSingleton<IDatabaseService,DatabaseService>();
            containerRegistry.RegisterSingleton<IUserRepository,UserRepository>();

            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
    }
}
