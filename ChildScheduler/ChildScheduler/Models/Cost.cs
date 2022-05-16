using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChildScheduler.Models
{
    public class Cost : ObservableObject
    {
        int costId;
        [JsonPropertyName("costId")]
        public int CostId
        {
            get => costId;
            set
            {
                SetProperty(ref costId, value);
            }
        }
        string costName = string.Empty;
        [JsonPropertyName("costName")]
        public string CostName
        {
            get => costName;
            set
            {
                SetProperty(ref costName, value);
            }
        }
        string costDescription = string.Empty;
        [JsonPropertyName("costDescription")]
        public string CostDescription
        {
            get => costDescription;
            set
            {
                SetProperty(ref costDescription, value);
            }
        }
        decimal? _value;
        [JsonPropertyName("value")]
        public decimal? Value
        {
            get => _value;
            set
            {
                SetProperty(ref _value, value);
            }
        }
        int categoryId;
        [JsonPropertyName("categoryId")]
        public int CategoryId
        {
            get => categoryId;
            set
            {
                SetProperty(ref categoryId, value);
            }
        }
        Category? category;
        [JsonPropertyName("category")]
        public Category? Category
        {
            get => category;
            set
            {
                SetProperty(ref category, value);
            }
        }
        int? eventId;
        [JsonPropertyName("eventId")]
        public int? EventId
        {
            get => eventId;
            set
            {
                SetProperty(ref eventId, value);
            }
        }
        Event? _event;
        [JsonPropertyName("event")]
        public Event? Event
        {
            get => _event;
            set
            {
                SetProperty(ref _event, value);
            }
        }
        DateTime costDate;
        [JsonPropertyName("costDate")]
        public DateTime CostDate
        {
            get => costDate;
            set
            {
                SetProperty(ref costDate, value);
            }
        }
    }
}
