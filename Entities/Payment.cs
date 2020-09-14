using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using LittleLogBook.Data.Entities.Base;

namespace LittleLogBook.Data.Entities
{
	public class Payment : BasePayment
	{
        public string AmountText
        {
            get
            {
                return base.Amount.ToString("N2");
            }
        }

        public string PaymentStatusText
        {
            get
            {
                string returnValue = "";

                switch (base.PaymentStatus)
                {
                    case EnumPaymentStatus.NotDone:
                        returnValue = "Pending";

                        break;
                    case EnumPaymentStatus.Approved:
                    case EnumPaymentStatus.Cancelled:
                        returnValue = base.PaymentStatus.ToString();

                        break;
                    case EnumPaymentStatus.UserCancelled:
                        returnValue = "Cancelled";

                        break;
                    case EnumPaymentStatus.ReceivedByProvider:
                        returnValue = "Received by provider";

                        break;
                    case EnumPaymentStatus.SettlementVoided:
                        returnValue = "Settlement voided";

                        break;
                    default:
                        break;
                }

                return returnValue;
            }
        }

        public string CustomerPaymentReferenceNumberText
        {
            get
            {
                var groupingSeperator = "-";
                var characterGrouping = 3;
                var stringValue = base.CustomerPaymentReferenceNumber.ToString("D9");
                var list = Enumerable
                    .Range(0, stringValue.Length / characterGrouping)
                    .Select(i => stringValue.Substring(i * characterGrouping, characterGrouping));

                return string.Join(groupingSeperator, list);
            }
        }

        public Payment(Guid CreatedByUserId, Guid UserId, EnumPaymentGatewayProvider PaymentGatewayProvider, string Reference, Guid PaymentReferenceId, EnumPaymentReferenceType PaymentReferenceType, string CurrencyId, double ExchangeRate, double Amount)
            : base(CreatedByUserId, UserId, PaymentGatewayProvider, Reference, PaymentReferenceId, PaymentReferenceType, CurrencyId, ExchangeRate, Amount)
		{

		}

		public Payment(Guid ViewedByUserId, SqlDataReader Reader) : base(ViewedByUserId, Reader)
		{

		}
	}
}