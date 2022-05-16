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
    public class ChangePasswordViewModel : ViewModelBase<AppUser>
    {

        private readonly HttpClient _client;
        private ChangeUserPassword _changeUser;

        public ChangeUserPassword ChangeUser
        {
            get { return _changeUser; }
            set { SetProperty(ref _changeUser, value); }
        }

        public ChangePasswordViewModel()
        {
            _client = HttpService.GetHttpClient();
            ChangeUser = new ChangeUserPassword();

        }
        AsyncCommand changePasswordCommand;
        public AsyncCommand ChangePasswordCommand => changePasswordCommand ??=
            new AsyncCommand(ExecuteChangePasswordCommand);
        private async Task ExecuteChangePasswordCommand()
        {
            if (string.IsNullOrWhiteSpace(ChangeUser.Password) || string.IsNullOrWhiteSpace(ChangeUser.NewPassword))
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Musisz uzupełnić dane...",
                    Cancel = "OK"
                });
                return;
            }

            if(await ChangePasswordAsync())
            {

                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Sukces",
                    Message = "Pomyślnie zmieniono hasło...",
                    Cancel = "OK"
                });
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                return;
            }
            await Dialogs.Alert(new AlertInfo
            {
                Title = "Błąd",
                Message = "Błąd podczas zmiany hasła...",
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
        public async Task<bool> ChangePasswordAsync()
        {
            if (ChangeUser == null)
            {
                return false;
            }

            //Settings.LastUpdate = DateTime.UtcNow;

            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }
            var serializedUser = JsonConvert.SerializeObject(ChangeUser);

            var response = await _client.PostAsync($"api/User/ChangePassword", new StringContent(serializedUser, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;

        }
    }
    public class ChangeUserPassword
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
