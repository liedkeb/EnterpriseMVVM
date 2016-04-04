
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
        private readonly IBusinessContext context;

        public MainViewModel(IBusinessContext context)
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
                NotifyPropertyChanged("CanModify");
            }
                
        }
        /// <summary>
        /// Gets a value indicating whether <see cref="SelectedCustomer"/> is not null.
        /// </summary>
        public bool CanModify
        {
            get
            {
                return SelectedCustomer != null;
            }
        }
        /// <summary>
        /// Gets a value indicationg whether the view model is valid.
        /// </summary>
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
            var customer = new Customer
            {
                FirstName = "New",
                LastName = "Customer",
                Email = "new@customer.com"
            };
            try
            {
                context.CreateCustomer(customer);
            }
            catch (Exception)
            {

                // TODO: cover later
                return;
            }
            Customers.Add(customer);

        }
        private void SaveCustomer()
        {
            context.UpdateCustomer(SelectedCustomer);
        }
        private void DeleteCustomer()
        {
            context.DeleteCustomer(SelectedCustomer);
            Customers.Remove(SelectedCustomer);
            SelectedCustomer = null;
        }

        private void GetCustomerList()
        {
            Customers.Clear();
            foreach (var customer in context.GetCustomerList())
                Customers.Add(customer);
            SelectedCustomer = null;
        }
    }
}
