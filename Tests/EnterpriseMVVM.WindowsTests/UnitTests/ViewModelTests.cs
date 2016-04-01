

namespace EnterpriseMVVM.Windows.Tests.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [TestClass]
    public class ViewModelTests
    {
        [TestMethod]
        public void IsAbstractBaseClass()
        {
            Type t = typeof(ViewModel);
            Assert.IsTrue(t.IsAbstract);
        }

        [TestMethod]
        public void IsIDataErrorInfo()
        {
            Assert.IsTrue(typeof(IDataErrorInfo).IsAssignableFrom(typeof(ViewModel)));
        }

        [TestMethod]
        public void IsObservableObject()
        {
            Assert.IsTrue(typeof(ViewModel).BaseType == typeof(ObservableObject));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void IDataErrorInfo_ErrorPropertyIsNotImplemented()
        {
            var viewModel = new StubViewModel();
            var value = viewModel.Error;
        }

        [TestMethod]
        public void IntexerPropertyValidatesPropertyNameWithInvalidValue()
        {
            var viewModel = new StubViewModel();
            Assert.IsNotNull(viewModel["RequiredProperty"]);
        }

        [TestMethod]
        public void IntexerPropertyValidatesPropertyNameWithValidValue()
        {
            var viewModel = new StubViewModel()
            {
                RequiredProperty = "Some Value"
            };
            Assert.IsNull(viewModel["RequiredProperty"]);
        }

        [TestMethod]
        public void IndexerReturnsErrorMessageForRequestedInvalidProperty()
        {
            var viewModel = new StubViewModel
            {
                RequiredProperty = null,
                SomeOtherProperty = null
            };
            var msg = viewModel["SomeOtherProperty"];

            Assert.IsTrue(msg.Contains("SomeOtherProperty"));
        }
    }

    class StubViewModel : ViewModel
    {
        [Required]
        public string RequiredProperty { get; set; }
        [Required]
        public string SomeOtherProperty { get;  set; }
    }
}
