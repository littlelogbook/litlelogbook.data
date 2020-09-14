using System;

namespace LittleLogBook.Data.Contracts
{
    public interface ICloudUserAudit
    {
        string AuditDescription { get; }
        
        Guid CloudUserAuditId { get; }
        
        Guid CloudUserId { get; }
        
        DateTime DateCreated { get; }
    }
}