using ChildScheduler.Models;
using ChildScheduler.ViewModels.Costs;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Views.Costs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CostDiagramPage : ContentPage
    {
        protected CostDiagramViewModel ViewModel => BindingContext as CostDiagramViewModel;
        private List<ChartEntry> _entries;

        public List<ChartEntry> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }

        public CostDiagramPage()
        {
            InitializeComponent();
            Entries = new List<ChartEntry>();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel != null)
            {
                var selected = ViewModel.SelectedMonth.GetValueOrDefault().Date;
                Entries.Clear();
                await ViewModel.RefreshCommand.ExecuteAsync();
                var items = ViewModel.Costs.Where(p => p.CostDate.Month == selected.Month && p.CostDate.Year == selected.Year).GroupBy(p => p.CategoryId).Select(x => new
                {
                    x.Key,
                    Label = ViewModel.Categories.FirstOrDefault(p => p.CategoryId == x.Key).CategoryName,
                    Value = x.Sum(c => c.Value)
                }).ToList();
                if (items == null || items.Count <= 0)
                    return;
                int i = 0;
                var random = new Random();
                var color = string.Format("#{0:X6}", random.Next(0x1000000));
                bool isSameCategory = false;
                var categoryName = items.FirstOrDefault().Label;
                foreach (var item in items.OrderBy(p => p.Label))
                {
                    if (item.Label == categoryName)
                        isSameCategory = true;
                    else
                    {
                        isSameCategory = false;
                        categoryName = item.Label;
                    }
                    if (!isSameCategory)
                    {
                        color = string.Format("#{0:X6}", random.Next(0x1000000));
                    }
                    Entries.Add(new ChartEntry((float)item.Value)
                    {
                        Label = item.Label,
                        ValueLabel = ((float)item.Value).ToString(),
                        Color = SKColor.Parse(color)
                    });
                    i = i + 1;
                }
            }
            this.chartView.Chart = new DonutChart() { Entries = Entries, LabelTextSize = 30f, };

        }

        private async void CategoriesListPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if(ViewModel != null)
            {
                var selected = ViewModel.SelectedMonth.GetValueOrDefault().Date;
                Entries.Clear();
                await ViewModel.RefreshCommand.ExecuteAsync();
                var items = ViewModel.Costs.Where(p => p.CostDate.Month == selected.Month && p.CostDate.Year == selected.Year).GroupBy(p => p.CategoryId).Select(x => new
                {
                    x.Key,
                    Label = ViewModel.Categories.FirstOrDefault(p => p.CategoryId == x.Key).CategoryName,
                    Value = x.Sum(c => c.Value)
                }).ToList();
                if (items == null || items.Count <= 0)
                    return;

                int i = 0; 
                var random = new Random();
                var color = string.Format("#{0:X6}", random.Next(0x1000000));
                bool isSameCategory = false;
                var categoryName = items.FirstOrDefault().Label;
                foreach (var item in items.OrderBy(p => p.Label))
                {
                    if(item.Label == categoryName)
                        isSameCategory = true;
                    else
                    {
                        isSameCategory = false;
                        categoryName = item.Label;
                    }
                    if(!isSameCategory)
                    {
                        color = string.Format("#{0:X6}", random.Next(0x1000000));
                    }
                    Entries.Add(new ChartEntry((float)item.Value)
                    {
                        Label = item.Label,
                        ValueLabel = ((float)item.Value).ToString(),
                        Color = SKColor.Parse(color)
                    });
                    i = i + 1;
                }
            }
            this.chartView.Chart = new DonutChart() { Entries = Entries, LabelTextSize = 30f };

        }

        private async void LoadData()
        {
            if(ViewModel != null)
            {
                var selected = ViewModel.SelectedMonth.GetValueOrDefault().Date;
                Entries.Clear();
                await ViewModel.RefreshCommand.ExecuteAsync();
                var items = ViewModel.Costs.Where(p => p.CostDate.Month == selected.Month && p.CostDate.Year == selected.Year).GroupBy(p => p.CategoryId).Select(x => new
                {
                    x.Key,
                    Label = ViewModel.Categories.FirstOrDefault(p => p.CategoryId == x.Key).CategoryName,
                    Value = x.Sum(c => c.Value)
                }).ToList();
                if (items == null || items.Count <= 0)
                    return;
                int i = 0;
                foreach (var item in items)
                {
                    Entries.Add(new ChartEntry((float)item.Value)
                    {
                        Label = item.Label,
                        ValueLabel = ((float)item.Value).ToString(),
                        Color = i == 0 ? SKColor.Parse("#3498db") : SKColor.Parse("#b455b6")
                    });
                    i = i + 1;
                }
            }
            this.chartView.Chart = new DonutChart() { Entries = Entries, LabelTextSize = 30f };

        }
    }
}