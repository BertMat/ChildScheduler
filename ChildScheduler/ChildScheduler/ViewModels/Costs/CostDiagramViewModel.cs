using ChildScheduler.Models;
using ChildScheduler.Popups;
using ChildScheduler.Utils;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChildScheduler.ViewModels.Costs
{
    public class CostDiagramViewModel : ViewModelBase<Models.Cost>
    {
        protected readonly HttpClient _client;
        private ObservableRangeCollection<Cost> _costs;

        public ObservableRangeCollection<Cost> Costs
        {
            get { return _costs; }
            set { SetProperty(ref _costs, value); }
        }
        private ObservableRangeCollection<Category> _categories;

        public ObservableRangeCollection<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }
        private Category _selectedCategory;

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }
        private DateTime? _selectedMonth;

        public DateTime? SelectedMonth
        {
            get { return _selectedMonth; }
            set { SetProperty(ref _selectedMonth, value); }
        }

        AsyncCommand addCostCommand;
        public AsyncCommand AddCostCommand => addCostCommand ??=
            new AsyncCommand(ExecuteAddNewCostCommand);

        public CostDiagramViewModel()
        {
            _client = HttpService.GetHttpClient();
            Categories = new ObservableRangeCollection<Category>();
            Costs = new ObservableRangeCollection<Cost>();
            SelectedMonth = DateTime.Today;
        }

        AsyncCommand refreshCommand;
        public AsyncCommand RefreshCommand => refreshCommand ??=
            new AsyncCommand(ExecuteRefreshCommand);

        public async Task ExecuteRefreshCommand()
        {
            IsBusy = true;
            await Task.WhenAll(
                FetchCosts(),
                FetchCategories()
                );
            IsBusy = false;
        }
        public async Task FetchCosts()
        {
            if (!LoginProvider.IsLogged())
                return;


            var json = await _client.GetStringAsync($"api/Costs");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Cost>>(json));
            if (!items.Any())
                return;
            if (SelectedMonth.HasValue)
                items = items.Where(p => p.CostDate.Month == SelectedMonth.GetValueOrDefault().Month && p.CostDate.Year == SelectedMonth.GetValueOrDefault().Year).OrderBy(p => p.CostDate).ToList();
            Costs.ReplaceRange(items);
        }
        public async Task ExecuteAddNewCostCommand()
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new AddNewCostPage(this));            
        }
        public async Task FetchCategories()
        {
            if (!LoginProvider.IsLogged())
                return;


            var json = await _client.GetStringAsync($"api/Categories");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Category>>(json));

            Categories.ReplaceRange(items);
        }
    }
}
