using System;
using System.Windows.Input;

namespace Couchbase.Lite.Mapping.Sample.Core.ViewModels
{
    public class LoginViewModel : BaseNotify
    {
        Action SignInSuccessful { get; set; }

        string _username;
        public string Username
        {
            get => _username;
            set
            {
                SetPropertyChanged(ref _username, value);
                SetPropertyChanged(nameof(CanSignIn));
            }
        }

        string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetPropertyChanged(ref _password, value);
                SetPropertyChanged(nameof(CanSignIn));
            }
        }

        public bool CanSignIn => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        ICommand _signInCommand;
        public ICommand SignInCommand
        {
            get
            {
                if (_signInCommand == null)
                {
                    _signInCommand = new Command(SignIn);
                }

                return _signInCommand;
            }
        }

        public LoginViewModel(Action signInSuccessful)
        {
            SignInSuccessful = signInSuccessful;
        }

        void SignIn()
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                AppInstance.User = new Models.UserCredentials
                {
                    Username = Username,
                    Password = Password
                };

                SignInSuccessful?.Invoke();
                SignInSuccessful = null;
            }
        }
    } 
}
