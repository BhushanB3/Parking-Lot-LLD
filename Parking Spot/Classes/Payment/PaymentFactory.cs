namespace Parking_Spot.Classes.Payment
{
    public class PaymentFactory
    {
        public static IPayment CreatePayObj(PaymentType paymentType)
        {
            return paymentType switch
            {
                PaymentType.CreditCard => new CreditCardPay(),
                PaymentType.Debitcard => new DebitCardPay(),
                PaymentType.UPI => new DebitCardPay(),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
