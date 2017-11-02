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
    /// <summary>
    /// This class receives UserVOs, provided by the UserProxy and passes them to the corresponding view.
    /// In addition it can be used to add/update or delete objects.
    /// </summary>
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


        /// <summary>
        /// If the user selects an item in the userList, an event will be triggered.
        /// This method is listening for this event and passes the selected user instance to UserProfileMediator. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userList_SelectUser(object sender, EventArgs e)
        {
            SendNotification(ApplicationFacade.USER_SELECTED, view.SelectedUser, null);
            Console.WriteLine("User selected");
        }
        /// <summary>
        /// If the new button has been pressed, an event will be triggered.
        /// This method is listening for the NewUser Event and passes a notification to the framework.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userList_NewUser(object sender, EventArgs e)
        {
            SendNotification(ApplicationFacade.NEW_USER, null, null);
            Console.WriteLine("New User Button pressed");
        }
        /// <summary>
        /// If a user has been selected and the delete button has been pressed, an event will be triggered.
        /// This method is listening for this event and passes notification to the framework.
        /// A command class is listening for this remove notification.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userList_RemoveUser(object sender, EventArgs e)
        {
            Console.WriteLine("Delete Button executed");
            SendNotification(ApplicationFacade.REMOVE_USER, view.SelectedUser, null);
        }
    }
}
