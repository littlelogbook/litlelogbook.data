using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Cloud;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Managers
{
    public class UserManager : IUserManager
	{
        private readonly IDataHandler _dataHandler;
        private readonly IUser _currentUser;

        public UserManager(IDataHandler dataHandler, IUser currentUser)
        {
            _dataHandler = dataHandler;
            _currentUser = currentUser;
        }

        public async Task<IUser> GetUserAsync(string emailAddress)
		{
            emailAddress = (emailAddress + "").Trim();

			if (emailAddress.Length == 0)
			{
				throw new ArgumentNullException("The specified email address may not be empty");
			}

            using (var command = _dataHandler.CreateCommand("GetCloudUserByEmailAddress"))
            {
                command.AddParameter("@EmailAddress", emailAddress, DbType.String);
                command.AddParameter("@ViewedByUserId", Constants.SystemUserId, DbType.Guid);

                using (var reader = command.OpenReader())
                {
                    if (await reader.ReadAsync())
                    {
                        return new CloudUser(Constants.SystemUserId, reader);
                    }
                }
            }

			return null;
		}

        public async Task<IUser> GetUserAsync(string emailAddress, string password)
        {
            emailAddress = (emailAddress + "").Trim();
            password = (password + "").Trim();

            if (emailAddress.Length == 0 || password.Length == 0)
            {
                throw new ArgumentNullException("Email address and password may not be empty");
            }

            return await GetUserAsync(_dataHandler, emailAddress, password);
        }


        public static async Task<IUser> GetUserAsync(IDataHandler dataHandler, string emailAddress, string password)
        {
            emailAddress = (emailAddress + "").Trim();
            password = (password + "").Trim();

            if (emailAddress.Length == 0 || password.Length == 0)
            {
                throw new ArgumentNullException("Email address and password may not be empty");
            }

            using (var command = dataHandler.CreateCommand("GetCloudUserByLogin"))
            {
                command.AddParameter("@EmailAddress", emailAddress, DbType.String);
                command.AddParameter("@Password", password, DbType.String);

                using (var reader = command.OpenReader())
                {
                    if (await reader.ReadAsync())
                    {
                        return new CloudUser(Constants.SystemUserId, reader);
                    }
                }
            }

            return null;
        }

        public async Task<IUser> GetUserAsync(Guid userId)
        {
            using (var command = _dataHandler.CreateCommand("GetCloudUserByUserId"))
            {
                command.AddParameter("@CloudUserId", userId, DbType.Guid);
                command.AddParameter("@ViewedByUserId", _currentUser.CloudUserId, DbType.Guid);

                using (var reader = command.OpenReader())
                {
                    if (await reader.ReadAsync())
                    {
                        return new CloudUser(Constants.SystemUserId, reader);
                    }
                }
            }

            return null;
        }

        public async Task<IEnumerable<IUserAudit>> GetUserAuditsAsync(Guid cloudUserId, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var returnValues = new List<IUserAudit>();

            using (var command = _dataHandler.CreateCommand("GetCloudUserAudits"))
            {
                command.AddParameter("@CloudUserId", cloudUserId, DbType.Guid);
                command.AddParameter("@DateFrom", dateFrom, DbType.DateTime);
                command.AddParameter("@DateTo", dateTo, DbType.DateTime);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        returnValues.Add(new CloudUserAudit(reader));
                    }
                }
            }

            return returnValues;
        }

        public async Task<bool> IsEmailAddressAvailableAsync(string emailAddress)
		{
			emailAddress = (emailAddress + "").Trim();

			if (emailAddress.Length == 0)
			{
				throw new ArgumentNullException("The specified email address may not be empty");
			}

            using (var command = _dataHandler.CreateCommand("IsCloudEmailAddressAvailable"))
            {
                command.AddParameter("@EmailAddress", emailAddress, DbType.String);

                var result = (await command.ExecuteScalarAsync()) + "";

                return "1".Equals(result) || "True".Equals(result, StringComparison.OrdinalIgnoreCase);
            }
		}

		public async Task<bool> UpdateUserAsync(IUser user)
		{
            using (var command = _dataHandler.CreateCommand("UpdateCloudUser"))
            {
                command.AddParameter("@CloudUserId", user.CloudUserId, DbType.Guid);
                command.AddParameter("@FirstName", user.FirstName, DbType.String);
                command.AddParameter("@LastName", user.LastName, DbType.String);
                command.AddParameter("@TitleId", (int)user.Title, DbType.Int32);
                command.AddParameter("@ContactNumber", user.ContactNumber, DbType.String);
                command.AddParameter("@PreferredCurrencyIso", user.PreferredCurrencyIso, DbType.String);
                command.AddParameter("@ModifiedByUserId", user.ViewedByUserId, DbType.Guid);

                if ((await command.ExecuteNonQueryAsync()) > 0)
                {
                    ((CloudUser)user).SetInternals(false, false, DateTime.UtcNow, user.ViewedByUserId);

                    return true;
                }
            }

			return false;
		}

		public async Task<bool> RegisterUserAsync(IUser user, string password)
		{
			user.EmailAddress = (user.EmailAddress + "").Trim();

			if (user.EmailAddress.Length == 0 || (password + "").Trim().Length == 0)
			{
				throw new ArgumentNullException("Email address and password may not be empty");
			}

			if ((password + "").Length > 50)
			{
				throw new ArgumentException("The specified new password is too long, it may not be more than 50 characters in length");
			}

            using (var command = _dataHandler.CreateCommand("RegisterCloudUser"))
            {
                command.AddParameter("@CloudUserId", user.CloudUserId, DbType.Guid);
                command.AddParameter("@CreatedByUserId", user.CreatedByUserId, DbType.Guid);
                command.AddParameter("@EmailAddress", user.EmailAddress, DbType.String);
                command.AddParameter("@Password", password, DbType.String);
                command.AddParameter("@FirstName", user.FirstName, DbType.String);
                command.AddParameter("@LastName", user.LastName, DbType.String);
                command.AddParameter("@TitleId", (int)user.Title, DbType.String);
                command.AddParameter("@PreferredCurrencyIso", user.PreferredCurrencyIso, DbType.String);
                command.AddParameter("@ContactNumber", user.ContactNumber, DbType.String);

                if ((await command.ExecuteNonQueryAsync()) > 0)
                {
                    user.CloudUserStatus = EnumCloudUserStatus.Unverified;

                    ((CloudUser)user).SetInternals(false, false, null, null);

                    //var verificationCode = VerificationCodeManager.GenerateVerificationCode(Constants.SystemUserId, user.EmailAddress,
                    //    EnumVerificationType.ForgotPassword, EnumVerificationCodeType.Complex, Constants.SystemConnectionId);

                    //SmtpManager.SendCloudUserEmailVerification(User, verificationCode);

                    return true;
                }
            }

			return false;
		}

		public async Task<bool> VerifyUserEmailAddressAsync(string emailAddress, string emailAddressVerificationCode)
		{
			emailAddress = (emailAddress + "").Trim();
            emailAddressVerificationCode = (emailAddressVerificationCode + "").Trim();

            if (emailAddress.Length == 0 || emailAddressVerificationCode.Length == 0)
			{
				throw new ArgumentNullException("User email address and verification code may not be empty");
			}

            using (var command = _dataHandler.CreateCommand("VerifyCloudUserEmailAddress"))
            {
                command.AddParameter("@EmailAddressVerificationCode", emailAddressVerificationCode, DbType.String);
                command.AddParameter("@EmailAddress", emailAddress, DbType.String);

                var result = (await command.ExecuteScalarAsync()) + "";

                return "1".Equals(result) || "TRUE".Equals(result, StringComparison.OrdinalIgnoreCase);
            }
		}

		public async Task<bool> ChangeUserPasswordAsync(Guid cloudUserId, string oldPassword, string newPassword)
		{
			if ((newPassword + "").Trim().Length == 0 || Guid.Empty.Equals(cloudUserId))
			{
				throw new ArgumentNullException("User ID and new password may not be empty");
			}

			if ((newPassword + "").Length > 50)
			{
				throw new ArgumentException("The specified new password is too long, it may not be more than 50 characters in length");
			}

            using (var command = _dataHandler.CreateCommand("ChangeUserPassword"))
            {
                command.AddParameter("@CloudUserId", cloudUserId, DbType.Guid);
                command.AddParameter("@OldPassword", oldPassword, DbType.String);
                command.AddParameter("@NewPassword", newPassword, DbType.String);

                return (await command.ExecuteNonQueryAsync()) > 0;
            }
		}

		public async Task<bool> ChangeUserPasswordAsync(string verificationCodeValue, string emailAddress, string newPassword)
		{
			if ((verificationCodeValue + "").Trim().Length == 0
                || (emailAddress + "").Trim().Length == 0
                || (newPassword + "").Trim().Length == 0)
			{
				throw new ArgumentNullException("Verification code, email address and password must be specified");
			}

			if ((newPassword + "").Length > 50)
			{
				throw new ArgumentException("The specified new password is too long, it may not be more than 50 characters in length");
			}

            using (var command = _dataHandler.CreateCommand("ChangeCloudUserForgottenPassword"))
            {
                command.AddParameter("@VerificationCodeValue", verificationCodeValue, DbType.String);
                command.AddParameter("@EmailAddress", emailAddress, DbType.String);
                command.AddParameter("@NewPassword", newPassword, DbType.String);

                return (await command.ExecuteNonQueryAsync()) > 0;
            }
		}

		public async Task<bool> ChangeUserEmailAddressAsync(IUser user, string emailAddress)
		{
			if (user == null)
			{
				throw new ArgumentException("The specified user does not exist");
			}

			if (user.EmailAddress == emailAddress)
			{
				throw new ArgumentException("The specified new email address is the same as the old email address");
			}

            using (var command = _dataHandler.CreateCommand("ChangeCloudUserEmailAddress"))
            {
                command.AddParameter("@CloudUserId", user.CloudUserId, DbType.Guid, ParameterDirection.Input);
                command.AddParameter("@EmailAddress", emailAddress, DbType.String, ParameterDirection.Input);
                command.AddParameter("@CloudUserStatus", EnumCloudUserStatus.Unverified, DbType.String, ParameterDirection.Input);
                command.AddParameter("@DateModified", DateTime.UtcNow, DbType.DateTime, ParameterDirection.Input);
                command.AddParameter("@ModifiedByUserId", user.ViewedByUserId, DbType.Guid, ParameterDirection.Input);

                if ((await command.ExecuteNonQueryAsync()) > 0)
                {
                    user.EmailAddress = emailAddress;
                    ((CloudUser)user).SetInternals(false, false, DateTime.UtcNow, user.ViewedByUserId);

                    if (user.EmailAddress.Equals(emailAddress, StringComparison.InvariantCultureIgnoreCase))
                    {
                        user.CloudUserStatus = EnumCloudUserStatus.Unverified;

                        //var verificationCode = VerificationCodeManager.GenerateVerificationCode(Constants.SystemUserId, user.EmailAddress, EnumVerificationType.ForgotPassword, EnumVerificationCodeType.Complex, Constants.SystemConnectionId);
                        //SmtpManager.SendCloudUserEmailVerification(user, verificationCode);
                    }
                }

                return true;
            }
		}
	}
}