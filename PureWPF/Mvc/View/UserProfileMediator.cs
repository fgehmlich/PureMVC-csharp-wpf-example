using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using PureWPF.Mvc.Model.Vo;
using PureWPF.Mvc.View.Components;
using PureWPF.PureMVC.Patterns;

namespace PureWPF.Mvc.View
{
    /// <summary>
    /// Mediator for UserProfile view.
    /// </summary>
    public class UserProfileMediator : MapMediator
    {
        public new const string NAME = "UserProfileMediator";
        private UserProfile view => (UserProfile)ViewComponent;

        public UserProfileMediator(UserProfile view):base(NAME, view)
        {
            MapHandler(ApplicationFacade.USER_SELECTED, SelectedUserHandler);
            MapHandler(ApplicationFacade.NEW_USER, newUserHandler);
        }
        public override void OnRegister()
        {
            base.OnRegister();
            view.setFormMode();
            view.UpdateUser += new EventHandler(updateUser);
        }

        private void newUserHandler(string arg1, object arg2, string arg3)
        {
            view.ShowUser(new UserVO("","","",""));
            view.setFormMode();
        }

        private void updateUser(object sender, EventArgs e)
        {
            Console.WriteLine("User updated");
            SendNotification(ApplicationFacade.SAVE_USER, view.User, null);
        }




        private void SelectedUserHandler(string arg1, object arg2, string arg3)
        {
            view.ShowUser((UserVO) arg2);
            view.setFormMode();
        }
    }
}
