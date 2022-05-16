using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChildScheduler.Models.Responses
{
    public class RegistrationResponse : ObservableObject
    {

        string token;
        [JsonPropertyName("token")]
        public string Token
        {
            get => token;
            set => SetProperty(ref token, value);
        }
        string refreshToken;
        [JsonPropertyName("refreshToken")]
        public string RefreshToken
        {
            get => refreshToken;
            set => SetProperty(ref refreshToken, value);
        }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        bool emailConfirmed = true;
        [JsonPropertyName("emailConfirmed")]
        public bool EmailConfirmed
        {
            get => emailConfirmed;
            set => SetProperty(ref emailConfirmed, value);
        }
    }
}
