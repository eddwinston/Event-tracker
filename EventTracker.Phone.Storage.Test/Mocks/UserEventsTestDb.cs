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

namespace EventTracker.Phone.Storage.Test.Mocks
{
    public class UserEventsTestDb : IUserEventsDb
    {
        private List<Event> list;

        public UserEventsTestDb()
        {
            list = new List<Event>();
        }
        public bool Delete(Api.Event ev)
        {
            foreach (var item in list)
            {
                if (item.Id == ev.Id)
                {
                    return list.Remove(item);
                }
            }
            return false;
        }

        public bool EventExist(Api.Event ev)
        {
            foreach (var item in list)
            {
                if (item.Id == ev.Id)
                    return true;
            }
            return false;
        }

        public System.Collections.Generic.List<Api.Event> FetchAll()
        {
            return this.list;
        }

        public bool Insert(Api.Event ev)
        {
            if (!EventExist(ev))
            {
                list.Add(ev);
                return true;
            }

            return false;
        }
    }
}
