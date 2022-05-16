using ChildScheduler.Models;
using ChildScheduler.ViewModels;
using ChildScheduler.ViewModels.Children;
using ChildScheduler.ViewModels.EducationalInstitutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Views.Children
{
    public partial class ChildDetailPage : ContentPage
    {
        protected ChildDetailViewModel ViewModel => BindingContext as ChildDetailViewModel;

        public ChildDetailPage()
        {
            InitializeComponent();
        }

        public ChildDetailPage(Models.Child child)
        {
            InitializeComponent();
            BindingContext = new ChildDetailViewModel(child);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel != null)
                await ViewModel.FetchChild();
            //await SetupMap();
        }

    }
}