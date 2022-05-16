using ChildScheduler.ViewModels.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChildScheduler.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        protected ChangePasswordViewModel ViewModel => BindingContext as ChangePasswordViewModel;
        public ChangePasswordPage()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}