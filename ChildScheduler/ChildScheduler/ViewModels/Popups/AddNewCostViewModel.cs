using ChildScheduler.Models;
using ChildScheduler.Popups;
using ChildScheduler.Utils;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ChildScheduler.ViewModels.Popups
{
    public class AddNewCostViewModel : ViewModelBase<Cost>
    {

        private readonly HttpClient _client;
        private Cost _cost;

        public Cost Cost
        {
            get { return _cost; }
            set { SetProperty(ref _cost, value); }
        }
        private string _buttonName;

        public string ButtonName
        {
            get { return IsEditing ? "EDYTUJ" : "DODAJ"; }
        }

        private ObservableRangeCollection<Category> _categories = new ObservableRangeCollection<Category>();

        public ObservableRangeCollection<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        private Family family;

        public Family Family
        {
            get { return family; }
            set { SetProperty(ref family, value); }
        }
        private bool _isEditing;

        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                SetProperty(ref _isEditing, value);
                OnPropertyChanged(ButtonName);
            }
        }

        public AddNewCostViewModel()
        {
            _client = HttpService.GetHttpClient();
            Cost = new Cost { CostDate = DateTime.Today };

        }
        public AddNewCostViewModel(Cost cost)
        {
            _client = HttpService.GetHttpClient();
            Cost = cost;
            IsEditing = true;

        }
        AsyncCommand addNewCostCommand;
        public AsyncCommand AddNewCostCommand => addNewCostCommand ??=
            new AsyncCommand(ExecuteAddNewCostCommand);
        AsyncCommand popupCommand;
        public AsyncCommand PopupCommand => popupCommand ??=
            new AsyncCommand(ExecutePopupCommand);

        private async Task ExecutePopupCommand()
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
        }

        private async Task ExecuteAddNewCostCommand()
        {
            if (string.IsNullOrWhiteSpace(Cost.CostName) 
                || !Cost.Value.HasValue
                || Cost.CostDate == null)
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Błąd",
                    Message = "Musisz uzupełnić dane...",
                    Cancel = "OK"
                });
                return;
            }

            if(!IsEditing && await AddCostAsync())
            {

                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Sukces",
                    Message = "Pomyślnie utworzono wydatek...",
                    Cancel = "OK"
                });
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                return;
            }
            else if(await UpdateChildAsync())
            {
                await Dialogs.Alert(new AlertInfo
                {
                    Title = "Sukces",
                    Message = "Pomyślnie edytowano wydatek...",
                    Cancel = "OK"
                });
                await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                return;
            }

            await Dialogs.Alert(new AlertInfo
            {
                Title = "Błąd",
                Message = "Błąd podczas tworzenia wydatku...",
                Cancel = "OK"
            });
            return;
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        Task OfflineAlert() => Dialogs.Alert(new AlertInfo
        {
            Cancel = "OK",
            Title = "Offline",
            Message = "Currently offline, please check internet connection."
        });

        public async Task FetchCategoriesAsync()
        {
            if (IsBusy || !LoginProvider.IsLogged())
                return;
            if (!IsConnected)
            {
                await OfflineAlert();
                return;
            }

            IsBusy = true;

            var json = await _client.GetStringAsync($"api/Categories");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Category>>(json));

            Categories.ReplaceRange(items);

            IsBusy = false;

        }
        public async Task<bool> AddCostAsync()
        {
            if (Cost == null || Cost.Category == null)
            {
                return false;
            }

            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }

            IsBusy = true;
            Cost.CategoryId = Cost.Category.CategoryId.GetValueOrDefault();
            var serializedCost = JsonConvert.SerializeObject(Cost);

            var response = await _client.PostAsync($"api/Costs/", new StringContent(serializedCost, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;

        }
        public async Task<bool> UpdateChildAsync()
        {
            if (Cost == null)
            {
                return false;
            }

            if (!IsConnected)
            {
                await OfflineAlert();
                return false;
            }

            IsBusy = true;

            var serializedCost = JsonConvert.SerializeObject(Cost);

            var response = await _client.PutAsync($"api/Costs", new StringContent(serializedCost, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;

        }
    }
}
