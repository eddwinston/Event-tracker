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
using System.Xml.Serialization;
using GalaSoft.MvvmLight;

namespace EventTracker.Phone.Api
{
    [XmlRoot("search")]
    public class EventSet : ViewModelBase
    {
        public EventSet()
        {
            TotalItems = 0;
            Events = null;
        }

        #region TotalItems
        /// <summary>
        /// The <see cref="TotalItems" /> property's name.
        /// </summary>
        public const string TotalItemsPropertyName = "TotalItems";

        private int _totalItems = 0;

        /// <summary>
        /// Gets the TotalItems property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "total_items")]

        public int TotalItems
        {
            get
            {
                return _totalItems;
            }

            set
            {
                if (_totalItems == value)
                {
                    return;
                }

                _totalItems = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(TotalItemsPropertyName);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// The <see cref="Events" /> property's name.
        /// </summary>
        public const string EventsPropertyName = "Events";

        private Event[] _events = null;

        /// <summary>
        /// Gets the Events property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlArray("events")]

        public Event[] Events
        {
            get
            {
                return _events;
            }

            set
            {
                if (_events == value)
                {
                    return;
                }

                _events = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(EventsPropertyName);
            }
        }
        #endregion
    }
}
