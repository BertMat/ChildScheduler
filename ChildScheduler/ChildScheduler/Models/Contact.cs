using Microsoft.AspNetCore.Identity;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChildScheduler.Models
{
    public class Contact : ObservableObject
    {
        public string DataPartitionId { get; set; } = string.Empty;

        int? id;
        [JsonPropertyName("id")]
        public int? Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        string contactAlias = string.Empty;
        [JsonPropertyName("contactAlias")]
        public string ContactAlias
        {
            get => contactAlias;
            set
            {
                SetProperty(ref contactAlias, value);
                // DisplayName is dependent on FirstName
                OnPropertyChanged(nameof(DisplayName));
                // DisplayLastNameFirst is dependent on FirstName
                OnPropertyChanged(nameof(DisplayFullName));
            }
        }

        string contactName = string.Empty;
        [JsonPropertyName("contactName")]
        public string ContactName
        {
            get => contactName;
            set
            {
                SetProperty(ref contactName, value);
                // DisplayName is dependent on FirstName
                OnPropertyChanged(nameof(DisplayName));
                // DisplayLastNameFirst is dependent on FirstName
                OnPropertyChanged(nameof(DisplayFullName));
            }
        }

        string contactSurname = string.Empty;
        [JsonPropertyName("contactSurname")]
        public string ContactSurname
        {
            get => contactSurname;
            set
            {
                SetProperty(ref contactSurname, value);
                // DisplayName is dependent on LastName
                OnPropertyChanged(nameof(DisplayName));
                // DisplayLastNameFirst is dependent on LastName
                OnPropertyChanged(nameof(DisplayFullName));
            }
        }

        string email = string.Empty;
        [JsonPropertyName("email")]
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        string phoneNumber = string.Empty;
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }

        string street = string.Empty;
        [JsonPropertyName("street")]
        public string Street
        {
            get => street;
            set
            {
                SetProperty(ref street, value);
                // AddressString is dependent on Street
                OnPropertyChanged(nameof(AddressString));
            }
        }

        string city = string.Empty;
        [JsonPropertyName("city")]
        public string City
        {
            get => city;
            set
            {
                SetProperty(ref city, value);
                // AddressString is dependent on City
                OnPropertyChanged(nameof(AddressString));
            }
        }
        string userId = string.Empty;
        [JsonPropertyName("userId")]
        public string UserId
        {
            get => userId;
            set
            {
                SetProperty(ref userId, value);
            }
        }
        IEnumerable<SocialMedia> socialMedias;
        [JsonPropertyName("socialMedias")]
        public IEnumerable<SocialMedia> SocialMedias
        {
            get => socialMedias;
            set
            {
                SetProperty(ref socialMedias, value);
            }
        }

        string postalCode = string.Empty;
        [JsonPropertyName("postalCode")]
        public string PostalCode
        {
            get => postalCode;
            set
            {
                SetProperty(ref postalCode, value);
                // AddressString is dependent on PostalCode
                OnPropertyChanged(nameof(AddressString));
                // StatePostal is dependent on PostalCode
                OnPropertyChanged(nameof(StatePostal));
            }
        }


        string state = string.Empty;
        [JsonPropertyName("state")]
        public string State
        {
            get => state;
            set
            {
                SetProperty(ref state, value);
                // AddressString is dependent on State
                OnPropertyChanged(nameof(AddressString));
                // StatePostal is dependent on State
                OnPropertyChanged(nameof(StatePostal));
            }
        }


        [JsonPropertyName("latitude")]
        public double Latitude { get; set; } = 0;

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; } = 0;


        string photoUrl = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/douc.jpg";
        [JsonPropertyName("photoUrl")]
        public string PhotoUrl
        {
            get => photoUrl;
            set
            {
                SetProperty(ref photoUrl, value);
                // SmallPhotoUrl is dependent on PhotoUrl
                OnPropertyChanged(nameof(SmallPhotoUrl));
            }
        }

        public string SmallPhotoUrl => PhotoUrl;

        [JsonIgnore]
        public string AddressString => string.Format(
            "{0} {1} {2} {3}",
            Street,
            !string.IsNullOrWhiteSpace(City) ? City + "," : "",
            State,
            PostalCode);

        [JsonIgnore]
        public string DisplayName => ToString();

        [JsonIgnore]
        public string DisplayFullName => $"{ContactName} {ContactSurname}";

        [JsonIgnore]
        public string StatePostal => State + " " + PostalCode;

        public override string ToString() => $"{ContactName} {ContactSurname}";
    }
}
