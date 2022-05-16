using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChildScheduler.Models
{
    public class SocialMedia : ObservableObject
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        
        private string socialMediaUrl;

        public string SocialMediaUrl
        {
            get { return socialMediaUrl; }
            set { SetProperty(ref socialMediaUrl, value); }
        }
        
        private int contactId;

        public int ContactId
        {
            get { return contactId; }
            set { SetProperty(ref contactId, value); }
        }
        
        private Contact contact;

        public Contact Contact
        {
            get { return contact; }
            set { SetProperty(ref contact, value); }
        }
    }
}
