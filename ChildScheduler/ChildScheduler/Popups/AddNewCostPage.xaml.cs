using ChildScheduler.ViewModels.Costs;
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
    public partial class AddNewCostPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        protected AddNewCostViewModel ViewModel => BindingContext as AddNewCostViewModel;
        protected CostDiagramViewModel Parent;
        public AddNewCostPage(CostDiagramViewModel viewModel = null)
        {
            InitializeComponent();
            if (viewModel != null)
                Parent = viewModel;
        }

        public AddNewCostPage(Models.Cost cost, CostDiagramViewModel viewModel = null)
        {
            InitializeComponent();
            BindingContext = new AddNewCostViewModel(cost);
            if(viewModel != null)
                Parent = viewModel;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel != null)
                await ViewModel.FetchCategoriesAsync();
        }
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            if (Parent != null)
                await Parent.FetchCosts();
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