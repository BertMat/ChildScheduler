using ChildScheduler.Models;
using ChildScheduler.Popups;
using ChildScheduler.Utils;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ChildScheduler.ViewModels.Popups
{
    public class AddNewChildViewModel : ViewModelBase<AppUser>
    {

        private readonly HttpClient _client;
        private Child _child;

        public Child Child
        {
            get { return _child; }
            set { SetProperty(ref _child, value); }
        }
        private string _buttonName;

        public string ButtonName
        {
            get { return IsEditing ? "EDYTUJ" : "DODAJ"; }
        }

        private Family family;

        public Family Family
        {
            get { return family; }
            set { SetProperty(ref family, value); }
        }
        private bool _isEditing;

        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                SetProperty(ref _isEditing, value);
                OnPropertyChanged(ButtonName);
            }
        }

        public AddNewChildViewModel()
        {
            _client = HttpService.GetHttpClient();
            Child = new Child { BirthDate = DateTime.Today };

        }
        public AddNewChildViewModel(Child child)
        {
            _client = HttpService.GetHttpClient();
            Child = child;
            IsEditing = true;

        }
        AsyncCommand addNewChildCommand;
        public AsyncCommand AddNewChildCommand => addNewChildCommand ??=
            new AsyncCommand(ExecuteAddNewChildCommand);
        AsyncCommand popupCommand;
        public AsyncCommand PopupCommand => popupCommand ??=
            new AsyncCommand(ExecutePopupCommand);

        private async Task ExecutePopupCommand()
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        private async Task ExecuteAddNewChildCommand()
        {
            await FetchFamilyAsync();
            if (string.IsNullOrWhiteSpace(Child.ChildName) 
                || !Child.Weight.HasValue
                || !Child.Height.HasValue
                || Child.BirthDate == null)
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Musisz uzupełnić dane...",
                    Cancel = "OK"
                });
                return;
            }

            if(!IsEditing && await AddChildAsync())
            {

                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Sukces",
                    Message = "Pomyślnie utworzono profil dziecka...",
                    Cancel = "OK"
                });
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                return;
            }
            else if(await UpdateChildAsync())
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Sukces",
                    Message = "Pomyślnie edytowano profil dziecka...",
                    Cancel = "OK"
                });
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                return;
            }

            await Dialogs.Alert(new AlertInfo
            {
                Title = "Błąd",
                Message = "Błąd podczas tworzenia profilu dziecka...",
                Cancel = "OK"
            });
            return;
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        Task OfflineAlert() => Dialogs.Alert(new AlertInfo
        {
            Cancel = "OK",
            Title = "Offline",
            Message = "Currently offline, please check internet connection."
        });

        public async Task FetchFamilyAsync()
        {
            if (IsBusy || !LoginProvider.IsLogged())
                return;
            if (!IsConnected)
            {
                await OfflineAlert();
                return;
            }

            IsBusy = true;

            var json = await _client.GetStringAsync($"api/Families");

            var item = await Task.Run(() => JsonConvert.DeserializeObject<Family>(json));

            Family = item;
            if(Family != null)
                Child.FamilyId = Family.FamilyId;

            IsBusy = false;

        }
        public async Task<bool> AddChildAsync()
        {
            if (Child == null)
            {
                return false;
            }

            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }

            IsBusy = true;

            var serializedChild = JsonConvert.SerializeObject(Child);

            var response = await _client.PostAsync($"api/Children/", new StringContent(serializedChild, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> UpdateChildAsync()
        {
            if (Child == null)
            {
                return false;
            }

            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }

            IsBusy = true;

            var serializedChild = JsonConvert.SerializeObject(Child);

            var response = await _client.PutAsync($"api/Children", new StringContent(serializedChild, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;

        }
    }
}
