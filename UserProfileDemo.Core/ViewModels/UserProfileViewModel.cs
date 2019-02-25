using System;
using System.Threading.Tasks;
using System.Windows.Input;
using UserProfileDemo.Core.Models;
using UserProfileDemo.Core.Respositories;

namespace UserProfileDemo.Core.ViewModels
{
    public class UserProfileViewModel : BaseViewModel
    {
        Action LogoutSuccessful { get; set; }

        UserProfile _userProfile;
        public UserProfile UserProfile
        {
            get => _userProfile;
            set => SetPropertyChanged(ref _userProfile, value);
        }

        ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new Command(Save);
                }

                return _saveCommand;
            }
        }

        ICommand _logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                if (_logoutCommand == null)
                {
                    _logoutCommand = new Command(Logout);
                }

                return _logoutCommand;
            }
        }

        public UserProfileViewModel(Action logoutSuccessful)
        {
            LogoutSuccessful = logoutSuccessful;

            LoadUserProfile();
        }

        async void LoadUserProfile()
        {
            IsBusy = true;

            UserProfile = await Task.Run(() =>
            {
                var userProfileId = $"user::{AppInstance.User.Username}";

                var up = UserProfileRepository.Instance.GetUserProfile(userProfileId);

                if (up == null)
                {
                    up = new UserProfile
                    {
                        Id = userProfileId,
                        Email = AppInstance.User.Username
                    };
                }

                return up;
            });

            IsBusy = false;
        }

        void Save()
        {
            UserProfileRepository.Instance.SaveUserProfile(UserProfile);
        }

        void Logout()
        {
            AppInstance.User = null;

            LogoutSuccessful?.Invoke();
            LogoutSuccessful = null;
        }
    }
}
