using ChildScheduler.Models;
using ChildScheduler.Views.Categories;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChildScheduler.ViewModels.Categories
{
    public class CategoryListViewModel : ViewModelBase<Category>
    {
        HttpClient client;
        //public Command LoginCommand { get; }

        public CategoryListViewModel()
        {
            client = HttpService.GetHttpClient();

            Title = "Kategorie";
        }

        public ObservableRangeCollection<Category> Categories { get; } = new ObservableRangeCollection<Category>();

        AsyncCommand loadCommand;
        public AsyncCommand LoadCommand => loadCommand ??=
            new AsyncCommand(ExecuteLoadCommand);

        public async Task ExecuteLoadCommand()
        {
            if (Categories.Count < 1)
                await FetchCategories();
        }
        AsyncCommand addNewCommand;
        public AsyncCommand AddNewCommand => addNewCommand ??=
            new AsyncCommand(ExecuteAddNewCommand);
        async Task ExecuteAddNewCommand() => await PushAsync(new CategoryCreatePage());

        AsyncCommand refreshCommand;
        public AsyncCommand RefreshCommand => refreshCommand ??=
            new AsyncCommand(ExecuteRefreshCommand);

        async Task ExecuteRefreshCommand()
        {
            await FetchCategories();
        }

        async Task FetchCategories()
        {
            if (IsBusy || !LoginProvider.IsLogged())
                return;

            IsBusy = true;

            await Task.Delay(1000);
            var json = await client.GetStringAsync($"api/Categories");

            var items = await Task.Run(() => JsonConvert.DeserializeObject<List<Category>>(json));

            Categories.ReplaceRange(items);

            IsBusy = false;
        }
    }
}
