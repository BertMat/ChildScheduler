using ChildScheduler.Models;
using ChildScheduler.ViewModels;
using ChildScheduler.Views.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Views.Calendar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdvancedPage : ContentPage
    {
        public static BindableProperty CalenderEventCommandProperty =
            BindableProperty.Create(nameof(CalenderEventCommand), typeof(ICommand), typeof(CalendarEvent), null);

        protected AdvancedPageViewModel ViewModel => BindingContext as AdvancedPageViewModel;
        public AdvancedPage()
        {
            InitializeComponent();
        }

        public ICommand CalenderEventCommand
        {
            get => (ICommand)GetValue(CalenderEventCommandProperty);
            set => SetValue(CalenderEventCommandProperty, value);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.ExecuteLoadCommand();
        }

        private async void addLabel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EventCreatePage(ViewModel), true);
        }

        private async void event_Clicked(object sender, EventArgs e)
        {
            /*var newEvent = sender as Event;
            if (ViewModel != null)
                await ViewModel.ExecuteShowEventDetailsCommand(newEvent);*/
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var label = sender as Label;
            var newEvent = new
            {
                StartTime = label.FormattedText.Spans[0].Text,
                Name = label.FormattedText.Spans[2].Text,
            };
            if(ViewModel != null)
            {
                await ViewModel.ExecuteShowEventDetailsCommand(newEvent.StartTime, newEvent.Name);
            }    
            /*if (ViewModel != null)
                await ViewModel.ExecuteShowEventDetailsCommand(newEvent);*/
            /*if (sender is Event eventModel)
                CalenderEventCommand?.Execute(eventModel);*/
        }
    }
}