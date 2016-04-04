
namespace EnterpriseMVVM.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class BusinessContext : IDisposable, IBusinessContext
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

        public void CreateCustomer(Customer customer)
        {
            Check.Require(customer.FirstName);
            Check.Require(customer.LastName);
            Check.Require(customer.Email);


            context.Customers.Add(customer);
            context.SaveChanges();

        }
        public void UpdateCustomer(Customer customer)
        {
            var entity = context.Customers.Find(customer.Id);

            if (entity == null)
            {
                throw new NotImplementedException("Handle appropriatly for using API design.");
            }
            context.Entry(customer).CurrentValues.SetValues(customer);
            context.SaveChanges();
        }
        public void DeleteCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new NotImplementedException("Handle appropriatly for using API design.");
            }
            context.Customers.Remove(customer);
            context.SaveChanges();
        }
        public ICollection<Customer> GetCustomerList()
        {
            return context.Customers.OrderBy(p => p.Id).ToArray();

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
