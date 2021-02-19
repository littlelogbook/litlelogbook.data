namespace LittleLogBook.Data.Contracts
{
    public interface INameValueStatistic
    {
        string ItemName { get; set; }
        
        int ItemValue { get; set; }
    }
}