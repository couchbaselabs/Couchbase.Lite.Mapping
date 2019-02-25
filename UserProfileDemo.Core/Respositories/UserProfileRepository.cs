using System;
using UserProfileDemo.Core.Models;

namespace UserProfileDemo.Core.Respositories
{
    public class UserProfileRepository : BaseRepository<UserProfile>
    {
        #region Properties

        static readonly Lazy<UserProfileRepository> lazy = new Lazy<UserProfileRepository>(() => new UserProfileRepository());
        public static UserProfileRepository Instance => lazy.Value;

        #endregion

        UserProfileRepository() : base("cbsample")
        { }

        public UserProfile GetUserProfile(string userProfileId) => Get(userProfileId);

        public void SaveUserProfile(UserProfile userProfile) => Set(userProfile);
    }
}


