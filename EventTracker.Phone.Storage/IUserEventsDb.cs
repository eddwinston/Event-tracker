using System;
using EventTracker.Phone.Api;
using System.Collections.Generic;

namespace EventTracker.Phone.Storage
{
    public interface IUserEventsDb
    {
        bool Delete(Event ev);
        bool EventExist(Event ev);
        List<Event> FetchAll();
        bool Insert(Event ev);
    }
}
