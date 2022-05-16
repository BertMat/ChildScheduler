using ChildScheduler.Models;
using ChildScheduler.Utils;
using ChildScheduler.Views.Events;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChildScheduler.ViewModels.Events
{
    public class EventListViewModel : ViewModelBase<Event>
    {
        HttpClient client;

        public EventListViewModel()
        {
            client = HttpService.GetHttpClient();

            Title = "Wydarzenia";
            SelectedMonth = DateTime.Today;
        }

        private DateTime? _selectedMonth;

        public DateTime? SelectedMonth
        {
            get { return _selectedMonth; }
            set { SetProperty(ref _selectedMonth, value); }
        }

        public ObservableRangeCollection<Event> Events { get; } = new ObservableRangeCollection<Event>();

        AsyncCommand detailsCommand;
        public AsyncCommand DetailsCommand => detailsCommand ??=
            new AsyncCommand(ExecuteDetailsCommand);

        AsyncCommand loadCommand;
        public AsyncCommand LoadCommand => loadCommand ??=
            new AsyncCommand(ExecuteLoadCommand);

        AsyncCommand showEventDetailsCommand;
        public AsyncCommand ShowEventDetailsCommand => showEventDetailsCommand ??=
            new AsyncCommand(ExecuteShowEventDetailsCommand);

        public async Task ExecuteShowEventDetailsCommand()
        {
            //await PushModalAsync(new EventDetailsPage(3), true);
        }
        public async Task ExecuteDetailsCommand()
        {
            await Dialogs.Alert(new AlertInfo
            {
                Title = "Wydarzenie",
                Message = "Pomyślnie usunięto wydarzenie.",
                Cancel = "OK"
            });
        }
        public async Task ExecuteLoadCommand()
        {
            if (Events.Count < 1)
                await FetchEvents();
        }

        AsyncCommand refreshCommand;
        public AsyncCommand RefreshCommand => refreshCommand ??=
            new AsyncCommand(ExecuteRefreshCommand);

        public async Task ExecuteRefreshCommand()
        {
            await FetchEvents();
        }

        async Task FetchEvents()
        {
            if (IsBusy || !LoginProvider.IsLogged())
                return;

            IsBusy = true;

            var json = await client.GetStringAsync($"api/Events");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Event>>(json));

            if(SelectedMonth.HasValue)
                items = items.Where(p => p.StartDate.Month == SelectedMonth.GetValueOrDefault().Month && p.StartDate.Year == SelectedMonth.GetValueOrDefault().Year).OrderBy(p => p.StartDate).ToList();

            Events.ReplaceRange(items);

            IsBusy = false;
        }
    }
}
