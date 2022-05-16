using ChildScheduler.Helpers;
using ChildScheduler.Models;
using ChildScheduler.Popups;
using ChildScheduler.Utils;
using ChildScheduler.Views.EducationalInstitutions;
using ChildScheduler.Views.Events;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Models;

namespace ChildScheduler.ViewModels.Families
{
    public class FamilyViewModel : ViewModelBase<Family>
    {
        HttpClient client;
        
        public FamilyViewModel()
        {
            client = HttpService.GetHttpClient();


            Title = "Rodzina";
        }
        public ObservableRangeCollection<Person> FamilyMembers { get; } = new ObservableRangeCollection<Person>();
        public ObservableRangeCollection<Category> Categories { get; } = new ObservableRangeCollection<Category>();
        public ObservableRangeCollection<Event> Events { get; } = new ObservableRangeCollection<Event>();
        public ObservableRangeCollection<Child> Children { get; } = new ObservableRangeCollection<Child>();
        public ObservableRangeCollection<EducationalInstitution> EducationalInstitutions { get; } = new ObservableRangeCollection<EducationalInstitution>();

        private Family family;

        public Family Family
        {
            get { return family; }
            set { SetProperty(ref family, value); }
        }
        private DateTime? currentDate;

        public DateTime? CurrentDate
        {
            get { return currentDate; }
            set { SetProperty(ref currentDate, value); }
        }

        private bool addVisible = true;

        public bool AddVisible
        {
            get { return addVisible; }
            set { SetProperty(ref addVisible, value); }
        }
        private bool categoriesVisibile = false;

        public bool CategoriesVisibile
        {
            get { return categoriesVisibile; }
            set { SetProperty(ref categoriesVisibile, value); }
        }
        private bool childrenVisible = false;

        public bool ChildrenVisible
        {
            get { return childrenVisible; }
            set { SetProperty(ref childrenVisible, value); }
        }
        private bool familyMembersVisible = false;

        public bool FamilyMembersVisible
        {
            get { return familyMembersVisible; }
            set { SetProperty(ref familyMembersVisible, value); }
        }
        private bool educationalInstVisible = false;

        public bool EducationalInstVisible
        {
            get { return educationalInstVisible; }
            set { SetProperty(ref educationalInstVisible, value); }
        }

        private string buttonTitle;

        public string ButtonTitle
        {
            get { return buttonTitle; }
            set { SetProperty(ref buttonTitle, value); }
        }

        private int amount;
        public int SelectedAmount
        {
            get { return amount; }
            set { SetProperty(ref amount, value); }
        }
        private int _leftDaysTillMonthEnd;
        public int LeftDaysTillMonthEnd
        {
            get { return _leftDaysTillMonthEnd; }
            set { SetProperty(ref _leftDaysTillMonthEnd, value); }
        }
        private int _doneTasksInMonth;
        public int DoneTasksInMonth
        {
            get { return _doneTasksInMonth; }
            set { SetProperty(ref _doneTasksInMonth, value); }
        }
        private Child _selectedChild;
        public Child SelectedChild
        {
            get { return _selectedChild; }
            set { SetProperty(ref _selectedChild, value); }
        }
        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { SetProperty(ref _selectedPerson, value); }
        }
        private EducationalInstitution _selectedEducationalInstitution;
        public EducationalInstitution SelectedEducationalInstitution
        {
            get { return _selectedEducationalInstitution; }
            set { SetProperty(ref _selectedEducationalInstitution, value); }
        }

        AsyncCommand showEventDetailsCommand;
        public AsyncCommand ShowEventDetailsCommand => showEventDetailsCommand ??=
            new AsyncCommand(ExecuteShowEventDetailsCommand);

        AsyncCommand showFamilyMembersCommand;
        public AsyncCommand ShowFamilyMembersCommand => showFamilyMembersCommand ??= new AsyncCommand(ExecuteShowFamilyMembersCommand);

        AsyncCommand showCategoriesCommand;
        public AsyncCommand ShowCategoriesCommand => showCategoriesCommand ??= new AsyncCommand(ExecuteShowCategoriesCommand);

        AsyncCommand showChildrenCommand;
        public AsyncCommand ShowChildrenCommand => showChildrenCommand ??= new AsyncCommand(ExecuteShowChildrenCommand);
        AsyncCommand showEducInstitutionsCommand;
        public AsyncCommand ShowEducInstitutionsCommand => showEducInstitutionsCommand ??= new AsyncCommand(ExecuteShowEducInstitutionsCommand);

