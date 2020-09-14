using System;
using System.Data.SqlClient;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Base;

namespace LittleLogBook.Data.Entities
{
    public class Country : BaseCountry, ICountry
    {
        public bool IsTaxRegistered => TaxRate > 1;

        public Country(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
        {

        }
    }
}
