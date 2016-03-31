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
            Assert.IsTrue(typeof (CustomerViewModel).BaseType == typeof (ViewModel));
        }

        [TestMethod]
        public void ValidationErrorWhenCustomerNameExceeds32Characters()
        {
            var viewModel = new CustomerViewModel
                            {
                                CustomerName = "1234567890abcd efghijkilmnopqrstv"
                            };

            Assert.IsNotNull(viewModel["CustomerName"]);
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
    }
}