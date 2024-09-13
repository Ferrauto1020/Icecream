using System.Text.Json;
using IcecreamApp.Shared.Dtos;

namespace IcecreamApp.Services
{
    public class AuthService
    {
        private const string AuthKey = "AuthKey";
        public LoggedInUser User { get;private set; }
        public string? Token { get;private set; }

        public void Signin(AuthResponseDto dto)
        {
            var serialized = JsonSerializer.Serialize(dto);
            Preferences.Default.Set(AuthKey, serialized);
            (User, Token) = dto;
        }
        public void Signout()
        {
            Preferences.Default.Remove(AuthKey);
            (User,Token) = (null,null);
        }
    }
}