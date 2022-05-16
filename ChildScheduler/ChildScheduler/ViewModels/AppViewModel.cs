using System;
using System.Collections.Generic;
using System.Text;
using ChildScheduler.LoginPages;
using ChildScheduler.Models;
using Xamarin.Forms;

namespace ChildScheduler.ViewModels
{
    public class AppViewModel : ViewModelBase<AppUser>
    {

        private bool isLoggedIn;

        public bool IsLoggedIn { get => isLoggedIn; set => SetProperty(ref isLoggedIn, value); }
        public AppViewModel()
        {
            MessagingCenter.Subscribe<LoginViewModel>(this, "loggedIn", (sender) =>
            {
                isLoggedIn = true;
            });

            MessagingCenter.Subscribe<LoginViewModel>(this, "notLoggedIn", (sender) =>
            {
                isLoggedIn = false;
            });

        }
    }
}
