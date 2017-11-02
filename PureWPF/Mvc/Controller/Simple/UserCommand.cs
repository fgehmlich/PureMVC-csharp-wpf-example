using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureWPF.Mvc.Model;
using PureWPF.Mvc.Model.Vo;
using PureWPF.PureMVC.Patterns.Command;

namespace PureWPF.Mvc.Controller.Simple
{
    public class UserCommand : MapCommand
    {
        private UserProxy userProxy;

        public UserCommand()
        {
            MapHandler(ApplicationFacade.SAVE_USER, saveUserHandler);
            MapHandler(ApplicationFacade.REMOVE_USER, removeUserHandler);
        }

        private void removeUserHandler(string arg1, object arg2, string arg3)
        {
            userProxy = Facade.RetrieveProxy(UserProxy.NAME) as UserProxy;
            UserVO userVo = (UserVO) arg2;
            userProxy.DeleteItem(userVo);
        }

        private void saveUserHandler(string arg1, object arg2, string arg3)
        {
            userProxy = Facade.RetrieveProxy(UserProxy.NAME) as UserProxy;
            UserVO userVo = (UserVO) arg2;
            Console.WriteLine(userVo.Id);
            if (userVo.Id==0)
            {
                userProxy.AddItem(userVo);
            }
            else
            {
                userProxy.UpdateItem(userVo);
            }
            
        }
    }
}
