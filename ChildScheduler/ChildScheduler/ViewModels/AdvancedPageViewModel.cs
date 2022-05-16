using ChildScheduler.Models;
using Xamarin.Plugin.Calendar.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net.Http;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using ChildScheduler.LoginPages;
using ChildScheduler.Views.Events;
using System.Diagnostics;
using MvvmHelpers;

namespace ChildScheduler.ViewModels
{
    public class AdvancedPageViewModel : ViewModelBase<Event>
    {
        HttpClient client;
        public ICommand DayTappedCommand => new MvvmHelpers.Commands.Command<DateTime>(async (date) => await DayTapped(date));
        public ICommand SwipeLeftCommand => new MvvmHelpers.Commands.Command(() => { MonthYear = MonthYear.AddMonths(2); });
        public ICommand SwipeRightCommand => new MvvmHelpers.Commands.Command(() => { MonthYear = MonthYear.AddMonths(-2); });
        public ICommand SwipeUpCommand => new MvvmHelpers.Commands.Command(() => { MonthYear = DateTime.Today; });

        public ICommand EventSelectedCommand => new MvvmHelpers.Commands.Command(async (item) => await ExecuteEventSelectedCommand(item));
        AsyncCommand loadCommand;
        public AsyncCommand LoadCommand => loadCommand ??=
            new AsyncCommand(ExecuteLoadCommand);
        public async Task ExecuteLoadCommand()
        {
            if(!LoginProvider.IsLogged())
                await Shell.Current.GoToAsync($"{nameof(MainPage)}");
            if (!EventList.Any())
            {
                await FetchEvents();
            }
        }

        private ObservableRangeCollection<Event> events = new ObservableRangeCollection<Event>();

        public ObservableRangeCollection<Event> Events
        {
            get { return events; }
            set { events = value; }
        }


        AsyncCommand refreshCommand;
        public AsyncCommand RefreshCommand => refreshCommand ??=
            new AsyncCommand(ExecuteRefreshCommand);

        async Task ExecuteRefreshCommand()
        {
            await FetchEvents();
        }
        AsyncCommand addEventCommand;
        public AsyncCommand AddEventCommand => addEventCommand ??=
            new AsyncCommand(ExecuteAddEventCommand);

        public async Task ExecuteAddEventCommand()
        {
            await PushModalAsync(new EventCreatePage(this), true);
        }
        public async Task ExecuteShowEventDetailsCommand(string startDate, string title)
        {
            var time = TimeSpan.Parse(startDate);
            var date = SelectedDate.Add(time);
            var _event = Events.Where(p => p.StartDate == date && p.EventName == title).FirstOrDefault();
            await PushModalAsync(new EventDetailsPage(this, _event));
        }

        public AdvancedPageViewModel() : base()
        {
            client = HttpService.GetHttpClient();
            Culture = CultureInfo.CreateSpecificCulture("pl-PL");
        }

        private IEnumerable<Event> GenerateEvents(int count, string name)
        {
            return Enumerable.Range(1, count).Select(x => new Event
            {
                EventName = $"{name} event{x}",
                EventDescription = $"This is {name} event{x}'s description!",
                StartDate = new DateTime(2000, 1, 1, (x * 2) % 24, (x * 3) % 60, 0)
            });
        }
        private EventCollection eventList = new EventCollection();

        public EventCollection EventList
        {
            get { return eventList; }
            set { SetProperty(ref eventList, value); }
        }


        private DateTime _monthYear = DateTime.Today;
        public DateTime MonthYear
        {
            get => _monthYear;
            set => SetProperty(ref _monthYear, value);
        }

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }
        private List<DateTime> _selectedDates;
        public List<DateTime> SelectedDates
        {
            get => _selectedDates;
            set => SetProperty(ref _selectedDates, value);
        }


        private CultureInfo _culture = CultureInfo.InvariantCulture;
        public CultureInfo Culture
        {
            get => _culture;
            set => SetProperty(ref _culture, value);
        }

        private static async Task DayTapped(DateTime date)
        {
            var message = $"Received tap event from date: {date}";
            await App.Current.MainPage.DisplayAlert("DayTapped", message, "Ok");
        }

        public async Task ExecuteEventSelectedCommand(object item)
        {
            if (item is Event eventModel)
            {
                var title = $"Selected: {eventModel.EventName}";
                var message = $"Starts: {eventModel.StartDate:HH:mm}{Environment.NewLine}Details: {eventModel.EventDescription}";
                await App.Current.MainPage.DisplayAlert(title, message, "Ok");
            }
        }
        async Task FetchEvents()
        {
            if (IsBusy || !LoginProvider.IsLogged() || !(await LoginProvider.IsAuthorized()))
                return;

            IsBusy = true;

            await Task.Delay(1000);
            var json = await client.GetStringAsync($"api/Events");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Event>>(json));

            Events.ReplaceRange(items);

            var date = new DateTime(DateTime.Now.Year-1, 1, 1);
            var lastDate = new DateTime(DateTime.Now.Year, 12, 31);
            EventList.Clear();
            while(date <= lastDate)
            {
                if(items.Where(p => p.StartDate.ToString("d") == date.ToString("d")).Any())
                    EventList.Add(date, items.Where(p => p.StartDate.ToString("d") == date.ToString("d")).OrderBy(p => p.StartDate).ToList());
                date = date.AddDays(1);
            }

            IsBusy = false;
        }

    }
}
