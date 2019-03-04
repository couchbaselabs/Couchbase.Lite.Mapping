using System;
using Couchbase.Lite.Mapping.Sample.Core.ViewModels;
using Xamarin.Forms;

namespace Couchbase.Lite.Mapping.Sample.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(Action signInSuccessful)
        {
            InitializeComponent();

            BindingContext = new LoginViewModel(signInSuccessful);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            userNameEntry.Focus();
        }
    }
}
