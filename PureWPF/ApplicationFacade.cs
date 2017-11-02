using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using PureWPF.Mvc.Controller.Macro;
using PureWPF.Mvc.Controller.Simple;

namespace PureWPF
{
    public class ApplicationFacade : Facade, IFacade
    {

        public const string STARTAPP      = "StartApp";
        public const string APP_READY     = "ApplicationReady";
        public const string USER_SELECTED = "UserSelected";

        public ApplicationFacade(string key) : base(key) { }

        protected override void InitializeController()
        {
            base.InitializeController();
            this.RegisterCommand(STARTAPP.ToString(), () => new StartCommand());
        }

        public void Start(MainWindow app)
        {
            SendNotification(STARTAPP, app);
        }
    }
}
