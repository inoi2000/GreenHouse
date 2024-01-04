using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GreenHouse.HttpModels.Requests
{
    public class RegisterRequest
    {
        public RegisterRequest() { }
        public RegisterRequest(string login, string password, string confirmedPassword)
        {
            Login = login;
            Password = password;
            ConfirmedPassword = confirmedPassword;
        }

        [Required, StringLength(8, ErrorMessage = "Логин не может быть более 8 символов"), JsonPropertyName("login")]
        public string Login { get; set; }

        [Required, StringLength(30, ErrorMessage = "Паполь должен быть не менее 8 символов", MinimumLength = 8), JsonPropertyName("password")]
        public string Password { get; set; }

        [Required, Compare(nameof(Password), ErrorMessage = "Пароли должны совпадать")]
        public string ConfirmedPassword { get; set; }
    }
}
