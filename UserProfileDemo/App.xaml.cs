using UserProfileDemo.Core;
using UserProfileDemo.Core.Services;
using UserProfileDemo.Pages;
using UserProfileDemo.Services;
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

            RegisterServices();

            MainPage = new LoginPage(OnSignInSuccessful);
        }

        void RegisterServices()
        {
            ServiceContainer.Register<IMediaService>(() => new MediaService());
        }

        void OnSignInSuccessful() => MainPage = new NavigationPage(new UserProfilePage(OnLogoutSuccesful));

        void OnLogoutSuccesful() => MainPage = new LoginPage(OnSignInSuccessful);
    }
}
