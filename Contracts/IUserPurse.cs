using System;

using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Contracts
{
    public interface IUserPurse
    {
        double Balance { get; }
        
        string PurseDescription { get; }
        
        string PurseName { get; }
        
        EnumPurseStatus PurseStatus { get; }
        
        EnumPurseType PurseType { get; }
        
        Guid UserId { get; }
        
        Guid UserPurseId { get; }
    }
}