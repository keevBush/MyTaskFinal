using System;
using LiteDB;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MyTask.Repositories;
using MyTask.Services;
using MyTask.ViewModels;
using MyTask.Views;
using MyTask.Core.Data.Interfaces;
using MyTask.Models;
using MyTask.Views.Popups;
using MyTasks.Core.Data.Interfaces;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
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
            AppCenter.Start("android=75e0577e-b876-444c-92cf-8f7b6a618a5c;" +
                            "ios=5c07eac0-82eb-42dc-8893-4367a6d342d2;",
                typeof(Analytics), typeof(Crashes));
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            InitializeMapping();
            await NavigationService.NavigateAsync("splashscreen-view");
        }

        private void InitializeMapping()
        {
            BsonMapper.Global.Entity<Task>()
                .Id(m => m.Id)
                .Field(m => m.Deadline,"deadline")
                .Field(m => m.Id,"_id")
                .Field(m => m.Description,"description")
                .Field(m => m.Labels,"labels")
                .Field(m => m.Name,"name")
                .Field(m => m.CreatedAt,"created_at")
                .DbRef(m => m.Steps, "Steps");
            BsonMapper.Global.Entity<Step>()
                .Id(m => m.Id)
                .Field(m => m.Id,"_id")
                .Field(m => m.Name,"name")
                .Field(m => m.Deadline,"deadline")
                .Field(m => m.RememberDate,"remember_date")
                .Field(m => m.Repeat,"repeat")
                .DbRef(m => m.Task, "Tasks");
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<SchedulerPage, SchedulerViewModel>("scheduler-view");
            containerRegistry.RegisterForNavigation<SplashscreenPage, SplashscreenViewModel>("splashscreen-view");
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterViewModel>("register-view");
            containerRegistry.RegisterForNavigation<NewTaskPopupPage, NewTaskViewModel>("new-task-popup");


            containerRegistry.RegisterSingleton<IUserRepository,UserRepository>();
            containerRegistry.RegisterSingleton<TaskRepository>();

            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            
            
        }
    }
}
