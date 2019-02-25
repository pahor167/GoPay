namespace GoPay.Models
{
    public class NextPaymentModel: GoPayBaseModel
    {
        public int Amount { get; set; }
        public GpCurrency Currency { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDescription { get; set; }
    }
}
