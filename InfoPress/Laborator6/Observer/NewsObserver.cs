using System;
using System.Collections.Generic;

namespace InfoPress.Observer
{
    public interface ISubscriber
    {
        void Update(string message);
    }

    public class UserSubscriber : ISubscriber
    {
        private string _name;
        public UserSubscriber(string name) => _name = name;

        public void Update(string message)
        {
            NotificationLog.Add($"[{DateTime.Now:HH:mm:ss}] Notificare trimisă către {_name}: {message}");
        }
    }

    public static class NotificationLog
    {
        public static List<string> Logs { get; } = new List<string>();
        public static void Add(string log) => Logs.Add(log);
    }

    public class NewsSubject
    {
        private List<ISubscriber> _subscribers = new List<ISubscriber>();

        public void Subscribe(ISubscriber subscriber) => _subscribers.Add(subscriber);
        public void Unsubscribe(ISubscriber subscriber) => _subscribers.Remove(subscriber);

        public void Notify(string message)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Update(message);
            }
        }
    }
}
