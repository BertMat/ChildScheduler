using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using ChildScheduler.Animations;

namespace ChildScheduler.LoginPages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>
            {
                await ViewAnimations.FadeAnimY(Logo);
                await ViewAnimations.FadeAnimY(LoginButton);
            });
        }
        protected void Back(object s, EventArgs e)
        {
            Navigation.PopAsync();
        }
        protected async void Login(object s, EventArgs e)
        {
        }
    }
}
