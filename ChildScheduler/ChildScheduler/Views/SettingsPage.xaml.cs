using ChildScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Views
{
    public partial class SettingsPage : ContentPage
    {
        protected SettingsViewModel ViewModel => BindingContext as SettingsViewModel;

        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = new SettingsViewModel();
        }
        async void CloseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}