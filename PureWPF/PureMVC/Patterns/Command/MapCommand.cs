using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using System;
using System.Collections.Generic;

namespace PureWPF.PureMVC.Patterns.Command
{
    /// <summary>
    /// This class avoids complex switch statements
    /// </summary>
    /** 
     * SimpleCommand offers the possibility to map notes with methods.
     * 
     * Benefits:
     * <ul>
     * 	<li>well-formed command-classes</li>
     * 	<li>commands are able to decide if they react on a note or not.</li>
     * 	<li>It is more easier to distinguish which method reacts on which notification.</li>
     * 	<li><b>Parameters are finally typed.</b></li>
     * </ul>
     * @example <p>Implementation of mapHandlers:</p>
     * <listing>
     * public class MyCommand extends SimpleCommand
     * {
     * 	public function MyCommand()
     * 	{
     * 		MapHandler(ApplicationFacade.SELECT, selectHandler);
     * 		MapHanlder(ApplicationFacade.EDIT, editHandler);
     * 	}
     * 	private void selectHandler(string name, Object body, string type)
     * 	{
     * 		//Executes the select-method of a proxy.
     * 		  ...
     * 	}
     * 	* 	private void editHandler(string name, Object body, string type)
     * 	{
     * 		//Executes the edit-method of a proxy.
     * 		  ...
     * 	}
     * }
     * </listing>
     */
    public class MapCommand : Notifier, INotification, ICommand
    {
        public string Name { get; set; }
        public object Body { get; set; }
        public string Type { get; set; }
        public INotification Notification { get; set; }

        private Dictionary<string, Action<string, Object, string>> map;

        public MapCommand()
        {
           this.map = new Dictionary<string, Action<string, Object, string>>();
        }
        
        public virtual void Execute(INotification Notification)
        {
            Name = Notification.Name;
            Body = Notification.Body;
            Type = Notification.Type;

            if (map[Notification.Name] != null)
            {
                Action<string, Object, string> handler = map[Notification.Name] as Action<string, Object, string>;
                handler(Name, Body, Type);
            }
        }
        /// <summary>
        /// Call this method in the inheriting command class. 
        /// </summary>
        /// <param name="note"></param>
        /// <param name="handler"></param>
        protected void MapHandler(string note, Action<string, Object, string> handler)
        {
            map[note] = handler;
        } 

}
}
