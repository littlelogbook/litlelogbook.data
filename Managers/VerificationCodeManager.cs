using System;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Common;
using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Managers
{
    public class VerificationCodeManager : IVerificationCodeManager
    {
        private readonly IDataHandler _dataHandler;
        private readonly ICloudUser _currentUser;

        public VerificationCodeManager(IDataHandler dataHandler, ICloudUser currentUser)
        {
            _dataHandler = dataHandler;
            _currentUser = currentUser;
        }

        public async Task<IVerificationCode> GenerateVerificationCodeAsync(string mnemonicToken,
            EnumVerificationType verificationType, EnumVerificationCodeType verificationCodeType)
        {
            var expirationDate = DateTime.UtcNow;
            var verificationCodeValue = Guid.NewGuid().ToString();

            switch (verificationType)
            {
                case EnumVerificationType.EmailAddress:
                    expirationDate = DateTime.UtcNow.AddDays(2);

                    if (verificationCodeType == EnumVerificationCodeType.Otp)
                        verificationCodeValue = verificationCodeValue.Split('-')[0];

                    break;
                case EnumVerificationType.ForgotPassword:
                    expirationDate = DateTime.UtcNow.AddDays(1);
                    verificationCodeValue = Guid.NewGuid().ToString();

                    if (verificationCodeType == EnumVerificationCodeType.Otp)
                        verificationCodeValue = verificationCodeValue.Split('-')[0];

                    break;
                case EnumVerificationType.Unknown:
                default:
                    throw new ArgumentException("Invalid or unknown verficiation type was specified");
            }

            var returnValue = new VerificationCode(_currentUser.CloudUserId, mnemonicToken,
                verificationCodeValue, verificationType, verificationCodeType, expirationDate);

            using (var command = _dataHandler.CreateCommand("CreateVerificationCode"))
            {
                command
                    .AddParameter("@VerificationCodeId", returnValue.VerificationCodeId, DbType.Guid)
                    .AddParameter("@MnemonicToken", returnValue.MnemonicToken, DbType.String)
                    .AddParameter("@VerificationCodeValue", returnValue.VerificationCodeValue, DbType.String)
                    .AddParameter("@VerificationTypeId", (int) returnValue.VerificationType, DbType.Int32)
                    .AddParameter("@VerificationCodeTypeId", (int) returnValue.VerificationCodeType, DbType.Int32)
                    .AddParameter("@ExpirationDate", returnValue.ExpirationDate, DbType.DateTime)
                    .AddParameter("@CreatedByUserId", returnValue.CreatedByUserId, DbType.Guid);

                if (await command.ExecuteNonQueryAsync() > 0)
                {
                    returnValue.SetInternals(false, false, null, null);

                    return returnValue;
                }
            }

            return null;
        }

        public async Task<bool> ConsumeVerificationCodeAsync(string mnemonicToken, string verificationCodeValue)
        {
            mnemonicToken = (mnemonicToken + "").Trim();
            verificationCodeValue = (verificationCodeValue + "").Trim();

            if (mnemonicToken.Length == 0 || verificationCodeValue.Length == 0)
            {
                throw new ArgumentNullException("The mnemonic token and verification code value must be specified");
            }

            using (var command = _dataHandler.CreateCommand("ConsumeVerificationCode"))
            {
                command
                    .AddParameter("@MnemonicToken", mnemonicToken, DbType.String)
                    .AddParameter("@VerificationCodeValue", verificationCodeValue, DbType.String);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }
}