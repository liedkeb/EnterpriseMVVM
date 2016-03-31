using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseMVVM.Windows
{
    public abstract class ViewModel : ObservableObject, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                return OnValidate(columnName);
            }
        }

        protected virtual string OnValidate(string propertyName)
        {
            var context = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            var results = new Collection<ValidationResult>();
            var isValid = Validator.TryValidateObject(this, context, results, true);
            return !isValid ? results[0].ErrorMessage : null;
        }

        public string Error {
            get { throw new NotSupportedException();  }
        }
    }
}
