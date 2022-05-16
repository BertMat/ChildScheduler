using ChildScheduler.LoginPages;
using ChildScheduler.ViewModels;
using ChildScheduler.Views;
using ChildScheduler.Views.Contacts;
using ChildScheduler.Views.LoginPages;
using ChildScheduler.Views.People;
using ChildScheduler.Views.Profile;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ChildScheduler
{
    public partial class AppShell : Xamarin.Forms.Shell
    {

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(CreateProfilePage), typeof(CreateProfilePage));
            Routing.RegisterRoute(nameof(ProfileOverviewPage), typeof(ProfileOverviewPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(ListPage), typeof(ListPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            this.CurrentItem = loginPage;
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

    }
}
