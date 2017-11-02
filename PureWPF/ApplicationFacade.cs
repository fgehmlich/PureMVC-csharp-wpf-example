using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using PureWPF.Mvc.Controller.Macro;
using PureWPF.Mvc.Controller.Simple;

namespace PureWPF
{
    /// <summary>
    /// ApplicationFacade is a concrete facade. It manages commands, mediators and proxys.
    ///  Generally it will be instantiated if the application has been completely created.
    /// </summary>
    public class ApplicationFacade : Facade, IFacade
    {
        //Notification messages for communication between the different acteurs.
        public const string STARTAPP      = "StartApp";
        public const string APP_READY     = "ApplicationReady";
        public const string USER_SELECTED = "UserSelected";
        public const string NEW_USER      = "NewUser";
        public const string SAVE_USER     = "SafeUser";
        public const string REMOVE_USER = "RemoveUser";

        /// <summary>
        /// Multiton ApplicationFacade factroy method.
        /// </summary>
        public ApplicationFacade(string key) : base(key){}

        /// <summary>
        /// <b>initializeController</b> registers commands with the controller.
        /// </summary>
        protected override void InitializeController()
        {
            base.InitializeController();
            this.RegisterCommand(STARTAPP, () => new StartCommand());
            this.RegisterCommand(SAVE_USER, () => new UserCommand());
            this.RegisterCommand(REMOVE_USER, () => new UserCommand());
        }
        
        /// <summary>
        /// Application startup
        /// </summary>
        /// <param name="app"></param>
        public void Start(MainWindow app)
        {
            SendNotification(STARTAPP, app);
        }
    }
}
