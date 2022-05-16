using ChildScheduler.Extensions;
using ChildScheduler.Utils;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ChildScheduler.Models;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Net.Http;

namespace ChildScheduler.ViewModels
{
    public class ContactViewModel : ViewModelBase<Models.Contact>
    {
        protected readonly HttpClient _client;
        public ContactViewModel()
        {
            _client = HttpService.GetHttpClient();
        }

        AsyncCommand<string> dialNumberCommand;

        public AsyncCommand<string> DialNumberCommand =>
            dialNumberCommand ??= new AsyncCommand<string>(ExecuteDialNumberCommand);

        async Task ExecuteDialNumberCommand(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return;

            try
            {
                PhoneDialer.Open(number.SanitizePhoneNumber());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Not Supported",
                    Message = "Phone calls are not supported on this device.",
                    Cancel = "OK"
                });
            }

        }

        AsyncCommand<string> messageNumberCommand;

        public AsyncCommand<string> MessageNumberCommand =>
            messageNumberCommand ??= new AsyncCommand<string>(ExecuteMessageNumberCommand);

        async Task ExecuteMessageNumberCommand(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return;

            try
            {
                await Sms.ComposeAsync(new SmsMessage(string.Empty, number.SanitizePhoneNumber()));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Not Supported",
                    Message = "Sms is not supported on this device.",
                    Cancel = "OK"
                });
            }
        }

        AsyncCommand<string> emailCommand;

        public AsyncCommand<string> EmailCommand =>
            emailCommand ??= new AsyncCommand<string>(ExecuteEmailCommand);
        AsyncCommand<string> socialCommand;

        public AsyncCommand<string> SocialCommand =>
            socialCommand ??= new AsyncCommand<string>(ExecuteSocialCommand);

        async Task ExecuteEmailCommand(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return;

            try
            {
                await Email.ComposeAsync(string.Empty, string.Empty, email);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Not Supported",
                    Message = "Email is not supported on this device.",
                    Cancel = "OK"
                });
            }
        }
        async Task ExecuteSocialCommand(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return;

            try
            {
                await Email.ComposeAsync(string.Empty, string.Empty, email);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Not Supported",
                    Message = "Email is not supported on this device.",
                    Cancel = "OK"
                });
            }
        }

        AsyncCommand<Models.Contact> getDirectionsCommand;

        public AsyncCommand<Models.Contact> GetDirectionsCommand =>
            getDirectionsCommand ??= new AsyncCommand<Models.Contact>(ExecuteGetDirectionsCommand);


        async Task ExecuteGetDirectionsCommand(Models.Contact MyContacts)
        {
            try
            {
                await Map.OpenAsync(new Placemark
                {
                    AdminArea = MyContacts.State,
                    Locality = MyContacts.City,
                    PostalCode = MyContacts.PostalCode,
                    Thoroughfare = MyContacts.AddressString
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
