using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChildScheduler.Models
{
    public class Category : ObservableObject
    {
        int? categoryId;
        [JsonPropertyName("categoryId")]
        public int? CategoryId
        {
            get => categoryId;
            set
            {
                SetProperty(ref categoryId, value);
            }
        }
        string categoryName = string.Empty;
        [JsonPropertyName("categoryName")]
        public string CategoryName
        {
            get => categoryName;
            set
            {
                SetProperty(ref categoryName, value);
            }
        }
        int? familyId;
        [JsonPropertyName("familyId")]
        public int? FamilyId
        {
            get => familyId;
            set
            {
                SetProperty(ref familyId, value);
            }
        }
    }
}
