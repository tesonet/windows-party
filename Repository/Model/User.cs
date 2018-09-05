using Core;
using FluentValidation;
using Newtonsoft.Json;

namespace Repository.Model
{
    public class User : NotifyPropertyChanged
    {
        private string _userName;
        private string _password;

        [JsonProperty(PropertyName = "username")]
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(() => UserName); }
        }
        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(() => Password); }
        }
             
        protected override IValidator CreateValidator()
        {
            return UserValidator.Instance;
        }
    }

    public class UserValidator : AbstractValidator<User>
    {
        private static UserValidator _instance;

        public static UserValidator Instance => _instance ?? (_instance = new UserValidator());

        public UserValidator()
        {
            RuleFor(o => o.UserName).NotEmpty().WithMessage("Username cannot be empty!");
        }
    }
}