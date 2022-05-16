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
    public class AddNewEducationalInsViewModel : ViewModelBase<AppUser>
    {

        private readonly HttpClient _client;
        private EducationalInstitution _educInstitution;

        public EducationalInstitution EducInstitution
        {
            get { return _educInstitution; }
            set { SetProperty(ref _educInstitution, value); }
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
        public AddNewEducationalInsViewModel()
        {
            _client = HttpService.GetHttpClient();
            EducInstitution = new EducationalInstitution { };

        }
        private bool _isEditing;

        public bool IsEditing
        {
            get { return _isEditing; }
            set { 
                SetProperty(ref _isEditing, value);
                OnPropertyChanged(ButtonName);
            }
        }

        public AddNewEducationalInsViewModel(EducationalInstitution institution)
        {
            _client = HttpService.GetHttpClient();
            EducInstitution = institution;
            IsEditing = true;
        }
        AsyncCommand addNewInstitutionCommand;
        public AsyncCommand AddNewInstitutionCommand => addNewInstitutionCommand ??=
            new AsyncCommand(ExecuteAddNewInstitutionCommand);
        AsyncCommand popupCommand;
        public AsyncCommand PopupCommand => popupCommand ??=
            new AsyncCommand(ExecutePopupCommand);

        private async Task ExecutePopupCommand()
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        private async Task ExecuteAddNewInstitutionCommand()
        {
            await FetchFamilyAsync();
            if (string.IsNullOrWhiteSpace(EducInstitution.Name) 
                || string.IsNullOrWhiteSpace(EducInstitution.FullName)
                || string.IsNullOrWhiteSpace(EducInstitution.PostalCode)
                || string.IsNullOrWhiteSpace(EducInstitution.City)
                || string.IsNullOrWhiteSpace(EducInstitution.State)
                || string.IsNullOrWhiteSpace(EducInstitution.Street))
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Musisz uzupełnić dane...",
                    Cancel = "OK"
                });
                return;
            }

            if(!IsEditing && await AddEducInstitutionAsync())
            {

                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Sukces",
                    Message = "Pomyślnie dodano placówkę nauczania...",
                    Cancel = "OK"
                });
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                return;
            }
            else if(await UpdateEducInstitutionAsync())
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Sukces",
                    Message = "Pomyślnie zaktualizowano placówkę nauczania...",
                    Cancel = "OK"
                });
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                return;
            }
            await Dialogs.Alert(new AlertInfo
            {
                Title = "Błąd",
                Message = "Błąd podczas tworzenia placówki nauczania...",
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
                EducInstitution.FamilyId = Family.FamilyId;

            IsBusy = false;

        }
        public async Task<bool> AddEducInstitutionAsync()
        {
            if (EducInstitution == null)
            {
                return false;
            }

            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }

            IsBusy = true;

            var serializedInstitution = JsonConvert.SerializeObject(EducInstitution);

            var response = await _client.PostAsync($"api/EducationalInstitutions", new StringContent(serializedInstitution, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> UpdateEducInstitutionAsync()
        {
            if (EducInstitution == null)
            {
                return false;
            }

            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }

            IsBusy = true;

            var serializedInstitution = JsonConvert.SerializeObject(EducInstitution);

            var response = await _client.PutAsync($"api/EducationalInstitutions", new StringContent(serializedInstitution, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;

        }
    }
}
