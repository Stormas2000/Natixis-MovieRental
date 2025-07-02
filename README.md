# MovieRental Exercise

This is a dummy representation of a movie rental system.
Can you help us fix some issues and implement missing features?

* The app is throwing an error when we start, please help us. Also, tell us what caused the issue.

**Error:**  
System.AggregateException: 'Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: MovieRental.Rental.IRentalFeatures Lifetime: Singleton ImplementationType: MovieRental.Rental.RentalFeatures': Cannot consume scoped service 'MovieRental.Data.MovieRentalDbContext' from singleton 'MovieRental.Rental.IRentalFeatures'.)'
**Answer:**
    The app fails to start because MovieRentalDbContext is registered as a scoped service, but RentalFeatures was registered as a singleton and depends on it.
    You cannot inject a scoped service into a singleton, because scoped services are created per request, while singletons are created only once for the application's lifetime.
    To fix this, we changed the registration of RentalFeatures to scoped, matching the lifetime of MovieRentalDbContext. After this change, the app starts successfully.

* The rental class has a method to save, but it is not async, can you make it async and explain to us what is the difference?
  An async version does not block the thread while waiting for the database operation, making the app more scalable and responsive, especially under high load.

* Please finish the method to filter rentals by customer name, and add the new endpoint.

* We noticed we do not have a table for customers, it is not good to have just the customer name in the rental.
   Can you help us add a new entity for this? Don't forget to change the customer name field to a foreign key, and fix your previous method!

* In the MovieFeatures class, there is a method to list all movies, tell us your opinion about it.
    There is no error handling (e.g., try-catch).
    It loads all movies without paging, which may cause performance issues or affect frontend usage negatively.

* No exceptions are being caught in this api, how would you deal with these exceptions?
    I would encapsulate the methods that might throw exceptions, log the detailed exception information to a log file or monitoring system, and return a generic error message to the client to avoid exposing internal details.

 ## Challenge (Nice to have)

We need to implement a new feature in the system that supports automatic payment processing. Given the advancements in technology, it is essential to integrate multiple payment providers into our system.

Here are the specific instructions for this implementation:

* Payment Provider Classes:
  * In the "PaymentProvider" folder, you will find two classes that contain basic (dummy) implementations of payment providers. These can be used as a starting point for your work.
* RentalFeatures Class:
  * Within the RentalFeatures class, you are required to implement the payment processing functionality.
* Payment Provider Designation:
  * The specific payment provider to be used in a rental is specified in the Rental model under the attribute named "PaymentMethod".
* Extensibility:
  * The system should be designed to allow the addition of more payment providers in the future, ensuring flexibility and scalability.
* Payment Failure Handling:
  * If the payment method fails during the transaction, the system should prevent the creation of the rental record. In such cases, no rental should be saved to the database.
