using System;
using System.Data;
using System.Data.SqlClient;
using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Base;

namespace LittleLogBook.Data.Entities
{
	public class Currency : BaseCurrency, ICurrency
	{
		public string ExchangeRateText
		{
			get
			{
				return Math.Round(ExchangeRate, 2).ToString("N2");
			}
		}

		public Currency(Guid CreatedByUserId, string CurrencyId, string CurrencySymbol, string CurrencyName, bool IsActive) : base(CreatedByUserId, CurrencyId, CurrencySymbol, CurrencyName, IsActive)
		{

		}

		public Currency(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
		{

		}

        internal void SetInternals(object isNew, bool v, DateTime utcNow, Guid cloudUserId)
        {
            throw new NotImplementedException();
        }
    }
}