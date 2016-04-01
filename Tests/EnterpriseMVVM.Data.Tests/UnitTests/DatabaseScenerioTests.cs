
namespace EnterpriseMVVM.Data.Tests.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DatabaseScenerioTests
    {
        [TestMethod]
        public void CanCreateDatabase()
        {
            using (var db = new DataContext())
            {
                db.Database.Create();
            }
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            using (var db = new DataContext())
            {
                if (db.Database.Exists())
                    db.Database.Delete();
            }
        }
    }
}
