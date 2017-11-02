using PureMVC.Interfaces;
using PureMVC.Patterns.Observer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PureWPF.PureMVC.Patterns
{
    /// <summary>
    /// This class maps notes with a handling method.
    /// </summary>
    public class MapMediator : Notifier, IMediator, INotifier
    {
        public const string NAME = "Mediator";

        public string MediatorName { get; protected set; }
        public object ViewComponent { get; set; }
        public INotification Notification { get; }
        public IList<string> interests;

        private string Name { get; set; }
        private object Body { get; set; }
        private string Type { get; set; }



        private Dictionary<string, Action<string, Object, string>> map;


        public MapMediator(string mediatorName = null, Object viewComponent = null)
        {
            MediatorName = mediatorName ?? MapMediator.NAME;
            ViewComponent = viewComponent;
            interests = new List<string>();
            map = new Dictionary<string, Action<string, Object, string>>();
        }


        public virtual string[] ListNotificationInterests()
        {
            return interests.ToArray();
        }

        public virtual void HandleNotification(INotification notification)
        {
            Name = notification.Name;
            Body = notification.Body;
            Type = notification.Type;
            
            if (map.ContainsKey(notification.Name))
            {
                Action<string, Object, string> handler = map[notification.Name] as Action<string, Object, string>;
                handler(Name, Body, Type);
            }
        }

        public virtual void OnRegister(){}

        public virtual void OnRemove(){ }

        /// <summary>
        /// Call this method in the inherting mediator class.
        /// If the notification has not been added to the map object, 
        /// it will be added to the interests array and to the map-dictionary.
        /// </summary>
        /// <param name="note"></param>
        /// <param name="handler"></param>
        protected virtual void MapHandler(string note, Action<string, Object, string> handler)
        {
            if (!map.ContainsKey(note))
            {
                interests.Add(note);
                map.Add(note, handler);
            }
        }
        
    }




}
