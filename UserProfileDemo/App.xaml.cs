using UserProfileDemo.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace UserProfileDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage(OnSignInSuccessful);
        }

        void OnSignInSuccessful() => MainPage = new NavigationPage(new UserProfilePage(OnLogoutSuccesful));

        void OnLogoutSuccesful() => MainPage = new LoginPage(OnSignInSuccessful);
    }
}
