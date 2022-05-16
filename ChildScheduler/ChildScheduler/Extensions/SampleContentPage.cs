using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChildScheduler.Extensions
{
    public class SampleContentPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.TryFireOnAppearing();
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.TryFireOnDisappearing();
        }
    }
}
