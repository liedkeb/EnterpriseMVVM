using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseMVVM.Windows;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseMVVM.DesktopClient.ViewModels
{
    public class CustomerViewModel : ViewModel
    {
        private string customerName;

        [Required]
        [StringLength(32, MinimumLength = 4)]
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
                NotifyPropertyChanged();
            }
        }

        protected override string OnValidate(string propertyName)
        {
            if (CustomerName!=null && !CustomerName.Contains(" "))
                return "Customer name must include both a first and a last name.";

            return base.OnValidate(propertyName);
        }
    }
}
