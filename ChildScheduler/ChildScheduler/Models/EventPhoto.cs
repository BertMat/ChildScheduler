using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using Xamarin.Forms;

namespace ChildScheduler.Models
{
    public class EventPhoto : ObservableObject
    {

        int eventPhotoId;
        [JsonPropertyName("eventPhotoId")]
        public int EventPhotoId
        {
            get => eventPhotoId;
            set
            {
                SetProperty(ref eventPhotoId, value);
            }
        }
        int eventId;
        [JsonPropertyName("eventId")]
        public int EventId
        {
            get => eventId;
            set
            {
                SetProperty(ref eventId, value);
            }
        }
        byte[] photo;
        [JsonPropertyName("photo")]
        public byte[] Photo
        {
            get => photo;
            set
            {
                SetProperty(ref photo, value);
            }
        }
        string eventPhotoDescription = string.Empty;
        [JsonPropertyName("eventPhotoDescription")]
        public string EventPhotoDescription
        {
            get => eventPhotoDescription;
            set
            {
                SetProperty(ref eventPhotoDescription, value);
            }
        }

        ImageSource image;
        public ImageSource Image
        {
            get => ImageSource.FromStream(() => new MemoryStream(Photo));
        }
    }
}
