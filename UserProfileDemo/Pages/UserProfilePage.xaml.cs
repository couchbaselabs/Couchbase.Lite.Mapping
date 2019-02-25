using System;
using UserProfileDemo.Core.ViewModels;
using Xamarin.Forms;

namespace UserProfileDemo.Pages
{
    public partial class UserProfilePage : ContentPage
    {
        public UserProfilePage(Action logoutSuccesful)
        {
            InitializeComponent();

            BindingContext = new UserProfileViewModel(logoutSuccesful);
        }
    }
}
