using System;

namespace EnterpriseMVVM.Data
{
    public sealed class BusinessContext : IDisposable
    {
        private readonly DataContext context;
        private bool disposed;

        public BusinessContext()
        {
            context = new DataContext();
        }

        public DataContext DataContext
        {
            get { return context; }
        }

        public Customer AddNewCustomer(string firstName, string lastName)
        {
            if (firstName == null)
                throw new ArgumentNullException("firstName", "firstName must be non-null");
            if (lastName == null)
                throw new ArgumentNullException("lastName", "lastName must be non-null");


            if (String.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("firstName must not be an empty string", "firstName");
            if (String.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("lastName must not be an empty string", "lastName");

            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName
            };

            context.Customers.Add(customer);
            context.SaveChanges();

            return customer;
        }

        #region IDisposable Members
        /// <summary>
        /// Performes application-defined tasks associated with freeing, releasing or resetting unmanaged resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed || !disposing)
                return;

            if (context != null)
                context.Dispose();

            disposed = true;
        }
        #endregion
    }
}
