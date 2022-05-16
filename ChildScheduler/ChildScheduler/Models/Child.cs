using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChildScheduler.Models
{
    public class Child : ObservableObject
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
        string childName = string.Empty;
        [JsonPropertyName("childName")]
        public string ChildName
        {
            get => childName;
            set => SetProperty(ref childName, value);
        }
        DateTime birthDate;
        [JsonPropertyName("birthDate")]
        public DateTime BirthDate
        {
            get => birthDate;
            set => SetProperty(ref birthDate, value);
        }
        decimal? height;
        [JsonPropertyName("height")]
        public decimal? Height
        {
            get => height;
            set => SetProperty(ref height, value);
        }
        decimal? weight;
        [JsonPropertyName("weight")]
        public decimal? Weight
        {
            get => weight;
            set => SetProperty(ref weight, value);
        }
        ICollection<ChildHistory> childHistories;

        [JsonPropertyName("childHistories")]
        public ICollection<ChildHistory> ChildHistories
        {
            get => childHistories;
            set => SetProperty(ref childHistories, value);
        }
    }
}
