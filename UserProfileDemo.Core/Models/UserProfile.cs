namespace UserProfileDemo.Core.Models
{
    public class UserProfile : BaseModel
    {
        public override string Type { get; set; } = "userprofile"; 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public byte[] ImageData { get; set; }
        public string Description { get; set; }
    }
}
