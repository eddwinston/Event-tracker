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
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using EventTracker.Phone.Api;

namespace EventTracker.Phone.Storage
{
    public class EventTrackerStorage
    {
        private const string _url = @"\cache.xml";
        private const string _temp = @"\temp.xml";
        private const string _eventCategoryTemp = @"\eventCategoryTemp.xml";

        private static XmlSerializer _serializer = new XmlSerializer(typeof(EventSet));
        private static XmlSerializer _tempSerializer = new XmlSerializer(typeof(Event));
        private static XmlSerializer _eventCategorySerializer = new XmlSerializer(typeof(EventCategory));

        private const string _eventCategoriesTemp = @"\eventCategoriesTemp.xml";
        private static XmlSerializer _eventCategoriesSerializer = new XmlSerializer(typeof(List<EventCategory>));

        #region Save and Load EventSet methods
        public static EventSet LoadEventSet()
        {
            // return value
            EventSet eventData;

            // deserialize information
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            try
            {
                using (Stream stream = store.OpenFile(_url, FileMode.Open))
                {
                    XmlReader reader = XmlReader.Create(stream);
                    eventData = (EventSet)_serializer.Deserialize(reader);
                    reader.Close();
                }
            }
            catch
            {
                // failed reading cached file
                // assuming this is first launch : return fake data
                eventData = null;
            }

            // return weather info
            return eventData;
        }

        public static void SaveEventSet(EventSet eventData)
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();

            // create a new file (overwrite existing)
            using (IsolatedStorageFileStream file = store.CreateFile(_url))
            {
                _serializer.Serialize(file, eventData);
            }
        }
        #endregion

        #region Save and Load event methods
        public static bool SaveEvent(Event temp)
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();

            try
            {
                // create a new file (overwrite existing)
                using (IsolatedStorageFileStream file = store.CreateFile(_temp))
                {
                    _tempSerializer.Serialize(file, temp);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Loads a single event data needed for passing event object across pages
        /// </summary>
        /// <returns></returns>
        public static Event LoadEvent()
        {
            // return value
            Event temp;

            // deserialize weather information
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            try
            {
                using (Stream stream = store.OpenFile(_temp, FileMode.Open))
                {
                    XmlReader reader = XmlReader.Create(stream);
                    temp = (Event)_tempSerializer.Deserialize(reader);
                    reader.Close();
                }
            }
            catch
            {
                // failed reading cached file
                // assuming this is first launch : return fake data
                temp = null;
            }

            // return weather info
            return temp;
        }
        #endregion

        #region EventCategory
        public static EventCategory LoadEventCategory()
        {
            // return value
            EventCategory temp;

            // deserialize weather information
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            try
            {
                using (Stream stream = store.OpenFile(_eventCategoryTemp, FileMode.Open))
                {
                    XmlReader reader = XmlReader.Create(stream);
                    temp = (EventCategory)_eventCategorySerializer.Deserialize(reader);
                    reader.Close();
                }
            }
            catch
            {
                // failed reading cached file
                // assuming this is first launch : return fake data
                temp = null;
            }

            // return weather info
            return temp;
        }

        public static bool SaveEventCategory(EventCategory eCategory)
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            
            try
            {
                // create a new file (overwrite existing)
                using (IsolatedStorageFileStream file = store.CreateFile(_eventCategoryTemp))
                {
                    _eventCategorySerializer.Serialize(file, eCategory);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region EventCategories (list)
        public static List<EventCategory> LoadEventCategories()
        {
            // return value
            List<EventCategory> temp;

            // deserialize information
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();
            try
            {
                using (Stream stream = store.OpenFile(_eventCategoriesTemp, FileMode.Open))
                {
                    XmlReader reader = XmlReader.Create(stream);
                    temp = (List<EventCategory>)_eventCategoriesSerializer.Deserialize(reader);
                    reader.Close();
                }
            }
            catch
            {
                // failed reading cached file
                // assuming this is first launch : return null
                temp = null;
            }

            // return info
            return temp;
        }

        public static bool SaveEventCategories(List<EventCategory> eCategories)
        {
            IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication();

            try
            {
                // create a new file (overwrite existing)
                using (IsolatedStorageFileStream file = store.CreateFile(_eventCategoriesTemp))
                {
                    _eventCategoriesSerializer.Serialize(file, eCategories);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

    }
}
