using System;

namespace LittleLogBook.Data.Contracts
{
    public interface IBackupCost
    {
        Guid CloudUserId { get; }
        
        int CreditCost { get; }
        
        long DataLength { get; }
        
        double PurseBalance { get; }
    }
}