using ChildScheduler.Services;
using ChildScheduler.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler
{
    public partial class App : Application
    {
        public static bool UseLocalDataSource = false;
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<APIDataSource>();


            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
