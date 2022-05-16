using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChildScheduler.Models
{
    public class Event : ObservableObject
    {

        int id;
        [JsonPropertyName("id")]
        public int Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
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
        Category category = new Category();
        [JsonPropertyName("category")]
        public Category Category
        {
            get => category;
            set
            {
                SetProperty(ref category, value);
            }
        }

        Family family = new Family();
        [JsonPropertyName("family")]
        public Family Family
        {
            get => family;
            set
            {
                SetProperty(ref family, value);
            }
        }
        string eventName = string.Empty;
        [JsonPropertyName("eventName")]
        public string EventName
        {
            get => eventName;
            set
            {
                SetProperty(ref eventName, value);
            }
        }

        string eventDescription = string.Empty;
        [JsonPropertyName("eventDescription")]
        public string EventDescription
        {
            get => eventDescription;
            set
            {
                SetProperty(ref eventDescription, value);
            }
        }
        DateTime startDate;
        [JsonPropertyName("startDate")]
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                SetProperty(ref startDate, value);
            }
        }
        DateTime? endDate;
        [JsonPropertyName("endDate")]
        public DateTime? EndDate
        {
            get => endDate;
            set
            {
                SetProperty(ref endDate, value);
            }
        }
        ICollection<Child> children = new List<Child>();
        [JsonPropertyName("children")]
        public ICollection<Child> Children
        {
            get => children;
            set
            {
                SetProperty(ref children, value);
            }
        }
        ICollection<Person> people = new List<Person>();
        [JsonPropertyName("people")]
        public ICollection<Person> People
        {
            get => people;
            set
            {
                SetProperty(ref people, value);
            }
        }
        ICollection<Contact> contacts = new List<Contact>();
        [JsonPropertyName("contacts")]
        public ICollection<Contact> Contacts
        {
            get => contacts;
            set
            {
                SetProperty(ref contacts, value);
            }
        }
        ICollection<Cost> costs = new List<Cost>();
        [JsonPropertyName("costs")]
        public ICollection<Cost> Costs
        {
            get => costs;
            set
            {
                SetProperty(ref costs, value);
            }
        }
        ICollection<EventPhoto> photos = new List<EventPhoto>();
        [JsonPropertyName("photos")]
        public ICollection<EventPhoto> Photos
        {
            get => photos;
            set
            {
                SetProperty(ref photos, value);
            }
        }
        int? educationalInstitutionId;
        [JsonPropertyName("educationalInstitutionId")]
        public int? EducationalInstitutionId
        {
            get => educationalInstitutionId;
            set
            {
                SetProperty(ref educationalInstitutionId, value);
            }
        }
        EducationalInstitution educationalInstitution = new EducationalInstitution();
        [JsonPropertyName("educationalInstitution")]
        public EducationalInstitution EducationalInstitution
        {
            get => educationalInstitution;
            set
            {
                SetProperty(ref educationalInstitution, value);
            }
        }
    }
}
