using System;
using System.Data.SqlClient;

using LittleLogBook.Data.Contracts;
using LittleLogBook.Data.Entities.Base;

namespace LittleLogBook.Data.Entities.Cloud
{
    public class CloudStorageProduct : BaseCloudStorageProduct, ICloudStorageProduct
    {
        public CloudStorageProduct(Guid CreatedByUserId, string ProductName) : base(CreatedByUserId, ProductName)
        {

        }

        public CloudStorageProduct(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
        {

        }

        public string ExtraText_01 { get; set; }
    }
}