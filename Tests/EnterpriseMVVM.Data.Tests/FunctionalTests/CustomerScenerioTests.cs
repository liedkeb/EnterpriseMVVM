
namespace EnterpriseMVVM.Data.Tests.FunctionalTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class CustomerScenerioTests : FunctionalTest
    {
        [TestMethod]
        public void AddNewCustomerIsPresisted()
        {
            using (var bc = new BusinessContext())
            {

                var customer = new Customer
                {
                    Email = "customer@nw.com",
                    FirstName = "Bartosz",
                    LastName = "Liedke"
                };
                bc.CreateCustomer(customer);

                bool exists = bc.DataContext.Customers.Any(c => c.Id == customer.Id);

                Assert.IsTrue(exists);
            }
        }
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
                bc.CreateCustomer(customer);
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
                bc.CreateCustomer(customer);
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
                bc.CreateCustomer(customer);
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
                bc.CreateCustomer(customer);
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
                bc.CreateCustomer(customer);
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
                bc.CreateCustomer(customer);
            }
        }
        [TestMethod]
        public void UpdateCustomer_ChangedValuesAreApplied()
        {
            using (var bc = new BusinessContext())
            {
                // Arrange
                var customer = new Customer
                {
                    Email = "customer@nw.com",
                    FirstName = "Bartosz",
                    LastName = "Liedke"
                };
                bc.CreateCustomer(customer);

                const string newEmail = "new_customer@nw.com",
                    newFirstName = "Adam",
                    newLastName = "Schmit";
                customer.Email = newEmail;
                customer.FirstName = newFirstName;
                customer.LastName = newLastName;

                // Act
                bc.UpdateCustomer(customer);

                // Assert
                bc.DataContext.Entry(customer).Reload();

                Assert.AreEqual(newEmail, customer.Email);
                Assert.AreEqual(newFirstName, customer.FirstName);
                Assert.AreEqual(newLastName, customer.LastName);


            }
        }
        [TestMethod]
        public void DeleteCustomer_RemovesCustomerFromDB()
        {
            using (var bc = new BusinessContext())
            {
                // Arrange
                var customer = new Customer { Email = "1@1.com", FirstName = "1", LastName = "a" };
                bc.CreateCustomer(customer);

                // Act
                bc.DeleteCustomer(customer);

                // Assert
                Assert.IsFalse(bc.DataContext.Customers.Any());
            }
        }
        [TestMethod]
        public void GetCustomerList_ReturnsExpectedCustomer()
        {
            using (var bc = new BusinessContext())
            {
                bc.CreateCustomer(new Customer { Email = "1@1.com", FirstName = "1", LastName = "a" });
                bc.CreateCustomer(new Customer { Email = "2@2.com", FirstName = "2", LastName = "b" });
                bc.CreateCustomer(new Customer { Email = "3@3.com", FirstName = "3", LastName = "c" });

                var customers = bc.GetCustomerList();

                Assert.IsTrue(customers.ElementAt(0).Id == 1);
                Assert.IsTrue(customers.ElementAt(1).Id == 2);
                Assert.IsTrue(customers.ElementAt(2).Id == 3);
            }
        }
    }
}
