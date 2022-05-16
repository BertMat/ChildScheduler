using ChildScheduler.ViewModels;
using ChildScheduler.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventCreatePage : ContentPage
    {
        protected EventCreateViewModel ViewModel => BindingContext as EventCreateViewModel;
        public AdvancedPageViewModel Parent { get; set; }
        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public EventCreatePage(AdvancedPageViewModel vm) 
        {
            Parent = vm;
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            await Task.WhenAll(ViewModel.ExecuteLoadCategoriesCommand(), ViewModel.FetchChildren()
                , ViewModel.FetchInstitutes(), ViewModel.FetchPeople()
                , ViewModel.FetchContacts());
            ViewModel.Event.StartDate = Parent.SelectedDate;
            ViewModel.Event.EndDate = Parent.SelectedDate;
            base.OnAppearing();
        }
        /*protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("Wyjście", "Chcesz wyjść?", "Yes", "No"))
                {
                    base.OnBackButtonPressed();

                }
            });


            return true;
        }*/
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await ViewModel.ExecuteCreateEventCommand();
            await this.Navigation.PopModalAsync(true);
            await Parent.ExecuteLoadCommand();
        }

        private void listChildren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var t = sender as Frame;
        }
    }
}