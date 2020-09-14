using System;

using LittleLogBook.Data.Entities;

namespace LittleLogBook.Data.Contracts
{
    public interface ICloudStorageProduct
    {
        Guid CloudStorageProductId { get; }
        
        int Credits { get; set; }
        
        string Description { get; set; }
        
        int OrderIndex { get; set; }
        
        double? PriceExVat { get; set; }
        
        string ProductImageUrl { get; set; }
        
        string ProductName { get; set; }
        
        EnumProductStatus ProductStatus { get; set; }
        
        double TotalPrice { get; set; }
        
        DateTime? ValidFrom { get; set; }
        
        DateTime? ValidTo { get; set; }
        bool IsDirty { get; }
    }
}