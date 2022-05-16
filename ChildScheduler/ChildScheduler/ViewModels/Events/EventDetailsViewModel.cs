using ChildScheduler.Models;
using ChildScheduler.Utils;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.MultiSelectListView;

namespace ChildScheduler.ViewModels.Events
{
    public class EventDetailsViewModel : ViewModelBase<Event>
    {
        HttpClient client;
        private Event _event = new Event();

        public Event Event
        {
            get { return _event; }
            set { SetProperty(ref _event, value); }
        }
        private ObservableRangeCollection<object> _selectedChildren = new ObservableRangeCollection<object>();

        public ObservableRangeCollection<object> SelectedChildren
        {
            get { return _selectedChildren; }
            set { SetProperty(ref _selectedChildren, value); }
        }

        private int? _eventId;

        public int? EventId
        {
            get { return _eventId; }
            set { SetProperty(ref _eventId, value); }
        }

        private Child _selectedChild;

        public Child SelectedChild
        {
            get { return _selectedChild; }
            set { SetProperty(ref _selectedChild, value); }
        }
        private Category _selectedCategory;

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }
        private TimeSpan _selectedTime = new TimeSpan(15, 0, 0);

        public TimeSpan SelectedTime
        {
            get { return _selectedTime; }
            set { SetProperty(ref _selectedTime, value); }
        }
        public AsyncCommand DisplayNameCommand => new AsyncCommand(async () =>
        {
            await Dialogs.Alert(new AlertInfo
            {
                Title = "Wydarzenie",
                Message = "Pomyślnie utworzono wydarzenie.",
                Cancel = "OK"
            });
        });
        public ObservableRangeCollection<Category> Categories { get; } = new ObservableRangeCollection<Category>();
        public ObservableRangeCollection<Child> Children { get; } = new ObservableRangeCollection<Child>();

        public EventDetailsViewModel()
        {

            client = HttpService.GetHttpClient();
        }

        AsyncCommand closePageCommand;
        public AsyncCommand ClosePageCommand => closePageCommand ??=
            new AsyncCommand(ExecuteClosePageCommand);

        public async Task ExecuteClosePageCommand()
        {
            await ExecuteCreateEventCommand();
            await PopModalAsync(true);
        }
        AsyncCommand createEventCommand;
        public AsyncCommand CreateEventCommand => createEventCommand ??=
            new AsyncCommand(ExecuteCreateEventCommand);

        public async Task ExecuteCreateEventCommand()
        {
            if (Event != null && SelectedCategory != null)
            {
                Event.CategoryName = SelectedCategory.CategoryName;
                if (SelectedChildren != null)
                {
                    foreach (var child in SelectedChildren)
                        Event.Children.Add(child as Child);
                }
                var selectedDate = Event.StartDate;
                Event.StartDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, SelectedTime.Hours, SelectedTime.Minutes, SelectedTime.Seconds);
                Event.EndDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, SelectedTime.Hours, SelectedTime.Minutes, SelectedTime.Seconds);
                var serializedEvent = JsonConvert.SerializeObject(Event);

                var response = await client.PostAsync($"api/Events", new StringContent(serializedEvent, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    await Dialogs.Alert(new AlertInfo
                    {
                        Title = "Wydarzenie",
                        Message = "Pomyślnie utworzono wydarzenie.",
                        Cancel = "OK"
                    });
                    return;
                }
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Błąd podczas tworzenia wydarzenia.",
                    Cancel = "OK"
                });
            }
        }

        AsyncCommand loadCategoriesCommand;
        public AsyncCommand LoadCategoriesCommand => loadCategoriesCommand ??=
            new AsyncCommand(ExecuteLoadCategoriesCommand);

        public async Task ExecuteLoadCategoriesCommand()
        {
            if (IsBusy || !LoginProvider.IsLogged())
                return;

            IsBusy = true;

            var json = await client.GetStringAsync($"api/Categories");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Category>>(json));

            Categories.ReplaceRange(items);

            IsBusy = false;
        }

        public async Task FetchChildren()
        {
            if (IsBusy || !LoginProvider.IsLogged())
                return;

            IsBusy = true;

            var json = await client.GetStringAsync($"api/Families/Children");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Child>>(json));

            Children.ReplaceRange(items);

            IsBusy = false;
        }

        public async Task FetchEvent(int? eventId)
        {
            if (IsBusy || !LoginProvider.IsLogged() || eventId == null)
                return;

            IsBusy = true;
            EventId = eventId;

            var json = await client.GetStringAsync($"api/Events/{EventId}");

            Event = await Task.Run(() => JsonConvert.DeserializeObject<Event>(json));
            SelectedCategory = Event.Category;
            SelectedTime = Event.StartDate.TimeOfDay;
            SelectedChildren.AddRange(Event.Children);

            IsBusy = false;
        }

    }
}
