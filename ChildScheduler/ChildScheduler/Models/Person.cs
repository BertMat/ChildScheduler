using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChildScheduler.Models
{
    public class Person : ObservableObject
    {
        string userId = string.Empty;
        [JsonPropertyName("userId")]
        public string UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }
        int personId;
        [JsonPropertyName("personId")]
        public int PersonId
        {
            get => personId;
            set => SetProperty(ref personId, value);
        }
        string personName = string.Empty;
        [JsonPropertyName("personName")]
        public string PersonName
        {
            get => personName;
            set { SetProperty(ref personName, value);
                // AddressString is dependent on State
                OnPropertyChanged(nameof(DisplayFullName));
            }
        }


        string personSurname = string.Empty;
        [JsonPropertyName("personSurname")]
        public string PersonSurname
        {
            get => personSurname;
            set
            {
                SetProperty(ref personSurname, value);
                // AddressString is dependent on State
                OnPropertyChanged(nameof(DisplayFullName));
            }
        }


        [JsonIgnore]
        public string DisplayFullName => $"{PersonName} {PersonSurname}";
    }
}
