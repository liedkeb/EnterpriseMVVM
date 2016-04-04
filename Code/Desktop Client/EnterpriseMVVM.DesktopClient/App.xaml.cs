using EnterpriseMVVM.Data;
using EnterpriseMVVM.DesktopClient.ViewModels;
using EnterpriseMVVM.DesktopClient.Views;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EnterpriseMVVM.DesktopClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var container = new UnityContainer();

            container.RegisterType<IBusinessContext, BusinessContext>();
            container.RegisterType<MainViewModel>();

            var window = new MainWindow
            {
                DataContext = container.Resolve<MainViewModel>()
            };

            window.ShowDialog();

        }
    }
}
