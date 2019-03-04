using System.IO;
using System.Threading.Tasks;
using Plugin.Media;
using Couchbase.Lite.Mapping.Sample.Core.Services;

namespace Couchbase.Lite.Mapping.Sample.Services
{
    public class MediaService : IMediaService
    {
        public async Task<byte[]> PickPhotoAsync()
        {
            var result = await CrossMedia.Current.PickPhotoAsync();

            return result != null ? GetBytesFromStream(result.GetStream()) : null;
        }

        byte[] GetBytesFromStream(Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}