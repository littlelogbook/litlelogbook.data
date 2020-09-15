using System;

namespace LittleLogBook.Data.Contracts
{
    public interface IUserAudit
    {
        string AuditDescription { get; }
        
        Guid CloudUserAuditId { get; }
        
        Guid CloudUserId { get; }
        
        DateTime DateCreated { get; }
    }
}