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
    public partial class AddNewEducationalInsPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        protected AddNewEducationalInsViewModel ViewModel => BindingContext as AddNewEducationalInsViewModel;
        public AddNewEducationalInsPage()
        {
            InitializeComponent();
        }
        public AddNewEducationalInsPage(Models.EducationalInstitution educationalInstitution)
        {
            InitializeComponent();
            BindingContext = new AddNewEducationalInsViewModel(educationalInstitution);
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