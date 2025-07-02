namespace MovieRental.PaymentProviders.PaymentFactory
{
    public interface IPaymentFactory
    {
        IPaymentProvider GetPaymentProvider(string paymentMethod);
    }
}
