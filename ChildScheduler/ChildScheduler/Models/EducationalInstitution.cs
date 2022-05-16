using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChildScheduler.Models
{
    public class EducationalInstitution : ObservableObject
    {

        int? id;
        [JsonPropertyName("id")]
        public int? Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        int familyId;
        [JsonPropertyName("familyId")]
        public int FamilyId
        {
            get => familyId;
            set => SetProperty(ref familyId, value);
        }
        string name = string.Empty;
        [JsonPropertyName("name")]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        string fullName = string.Empty;
        [JsonPropertyName("fullName")]
        public string FullName
        {
            get => fullName;
            set => SetProperty(ref fullName, value);
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
                // StatePostal is dependent on State
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
        [JsonIgnore]
        public string AddressString => string.Format(
            "{0} {1} {2} {3}",
            Street,
            !string.IsNullOrWhiteSpace(City) ? City + "," : "",
            State,
            PostalCode);

        [JsonIgnore]
        public string StatePostal => State + " " + PostalCode;


        ICollection<Child> children;
        [JsonPropertyName("children")]
        public ICollection<Child> Children
        {
            get => children;
            set
            {
                SetProperty(ref children, value);
            }
        }
        ICollection<Event> events;
        [JsonPropertyName("events")]
        public ICollection<Event> Events
        {
            get => events;
            set
            {
                SetProperty(ref events, value);
            }
        }

    }
}
