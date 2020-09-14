using System.Threading.Tasks;

namespace LittleLogBook.Data.Contracts
{
    public interface IVerificationCodeManager
    {
        Task<bool> ConsumeVerificationCodeAsync(string mnemonicToken, string verificationCodeValue);
        
        Task<IVerificationCode> GenerateVerificationCodeAsync(string mnemonicToken,
            EnumVerificationType verificationType, EnumVerificationCodeType verificationCodeType);
    }
}