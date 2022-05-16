using ChildScheduler.Models;
using ChildScheduler.Utils;
using ChildScheduler.Views;
using ChildScheduler.Views.Contacts;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChildScheduler.ViewModels
{
    public class DetailViewModel : ContactViewModel
    {
        public DetailViewModel()
        {

        }
        public DetailViewModel(Contact contact)
        {
            Contact = contact;
        }

        public Contact Contact { get; private set; }

        public bool HasEmailAddress => !string.IsNullOrWhiteSpace(Contact?.Email);

        public bool HasPhoneNumber => !string.IsNullOrWhiteSpace(Contact?.PhoneNumber);

        public bool HasAddress => !string.IsNullOrWhiteSpace(Contact?.AddressString);



        AsyncCommand deleteCommand;

        public AsyncCommand DeleteCommand => deleteCommand ?? (deleteCommand = new AsyncCommand(ExecuteDeleteCommand));

        async Task ExecuteDeleteCommand()
        {
            await Dialogs.Question(new QuestionInfo
            {
                Title = string.Format("Delete {0}?", Contact.DisplayName),
                Question = null,
                Positive = "Delete",
                Negative = "Cancel",
                OnCompleted = new Action<bool>(async result =>
                {
                    if (!result)
                        return;

                    await DataSource.RemoveItem(Contact);

                    await PopAsync();
                })
            });
        }

        public async Task DisplayGeocodingError()
        {
            await Dialogs.Alert(new AlertInfo
            {
                Title = "Geocoding Error",
                Message = "Please make sure the address is valid, or that you have a network connection.",
                Cancel = "OK"
            });

            IsBusy = false;
        }
    }
}
