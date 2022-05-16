using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ChildScheduler.Models
{
    public class AppUser : ObservableObject
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

        string email = string.Empty;
        [JsonPropertyName("email")]
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        string userId = string.Empty;
        [JsonPropertyName("userId")]
        public string UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }


        string password = string.Empty;
        [JsonPropertyName("password")]
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        bool emailConfirmed = true;
        [JsonPropertyName("emailConfirmed")]
        public bool EmailConfirmed
        {
            get => emailConfirmed;
            set => SetProperty(ref emailConfirmed, value);
        }
    }
}
