using ChildScheduler.Extensions;
using ChildScheduler.Models;
using ChildScheduler.Utils;
using ChildScheduler.Views;
using ChildScheduler.Views.Contacts;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChildScheduler.ViewModels
{
    public class EditViewModel : ViewModelBase<Models.Contact>
    {
        HttpClient client;
        readonly bool isNew;

        public EditViewModel()
        {
            client = HttpService.GetHttpClient();
            Contact = new Contact();
            isNew = true;
            Title = "New Contact";
            //Contact.SocialMedias = new List<SocialMedia>();
        }

        public Contact Contact { private set; get; }

        Command saveCommand;

        public Command SaveCommand => saveCommand ?? (saveCommand = new Command(async () => await ExecuteSaveCommand()));

        async Task ExecuteSaveCommand()
        {
            if (string.IsNullOrWhiteSpace(Contact.ContactSurname) || string.IsNullOrWhiteSpace(Contact.ContactName))
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Invalid name!",
                    Message = "An MyContacts must have both a first and last name.",
                    Cancel = "OK"
                });
                return;
            }

            if (!RequiredAddressFieldCombinationIsFilled)
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Invalid address!",
                    Message = "You must enter either a street, city, and state combination, or a postal code.",
                    Cancel = "OK"
                });
                return;
            }

            var serializedContact = JsonConvert.SerializeObject(Contact);

            var response = await client.PostAsync($"api/Contacts", new StringContent(serializedContact, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Kontakt",
                    Message = "Pomyślnie utworzono kontakt.",
                    Cancel = "OK"
                });
                await PopAsync();
                return;
            }
            await Dialogs.Alert(new AlertInfo
            {
                Title = "Błąd",
                Message = "Błąd podczas tworzenia kontaktu.",
                Cancel = "OK"
            });
        }

        AsyncCommand newCommand;
        public AsyncCommand NewCommand => newCommand ??=
            new AsyncCommand(ExecuteNewCommand);
        Task ExecuteNewCommand() => PushAsync(new EditPage());

        bool RequiredAddressFieldCombinationIsFilled
        {
            get
            {
                if (!Contact.Street.IsNullOrWhiteSpace() && !Contact.City.IsNullOrWhiteSpace() && !Contact.State.IsNullOrWhiteSpace())
                {
                    return true;
                }

                if (!Contact.PostalCode.IsNullOrWhiteSpace() && (Contact.Street.IsNullOrWhiteSpace() || Contact.City.IsNullOrWhiteSpace() || Contact.State.IsNullOrWhiteSpace()))
                {
                    return true;
                }

                if (Contact.AddressString.IsNullOrWhiteSpace())
                {
                    return true;
                }

                return false;
            }
        }
    }
}
