using Couchbase.Lite.Mapping.Sample.Core;
using Couchbase.Lite.Mapping.Sample.Core.Services;
using Couchbase.Lite.Mapping.Sample.Pages;
using Couchbase.Lite.Mapping.Sample.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Couchbase.Lite.Mapping.Sample
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
            ServiceContainer.Register<IAlertService>(() => new AlertService());
            ServiceContainer.Register<IMediaService>(() => new MediaService());
        }

        void OnSignInSuccessful() => MainPage = new NavigationPage(new UserProfilePage(OnLogoutSuccesful));

        void OnLogoutSuccesful() => MainPage = new LoginPage(OnSignInSuccessful);
    }
}
