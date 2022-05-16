using ChildScheduler.Models;
using ChildScheduler.ViewModels.Families;
using ChildScheduler.Views.Children;
using ChildScheduler.Views.EducationalInstitutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Views.Families
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FamilyPage : ContentPage
    {
        protected FamilyViewModel ViewModel => BindingContext as FamilyViewModel;
        public FamilyPage()
        {
            InitializeComponent();
        }

        async void ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            ((CollectionView)sender).SelectedItem = null;
        }
        async void ChildSelected(object sender, SelectionChangedEventArgs e)
        {
            var current = e.CurrentSelection.FirstOrDefault() as Child;
            if(current != null)
                await Navigation.PushAsync(new ChildDetailPage(current));

            ((CollectionView)sender).SelectedItem = null;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.ExecuteLoadCommand();
        }

        private void ItemSelected(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void familyMembersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var choice = await DisplayActionSheet("Akcje", "Anuluj", "USUŃ Z RODZINY", "ZMIEŃ GŁOWĘ RODZINY");
            if(choice != null)
                await ViewModel.ExecuteChangeFamilyOwnerCommand(choice);

        }
    }
}