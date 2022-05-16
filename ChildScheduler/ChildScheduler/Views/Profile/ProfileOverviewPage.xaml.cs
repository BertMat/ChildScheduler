using ChildScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileOverviewPage : ContentPage
    {
        protected ProfileOverviewViewModel ViewModel => BindingContext as ProfileOverviewViewModel;
        public ProfileOverviewPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.CheckIfUserIsLoggedIn();
            await ViewModel.ExecuteRefreshCommand();
        }
    }
}