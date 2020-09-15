namespace LittleLogBook.Data.Contracts
{
    public interface IIpLocation
    {
        int CountryId { get; }
        
        double IpFrom { get; }
        
        double IpTo { get; }
    }
}