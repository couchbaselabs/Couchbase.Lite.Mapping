using System.Threading.Tasks;
using Couchbase.Lite.Mapping.Sample.Core.Services;
using Xamarin.Forms;

namespace Couchbase.Lite.Mapping.Sample.Services
{
    public class AlertService : IAlertService
    {
        public Task ShowMessage(string title, string message, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}