using System;

namespace LittleLogBook.Data.Contracts
{
    public interface IStatistic
    {
        string ItemLabel { get; set; }
        
        string ItemName { get; set; }
        
        string ItemValue { get; set; }
        
        Guid ViewedByUserId { get; }
    }
}