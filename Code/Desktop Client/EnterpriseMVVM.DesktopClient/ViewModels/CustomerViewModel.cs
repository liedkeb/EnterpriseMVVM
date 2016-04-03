
namespace EnterpriseMVVM.DesktopClient.ViewModels
{
    using EnterpriseMVVM.Windows;
    using EnterpriseMVVM.Data;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    public class CustomerViewModel : ViewModel
    {
        private string customerName;

        public CustomerViewModel()
        {
            Customers = new ObservableCollection<Customer>();
        }
 
        [Required]
        [StringLength(32, MinimumLength = 4)]
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
                NotifyPropertyChanged();
            }
        }
        public ICollection<Customer> Customers { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsValid
        {
            get
            {
                return !String.IsNullOrWhiteSpace(FirstName) &&
                    !String.IsNullOrWhiteSpace(LastName) &&
                    !String.IsNullOrWhiteSpace(Email);
            }
        }

        public ActionCommand AddCustomerCommand
        {
            get
            {
                return new ActionCommand(p => AddCustomer(FirstName, LastName, Email),
                    p=> IsValid);
            }
        }
        private void AddCustomer(string firstName, string lastName, string email)
        {
            using (var api = new BusinessContext())
            {
                var customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };
                try
                {
                    api.AddNewCustomer(customer);
                }
                catch (Exception)
                {

                    // TODO: cover later
                    return;
                }
                Customers.Add(customer);
            }
        }
    }
}
