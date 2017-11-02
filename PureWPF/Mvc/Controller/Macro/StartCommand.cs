using PureMVC.Patterns.Command;
using PureWPF.Mvc.Controller.Simple;

namespace PureWPF.Mvc.Controller.Macro
{
    public class StartCommand : MacroCommand
    {
        public StartCommand()
        {
            AddSubCommand(() => new ModelCommand());
            AddSubCommand(() => new ViewCommand());
        }
        

    }
}
