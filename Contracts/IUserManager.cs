using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleLogBook.Data.Contracts
{
    public interface IUserManager
    {
        IUser CurrentUser { get; }

        Task<IUser> GetUserAsync(Guid userId);

        Task<IUser> GetUserAsync(string emailAddress);

        Task<IEnumerable<IUserAudit>> GetUserAuditsAsync(Guid cloudUserId, DateTime? dateFrom = null, DateTime? dateTo = null);

        Task<bool> IsEmailAddressAvailableAsync(string emailAddress);

        Task<bool> UpdateUserAsync(IUser user);

        Task<bool> RegisterUserAsync(IUser user, string password);

        Task<bool> VerifyUserEmailAddressAsync(string emailAddress, string emailAddressVerificationCode);

        Task<bool> ChangeUserPasswordAsync(Guid cloudUserId, string oldPassword, string newPassword);

        Task<bool> ChangeUserPasswordAsync(string verificationCodeValue, string emailAddress, string newPassword);

        Task<bool> ChangeUserEmailAddressAsync(IUser user, string emailAddress);
	}
}