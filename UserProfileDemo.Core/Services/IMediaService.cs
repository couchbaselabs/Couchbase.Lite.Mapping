using System.Threading.Tasks;

namespace UserProfileDemo.Core.Services
{
    public interface IMediaService
    {
        Task<byte[]> PickPhotoAsync();
    }
}
