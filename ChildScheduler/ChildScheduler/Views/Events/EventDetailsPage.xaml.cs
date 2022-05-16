using ChildScheduler.Extensions;
using ChildScheduler.Models;
using ChildScheduler.ViewModels;
using ChildScheduler.ViewModels.Events;
using MvvmHelpers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Views.Events
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailsPage : ContentPage
    {
        private MediaFile _mediaFile;
        protected EventCreateViewModel ViewModel => BindingContext as EventCreateViewModel;
        public AdvancedPageViewModel Parent { get; set; }
        public Event Event { get; set; }
        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public EventDetailsPage(AdvancedPageViewModel vm, Event eventDto)
        {
            Parent = vm;
            InitializeComponent();
            Event = eventDto;

        }

        protected override async void OnAppearing()
        {
            await ViewModel.FetchEventAsync(Event.Id);
            await Task.WhenAll(ViewModel.ExecuteLoadCategoriesCommand(), ViewModel.FetchChildren()
                , ViewModel.FetchInstitutes(), ViewModel.FetchPeople()
                , ViewModel.FetchContacts(), ViewModel.FetchPhotos());
            ViewModel.SelectedCategory = Event.Category;
            ViewModel.SelectedInstitution = ViewModel.Event.EducationalInstitution;
            ViewModel.SelectedChildren.ReplaceRange(Event.Children);
            ViewModel.SelectedPeople.ReplaceRange(Event.People);
            ViewModel.SelectedContacts.ReplaceRange(Event.Contacts);
            ViewModel.IsEditing = true;
            //ViewModel.SelectedInstitution = Event.
            ViewModel.Event.StartDate = Parent.SelectedDate;
            ViewModel.Event.EndDate = Parent.SelectedDate;
            var t = listChildren.SelectedItems;
            if(ViewModel.SelectedCategory != null)
                CategoriesListPicker.SelectedIndex = CategoriesListPicker.Items.ToList().FindIndex(p => p == ViewModel.SelectedCategory.CategoryName);
            if (ViewModel.SelectedInstitution != null)
                InstitutesListPicker.SelectedIndex = InstitutesListPicker.Items.ToList().FindIndex(p => p == ViewModel.SelectedInstitution.Name);
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
        async void Delete_Clicked(object sender, System.EventArgs e)
        {
            await ViewModel.ExecuteDeleteEventCommand();
            await this.Navigation.PopModalAsync(true);
            await Parent.ExecuteLoadCommand();
        }
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await ViewModel.ExecuteCreateEventCommand();
            await this.Navigation.PopModalAsync(true);
            await Parent.ExecuteLoadCommand();
        }

        private async void PickPhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No PickPhoto", ":( No PickPhoto available.", "OK");
                return;
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (_mediaFile == null)
                return;

            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(_mediaFile.GetStream()),
            "\"file\"",
            $"\"{_mediaFile.Path}\"");

            await ViewModel.SendFile(content);
        }

        private void listChildren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var t = sender as Frame;
        }

    }
}