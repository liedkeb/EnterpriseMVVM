using System.Collections.Generic;

namespace EnterpriseMVVM.Data
{
    public interface IBusinessContext
    {
        /// <summary>
        /// Adds a new customer entity to the data store.
        /// </summary>
        void CreateCustomer(Customer customer);

        /// <summary>
        /// Removes customer from the data store.
        /// </summary>
        void DeleteCustomer(Customer customer);
        
        /// <summary>
        /// Gets a collection of customers.
        /// </summary>
        /// <returns>Returns a collection of <see cref="Customer"/> entities ordered by primiary key.</returns>
        ICollection<Customer> GetCustomerList();

        /// <summary>
        /// Updates the specified customer by applying the values passed in over the existing values from the data store.
        /// </summary>
        /// <param name="customer">The customer entity containing the new values to persist.</returns>
        void UpdateCustomer(Customer customer);
    }
}