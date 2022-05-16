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
    public class EventCreateViewModel : ViewModelBase<Event>
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

        private ObservableRangeCollection<object> _selectedPeople = new ObservableRangeCollection<object>();

        public ObservableRangeCollection<object> SelectedPeople
        {
            get { return _selectedPeople; }
            set { SetProperty(ref _selectedPeople, value); }
        }

        private ObservableRangeCollection<object> _selectedContacts = new ObservableRangeCollection<object>();

        public ObservableRangeCollection<object> SelectedContacts
        {
            get { return _selectedContacts; }
            set { SetProperty(ref _selectedContacts, value); }
        }
        public bool IsEditing { get; set; } = false;
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
        private EducationalInstitution _selectedInstitution;

        public EducationalInstitution SelectedInstitution
        {
            get { return _selectedInstitution; }
            set { SetProperty(ref _selectedInstitution, value); }
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
        public ObservableRangeCollection<EducationalInstitution> EducationalInstitutions { get; } = new ObservableRangeCollection<EducationalInstitution>();
        public ObservableRangeCollection<Person> People { get; } = new ObservableRangeCollection<Person>();
        public ObservableRangeCollection<Contact> Contacts { get; } = new ObservableRangeCollection<Contact>();

        public EventCreateViewModel()
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

        public async Task ExecuteDeleteEventCommand()
        {
            await Dialogs.Question(new QuestionInfo
            {
                Title = "Wydarzenie",
                Question = "Czy na pewno chcesz usunąć wydarzenie?",
                Negative = "Anuluj",
                Positive = "Zatwierdź",
                OnCompleted = new Action<bool>(async result =>
                {
                    if (!result)
                        return;

                    await DeleteEvent();
                })
            });
        }
        public async Task DeleteEvent()
        {
            if (Event != null && Event.Id != null)
            {
                var response = await client.DeleteAsync($"api/Events/{Event.Id}");

                if (response.IsSuccessStatusCode)
                {
                    await Dialogs.Alert(new AlertInfo
                    {
                        Title = "Wydarzenie",
                        Message = "Pomyślnie usunięto wydarzenie.",
                        Cancel = "OK"
                    });
                    return;
                }
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Błąd podczas usuwania wydarzenia.",
                    Cancel = "OK"
                });
            }
        }
        public async Task ExecuteCreateEventCommand()
        {
            if (Event != null && SelectedCategory != null)
            {
                Event.CategoryName = SelectedCategory.CategoryName;
                Event.EducationalInstitution = SelectedInstitution;
                if (SelectedChildren != null)
                {
                    Event.Children.Clear();
                    foreach(var child in SelectedChildren)
                        Event.Children.Add(child as Child);
                }
                var selectedDate = Event.StartDate;
                if (SelectedContacts != null)
                {
                    Event.Contacts.Clear();

                    foreach (var contact in SelectedContacts)
                    {
                        Event.Contacts.Add(contact as Contact);
                    }
                }
                if (SelectedPeople != null)
                {
                    Event.People.Clear();
                    foreach (var person in SelectedPeople)
                        Event.People.Add(person as Person);
                }
                if (Event.EducationalInstitution != null)
                    Event.EducationalInstitutionId = Event.EducationalInstitution.Id;
                Event.StartDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, SelectedTime.Hours, SelectedTime.Minutes, SelectedTime.Seconds);
                Event.EndDate = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, SelectedTime.Hours, SelectedTime.Minutes, SelectedTime.Seconds);
                var serializedEvent = JsonConvert.SerializeObject(Event);
                var response = new HttpResponseMessage();
                if (IsEditing)
                {
                    response = await client.PutAsync($"api/Events", new StringContent(serializedEvent, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        await Dialogs.Alert(new AlertInfo
                        {
                            Title = "Wydarzenie",
                            Message = "Pomyślnie zaktualizowano wydarzenie.",
                            Cancel = "OK"
                        });
                        return;
                    }
                    await Dialogs.Alert(new AlertInfo
                    {
                        Title = "Błąd",
                        Message = "Błąd podczas aktualizacji wydarzenia.",
                        Cancel = "OK"
                    });
                }
                else
                {
                    response = await client.PostAsync($"api/Events", new StringContent(serializedEvent, Encoding.UTF8, "application/json"));
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
        }

        AsyncCommand loadCategoriesCommand;
        public AsyncCommand LoadCategoriesCommand => loadCategoriesCommand ??=
            new AsyncCommand(ExecuteLoadCategoriesCommand);

        public async Task ExecuteLoadCategoriesCommand()
        {
            if (!LoginProvider.IsLogged())
                return;

            var json = await client.GetStringAsync($"api/Categories");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Category>>(json));

            Categories.ReplaceRange(items);

        }
        public async Task FetchEventAsync(int eventId)
        {
            if (!LoginProvider.IsLogged())
                return;

            var json = await client.GetStringAsync($"api/Events/{eventId}");

            var item = await Task.Run(() => JsonConvert.DeserializeObject<Event>(json));

            Event = item;

        }

        public async Task FetchChildren()
        {
            if (!LoginProvider.IsLogged())
                return;


            var json = await client.GetStringAsync($"api/Children");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Child>>(json));

            Children.ReplaceRange(items);

        }

        public async Task FetchPeople()
        {
            if (!LoginProvider.IsLogged())
                return;


            var json = await client.GetStringAsync($"api/Families/members");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Person>>(json));

            People.ReplaceRange(items);

        }
        public async Task FetchContacts()
        {
            if (!LoginProvider.IsLogged())
                return;


            var json = await client.GetStringAsync($"api/Contacts");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Contact>>(json));

            Contacts.ReplaceRange(items);

        }
        public async Task FetchPhotos()
        {
            if (!LoginProvider.IsLogged())
                return;


            var json = await client.GetStringAsync($"api/Events/Photos/{Event.Id}");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<EventPhoto>>(json));

            Event.Photos = items;

        }
        public async Task FetchInstitutes()
        {
            if (!LoginProvider.IsLogged())
                return;


            var json = await client.GetStringAsync($"api/EducationalInstitutions");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<EducationalInstitution>>(json));
            items.Add(new EducationalInstitution
            {
                Id = null,
                Name = "BRAK",
                FullName = "BRAK"
            });
            EducationalInstitutions.ReplaceRange(items);

        }
        public async Task ExecuteSendFile(MultipartFormDataContent file)
        {
            await Dialogs.Question(new QuestionInfo
            {
                Title = "Zdjęcie",
                Question = "Na pewno chcesz dodać zdjęcie do wydarzenia?",
                Negative = "Anuluj",
                Positive = "Zatwierdź",
                OnCompleted = new Action<bool>(async result =>
                {
                    if (!result)
                        return;

                    await SendFile(file);
                })
            });
        }
        public async Task SendFile(MultipartFormDataContent file)
        {
            if (!LoginProvider.IsLogged())
                return;


            var json = await client.PostAsync($"api/Events/UploadFile/{Event.Id}", file);

            if (json.IsSuccessStatusCode)
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Zdjęcie",
                    Message = "Pomyślnie dodano zdjęcie.",
                    Cancel = "OK"
                });
                return;
            }

            await Dialogs.Alert(new AlertInfo
            {
                Title = "Błąd",
                Message = "Błąd podczas dodawania zdjęcia.",
                Cancel = "OK"
            });
        }
    }
}
