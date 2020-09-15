using System;
using System.Data;
using System.Threading.Tasks;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities;
using LittleLogBook.Data.SqlConnectivity;

namespace LittleLogBook.Data.Managers
{
    public class BasketManager : ManagerBase, IBasketManager
    {
        private readonly IDataHandler _dataHandler;

        internal BasketManager(IDataHandler dataHandler, IUser currentUser)
            : base(currentUser)
        {
            _dataHandler = dataHandler;
        }

        public async Task<bool> AddBasketItem(Guid BasketItemId, Guid UserId, Guid ItemReferenceId, EnumPaymentReferenceType ItemReferenceType,
            double NettAmount, double Quantity, double TotalNettAmount, double TaxRate, double TotalTax, double TotalAmount)
        {
            using (var command = _dataHandler.CreateCommand("InsertBasketItem"))
            {
                command.AddParameter("@BasketItemId", BasketItemId, DbType.Guid);
                command.AddParameter("@UserId", UserId, DbType.Guid);
                command.AddParameter("@ItemReferenceId", ItemReferenceId, DbType.Guid);
                command.AddParameter("@ItemReferenceTypeId", (int)ItemReferenceType, DbType.Int32);
                command.AddParameter("@NettAmount", NettAmount, DbType.Decimal);
                command.AddParameter("@Quantity", Quantity, DbType.Decimal);
                command.AddParameter("@TotalNettAmount", TotalNettAmount, DbType.Decimal);
                command.AddParameter("@TaxRate", TaxRate, DbType.Decimal);
                command.AddParameter("@TotalTax", TotalTax, DbType.Decimal);
                command.AddParameter("@TotalAmount", TotalAmount, DbType.Decimal);
                command.AddParameter("@CreatedByUserId", CurrentUser.CloudUserId, DbType.Guid);

                return (await command.ExecuteNonQueryAsync()) > 0;
            }
        }
    }
}
