using ChildScheduler.Models;
using ChildScheduler.Utils;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChildScheduler.ViewModels
{
    public class CreateProfileViewModel : ViewModelBase<Person>
    {
        HttpClient client;
        public Person Person
        {
            private set; get;
        }
        //public Command LoginCommand { get; }

        public CreateProfileViewModel()
        {
            client = HttpService.GetHttpClient();

            Person = new Person();

            Title = "Profil";
            //LoginCommand = new Command(OnLoginClicked);
            AcceptCommand = new AsyncCommand(async () => await ExecuteAcceptCommand());
        }

        public AsyncCommand AcceptCommand { get; }
        async Task ExecuteAcceptCommand()
        {
            if (string.IsNullOrWhiteSpace(Person.PersonName) || string.IsNullOrWhiteSpace(Person.PersonSurname))
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błędne dane..",
                    Message = "Musisz podać imię oraz nazwisko..",
                    Cancel = "OK"
                });
                return;
            }


            if (await Create(Person))
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Profil",
                    Message = $"Pomyślnie założono profil...{Environment.NewLine}",
                    Cancel = "OK"
                });
                await PopToRootAsync();
                //await Shell.Current.GoToAsync("//ListPage");
                return;
            }            
        }
        async Task<bool> Create(Person person)
        {
            var serializedPerson = JsonConvert.SerializeObject(person);

            var response = await client.PostAsync($"api/Person", new StringContent(serializedPerson, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
