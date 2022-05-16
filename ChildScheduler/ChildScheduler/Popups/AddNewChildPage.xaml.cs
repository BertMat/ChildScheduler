using ChildScheduler.ViewModels.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewChildPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        protected AddNewChildViewModel ViewModel => BindingContext as AddNewChildViewModel;
        public AddNewChildPage()
        {
            InitializeComponent();
        }

        public AddNewChildPage(Models.Child child)
        {
            InitializeComponent();
            BindingContext = new AddNewChildViewModel(child);
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
        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}