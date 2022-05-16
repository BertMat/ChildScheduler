using ChildScheduler.Interfaces;
using ChildScheduler.Models;
using ChildScheduler.Models.Responses;
using ChildScheduler.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(LoginProvider))]
namespace ChildScheduler.Services
{
    public class LoginProvider : ILoginProvider
    {
        HttpClient httpClient;
        public AppUser User { get; set; }


        IHttpService _httpService;
        public IHttpService HttpService => _httpService ??= DependencyService.Get<IHttpService>();

        public LoginProvider()
        {
            httpClient = HttpService.GetHttpClient();
        }
        public AppUser GetUser()
        {
            return User;
        }
        public bool IsLogged()
        {
            return User != null;
        }
        public async Task<bool> IsAuthorized()
        {
            try
            {
                var response = await httpClient.GetAsync($"api/User/Authorization");
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }


        }
        public async Task<AppUser> LoginAsync(AppUser user)
        {
            User = user;
            var serializedUser = JsonConvert.SerializeObject(User);

            var response = await httpClient.PostAsync($"api/User/Login", new StringContent(serializedUser, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var jsonUser = JsonConvert.DeserializeObject<AppUser>(json);
                User.Token = jsonUser.Token;
                User.RefreshToken = jsonUser.RefreshToken;
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var json = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<RegistrationResponse>(json);
                if(jsonResponse.Errors.Contains("Konto nie jest aktywowane"))
                    User.EmailConfirmed = jsonResponse.EmailConfirmed;
            }
            return User;
        }
        public async Task<bool> RegisterAsync(AppUser user)
        {
            User = user;
            var serializedUser = JsonConvert.SerializeObject(User);

            var response = await httpClient.PostAsync($"api/User/Register", new StringContent(serializedUser, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

    }
}
