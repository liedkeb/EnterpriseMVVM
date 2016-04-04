// -----------------------------------------------------------------------------
//  <copyright file="CustomerViewModelTests.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------
namespace EnterpriseMVVM.DesktopClient.Tests.UnitTests
{
    using ViewModels;
    using Windows;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Data;
    using Data.Tests;
    using System.Linq;
    [TestClass]
    public class MainViewModelTests : FunctionalTest
    {
        [TestMethod]
        public void IsViewModel()
        {
            Assert.IsTrue(typeof(MainViewModel).BaseType == typeof(ViewModel));
        }


        [TestMethod]
        public void AddCustomerCannotExecuteWhenFirstNameIsNotValid()
        {
            var viewModel = new MainViewModel
            {
                SelectedCustomer = new Customer
                {
                    FirstName = null,
                    LastName = "Liedke",
                    Email = "noreply@abc.com"
                }
            };

            Assert.IsFalse(viewModel.AddCustomerCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddCustomerCannotExecuteWhenLastNameIsNotValid()
        {
            var viewModel = new MainViewModel
            {
                SelectedCustomer = new Customer
                {
                    FirstName = "Bartosz",
                    LastName = null,
                    Email = "noreply@abc.com"
                }
            };

            Assert.IsFalse(viewModel.AddCustomerCommand.CanExecute(null));
        }
        [TestMethod]
        public void AddCustomerCannotExecuteWhenEmailIsNotValid()
        {
            var viewModel = new MainViewModel
            {
                SelectedCustomer = new Customer
                {
                    FirstName = "Bartosz",
                    LastName = "Liedke",
                    Email = null
                }
            };

            Assert.IsFalse(viewModel.AddCustomerCommand.CanExecute(null));
        }
        [TestMethod]
        public void AddCustomerCommandAddsCustomerToCustomerCollectionWhenExecutedSuccessfully()
        {
            var viewModel = new MainViewModel
            {
                SelectedCustomer = new Customer
                {
                    FirstName = "Bartosz",
                    LastName = "Liedke",
                    Email = "noreply@abc.com"
                }
            };
            viewModel.AddCustomerCommand.Execute();

            Assert.IsTrue(viewModel.Customers.Count == 1);
            
        }
        [TestMethod]
        public void GetCustomerListCommandPopulatesCustomersProperty()
        {
            using (var bc = new BusinessContext())
            {
                bc.AddNewCustomer(new Customer { Email = "1@1.com", FirstName = "1", LastName = "a" });
                bc.AddNewCustomer(new Customer { Email = "2@2.com", FirstName = "2", LastName = "b" });
                bc.AddNewCustomer(new Customer { Email = "3@3.com", FirstName = "3", LastName = "c" });

                var viewModel = new MainViewModel(bc);

                viewModel.GetCustomerListCommand.Execute(null);

                Assert.IsTrue(viewModel.Customers.Count == 3);
            }
        }
        [TestMethod]
        public void SaveCommand_UpdateSelectedCustomerFirstName()
        {
            using (var bc = new BusinessContext())
            {
                // Arrange
                bc.AddNewCustomer(new Customer { Email = "1@1.com", FirstName = "1", LastName = "a" });

                var viewModel = new MainViewModel(bc);

                viewModel.GetCustomerListCommand.Execute(null);
                viewModel.SelectedCustomer = viewModel.Customers.First();

                // Act
                viewModel.SelectedCustomer.FirstName = "newFirstName";
                viewModel.SaveCustomerCommand.Execute(null);

                // Assert
                var customer = bc.DataContext.Customers.Single();
                bc.DataContext.Entry(customer).Reload();
                Assert.AreEqual(viewModel.SelectedCustomer.FirstName, customer.FirstName);
            }
        }
        [TestMethod]
        public void DeleteCommand_DeletesCustomerFromContext()
        {
            using (var bc = new BusinessContext())
            {            // Arrange
                bc.AddNewCustomer(new Customer { Email = "1@1.com", FirstName = "1", LastName = "a" });

                var viewModel = new MainViewModel(bc);

                viewModel.GetCustomerListCommand.Execute(null);
                viewModel.SelectedCustomer = viewModel.Customers.First();

                // Act
                viewModel.DeleteCustomerCommand.Execute(null);

                // Assert
                Assert.IsFalse(viewModel.Customers.Any());
            }

        }
    }
}