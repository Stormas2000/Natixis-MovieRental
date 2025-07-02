namespace MovieRental.PaymentProviders.PaymentFactory
{
    public class PaymentFactory  : IPaymentFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentProvider GetPaymentProvider(string paymentMethod)
        {
            return paymentMethod switch
            {
                "MbWay" => _serviceProvider.GetRequiredService<MbWayProvider>(),
                "Paypal" => _serviceProvider.GetRequiredService<PayPalProvider>(),
                _ => throw new NotSupportedException($"'{paymentMethod}' not supported")
            };
        }
    }
}
