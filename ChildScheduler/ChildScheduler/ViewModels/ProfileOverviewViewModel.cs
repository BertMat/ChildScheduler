using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using ChildScheduler.Helpers;
using ChildScheduler.Models;
using System.Net.Http;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using ChildScheduler.Utils;
using Xamarin.Essentials;
using Newtonsoft.Json;
using ChildScheduler.LoginPages;
using Xamarin.Forms;
using ChildScheduler.Popups;

namespace ChildScheduler.ViewModels
{
    public class ProfileOverviewViewModel : ViewModelBase<Person>
    {
        private readonly HttpClient _client;
        private Person _person;

        public Person Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        public AppUser User { private set; get; }

        public ProfileOverviewViewModel()
        {
            _client = HttpService.GetHttpClient();

        }

        AsyncCommand applyCommand;
        public AsyncCommand ApplyCommand => applyCommand ??=
            new AsyncCommand(ExecuteSaveCommand);
        AsyncCommand logoutCommand;
        public AsyncCommand LogoutCommand => logoutCommand ??=
            new AsyncCommand(ExecuteLogoutCommand);
        AsyncCommand changePasswordCommand;
        public AsyncCommand ChangePasswordCommand => changePasswordCommand ??=
            new AsyncCommand(ExecuteChangePasswordCommand);

        private async Task<bool> ExecuteChangePasswordCommand()
        {
            //Acr.UserDialogs.UserDialogs.Instance.ShowLoading("TEST");
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new ChangePasswordPage());
            return true;
        }

        async Task ExecuteSaveCommand()
        {
            if (string.IsNullOrWhiteSpace(Person.PersonName) || string.IsNullOrWhiteSpace(Person.PersonSurname))
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Musisz uzupełnić dane...",
                    Cancel = "OK"
                });
                return;
            }

            if (await UpdatePerson(Person))
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Dane",
                    Message = "Pomyślnie zaktualizowano profil.",
                    Cancel = "OK"
                });
                return;
            }
            await Dialogs.Alert(new AlertInfo
            {
                Title = "Błąd",
                Message = "Logowanie nie powiodło się..",
                Cancel = "OK"
            });
            return;

        }
        public async Task ExecuteLogoutCommand()
        {
            HttpService.SetAuthToken(string.Empty);
            Dialogs.Alert(new AlertInfo
            {
                Title = "Wylogowanie",
                Message = "Wylogowano z konta",
                Cancel = "OK"
            });
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        Task OfflineAlert() => Dialogs.Alert(new AlertInfo
        {
            Cancel = "OK",
            Title = "Offline",
            Message = "Currently offline, please check internet connection."
        });

        public async Task CheckIfUserIsLoggedIn()
        {
            if (!LoginProvider.IsLogged())
            {
                await PushAsync(new MainPage());
            }
        }
        public async Task<bool> UpdatePerson(Person person)
        {
            if (person == null)
            {
                return false;
            }

            //Settings.LastUpdate = DateTime.UtcNow;

            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }
            var serializedUser = JsonConvert.SerializeObject(Person);

            var response = await _client.PutAsync($"api/Person", new StringContent(serializedUser, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }


        AsyncCommand refreshCommand;
        public AsyncCommand RefreshCommand => refreshCommand ??=
            new AsyncCommand(ExecuteRefreshCommand);

        public async Task ExecuteRefreshCommand()
        {
            await FetchPerson();
        }
        async Task FetchPerson()
        {
            if (IsBusy || !LoginProvider.IsLogged())
                return;

            IsBusy = true;

            var json = await _client.GetStringAsync($"api/Person");

            var item = await Task.Run(() => JsonConvert.DeserializeObject<Person>(json));

            Person = item;

            IsBusy = false;
        }
    }
}
