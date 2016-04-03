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

        public void AddNewCustomer(Customer customer)
        {
            Check.Require(customer.FirstName);
            Check.Require(customer.LastName);
            Check.Require(customer.Email);


            context.Customers.Add(customer);
            context.SaveChanges();

        }

        static class Check
        {
            public static void Require(string value)
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Trim().Length==0)
                    throw new ArgumentException();
            }
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
