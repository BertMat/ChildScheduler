using ChildScheduler.LoginPages;
using ChildScheduler.Models;
using ChildScheduler.Utils;
using ChildScheduler.Views;
using ChildScheduler.Views.People;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ChildScheduler.ViewModels
{
    public class RegisterViewModel : ViewModelBase<AppUser>
    {
        readonly bool isNew;
        HttpClient client;
        //public Command LoginCommand { get; }

        public RegisterViewModel()
        {
            client = HttpService.GetHttpClient();

            User = new AppUser();
            isNew = true;
            Title = "Rejestracja";
            //LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new AsyncCommand(async () => await ExecuteRegisterCommand());
        }

        AsyncCommand loginCommand;
        public AsyncCommand LoginCommand => loginCommand ??=
            new AsyncCommand(ExecuteLoginCommand);
        async Task ExecuteLoginCommand() => await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        /*private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync("..");
        }*/

        public AppUser User { private set; get; }

        public AsyncCommand RegisterCommand { get; }

        async Task ExecuteRegisterCommand()
        {
            if (string.IsNullOrWhiteSpace(User.Email) || string.IsNullOrWhiteSpace(User.Password))
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błędne dane..",
                    Message = "Musisz podać adres email oraz hasło..",
                    Cancel = "OK"
                });
                return;
            }


            if (isNew)
            {
                if(await Register(User))
                {
                    await Dialogs.Alert(new AlertInfo
                    {
                        Title = "Rejestracja",
                        Message = $"Pomyślnie zarejestrowano...{Environment.NewLine}" +
                        $"Aby dokończyć rejestrację musisz ją potwierdzić poprzez link aktywacyjny wysłany na adres email.",
                        Cancel = "OK"
                    });
                    await Shell.Current.GoToAsync($"{nameof(MainPage)}");
                    return;
                }
            }
            else
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Rejestracja nie powiodła się..",
                    Cancel = "OK"
                });
            }

            return;

        }
        public async Task<bool> Register(AppUser user)
        {
            if (user == null)
                return false;

            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }
            if (await LoginProvider.RegisterAsync(user))
            {
                return true;
            }
            else
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Rejestreacja nie powiodła się..",
                    Cancel = "OK"
                });
            }

            return false;
        }



        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        Task OfflineAlert() => Dialogs.Alert(new AlertInfo
        {
            Cancel = "OK",
            Title = "Offline",
            Message = "Currently offline, please check internet connection."
        });
        /*Command loginCommand;

        public Command LoginCommand => loginCommand ?? (loginCommand = new Command(async () => await ExecuteLoginCommand()));

        async Task ExecuteLoginCommand()
        {
            if (string.IsNullOrWhiteSpace(User.Email) || string.IsNullOrWhiteSpace(User.Password))
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Invalid name!",
                    Message = "An MyContacts must have both a first and last name.",
                    Cancel = "OK"
                });
                return;
            }


            if (isNew)
            {
                await DataSource.Register(User);
            }
            else
            {
                //await DataSource.UpdateItem(Contact);
            }
            await PopAsync();
        }*/
    }
}
