using System.Threading.Tasks;

using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Contracts
{
    public interface IIpLocationManager
    {
        Task<IpLocation> GetIpLocationAsync(string dottedIpAddress);
    }
}