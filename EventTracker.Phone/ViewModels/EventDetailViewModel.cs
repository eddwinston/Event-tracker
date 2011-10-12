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
using EventTracker.Phone.Storage;
using EventTracker.Phone.Api;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Shell;

namespace EventTracker.Phone
{
    public class EventDetailViewModel : EventTrackerViewModelBase
    {
        private ApplicationBar _appBar;
        private ApplicationBarIconButton _saveBtn;
        private ApplicationBarIconButton _deleteBtn;
        UserEventsDb _db = new UserEventsDb();

        public EventDetailViewModel()
            : base()
        {
            this.PageTitle = "detail";
            Event = EventTrackerStorage.LoadEvent();

            InMyEvents = _db.EventExist(Event);

            SetUpDefaultAppBar();
        }

        #region Event
        /// <summary>
        /// The <see cref="Event" /> property's name.
        /// </summary>
        public const string EventPropertyName = "Event";

        private Event _event = null;

        /// <summary>
        /// Gets the Event property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public Event Event
        {
            get
            {
                return _event;
            }

            set
            {
                if (_event == value)
                {
                    return;
                }

                _event = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(EventPropertyName);
            }
        }
        #endregion

        #region AddressAvailable
        /// <summary>
        /// Gets the AddressAvailable property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool AddressAvailable
        {
            get
            {
                if (!string.IsNullOrEmpty(Event.VenueAddress))
                {
                    return true;
                }

                return false;
            }
        }
        #endregion

        #region PostalCodeAvailable
        /// <summary>
        /// Gets the PostalCodeAvailable property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool PostalCodeAvailable
        {
            get
            {
                if (AddressAvailable && !string.IsNullOrEmpty(Event.PostalCode))
                    return true;

                return false;
            }
        }
        #endregion

        #region StopTimeAvailable
        public bool StopTimeAvailable
        {
            get
            {
                if (!string.IsNullOrEmpty(Event.StopTime))
                    return true;

                return false;
            }
        }
        #endregion

        #region InMyEvents
        /// <summary>
        /// The <see cref="InMyEvents" /> property's name.
        /// </summary>
        public const string InMyEventsPropertyName = "InMyEvents";

        private bool _inMyEvents = false;

        /// <summary>
        /// Gets the InMyEvents property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool InMyEvents
        {
            get
            {
                return _inMyEvents;
            }

            set
            {
                if (_inMyEvents == value)
                {
                    return;
                }

                _inMyEvents = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(InMyEventsPropertyName);
            }
        }
        #endregion

        private void SetUpDefaultAppBar()
        {
            _appBar = new ApplicationBar();

            _appBar.IsVisible = true;
            _appBar.IsMenuEnabled = true;
            _appBar.Opacity = 0.5;
            _appBar.ForegroundColor = Color.FromArgb(255, 255, 255, 255);
            _appBar.BackgroundColor = Color.FromArgb(255, 0, 0, 0);

            _saveBtn = new ApplicationBarIconButton(new Uri("/Resources/Icons/ApplicationBar/appbar.save.rest.png", UriKind.Relative));
            _saveBtn.Text = "Save";
            _saveBtn.Click += new EventHandler(saveBtn_Click);

            var mapBtn = new ApplicationBarIconButton(new Uri("/Resources/Icons/ApplicationBar/Location.png", UriKind.Relative));
            mapBtn.Text = "Locate";
            mapBtn.Click += new EventHandler(mapBtn_Click);

            _deleteBtn = new ApplicationBarIconButton(new Uri("/Resources/Icons/ApplicationBar/appbar.close.rest.png", UriKind.Relative));
            _deleteBtn.Text = "Delete";
            _deleteBtn.Click += new EventHandler(_deleteBtn_Click);

            if (InMyEvents)
                _appBar.Buttons.Add(_deleteBtn);
            else
                _appBar.Buttons.Add(_saveBtn);

            _appBar.Buttons.Add(mapBtn);

            this.DefaultAppBar = _appBar;
        }

        protected void mapBtn_Click(object sender, EventArgs e)
        {
            GoToMapView();
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Add this events to your events collections?", "Save?", MessageBoxButton.OKCancel);
            if (messageBoxResult == MessageBoxResult.OK || messageBoxResult == MessageBoxResult.Yes)
            {
                var b = _db.Insert(Event);
                if (b)
                {
                    _appBar.Buttons[0] = _deleteBtn;
                    SendMyEventListUpdateMessage();
                }
            }
        }

        protected void _deleteBtn_Click(object sender, EventArgs e)
        {
            var messageBoxResult = MessageBox.Show("This event will be deleted from your collection?", "Delete?", MessageBoxButton.OKCancel);
            if (messageBoxResult == MessageBoxResult.OK || messageBoxResult == MessageBoxResult.Yes)
            {
                var b = _db.Delete(Event);

                if (b)
                {
                    _appBar.Buttons[0] = _saveBtn;
                    SendMyEventListUpdateMessage();
                }
            }
        }

        #region Send my events list and upate message after update or delete of an event
        private static void SendMyEventListUpdateMessage()
        {
            Messenger.Default.Send<bool>(true, "UpdateMyEventsAfterSaveOrDelete");
        }
        #endregion

        private void GoToMapView()
        {
            SendNavigationRequestMessage(new Uri("/Views/Map.xaml", UriKind.Relative));
        }
    }
}
