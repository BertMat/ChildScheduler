using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChildScheduler.Models
{
    public class Family : ObservableObject
    {
        int familyId;
        [JsonPropertyName("familyId")]
        public int FamilyId
        {
            get => familyId;
            set
            {
                SetProperty(ref familyId, value);
            }
        }
        string userId;
        [JsonPropertyName("userId")]
        public string UserId
        {
            get => userId;
            set
            {
                SetProperty(ref userId, value);
            }
        }
        string familyName = string.Empty;
        [JsonPropertyName("familyName")]
        public string FamilyName
        {
            get => familyName;
            set
            {
                SetProperty(ref familyName, value);
            }
        }
        ICollection<Person> people;
        [JsonPropertyName("people")]
        public ICollection<Person> People
        {
            get => people;
            set
            {
                SetProperty(ref people, value);
            }
        }
        ICollection<Category> category;
        [JsonPropertyName("category")]
        public ICollection<Category> Categories
        {
            get => category;
            set
            {
                SetProperty(ref category, value);
            }
        }
    }
}
