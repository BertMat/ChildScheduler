using ChildScheduler.Animations;
using ChildScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChildScheduler.LoginPages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () =>
            {
                //await ViewAnimations.FadeAnimY(EmailPancake);

            });
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
        private void Button_Clicked(object sender, EventArgs e)
        {
            var vm = this.BindingContext as LoginViewModel;
            vm.LoginCommand.ExecuteAsync();
            //vm.LoginCommand.Execute(sender);
        }
        protected async void ClickLoginPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPages.LoginPage());
        }
        protected void ClickDellyShop(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://codecanyon.net/item/dellyshop-ecommerce-application-template-xamarin-forms-androidios/25307064"));
        }
        protected void ClickXFShop(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://codecanyon.net/item/xfshop-ecommerce-application-template-cross-platformandroidios/24853588"));

        }
    }
}
