using System.Threading.Tasks;

namespace Couchbase.Lite.Mapping.Sample.Core.Services
{
    public interface IMediaService
    {
        Task<byte[]> PickPhotoAsync();
    }
}
