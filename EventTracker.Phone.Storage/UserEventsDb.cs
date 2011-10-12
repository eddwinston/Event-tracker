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
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml;

namespace EventTracker.Phone.Storage
{
    public class UserEventsDb : EventTracker.Phone.Storage.IUserEventsDb
    {
        private const string _url = @"\db.xml";
        private XmlSerializer _serializer;
        IsolatedStorageFile _store;
        private List<Event> _lst;

        public UserEventsDb()
        {
            _serializer = new XmlSerializer(typeof(List<Event>));
            _store = IsolatedStorageFile.GetUserStoreForApplication();
            _lst = FetchAll();

            if (_lst == null)
            {
                this._lst = new List<Event>();
            }
        }

        public bool Insert(Event ev)
        {
            if (!EventExist(ev))
            {
                this._lst.Add(ev);
                return SaveAll();
            }

            return false;
        }

        public List<Event> FetchAll()
        {
            List<Event> events = null;
            try
            {
                if (_store.FileExists(_url))
                {
                    using (Stream stream = _store.OpenFile(_url, FileMode.Open))
                    {
                        XmlReader reader = XmlReader.Create(stream);
                        events = (List<Event>)_serializer.Deserialize(reader);
                        reader.Close();
                    }
                }
            }
            catch
            {
                // failed reading cached file
                // assuming this is first launch : return fake data
                events = null;
            }

            return events;
        }

        private bool SaveAll()
        {
            if (_lst != null)
            {
                using (IsolatedStorageFileStream file = _store.CreateFile(_url))
                {
                    _serializer.Serialize(file, _lst);
                    return true;
                }
            }

            return false;
        }

        private bool SaveAll(IList<Event> list)
        {
            IsolatedStorageFileStream file = null;
            bool status = false;
            try
            {
                if (!_store.FileExists(_url))
                {
                    // create a new file (overwrite existing)
                    file = _store.CreateFile(_url);
                }

                _serializer.Serialize(file, _lst);
                status = true;
            }
            catch
            {
                status = false;
            }
            finally
            {
                if (file != null)
                    file.Close();
            }

            return status;
        }


        public bool EventExist(Event ev)
        {
            if (this._lst != null)
            {
                foreach (var e in _lst)
                {
                    if (ev.Id == e.Id)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Delete(Event ev)
        {
            if (this._lst != null)
            {
                foreach (var e in this._lst)
                {
                    if (ev.Id == e.Id)
                    {
                        // Delete event
                        var ss = this._lst.Remove(e);
                        return SaveAll();
                    }
                }
            }

            return false;
        }
    }
}
