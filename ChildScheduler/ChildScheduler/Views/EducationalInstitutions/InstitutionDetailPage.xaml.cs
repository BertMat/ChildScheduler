using ChildScheduler.Models;
using ChildScheduler.ViewModels;
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

namespace ChildScheduler.Views.EducationalInstitutions
{
    public partial class InstitutionDetailPage : ContentPage
    {
        protected InstitutionDetailViewModel ViewModel => BindingContext as InstitutionDetailViewModel;

        public InstitutionDetailPage()
        {
            InitializeComponent();
        }

        public InstitutionDetailPage(Models.EducationalInstitution educationalInstitution)
        {
            InitializeComponent();
            BindingContext = new InstitutionDetailViewModel(educationalInstitution);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await SetupMap();
        }

        /// <summary>
        /// Sets up the map.
        /// </summary>
        /// <returns>A Task.</returns>
        async Task SetupMap()
        {
            if (ViewModel.HasAddress)
            {
                MyContactsMap.IsVisible = false;

                // set to a default position
                Location position;

                try
                {
                    position = (await Geocoding.GetLocationsAsync(ViewModel.Institution.AddressString)).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    ViewModel.DisplayGeocodingError();
                    return;
                }

                // if lat and lon are both 0, then it's assumed that position acquisition failed
                if (position == null || (position.Latitude == 0 && position.Longitude == 0))
                {
                    ViewModel.DisplayGeocodingError();
                    return;
                }

                var xfpos = new Position(position.Latitude, position.Longitude);

                if (DeviceInfo.Platform != DevicePlatform.UWP)
                {
                    var pin = new Pin()
                    {
                        Type = PinType.Place,
                        Position = xfpos,
                        Label = ViewModel.Institution.FullName,
                        Address = ViewModel.Institution.AddressString
                    };

                    MyContactsMap.Pins.Clear();

                    MyContactsMap.Pins.Add(pin);
                }

                MyContactsMap.MoveToRegion(MapSpan.FromCenterAndRadius(xfpos, Distance.FromMiles(10)));

                MyContactsMap.IsVisible = true;
            }
        }
    }
}