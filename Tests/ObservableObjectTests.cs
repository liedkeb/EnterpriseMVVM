using EnterpriseMVVM.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{

    [TestClass]
    public class ObservableObjectTests
    {
        [TestMethod]
        public void PropertyChangedEventHandlerIsRised()
        {
            var obj = new StubObservableObject();

            bool raised = false;
            obj.PropertyChanged += (sender, e) =>
            {
                Assert.IsTrue(e.PropertyName == "ChangedProperty");
                raised = true;
            };

            obj.ChangedProperty = "Some Value";

            if (!raised) Assert.Fail("PropertyChanged was never invoded.");
        }
    }

    class StubObservableObject : ObservableObject
    {
        private string changedProperty;
        public string ChangedProperty
        {
            get { return changedProperty; }
            set
            {
                changedProperty = value;
                NotifyPropertyChanged();
            }
        }
    }
}
