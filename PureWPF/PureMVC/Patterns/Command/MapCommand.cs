using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using System;
using System.Collections.Generic;

namespace PureWPF.PureMVC.Patterns.Command
{
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

        protected void MapHandler(string note, Action<string, Object, string> handler)
        {
            map[note] = handler;
        } 

}
}
