
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChildScheduler.Models
{
    public class ChildHistory : ObservableObject
    {
        int? id;
        [JsonPropertyName("id")]
        public int? Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        int? childId;
        [JsonPropertyName("childId")]
        public int? ChildId
        {
            get => childId;
            set => SetProperty(ref childId, value);
        }
        Child child;
        [JsonPropertyName("child")]
        public Child Child
        {
            get => child;
            set => SetProperty(ref child, value);
        }
        decimal height;
        [JsonPropertyName("height")]
        public decimal Height
        {
            get => height;
            set => SetProperty(ref height, value);
        }
        decimal weight;
        [JsonPropertyName("height")]
        public decimal Weight
        {
            get => weight;
            set => SetProperty(ref weight, value);
        }
        DateTime? created;
        [JsonPropertyName("created")]
        public DateTime? Created
        {
            get => created;
            set => SetProperty(ref created, value);
        }
    }
}
