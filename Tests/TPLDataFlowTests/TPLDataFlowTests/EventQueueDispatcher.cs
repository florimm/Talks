﻿namespace TPLDataFlowTests
{
    public class EventQueueDispatcher
    {
        private readonly Subscriber _subscriber;
        private readonly IServices _services = new StandardServices();

        public EventQueueDispatcher()
        {
            _subscriber = new Subscriber(_services);
            AddResolver(new CompositeSubscription());
        }

        public void Subscribe(object subscriber)
        {
            _subscriber.Subscribe(subscriber);
        }

        public void Publish(object message)
        {
            var subs = _subscriber.GetSubscriptionsFor(message);
            foreach (var sub in subs)
                sub.Push(message);
        }

        private void AddResolver(ISubscriptionResolver resolver)
        {
            _subscriber.AddResolver(resolver);
        }
    }
}