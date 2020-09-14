using System;

namespace LittleLogBook.Data.Contracts
{
    public interface IUserPurseTransaction
    {
        double TransactionCredits { get; }
        
        string TransactionDescription { get; }
        
        Guid UserPurseId { get; }
        
        Guid UserPurseTransactionId { get; }
    }
}