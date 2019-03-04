using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Couchbase.Lite.Mapping.Sample.Core.Models;
using Couchbase.Lite.Mapping.Sample.Core.Respositories;
using Couchbase.Lite.Mapping.Sample.Core.Services;

namespace Couchbase.Lite.Mapping.Sample.Core.ViewModels
{
    public class UserProfileViewModel : BaseViewModel
    {
        Action LogoutSuccessful { get; set; }

        string UserProfileId => $"user::{AppInstance.User.Username}";

        string _name;
        public string Name
        {
            get => _name;
            set => SetPropertyChanged(ref _name, value);
        }

        string _email;
        public string Email
        {
            get => _email;
            set => SetPropertyChanged(ref _email, value);
        }

        string _address;
        public string Address
        {
            get => _address;
            set => SetPropertyChanged(ref _address, value);
        }

        byte[] _imageData;
        public byte[] ImageData
        {
            get => _imageData;
            set => SetPropertyChanged(ref _imageData, value);
        }

        IAlertService _alertService;
        IAlertService AlertService
        {
            get
            {
                if (_alertService == null)
                {
                    _alertService = ServiceContainer.Resolve<IAlertService>();
                }

                return _alertService;
            }
        }

        IMediaService _mediaService;
        IMediaService MediaService
        {
            get
            {
                if (_mediaService == null)
                {
                    _mediaService = ServiceContainer.Resolve<IMediaService>();
                }

                return _mediaService;
            }
        }

        ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new Command(async() => await Save());
                }

                return _saveCommand;
            }
        }

        ICommand _selectImageCommand;
        public ICommand SelectImageCommand
        {
            get
            {
                if (_selectImageCommand == null)
                {
                    _selectImageCommand = new Command(async () => await SelectImage());
                }

                return _selectImageCommand;
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

            var userProfile = await Task.Run(() =>
            {
                var up = UserProfileRepository.Instance.GetUserProfile(UserProfileId);

                if (up == null)
                {
                    up = new UserProfile
                    {
                        Id = UserProfileId,
                        Email = AppInstance.User.Username
                    };
                }

                return up;
            });

            if (userProfile != null)
            {
                Name = userProfile.Name;
                Email = userProfile.Email;
                Address = userProfile.Address;
                ImageData = userProfile.ImageData;
            }

            IsBusy = false;
        }

        Task Save()
        {
            var userProfile = new UserProfile
            {
                Id = UserProfileId,
                Name = Name,
                Email = Email,
                Address = Address, 
                ImageData = ImageData
            };

            UserProfileRepository.Instance.SaveUserProfile(userProfile);

            return AlertService.ShowMessage(null, "Successfully updated profile!", "OK");
        }

        async Task SelectImage()
        {
            var imageData = await MediaService.PickPhotoAsync();

            if (imageData != null)
            {
                ImageData = imageData;
            }
        }

        void Logout()
        {
            AppInstance.User = null;

            LogoutSuccessful?.Invoke();
            LogoutSuccessful = null;
        }
    }
}
