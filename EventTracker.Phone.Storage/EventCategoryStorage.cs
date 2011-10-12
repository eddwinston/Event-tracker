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
using System.IO.IsolatedStorage;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace EventTracker.Phone.Storage
{
    public class EventCategoryStorage : IStorage<EventCategory>
    {
        private const string _eventCategoryTemp = @"\eventCategoryTemp.xml";

        private XmlSerializer _eventCategorySerializer = new XmlSerializer(typeof(EventCategory));


        #region Load
        public EventCategory Load()
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
        #endregion

        #region Save
        public bool Save(EventCategory eCategory)
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
    }
}
