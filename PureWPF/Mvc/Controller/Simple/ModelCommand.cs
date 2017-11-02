using PureWPF.PureMVC.Patterns.Command;
using System;
using PureWPF.Mvc.Model;

namespace PureWPF.Mvc.Controller.Simple
{
    public class ModelCommand : MapCommand
    {

        public ModelCommand()
        {
            MapHandler(ApplicationFacade.STARTAPP, customHandler);
        }

        private void customHandler(string arg1, object arg2, string arg3)
        {
            Console.WriteLine("ModelCommand executed");
            Facade.RegisterProxy(new UserProxy());
        }
    }
}
