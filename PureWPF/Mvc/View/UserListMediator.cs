using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureWPF.Mvc.Model;
using PureWPF.Mvc.Model.Vo;
using PureWPF.Mvc.View.Components;
using PureWPF.PureMVC.Patterns;

namespace PureWPF.Mvc.View
{
    public class UserListMediator : MapMediator
    {
        public new const string NAME = "UserListMediator";

        private UserProxy userProxy;
        private UserList view => (UserList)ViewComponent;

        public UserListMediator(UserList view):base(NAME, view){}
        public override void OnRegister()
        {
            base.OnRegister();
            view.SelectUser += new EventHandler(userList_SelectUser);
            view.NewUser += new EventHandler(userList_NewUser);
            view.DeleteUser += new EventHandler(userList_RemoveUser);

            userProxy = (UserProxy)Facade.RetrieveProxy(UserProxy.NAME);
            view.LoadUsers(userProxy.Users);
        }



        private void userList_SelectUser(object sender, EventArgs e)
        {
            SendNotification(ApplicationFacade.USER_SELECTED, view.SelectedUser, null);
            Console.WriteLine("User selected");
        }

        private void userList_NewUser(object sender, EventArgs e)
        {
            SendNotification(ApplicationFacade.NEW_USER, null, null);
            Console.WriteLine("New User Button pressed");
        }


        private void userList_RemoveUser(object sender, EventArgs e)
        {
            Console.WriteLine("Delete Button executed");
            SendNotification(ApplicationFacade.REMOVE_USER, view.SelectedUser, null);
        }
    }
}
