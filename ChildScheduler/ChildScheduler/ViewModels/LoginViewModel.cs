using ChildScheduler.Models;
using ChildScheduler.Utils;
using ChildScheduler.Views.Contacts;
using ChildScheduler.Views.LoginPages;
using ChildScheduler.Views.People;
using ChildScheduler.Views.Profile;
using MvvmHelpers.Commands;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChildScheduler.ViewModels
{
    public class LoginViewModel : ViewModelBase<AppUser>
    {
        //public Command LoginCommand { get; }
        //public Command RegisterCommand { get; }

        private readonly HttpClient _client;
        public LoginViewModel()
        {
            _client = HttpService.GetHttpClient();
            User = new AppUser();
            //LoginCommand = new Command(OnLoginClicked);
            //RegisterCommand = new Command(OnRegisterClicked);
        }

        AsyncCommand registerCommand;
        public AsyncCommand RegisterCommand => registerCommand ??=
            new AsyncCommand(ExecuteRegisterCommand);
        async Task ExecuteRegisterCommand() => await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        AsyncCommand loginCommand;
        public AsyncCommand LoginCommand => loginCommand ??=
            new AsyncCommand(ExecuteLoginCommand);
        //Task ExecuteLoginCommand() => PushAsync(new ListPage());
        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"{nameof(ListPage)}");
            //await Navigation
        }
        public async void OnRegisterClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }
        private bool _splashVisible = false;

        public bool SplashVisible
        {
            get { return _splashVisible; }
            set { SetProperty(ref _splashVisible, value); }
        }

        async Task ExecuteLoginCommand()
        {
            if (string.IsNullOrEmpty(User.Email))
            {
                User.Password = "Bartek4899m1#";
                User.Email = "bartek4899m@gmail.com";
            }
            if (string.IsNullOrWhiteSpace(User.Email) || string.IsNullOrWhiteSpace(User.Password))
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Musisz uzupełnić dane...",
                    Cancel = "OK"
                });
                return;
            }
            SplashVisible = true;
            if (await Login(User))
            {
                MessagingCenter.Send<LoginViewModel>(this,
                    LoginProvider.IsLogged() ? "loggedIn" : "notLoggedIn"
                );

                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Logowanie",
                    Message = "Pomyślnie zalogowano.",
                    Cancel = "OK"
                });
                if(await HasProfile())
                {
                    await Shell.Current.GoToAsync($"{nameof(ProfileOverviewPage)}");
                    SplashVisible = false;
                    return;
                }

                await Shell.Current.GoToAsync($"{nameof(CreateProfilePage)}");
                SplashVisible = false;
                return;
            }

            MessagingCenter.Send<LoginViewModel>(this,
                LoginProvider.IsLogged() ? "loggedIn" : "notLoggedIn"
            );
            SplashVisible = false;
            await Dialogs.Alert(new AlertInfo
            {
                Title = "Błąd",
                Message = "Logowanie nie powiodło się..",
                Cancel = "OK"
            });
            return;

        }
        public AppUser User { private set; get; }


        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        Task OfflineAlert() => Dialogs.Alert(new AlertInfo
        {
            Cancel = "OK",
            Title = "Offline",
            Message = "Currently offline, please check internet connection."
        });
        public async Task<bool> HasProfile()
        {
            var response = await _client.GetAsync($"api/Person");

            return response.IsSuccessStatusCode;
        }
        public async Task CheckIfUserIsLoggedIn()
        {
            if(LoginProvider.IsLogged())
            {
                await PushAsync(new ListPage());
            }
        }
        public async Task<bool> Login(AppUser user)
        {
            if (user == null)
                return false;

            //Settings.LastUpdate = DateTime.UtcNow;

            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }
            user = await LoginProvider.LoginAsync(user);
            if (user.Token != null)
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var token = jwtHandler.ReadJwtToken(user.Token);
                HttpService.SetAuthToken(user.Token);
                return true;
            }
            else if(!user.EmailConfirmed)
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = $"Konto nie jest aktywowane.{Environment.NewLine}Sprawdź pocztę w celu aktywacji.",
                    Cancel = "OK"
                });
            }

            return false;
        }
    }
}
