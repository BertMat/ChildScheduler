using ChildScheduler.Interfaces;
using ChildScheduler.Models;
using ChildScheduler.Services;
using ChildScheduler.Utils;
using ChildScheduler.Views;
using ChildScheduler.Views.Contacts;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChildScheduler.ViewModels
{
    public class ListViewModel : ContactViewModel
    {
        public DateTime LastUpdate { get; set; }
        public ListViewModel()
        {
        }


        public ObservableRangeCollection<Contact> Contacts { get; } = new ObservableRangeCollection<Contact>();

        AsyncCommand loadCommand;
        public AsyncCommand LoadCommand => loadCommand ??=
            new AsyncCommand(ExecuteLoadCommand);

        public async Task ExecuteLoadCommand()
        {
            if (Contacts.Count < 1 || LastUpdate < Settings.LastUpdate)
                await FetchContacts();
        }

        AsyncCommand refreshCommand;
        public AsyncCommand RefreshCommand => refreshCommand ??=
            new AsyncCommand(ExecuteRefreshCommand);

        async Task ExecuteRefreshCommand()
        {
            await FetchContacts();
        }

        async Task FetchContacts()
        {
            if (IsBusy || !LoginProvider.IsLogged())
                return;

            IsBusy = true;

            await Task.Delay(1000);
            var json = await _client.GetStringAsync($"api/Contacts");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Contact>>(json));

            Contacts.ReplaceRange(items);

            LastUpdate = DateTime.UtcNow;

            IsBusy = false;
        }

        AsyncCommand newCommand;
        public AsyncCommand NewCommand => newCommand ??=
            new AsyncCommand(ExecuteNewCommand);
        Task ExecuteNewCommand() => PushAsync(new EditPage());

        AsyncCommand showSettingsCommand;
        public AsyncCommand ShowSettingsCommand => showSettingsCommand ??=
            new AsyncCommand(ExecuteShowSettingsCommand);

        Task ExecuteShowSettingsCommand() => PushModalAsync(new SettingsPage());
    }
}
