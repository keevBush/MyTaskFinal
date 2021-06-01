using MyTasks.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace MyTask.Services
{
    public enum SnackBarType
    {
        Default,Error,Warning
    }
    public enum SnackbarDuration
    {
        Short,Long
    }
    public static class SnackbarServices
    {
        public static async Task ShowDefaultSnackBarAsync(this Page page,string message, SnackbarDuration duration = SnackbarDuration.Short)
        {
            var messageOptions = new MessageOptions
            {
                Foreground = Color.White,
                Message = message
            };
            var options = new ToastOptions
            {
                MessageOptions = messageOptions,
                Duration = duration == SnackbarDuration.Long ? TimeSpan.FromMilliseconds(5000): TimeSpan.FromMilliseconds(2500),
                BackgroundColor = Color.FromHex("5C72EF"),
                IsRtl = false,
            };
            await page.DisplayToastAsync(options);
        }

        public static async  Task ShowWarningSnackBarAsync(this Page page, string message, SnackbarDuration duration = SnackbarDuration.Short)
        {
            var messageOptions = new MessageOptions
            {
                Foreground = Color.Black,
                Message = message
            };
            var options = new ToastOptions
            {
                MessageOptions = messageOptions,
                Duration = duration == SnackbarDuration.Long ? TimeSpan.FromMilliseconds(5000) : TimeSpan.FromMilliseconds(2500),
                BackgroundColor = Color.FromHex("ffd000"),
                IsRtl = false,
            };
            await page.DisplayToastAsync(options);
        }

        public static async  Task ShowErrorSnackBarAsync(this Page page, string message, SnackbarDuration duration = SnackbarDuration.Short)
        {
            var messageOptions = new MessageOptions
            {
                Foreground = Color.White,
                Message = message
            };
            var options = new ToastOptions
            {
                MessageOptions = messageOptions,
                Duration = duration == SnackbarDuration.Long ? TimeSpan.FromMilliseconds(5000) : TimeSpan.FromMilliseconds(2500),
                BackgroundColor = Color.FromHex("e22f2f"),
                IsRtl = false,
            };
            await page.DisplayToastAsync(options);
        }

        public static async  Task ShowSuccessSnackBarAsync(this Page page, string message, SnackbarDuration duration = SnackbarDuration.Short)
        {
            var messageOptions = new MessageOptions
            {
                Foreground = Color.White,
                Message = message
            };
            
            var options = new SnackBarOptions()
            {
                MessageOptions = messageOptions,
                Duration = duration == SnackbarDuration.Long ? TimeSpan.FromMilliseconds(5000) : TimeSpan.FromMilliseconds(2500),
                BackgroundColor = Color.FromHex("20be28"),
                IsRtl = false,

            };
            await page.DisplaySnackBarAsync(options);
        }
    }
}
