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
using EventTracker.Phone.Api;
using System.Collections.Generic;

namespace EventTracker.Phone.Services
{
    public class FakeHttpService : IEventTrakcerService
    {

        public void GetEvents(EventSearchParams searchParams, Action<Api.EventSet> onGetCompleted = null, Action onBegin = null, Action<Exception> onError = null, Action onFinally = null)
        {
            if (onBegin != null)
                onBegin();

            if (onGetCompleted != null)
            {
                const int total = 20;

                Api.EventSet eventSet = new Api.EventSet();
                var events = new Event[total];
                for (int i = 0; i < total; i++)
                {
                    events[i] = new Event
                    {
                        Id = string.Format("{0} - {1}", "event", i),
                        CityName = "City " + i,
                        Country = "Country-" + i,
                        Description = "Description-" + i,
                        Latitude = "3430.0",
                        Longitude = "89.023",
                        PostalCode = "783" + i,
                        Price = i.ToString(),
                        RecurString = "Recuring String " + i,
                        StartTime = DateTime.Now.ToLongDateString(),
                        StopTime = DateTime.Now.AddDays(2).ToLongDateString(),
                        Title = "Title - " + i,
                        Url = "http://example.com",
                        VenueAddress = "Venue Address " + i,
                        VenueName = "Venue name " + i
                    };
                }
                eventSet.Events = events;
                onGetCompleted(eventSet);
            }

            if (onFinally != null)
                onFinally();
        }

        public void GetCategories(Action<System.Collections.Generic.IEnumerable<Api.EventCategory>> onGetCompleted = null, Action onBegin = null, Action<Exception> onError = null, Action onFinally = null)
        {
            if (onGetCompleted != null)
            {
                List<EventCategory> categories = new List<EventCategory>();

                for (int i = 0; i < 20; i++)
                {
                    categories.Add(new EventCategory
                    {
                        Id = "cat" + i,
                        Name = "Category - " + i
                    });
                }

                onGetCompleted(categories);
            }
        }

        public void GetVenue(string eventId, Action<Api.Venue> onGetCompleted = null, Action onBegin = null, Action<Exception> onError = null, Action onFinally = null)
        {
            throw new NotImplementedException();
        }
    }
}
