
namespace EnterpriseMVVM.Data.Tests.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    [TestClass]
    public class BusinessContextTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewCustomer_ThrowsException_WhenEmailIsNull()
        {
            using (var bc = new BusinessContext())
            {
                var customer = new Customer
                {
                    Email = null,
                    FirstName = "Bartosz",
                    LastName = "Liedke"
                };
                bc.AddNewCustomer(customer);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewCustomer_ThrowsException_WhenEmailIsEmpty()
        {
            using (var bc = new BusinessContext())
            {
                var customer = new Customer
                {
                    Email = "",
                    FirstName = "Bartosz",
                    LastName = "Liedke"
                };
                bc.AddNewCustomer(customer);
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewCustomer_ThrowsException_WhenFirstNameIsNull()
        {
            using (var bc = new BusinessContext())
            {
                var customer = new Customer
                {
                    Email = "customer@nw.com",
                    FirstName = null,
                    LastName = "Liedke"
                };
                bc.AddNewCustomer(customer);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewCustomer_ThrowsException_whenFirstNameIsEmpty()
        {
            using (var bc = new BusinessContext())
            {
                var customer = new Customer
                {
                    Email = "customer@nw.com",
                    FirstName = "",
                    LastName = "Liedke"
                };
                bc.AddNewCustomer(customer);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewCustomer_ThrowsException_WhenLastNameIsNull()
        {
            using (var bc = new BusinessContext())
            {
                var customer = new Customer
                {
                    Email = "customer@nw.com",
                    FirstName = "Bartosz",
                    LastName = null
                };
                bc.AddNewCustomer(customer);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewCustomer_ThrowsException_whenLastNameIsEmpty()
        {
            using (var bc = new BusinessContext())
            {
                var customer = new Customer
                {
                    Email = "customer@nw.com",
                    FirstName = "Bartosz",
                    LastName = ""
                };
                bc.AddNewCustomer(customer);
            }
        }
    }
}
