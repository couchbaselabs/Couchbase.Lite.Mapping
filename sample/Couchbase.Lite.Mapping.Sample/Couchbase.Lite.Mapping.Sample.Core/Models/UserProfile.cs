namespace Couchbase.Lite.Mapping.Sample.Core.Models
{
    public class UserProfile
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public byte[] ImageData { get; set; }
        public string Description { get; set; }
    }
}
