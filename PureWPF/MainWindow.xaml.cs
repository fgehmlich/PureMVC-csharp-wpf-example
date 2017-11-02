using PureMVC.Patterns.Facade;
using System;

namespace PureWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ApplicationFacade facade = (ApplicationFacade)Facade.GetInstance("ApplicationFacade", () => new ApplicationFacade("ApplicationFacade"));
            facade.Start(this);
        }       
      
    }
}
