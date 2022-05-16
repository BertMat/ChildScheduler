using ChildScheduler.Models;
using ChildScheduler.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Views.Contacts
{
    public partial class ListPage : ContentPage
    {
        protected ListViewModel ViewModel => BindingContext as ListViewModel;

        public ListPage()
        {
            InitializeComponent();
            BindingContext = new ListViewModel();
        }

        async void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is Contact a))
                return;

            await Navigation.PushAsync(new DetailPage(a));

            ((ListView)sender).SelectedItem = null;
        }
        void ItemTapped(object sender, ItemTappedEventArgs e) =>
            ((ListView)sender).SelectedItem = null;


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Update the list if needed. View Model handles this logic.
            await ViewModel.ExecuteLoadCommand();
        }


        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            contactsList.ItemsSource = ViewModel.Contacts.Where(p => p.DisplayFullName.Contains(searchBar.Text));
        }
    }
}
