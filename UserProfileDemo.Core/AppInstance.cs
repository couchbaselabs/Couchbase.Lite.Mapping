using UserProfileDemo.Core.Models;

namespace UserProfileDemo.Core
{
    public static class AppInstance
    {
        // For demo purposes only! In production apps, credentials must be stored more securely.
        public static UserCredentials User { get; set; }
    }
}
