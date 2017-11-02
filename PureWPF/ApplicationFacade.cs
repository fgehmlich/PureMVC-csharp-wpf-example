using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using PureWPF.Mvc.Controller.Macro;
using PureWPF.Mvc.Controller.Simple;

namespace PureWPF
{
    public class ApplicationFacade : Facade, IFacade
    {
        //Notification messages for communication between the different acteurs.
        public const string STARTAPP      = "StartApp";
        public const string APP_READY     = "ApplicationReady";
        public const string USER_SELECTED = "UserSelected";
        public const string NEW_USER      = "NewUser";
        public const string SAVE_USER     = "SafeUser";
        public const string REMOVE_USER = "RemoveUser";

        public ApplicationFacade(string key) : base(key) { }

        //All commands must be registered first in this method.
        protected override void InitializeController()
        {
            base.InitializeController();
            this.RegisterCommand(STARTAPP, () => new StartCommand());
            this.RegisterCommand(SAVE_USER, () => new UserCommand());
            this.RegisterCommand(REMOVE_USER, () => new UserCommand());
        }
        //executed at startup.
        //Starting Point.
        public void Start(MainWindow app)
        {
            SendNotification(STARTAPP, app);
        }
    }
}
