using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureWPF.Mvc.Model;
using PureWPF.Mvc.View.Components;
using PureWPF.PureMVC.Patterns;

namespace PureWPF.Mvc.View
{
    public class UserListMediator : MapMediator
    {
        public new const string NAME = "UserListMediator";
        private UserProxy userProxy;
        private UserList view => (UserList)ViewComponent;

        public UserListMediator(UserList view):base(NAME, view)
        {
            MapHandler(ApplicationFacade.APP_READY, customHandler);    
        }

        private void customHandler(string arg1, object arg2, string arg3)
        {
            Console.WriteLine("Nachricht????");
        }

        private void userList_SelectUser(object sender, EventArgs e)
        {
            SendNotification(ApplicationFacade.USER_SELECTED, view.SelectedUser, null);
        }

        public override void OnRegister()
        {
            base.OnRegister();
            view.SelectUser += new EventHandler(userList_SelectUser);

            userProxy = (UserProxy) Facade.RetrieveProxy(UserProxy.NAME);
            view.LoadUsers(userProxy.Users);
        }
    }
}
