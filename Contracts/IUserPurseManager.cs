using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleLogBook.Data.Contracts
{
    public interface IUserPurseManager
    {
        Task<IUserPurse> GetUserPurseAsync(Guid userId);
        
        Task<IEnumerable<IUserPurseTransaction>> GetUserPurseTransactionsAsync(Guid userPurseId);
    }
}