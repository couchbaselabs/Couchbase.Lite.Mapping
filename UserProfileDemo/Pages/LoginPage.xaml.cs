using System;
using UserProfileDemo.Core.ViewModels;
using Xamarin.Forms;

namespace UserProfileDemo.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(Action signInSuccessful)
        {
            InitializeComponent();

            BindingContext = new LoginViewModel(signInSuccessful);
        }
    }
}