        AsyncCommand loadCommand;
        public AsyncCommand LoadCommand => loadCommand ??=
            new AsyncCommand(ExecuteLoadCommand);
        AsyncCommand<string> changeFamilyOwnerCommand;
        public AsyncCommand<string> ChangeFamilyOwnerCommand =>
            changeFamilyOwnerCommand ??= new AsyncCommand<string>(ExecuteChangeFamilyOwnerCommand);
        AsyncCommand createFamilyCommand;
        public AsyncCommand CreateFamilyCommand => createFamilyCommand ??=
            new AsyncCommand(ExecuteCreateFamily);
        AsyncCommand leaveFamilyCommand;
        public AsyncCommand LeaveFamilyCommand => leaveFamilyCommand ??=
            new AsyncCommand(ExecuteLeaveFamily);
        AsyncCommand addFamilyMember;
        public AsyncCommand AddFamilyMember => addFamilyMember ??=
            new AsyncCommand(ExecuteAddFamilyMember);
        AsyncCommand addCategory;
        public AsyncCommand AddCategory => addCategory ??=
            new AsyncCommand(ExecuteAddCategory);
        AsyncCommand addChild;
        public AsyncCommand AddChild => addChild ??=
            new AsyncCommand(ExecuteAddChild);
        AsyncCommand addEducIntitution;
        public AsyncCommand AddEducIntitution => addEducIntitution ??=
            new AsyncCommand(ExecuteAddEducationalIns);
        AsyncCommand onSelectInstitutionCommand;
        public AsyncCommand OnSelectInstitutionCommand => onSelectInstitutionCommand ??=
            new AsyncCommand(OnSelectInstitution);

