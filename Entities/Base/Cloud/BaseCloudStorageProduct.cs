using System;
using System.Data;
using System.Data.SqlClient;

namespace LittleLogBook.Data.Entities.Base
{
    public class BaseCloudStorageProduct : BaseEntity
    {
        private Guid _cloudStorageProductId;
        private string _productName;
        private string _productImageUrl;
        private string _description;
        private double _totalPrice;
        private double? _priceExVat;
        private int _credits;
        private DateTime? _validFrom;
        private DateTime? _validTo;
        private EnumProductStatus _productStatus;
        private int _orderIndex;

        public Guid CloudStorageProductId
        {
            get
            {
                return _cloudStorageProductId;
            }
        }

        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                if (_productName != value)
                {
                    _productName = value;

                    base.IsDirty = true;
                }
            }
        }

        public string ProductImageUrl
        {
            get
            {
                return _productImageUrl;
            }
            set
            {
                if (_productImageUrl != value)
                {
                    _productImageUrl = value;

                    base.IsDirty = true;
                }
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;

                    base.IsDirty = true;
                }
            }
        }

        public double TotalPrice
        {
            get
            {
                return _totalPrice;
            }
            set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;

                    base.IsDirty = true;
                }
            }
        }

        public double? PriceExVat
        {
            get
            {
                return _priceExVat;
            }
            set
            {
                if (_priceExVat != value)
                {
                    _priceExVat = value;

                    base.IsDirty = true;
                }
            }
        }

        public int Credits
        {
            get
            {
                return _credits;
            }
            set
            {
                if (_credits != value)
                {
                    _credits = value;

                    base.IsDirty = true;
                }
            }
        }

        public DateTime? ValidFrom
        {
            get
            {
                return _validFrom;
            }
            set
            {
                if (_validFrom != value)
                {
                    _validFrom = value;

                    base.IsDirty = true;
                }
            }
        }

        public DateTime? ValidTo
        {
            get
            {
                return _validTo;
            }
            set
            {
                if (_validTo != value)
                {
                    _validTo = value;

                    base.IsDirty = true;
                }
            }
        }

        public EnumProductStatus ProductStatus
        {
            get
            {
                return _productStatus;
            }
            set
            {
                if (_productStatus != value)
                {
                    _productStatus = value;

                    base.IsDirty = true;
                }
            }
        }

        public int OrderIndex
        {
            get
            {
                return _orderIndex;
            }
            set
            {
                if (_orderIndex != value)
                {
                    _orderIndex = value;

                    base.IsDirty = true;
                }
            }
        }

        public BaseCloudStorageProduct(Guid CreatedByUserId, string ProductName) : base(CreatedByUserId)
        {
            _productName = ProductName;
        }

        public BaseCloudStorageProduct(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
        {
            string fieldname = null;

            fieldname = "CloudStorageProductId";
            _cloudStorageProductId = Reader.GetGuid(Reader.GetOrdinal(fieldname));

            fieldname = "ProductName";
            _productName = Reader.GetString(Reader.GetOrdinal(fieldname));

            fieldname = "ProductImageUrl";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _productImageUrl = Reader.GetString(Reader.GetOrdinal(fieldname));
            }

            fieldname = "Description";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _description = Reader.GetString(Reader.GetOrdinal(fieldname));
            }

            fieldname = "TotalPrice";
            _totalPrice = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));

            fieldname = "PriceExVat";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _priceExVat = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));
            }

            fieldname = "PriceExVat";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _priceExVat = Convert.ToDouble(Reader.GetDecimal(Reader.GetOrdinal(fieldname)));
            }

            fieldname = "Credits";
            _credits = Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "ValidFrom";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _validFrom = Reader.GetDateTime(Reader.GetOrdinal(fieldname));
            }

            fieldname = "ValidTo";
            if (!Reader.IsDBNull(Reader.GetOrdinal(fieldname)))
            {
                _validTo = Reader.GetDateTime(Reader.GetOrdinal(fieldname));
            }

            fieldname = "ProductStatus";
            _productStatus = (EnumProductStatus) Reader.GetInt32(Reader.GetOrdinal(fieldname));

            fieldname = "OrderIndex";
            _orderIndex = Reader.GetInt32(Reader.GetOrdinal(fieldname));
        }
    }
}