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

    [TestClass]
    public class CustomerViewModelTests
    {
        [TestMethod]
        public void IsViewModel()
        {
            Assert.IsTrue(typeof(CustomerViewModel).BaseType == typeof(ViewModel));
        }



        [TestMethod]
        public void ValidationErrorWhenCustomerNameIsNotGreaterThanOrEqualTo2Characters()
        {
            var viewModel = new CustomerViewModel
            {
                CustomerName = "B"
            };

            Assert.IsNotNull(viewModel["CustomerName"]);
        }

        [TestMethod]
        public void NoValidationErrorWhenCustomerNameMeetsAllRequirements()
        {
            var viewModel = new CustomerViewModel
            {
                CustomerName = "David Anderson"
            };

            Assert.IsNull(viewModel["CustomerName"]);
        }

        [TestMethod]
        public void ValidationErrorWhenCustomerNameIsNotProvided()
        {
            var viewModel = new CustomerViewModel
            {
                CustomerName = null
            };

            Assert.IsNotNull(viewModel["CustomerName"]);
        }

        [TestMethod]
        public void AddCustomerCannotExecuteWhenFirstNameIsNotValid()
        {
            var viewModel = new CustomerViewModel
            {
                FirstName = null,
                LastName = "Liedke",
                Email = "noreply@abc.com"
            };

            Assert.IsFalse(viewModel.AddCustomerCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddCustomerCannotExecuteWhenLastNameIsNotValid()
        {
            var viewModel = new CustomerViewModel
            {
                FirstName = "Bartosz",
                LastName = null,
                Email = "noreply@abc.com"
            };

            Assert.IsFalse(viewModel.AddCustomerCommand.CanExecute(null));
        }
        [TestMethod]
        public void AddCustomerCannotExecuteWhenEmailIsNotValid()
        {
            var viewModel = new CustomerViewModel
            {
                FirstName = "Bartosz",
                LastName = "Liedke",
                Email = null
            };

            Assert.IsFalse(viewModel.AddCustomerCommand.CanExecute(null));
        }
        [TestMethod]
        public void AddCustomerCommandAddsCustomerToCustomerCollectionWhenExecutedSuccessfully()
        {
            var viewModel = new CustomerViewModel
            {
                FirstName = "Bartosz",
                LastName = "Liedke",
                Email = "noreply@abc.com"
            };
            viewModel.AddCustomerCommand.Execute();

            Assert.IsTrue(viewModel.Customers.Count == 1);
            
        }
    }
}