        public async Task ExecuteShowCategoriesCommand()
        {
            CategoriesVisibile = !CategoriesVisibile;
        }
        public async Task ExecuteShowChildrenCommand()
        {
            ChildrenVisible = !ChildrenVisible;
        }
        public async Task ExecuteShowFamilyMembersCommand()
        {
            FamilyMembersVisible = !FamilyMembersVisible;
        }
        public async Task ExecuteShowEventDetailsCommand()
        {
            var selected = SelectedChild;
        }
        public async Task ExecuteShowEducInstitutionsCommand()
        {
            EducationalInstVisible = !EducationalInstVisible;
        }
        public async Task ExecuteLoadCommand()
        {
            if (Family == null)
            {
                var result = await CheckIfUserHasFamily();
                if (result == HttpStatusCode.OK)
                {
                    AddVisible = false;
                    ButtonTitle = "OPUŚĆ RODZINĘ";
                    await ExecuteRefreshCommand();
                }
                else
                {
                    AddVisible = true;
                    ButtonTitle = "UTWÓRZ RODZINĘ";
                }
            }
        }
        public async Task ChangeFamilyOwnerAsync()
        {
            if (SelectedPerson != null)
            {
                var response = await client.GetAsync($"api/Families/Owner/{SelectedPerson.PersonId}");
                if (response.IsSuccessStatusCode)
                {
                    await Dialogs.Alert(new AlertInfo
                    {
                        Title = "Rodzina",
                        Message = "Pomyślnie zmieniono głowę rodziny",
                        Cancel = "OK"
                    });
                    await ExecuteLoadCommand();
                    return;
                }
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Błąd podczas zmiany głowy rodziny",
                    Cancel = "OK"
                });
            }
        }
        public async Task DeletePersonFromFamily()
        {
            if (SelectedPerson != null)
            {
                var response = await client.GetAsync($"api/Families/Delete/{SelectedPerson.PersonId}");
                if (response.IsSuccessStatusCode)
                {
                    await Dialogs.Alert(new AlertInfo
                    {
                        Title = "Rodzina",
                        Message = "Pomyślnie usunięto członka rodziny",
                        Cancel = "OK"
                    });
                    await ExecuteLoadCommand();
                    return;
                }
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Błąd podczas usuwania członka rodziny",
                    Cancel = "OK"
                });
            }
        }
        public async Task ExecuteChangeFamilyOwnerCommand(string choice)
        {
            if (choice == "USUŃ Z RODZINY")
            {
                await Dialogs.Question(new QuestionInfo
                {
                    Title = "Rodzina",
                    Question = "Czy chcesz usunąć członka rodziny?",
                    Positive = "OK",
                    OnCompleted = new Action<bool>(async result =>
                    {
                        if (!result)
                            return;

                        await DeletePersonFromFamily();
                        await ExecuteLoadCommand();
                    }),
                    Negative = "Anuluj"
                });
            }
            else if(choice == "ZMIEŃ GŁOWĘ RODZINY")
            {
                await Dialogs.Question(new QuestionInfo
                {
                    Title = "Rodzina",
                    Question = "Czy chcesz zmienić głowę rodziny?",
                    Positive = "OK",
                    OnCompleted = new Action<bool>(async result =>
                    {
                        if (!result)
                            return;

                        await ChangeFamilyOwnerAsync();
                    }),
                    Negative = "Anuluj"
                });
            }
        }
        public async Task OnSelectInstitution()
        {
            if(SelectedEducationalInstitution != null)
                await PushAsync(new InstitutionDetailPage(SelectedEducationalInstitution));

            SelectedEducationalInstitution = null;
        }

        public async Task<HttpStatusCode> CheckIfUserHasFamily()
        {
            var family = await client.GetAsync($"api/Families");

            return family.StatusCode;
        }
        public async Task ExecuteCreateFamily()
        {
            if (AddVisible)
            {
                var familyName = await Dialogs.Input(new QuestionInfo
                {
                    Title = "Rodzina",
                    Question = "Podaj nazwę rodziny.",
                    Positive = "OK",
                    Negative = "Anuluj"
                });
                if (familyName == null) return;

                var serializedFamily = JsonConvert.SerializeObject(new { FamilyName = familyName });

                var response = await client.PostAsync($"api/Families", new StringContent(serializedFamily, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    await Dialogs.Alert(new AlertInfo
                    {
                        Title = "Rodzina",
                        Message = "Pomyślnie utworzono rodzinę",
                        Cancel = "OK"
                    });
                    await ExecuteLoadCommand();
                    return;
                }
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Błąd podczas tworzenia rodziny",
                    Cancel = "OK"
                });
            }
            else
            {
                await ExecuteLeaveFamily();
            }
            
        }
        public async Task ExecuteLeaveFamily()
        {
            await Dialogs.Question(new QuestionInfo
            {
                Title = "Rodzina",
                Question = "Czy chcesz odejść od rodziny?",
                Positive = "OK",
                OnCompleted = new Action<bool>(async result =>
                {
                    if (!result)
                        return;

                    await LeaveFamily();
                }),
                Negative = "Anuluj"
            });


        }
        public async Task LeaveFamily()
        {

            var response = await client.DeleteAsync($"api/Families/Leave");

            if (!response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var msg = JsonConvert.DeserializeObject<string>(json);
                await Dialogs.Alert(new AlertInfo
                { 
                    Title = "Błąd",
                    Message = msg,
                    Cancel = "OK"
                });
                return;
            };
            await Dialogs.Alert(new AlertInfo
            {
                Title = "Rodzina",
                Message = "Pomyślnie opuszczono rodzinę.",
                Cancel = "OK"
            });

        }
        public async Task ExecuteAddFamilyMember()
        {
            if (Family != null)
            {
                var email = await Dialogs.Input(new QuestionInfo
                {
                    Title = "Zaproszenie",
                    Question = "Wpisz adres e-mail członka rodziny",
                    Positive = "OK",
                    Negative = "Anuluj"
                });
                if (email == null) return;

                var serializedUser = JsonConvert.SerializeObject(email);

                var response = await client.PostAsync($"api/User/Invitation", new StringContent(serializedUser, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode) {
                    await Dialogs.Alert(new AlertInfo
                    {
                        Title = "Zaproszenie",
                        Message = "Pomyślnie wysłano zaproszenie.",
                        Cancel = "OK"
                    });
                    return; 
                }
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Błąd podczas wysyłania zaproszenia.",
                    Cancel = "OK"
                });
            }
            else
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Błąd ",
                    Cancel = "OK"
                });
            }
        }
        public async Task ExecuteAddCategory()
        {
            if (Family != null)
            {
                var categoryName = await Dialogs.Input(new QuestionInfo
                {
                    Title = "Utwórz kategorię",
                    Question = "Wprowadź nazwę kategorii",
                    Positive = "OK",
                    Negative = "Anuluj"
                });
                if (categoryName == null) return;

                var family = await client.GetStringAsync($"api/Families");
                var deserializedFamily = JsonConvert.DeserializeObject<Family>(family);

                var newCategory = new
                {
                    FamilyId = deserializedFamily.FamilyId,
                    CategoryName = categoryName
                };
                var serializedCategory = JsonConvert.SerializeObject(newCategory);

                var response = await client.PostAsync($"api/Categories", new StringContent(serializedCategory, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode) {
                    await Dialogs.Alert(new AlertInfo
                    {
                        Title = "Kategoria",
                        Message = "Pomyślnie utworzono kategorię.",
                        Cancel = "OK"
                    });
                    await FetchCategories();
                    return; 
                }
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Błąd podczas tworzenia kategorii.",
                    Cancel = "OK"
                });
            }
        }
        public async Task ExecuteAddChild()
        {
            if (Family != null)
            {
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new AddNewChildPage());
            }
            else
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Błąd podczas wysyłania zaproszenia.",
                    Cancel = "OK"
                });
            }
        }
        public async Task ExecuteAddEducationalIns()
        {
            if (Family != null)
            {
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new AddNewEducationalInsPage());
            }
            else
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Błąd podczas tworzenia placówki nauczania.",
                    Cancel = "OK"
                });
            }
        }

        AsyncCommand refreshCommand;
        public AsyncCommand RefreshCommand => refreshCommand ??=
            new AsyncCommand(ExecuteRefreshCommand);

        async Task ExecuteRefreshCommand()
        {
            var result = await CheckIfUserHasFamily();
            IsBusy = true;
            if (result == HttpStatusCode.OK)
            {
                await Task.WhenAll(
                    FetchFamily(),
                    FetchFamilyMembers(),
                    FetchCategories(),
                    FetchChildren(),
                    FetchEvents(),
                    FetchInstitutions());
                IsBusy = false;
                return;
            }
            IsBusy = false;
        }

        async Task FetchFamily()
        {
            if (!LoginProvider.IsLogged())
                return;


            var json = await client.GetStringAsync($"api/Families");

            var item = await Task.Run(() => JsonConvert.DeserializeObject<Family>(json));

            Family = item;

        }
        async Task FetchFamilyMembers()
        {
            if (!LoginProvider.IsLogged() || !(await LoginProvider.IsAuthorized()))
                return;


            var json = await client.GetStringAsync($"api/Families/members");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Person>>(json));

            FamilyMembers.ReplaceRange(items);

        }
        async Task FetchCategories()
        {
            if (!LoginProvider.IsLogged() || !(await LoginProvider.IsAuthorized()))
                return;


            var json = await client.GetStringAsync($"api/Categories");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Category>>(json));

            Categories.ReplaceRange(items);

        }
        async Task FetchChildren()
        {
            if (!LoginProvider.IsLogged() || !(await LoginProvider.IsAuthorized()))
                return;


            var json = await client.GetStringAsync($"api/Children/");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Child>>(json));

            Children.ReplaceRange(items);

        }
        async Task FetchInstitutions()
        {
            if (!LoginProvider.IsLogged() || !(await LoginProvider.IsAuthorized()))
                return;


            var json = await client.GetStringAsync($"api/EducationalInstitutions/");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<EducationalInstitution>>(json));

            EducationalInstitutions.ReplaceRange(items);

        }
        async Task FetchEvents()
        {
            if (!LoginProvider.IsLogged() || !(await LoginProvider.IsAuthorized()))
                return;

            var json = await client.GetStringAsync($"api/Events");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Event>>(json));

            items = items.OrderBy(p => p.StartDate).ToList();
            var events = items.Where(p => p.StartDate.Date >= DateTime.Today.Date).ToList();
            var doneEvents = items.Where(p => p.StartDate.Date.Month == DateTime.Today.Date.Month && p.StartDate.Date < DateTime.Today.Date).ToList();
            Events.ReplaceRange(events);
            AnimatedSelectedText(events.Count);
            AnimatedDoneText(doneEvents.Count);
            var currentDate = DateTime.UtcNow;
            var leftDays  = DateTime.DaysInMonth(currentDate.Year, currentDate.Month) - currentDate.Day;

            AnimatedText(leftDays);

        }

        private void AnimatedText(float amount)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Device.StartTimer(TimeSpan.FromSeconds(1 / 500f), () =>
            {
                double t = stopwatch.Elapsed.TotalMilliseconds % 500 / 500;

                LeftDaysTillMonthEnd = (int)Math.Min((float)amount, (float)(10 * t) + LeftDaysTillMonthEnd);

                if (LeftDaysTillMonthEnd >= (float)amount)
                {
                    stopwatch.Stop();
                    return false;
                }

                return true;
            });
        }
        private void AnimatedDoneText(float amount)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Device.StartTimer(TimeSpan.FromSeconds(1 / 500f), () =>
            {
                double t = stopwatch.Elapsed.TotalMilliseconds % 500 / 500;

                DoneTasksInMonth = (int)Math.Min((float)amount, (float)(10 * t) + DoneTasksInMonth);

                if (DoneTasksInMonth >= (float)amount)
                {
                    stopwatch.Stop();
                    return false;
                }

                return true;
            });
        }

        private void AnimatedSelectedText(float amount)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Device.StartTimer(TimeSpan.FromSeconds(1 / 500f), () =>
            {
                double t = stopwatch.Elapsed.TotalMilliseconds % 500 / 500;

                SelectedAmount = (int)Math.Min((float)amount, (float)(10 * t) + SelectedAmount);

                if (SelectedAmount >= (float)amount)
                {
                    stopwatch.Stop();
                    return false;
                }

                return true;
            });
        }
    }
}
