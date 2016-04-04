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
    using System.Collections.Generic;
    using Moq;
    [TestClass]
    public class MainViewModelTests 
    {
        private Mock<IBusinessContext> mock;
        private List<Customer> store;

        [TestInitialize]
        public void TestInitialze()
        {
            store = new List<Customer>();

            mock = new Mock<IBusinessContext>();
            mock.Setup(m => m.GetCustomerList()).Returns(store);
            mock.Setup(m => m.CreateCustomer(It.IsAny<Customer>())).Callback<Customer>(customer => store.Add(customer));
            mock.Setup(m => m.DeleteCustomer(It.IsAny<Customer>())).Callback<Customer>(customer => store.Remove(customer));
            mock.Setup(m => m.UpdateCustomer(It.IsAny<Customer>())).Callback<Customer>(customer =>
            {
                int i = store.IndexOf(customer);
                store[i] = customer;
            });

        }


        [TestMethod]
        public void IsViewModel()
        {
            Assert.IsTrue(typeof(MainViewModel).BaseType == typeof(ViewModel));
        }


        [TestMethod]
        public void AddCommand_CannotExecuteWhenFirstNameIsNotValid()
        {
            var viewModel = new MainViewModel(mock.Object)
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
        public void AddCommand_CannotExecuteWhenLastNameIsNotValid()
        {
            var viewModel = new MainViewModel(mock.Object)
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
        public void AddCommand_CannotExecuteWhenEmailIsNotValid()
        {
            var viewModel = new MainViewModel(mock.Object)
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
        public void AddCommand_CommandAddsCustomerToCustomerCollectionWhenExecutedSuccessfully()
        {
            var viewModel = new MainViewModel(mock.Object)
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
        public void CanModify_ShouldEqualTrueWhenSelectedCustomerIsNotNull()
        {
            var viewModel = new MainViewModel(mock.Object)
            {
                SelectedCustomer = new Customer()
            };
            Assert.IsTrue(viewModel.CanModify);
        }
        [TestMethod]
        public void CanModify_ShouldEqualFalseWhenSelectedCustomerIsNull()
        {
            var viewModel = new MainViewModel(mock.Object)
            {
                SelectedCustomer = null
            };
            Assert.IsFalse(viewModel.CanModify);
        }

        [TestMethod]
        public void GetCustomerListCommand_ListCommandPopulatesCustomersProperty()
        {
            mock.Object.CreateCustomer(new Customer { Email = "1@1.com", FirstName = "1", LastName = "a" });
            mock.Object.CreateCustomer(new Customer { Email = "2@2.com", FirstName = "2", LastName = "b" });
            mock.Object.CreateCustomer(new Customer { Email = "3@3.com", FirstName = "3", LastName = "c" });

            var viewModel = new MainViewModel(mock.Object);

            viewModel.GetCustomerListCommand.Execute(null);

            Assert.IsTrue(viewModel.Customers.Count == 3);
        }
        [TestMethod]
        public void GetCustomerListCommand_SelectedCustomerIsSetToNullWhenExecuded()
        {
            var viewModel = new MainViewModel(mock.Object)
            {
                SelectedCustomer = new Customer { Id = 1, FirstName = "Test", LastName = "Customer", Email = "tt@ee.com" }
            };
            viewModel.GetCustomerListCommand.Execute(null);
            Assert.IsNull(viewModel.SelectedCustomer);
        }
        [TestMethod]
        public void SaveCommand_InvokesIBusinessContextUpdateCustomerMethod()
        {
            // Arrange
            mock.Object.CreateCustomer(new Customer { Email = "1@1.com", FirstName = "1", LastName = "a" });

            var viewModel = new MainViewModel(mock.Object);
            viewModel.GetCustomerListCommand.Execute(null);
            viewModel.SelectedCustomer = viewModel.Customers.First();

            // Act
            viewModel.SelectedCustomer.Email = "newValue";
            viewModel.SaveCustomerCommand.Execute(null);

            // Assert
            mock.Verify(m => m.UpdateCustomer(It.IsAny<Customer>()), Times.Once);
        }

        [TestMethod]
        public void DeleteCommand_DeletesCustomerFromContext()
        {
            mock.Object.CreateCustomer(new Customer { Email = "1@1.com", FirstName = "1", LastName = "a" });

            var viewModel = new MainViewModel(mock.Object);

            viewModel.GetCustomerListCommand.Execute(null);
            viewModel.SelectedCustomer = viewModel.Customers.First();

            // Act
            viewModel.DeleteCustomerCommand.Execute(null);

            // Assert
            mock.Verify(m => m.DeleteCustomer(It.IsAny<Customer>()), Times.Once);

        }
        [TestMethod]
        public void PropertyChanged_IsReisedForSelectedCustomerWhenSelectedCustomerPropertyHasChanged()
        {
            var viewModel = new MainViewModel(mock.Object);

            bool eventRaised = false;

            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "SelectedCustomer")
                    eventRaised = true;
            };

            viewModel.SelectedCustomer = null;

            Assert.IsTrue(eventRaised);
        }
        [TestMethod]
        public void PropertyChanged_IsReisedForCanModifyWhenSelectedCustomerPropertyHasChanged()
        {
            var viewModel = new MainViewModel(mock.Object);

            bool eventRaised = false;

            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "CanModify")
                    eventRaised = true;
            };

            viewModel.SelectedCustomer = null;

            Assert.IsTrue(eventRaised);
        }

        
    }
}