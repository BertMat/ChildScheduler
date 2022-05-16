using ChildScheduler.Interfaces;
using ChildScheduler.LoginPages;
using ChildScheduler.Models;
using ChildScheduler.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChildScheduler.Services
{
    public class APIDataSource : IDataSource<Models.Contact>
    {

        HttpClient client;
        List<Models.Contact> Contacts;

        public static string BackendUrl =
            DeviceInfo.Platform == DevicePlatform.Android ? "https://192.168.10.73:5001" : "https://192.168.10.73:5001";
        private readonly ILoginProvider _loginProvider;

        IHttpService _httpService;
        public IHttpService HttpService => _httpService ??= DependencyService.Get<IHttpService>();
        public APIDataSource()
        {


            Contacts = new List<Models.Contact>();
            _loginProvider = DependencyService.Get<ILoginProvider>();
            client = HttpService.GetHttpClient();

        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Models.Contact>> GetItems()
        {
            //Settings.LastUpdate = DateTime.UtcNow;

            if (IsConnected && _loginProvider.IsLogged())
            {
                var json = await client.GetStringAsync($"api/Contacts");

                Contacts = await Task.Run(() => JsonConvert.DeserializeObject<List<Models.Contact>>(json));
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(MainPage)}");
            }


            return Contacts;
        }

        public async Task<Models.Contact> GetItem(string id)
        {


            if (!IsConnected)
            {
                await OfflineAlert();
                return null;
            }

            if (id != null)
            {
                var json = await client.GetStringAsync($"api/Contacts/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Models.Contact>(json));
            }


            return null;
        }

        public async Task<bool> AddItem(Models.Contact contact)
        {
            if (contact == null)
                return false;

            //Settings.LastUpdate = DateTime.UtcNow;

            var serializedContact = JsonConvert.SerializeObject(contact);


            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }

            var response = await client.PostAsync($"api/Contacts", new StringContent(serializedContact, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var c = JsonConvert.DeserializeObject<Models.Contact>(json);
                contact.Id = c.Id;
                return true;
            }

            return false;
        }
        public async Task<bool> Register(AppUser user)
        {
            if (user == null)
                return false;

            //Settings.LastUpdate = DateTime.UtcNow;

            var serializedUser = JsonConvert.SerializeObject(user);


            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }

            var response = await client.PostAsync($"api/User/Register", new StringContent(serializedUser, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var jsonUser = JsonConvert.DeserializeObject<AppUser>(json);
                user.Token = jsonUser.Token;
                user.RefreshToken = jsonUser.RefreshToken;
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateItem(Models.Contact contact)
        {
            if (contact == null || contact.Id == null)
                return false;

            Settings.LastUpdate = DateTime.UtcNow;


            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }

            var serializedContact = JsonConvert.SerializeObject(contact);

            var response = await client.PutAsync($"api/Contacts", new StringContent(serializedContact, Encoding.UTF8, "application/json"));


            return response.IsSuccessStatusCode;
        }


        public async Task<bool> RemoveItem(Models.Contact contact)
        {
            var id = contact?.Id;

            if (id != null && !IsConnected)
                return false;

            //Settings.LastUpdate = DateTime.UtcNow;


            var response = await client.DeleteAsync($"api/Contacts/{id}");


            return response.IsSuccessStatusCode;
        }


        Task OfflineAlert() => Dialogs.Alert(new AlertInfo
        {
            Cancel = "OK",
            Title = "Offline",
            Message = "Currently offline, please check internet connection."
        });

    }
}
