
namespace EnterpriseMVVM.DesktopClient.ViewModels
{
    using Windows;
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    public class MainViewModel : ViewModel
    {
        private Customer selectedCustomer;
        private readonly BusinessContext context;

        public MainViewModel() : this(new BusinessContext())
        {
        }

        public MainViewModel(BusinessContext context)
        {
            this.context = context;
            Customers = new ObservableCollection<Customer>();
        }


        public ICollection<Customer> Customers { get; set; }
        public ICommand GetCustomerListCommand
        {
            get { return new ActionCommand(p => GetCustomerList()); }
        }

        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                selectedCustomer = value;
                NotifyPropertyChanged();
            }
                
        }

        public bool IsValid
        {
            get
            {
                return SelectedCustomer==null ||  
                    (!String.IsNullOrWhiteSpace(SelectedCustomer.FirstName) &&
                    !String.IsNullOrWhiteSpace(SelectedCustomer.LastName) &&
                    !String.IsNullOrWhiteSpace(SelectedCustomer.Email));
            }
        }

        public ActionCommand AddCustomerCommand
        {
            get
            {
                return new ActionCommand(p => AddCustomer(),
                    p=> IsValid);
            }
        }
        public ActionCommand SaveCustomerCommand
        {
            get
            {
                return new ActionCommand(p => SaveCustomer(),
                    p => IsValid);
            }
        }
        public ActionCommand DeleteCustomerCommand
        {
            get
            {
                return new ActionCommand(p => DeleteCustomer());
            }
        }
        private void AddCustomer()
        {
            using (var api = new BusinessContext())
            {
                var customer = new Customer
                {
                    FirstName = "New",
                    LastName = "Customer",
                    Email = "new@customer.com"
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
        private void SaveCustomer()
        {
            context.UpdateCustomer(SelectedCustomer);
        }
        private void DeleteCustomer()
        {
            context.DeleteCustomer(SelectedCustomer);
            Customers.Remove(SelectedCustomer);
        }

        private void GetCustomerList()
        {
            Customers.Clear();
            foreach (var customer in context.GetCustomerList())
                Customers.Add(customer);
        }
    }
}
