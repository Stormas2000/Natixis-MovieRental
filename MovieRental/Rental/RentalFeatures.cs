using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.PaymentProviders;
using MovieRental.PaymentProviders.PaymentFactory;
using System.ComponentModel;

namespace MovieRental.Rental
{
	public class RentalFeatures : IRentalFeatures
	{
		private readonly MovieRentalDbContext _movieRentalDb;
		private readonly IPaymentFactory _paymentFactory;
		private readonly int DAY_RATE = 10;
		public RentalFeatures(MovieRentalDbContext movieRentalDb,IPaymentFactory paymentFactory)
		{
			_movieRentalDb = movieRentalDb;
			_paymentFactory = paymentFactory;
		}

	

		public async Task<Rental> Save(Rental rental)
		{
			double price = rental.DaysRented * 10;
			bool paymentSucess = await HandlePayment(rental.PaymentMethod, price);
            if (paymentSucess)
			{
                await _movieRentalDb.Rentals.AddAsync(rental);
                await _movieRentalDb.SaveChangesAsync();
                return rental;
            }
			return null;
		}

		
		public IEnumerable<Rental> GetRentalsByCustomerName(string customerName)
		{

			return _movieRentalDb.Rentals.Include(r => r.Customer).Where(c => c.Customer.CustomerName == customerName).ToList();
        }

		
		private async Task<bool> HandlePayment(string paymentMethod, double price)
		{
            IPaymentProvider payProvider = _paymentFactory.GetPaymentProvider(paymentMethod);
            try
			{
               
				return await payProvider.Pay(price); 
            }
			catch (Exception ex)
			{
				throw new Exception("Error in payment");
			}
           
			
		}
	}
}
