using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using EventTracker.Phone.Api;

namespace EventTracker.Phone.Services
{
    public interface IEventTrakcerService
    {
        void GetEvents(EventSearchParams searchParams, Action<EventSet> onGetCompleted = null, Action onBegin = null, Action<Exception> onError = null, Action onFinally = null);

        void GetCategories(Action<IEnumerable<EventCategory>> onGetCompleted = null, Action onBegin = null, Action<Exception> onError = null, Action onFinally = null);

        void GetVenue(string eventId, Action<Venue> onGetCompleted = null, Action onBegin = null, Action<Exception> onError = null, Action onFinally = null);
    }
}
