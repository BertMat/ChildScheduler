using ChildScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Views.LoginPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as RegisterViewModel;

            vm.RegisterCommand.ExecuteAsync();
        }

        private async void OnClicked(object sender, EventArgs e)
        {
            ButtonWithSpinner.IsBusy = !ButtonWithSpinner.IsBusy;

            // simulate long time process
            if (ButtonWithSpinner.IsBusy)
            {
                await Task.Delay(3000);
                OnClicked(this, EventArgs.Empty);
            }
        }
    }
}