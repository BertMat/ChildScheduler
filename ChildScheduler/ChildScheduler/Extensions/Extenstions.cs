using ChildScheduler.ViewModels;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChildScheduler.Extensions
{
    public static class Extensions
    {

        public static void TryFireOnAppearing(this Page page)
            => (page.BindingContext as ViewModelBase<ObservableObject>)?.OnAppearing();

        public static void TryFireOnDisappearing(this Page page)
            => (page.BindingContext as ViewModelBase<ObservableObject>)?.OnDisappearing();
    }
}
