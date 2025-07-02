using System.ComponentModel.DataAnnotations;
namespace MovieRental.Customer
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }         // Primary Key
        public string CustomerName { get; set; }    // Customer Name

    }
}
