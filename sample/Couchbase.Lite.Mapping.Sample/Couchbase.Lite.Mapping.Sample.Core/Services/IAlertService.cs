using System.Threading.Tasks;

namespace Couchbase.Lite.Mapping.Sample.Core.Services
{
    public interface IAlertService
    {
        Task ShowMessage(string title, string message, string cancel);
    }
}
