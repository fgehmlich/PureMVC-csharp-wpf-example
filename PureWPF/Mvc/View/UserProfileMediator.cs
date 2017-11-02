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
    public class UserProfileMediator : MapMediator
    {
        public new const string NAME = "UserProfileMediator";
        private UserProfile view => (UserProfile)ViewComponent;

        public UserProfileMediator(UserProfile view):base(NAME, view)
        {
            MapHandler(ApplicationFacade.USER_SELECTED, SelectedUserHandler);
            MapHandler(ApplicationFacade.APP_READY, readyHandler);
        }

        private void readyHandler(string arg1, object arg2, string arg3)
        {
            Console.WriteLine("TEST TEST TEST TEST");
        }
        private void SelectedUserHandler(string arg1, object arg2, string arg3)
        {
            view.ShowUser((UserVO) arg2);
        }
    }
}
