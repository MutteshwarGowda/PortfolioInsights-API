using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Domain.Aggregates.UserProfileAggregate
{
    public class UserProfile
    {
        private UserProfile()
        {

        }
        public Guid UserProfileId { get; private set; }
        public string IdentityId { get; private set; }
        public BasicInfo BasicInfo { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime LastModified { get; private set; }

        public static UserProfile CreateUserProfile(string identityId, BasicInfo basicInfo)
        {
   
            return new UserProfile()
            {
                IdentityId = identityId,
                BasicInfo = basicInfo,
                DateCreated = DateTime.UtcNow,
                LastModified = DateTime.UtcNow
            };

        }

        public void UpdateBasicInfo(BasicInfo updateBasicInfo)
        {
            BasicInfo = BasicInfo.WithUpdatedFields(
                updateBasicInfo.FirstName,
                updateBasicInfo.LastName,
                updateBasicInfo.EmailAddress,
                updateBasicInfo.Phone,
                updateBasicInfo.CurrentCity
            );

            LastModified = DateTime.UtcNow;
        }
    }
}
