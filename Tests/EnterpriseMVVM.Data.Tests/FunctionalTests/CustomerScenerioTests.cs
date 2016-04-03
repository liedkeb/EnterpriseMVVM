
namespace EnterpriseMVVM.Data.Tests.FunctionalTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                bc.AddNewCustomer(customer);

                bool exists = bc.DataContext.Customers.Any(c => c.Id == customer.Id);

                Assert.IsTrue(exists);
            }
        }
    }
}
