using ChildScheduler.ViewModels;
using ChildScheduler.Views.Events;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarFooter : ContentView
    {
        protected AdvancedPageViewModel ViewModel => BindingContext as AdvancedPageViewModel;
        public CalendarFooter()
        {
            InitializeComponent();
        }
        AsyncCommand addEventCommand;
        public AsyncCommand AddEventCommand => addEventCommand ??=
            new AsyncCommand(ExecuteAddEventCommand);

        public async Task ExecuteAddEventCommand()
        {
            await Navigation.PushModalAsync(new EventCreatePage(ViewModel), true);
        }

        private async void addLabel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EventCreatePage(ViewModel), true);
        }
    }
}