using PureWPF.Mvc.View;
using PureWPF.PureMVC.Patterns.Command;
using System;

namespace PureWPF.Mvc.Controller.Simple
{
    /// <summary>
    /// This command will be executed at the beginning 
    /// and registers the UserListMediator and the UserProfileMediator at the ApplicationFacade.
    /// </summary>
    public class ViewCommand : MapCommand
    {

        public ViewCommand()
        {
            MapHandler(ApplicationFacade.STARTAPP, customHandler);
        }

        private void customHandler(string s, Object o, string arg3)
        {

            Console.WriteLine("ViewCommand executed ");
            MainWindow window = (MainWindow) o;

            Facade.RegisterMediator(new UserListMediator(window.UserList));
            Facade.RegisterMediator(new UserProfileMediator(window.UserProfile));

            SendNotification(ApplicationFacade.APP_READY,null,null);
        }
    }
}
