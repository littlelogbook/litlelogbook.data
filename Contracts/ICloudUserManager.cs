using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LittleLogBook.Data.Entities.Cloud;

namespace LittleLogBook.Data.Contracts
{
    public interface ICloudUserManager
    {
        Task<ICloudUser> GetUserAsync(Guid userId);

        Task<ICloudUser> GetUserAsync(string emailAddress);

        Task<ICloudUser> GetUserAsync(string emailAddress, string password);

        Task<IEnumerable<ICloudUserAudit>> GetUserAuditsAsync(Guid cloudUserId, DateTime? dateFrom = null, DateTime? dateTo = null);

        Task<bool> IsEmailAddressAvailableAsync(string emailAddress);

        Task<bool> UpdateUserAsync(CloudUser user);

        Task<bool> RegisterUserAsync(CloudUser user, string password);

        Task<bool> VerifyUserEmailAddressAsync(string emailAddress, string emailAddressVerificationCode);

        Task<bool> ChangeUserPasswordAsync(Guid cloudUserId, string oldPassword, string newPassword);

        Task<bool> ChangeUserPasswordAsync(string verificationCodeValue, string emailAddress, string newPassword);

        Task<bool> ChangeUserEmailAddressAsync(CloudUser user, string emailAddress);
	}
}