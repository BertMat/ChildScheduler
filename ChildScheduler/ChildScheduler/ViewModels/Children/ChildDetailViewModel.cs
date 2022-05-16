using ChildScheduler.Models;
using ChildScheduler.Popups;
using ChildScheduler.Utils;
using ChildScheduler.Views;
using ChildScheduler.Views.Contacts;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ChildScheduler.ViewModels.Children
{
    public class ChildDetailViewModel : ViewModelBase<Models.Child>
    {
        protected readonly HttpClient _client;
        public ChildDetailViewModel()
        {
            _client = HttpService.GetHttpClient();
        }
        public ChildDetailViewModel(Child child)
        {
            _client = HttpService.GetHttpClient();
            Child = child;
        }

        AsyncCommand refreshCommand;
        public AsyncCommand RefreshCommand => refreshCommand ??=
            new AsyncCommand(ExecuteRefreshCommand);

        async Task ExecuteRefreshCommand()
        {
            await FetchChild();
        }
       public async Task FetchChild()
        {
            if (IsBusy || !LoginProvider.IsLogged())
                return;

            IsBusy = true;

            var json = await _client.GetStringAsync($"api/Children/{Child.Id}");

            var item = await Task.Run(() => JsonConvert.DeserializeObject<Child>(json));

            Child = item;

            IsBusy = false;
        }
        private Child _child;
        public Child Child
        {
            get { return _child; }
            set { SetProperty(ref _child, value); }
        }

        AsyncCommand editCommand;
        public AsyncCommand EditCommand => editCommand ??=
            new AsyncCommand(ExecuteEditCommand);
        async Task ExecuteEditCommand() =>
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new AddNewChildPage(Child));

        AsyncCommand deleteCommand;

        public AsyncCommand DeleteCommand => deleteCommand ?? (deleteCommand = new AsyncCommand(ExecuteDeleteCommand));

        async Task ExecuteDeleteCommand()
        {
            await Dialogs.Question(new QuestionInfo
            {
                Title = string.Format("Delete {0}?", Child.ChildName),
                Question = null,
                Positive = "Delete",
                Negative = "Cancel",
                OnCompleted = new Action<bool>(async result =>
                {
                    if (!result)
                        return;

                    await DataSource.RemoveItem(Child);

                    await PopAsync();
                })
            });
        }

        public async Task DisplayGeocodingError()
        {
            await Dialogs.Alert(new AlertInfo
            {
                Title = "Geocoding Error",
                Message = "Please make sure the address is valid, or that you have a network connection.",
                Cancel = "OK"
            });

            IsBusy = false;
        }
        AsyncCommand<Models.EducationalInstitution> getDirectionsCommand;

        public AsyncCommand<Models.EducationalInstitution> GetDirectionsCommand =>
            getDirectionsCommand ??= new AsyncCommand<Models.EducationalInstitution>(ExecuteGetDirectionsCommand);


        async Task ExecuteGetDirectionsCommand(Models.EducationalInstitution institution)
        {
            try
            {
                await Map.OpenAsync(new Placemark
                {
                    AdminArea = institution.State,
                    Locality = institution.City,
                    PostalCode = institution.PostalCode,
                    Thoroughfare = institution.AddressString
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Not Supported",
                    Message = "Unable to open a map application on the device..",
                    Cancel = "OK"
                });
            }
        }
    }
}
