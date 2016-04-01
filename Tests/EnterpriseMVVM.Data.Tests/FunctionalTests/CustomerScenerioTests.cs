
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
                Customer entity = bc.AddNewCustomer("Bartek", "Liedke");

                Assert.IsNotNull(entity);

                bool exists = bc.DataContext.Customers.Any(c => c.Id == entity.Id);

                Assert.IsTrue(exists);
            }
        }
    }
}
