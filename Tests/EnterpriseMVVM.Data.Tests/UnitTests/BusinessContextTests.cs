
namespace EnterpriseMVVM.Data.Tests.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    [TestClass]
    public class BusinessContextTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewCustomer_ThrowsException_WhenFirstNameIsNull()
        {
            using (var bc = new BusinessContext())
            {
                bc.AddNewCustomer(null, "Liedke");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewCustomer_ThrowsException_whenFirstNameIsEmpty()
        {
            using (var bc = new BusinessContext())
            {
                bc.AddNewCustomer("", "Liedke");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewCustomer_ThrowsException_WhenLastNameIsNull()
        {
            using (var bc = new BusinessContext())
            {
                bc.AddNewCustomer("Bartosz", null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewCustomer_ThrowsException_whenLastNameIsEmpty()
        {
            using (var bc = new BusinessContext())
            {
                bc.AddNewCustomer("Bartosz", "");
            }
        }
    }
}
