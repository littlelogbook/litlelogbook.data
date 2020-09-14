using System;

namespace LittleLogBook.Data.Contracts
{
    public interface IVerificationCode
    {
        DateTime ExpirationDate { get; set; }
        
        string MnemonicToken { get; set; }
        
        Guid VerificationCodeId { get; }
        
        EnumVerificationCodeType VerificationCodeType { get; set; }
        
        string VerificationCodeValue { get; set; }
        
        EnumVerificationType VerificationType { get; set; }
        
        string VerificationCodeValueUrlEncoded { get; }
    }
}