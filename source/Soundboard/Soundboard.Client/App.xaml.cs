using Microsoft.Extensions.DependencyInjection;
using Soundboard.Client.Services;
using Soundboard.Client.ViewModels;
using Soundboard.Client.Views;
using Soundboard.Lib.Contracts;
using System;
using System.Windows;

namespace Soundboard.Client
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = ConfigureServices();
            var shellView = services.GetRequiredService<ShellView>();
            shellView.Show();

            base.OnStartup(e);
        }

        private static IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<ShellViewModel>()
                .AddSingleton<SoundboardViewModel>()
                .AddScoped<IAudioFilePlayer, NAudioFilePlayer>()
                .AddScoped<IAudioInputForwarder, NAudioInputForwarder>()
                .AddScoped<IFilePicker, Win32FilePicker>()
                .AddTransient<ShellView>(BuildShellView)
                .BuildServiceProvider();
        }

        private static ShellView BuildShellView(IServiceProvider services)
        {
            return new ShellView
            {
                DataContext = services.GetRequiredService<ShellViewModel>()
            };
        }
    }
}
