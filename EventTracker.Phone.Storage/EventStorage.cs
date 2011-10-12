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
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml;

namespace EventTracker.Phone.Storage
{
    public class EventStorage : IStorage<Event>
    {
        private const string _temp = @"\temp.xml";
        private XmlSerializer _tempSerializer = new XmlSerializer(typeof(Event));
        private IsolatedStorageFile _store = IsolatedStorageFile.GetUserStoreForApplication();

        #region Save
        public bool Save(Event temp)
        {
            
            try
            {
                // create a new file (overwrite existing)
                using (IsolatedStorageFileStream file = _store.CreateFile(_temp))
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
        #endregion

        #region Load
        /// <summary>
        /// Loads a single event data needed for passing event object across pages
        /// </summary>
        /// <returns></returns>
        public Event Load()
        {
            // return value
            Event temp;

            // deserialize weather information
            try
            {
                using (Stream stream = _store.OpenFile(_temp, FileMode.Open))
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
