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
using System.Collections.ObjectModel;
using EventTracker.Phone.Storage;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace EventTracker.Phone
{
    public class MyEventsViewModel : EventTrackerViewModelBase
    {
        public MyEventsViewModel()
        {
            this.PageTitle = "bookmarks";

            BindEventSelectionChanged();

            FetchUserEvents();

            Messenger.Default.Register<bool>(this, "UpdateMyEventsAfterSaveOrDelete", update =>
            {
                if (update)
                {
                    FetchUserEvents();
                }
            });
        }

        #region All events
        /// <summary>
        /// The <see cref="AllEvents" /> property's name.
        /// </summary>
        public const string AllEventsPropertyName = "AllEvents";

        private ObservableCollection<Event> _allEvents = new ObservableCollection<Event>();

        /// <summary>
        /// Gets the AllEvents property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<Event> AllEvents
        {
            get
            {
                return _allEvents;
            }

            set
            {
                if (_allEvents == value)
                {
                    return;
                }

                _allEvents = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(AllEventsPropertyName);
            }
        }
        #endregion

        #region IsLoading
        /// <summary>
        /// The <see cref="IsLoading" /> property's name.
        /// </summary>
        public const string IsLoadingPropertyName = "IsLoading";

        private bool _isLoading = false;

        /// <summary>
        /// Gets the IsLoading property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                if (_isLoading == value)
                {
                    return;
                }

                _isLoading = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsLoadingPropertyName);
            }
        }
        #endregion

        #region NoEventsFound
        /// <summary>
        /// The <see cref="NoEventsFound" /> property's name.
        /// </summary>
        public const string NoEventsFoundPropertyName = "NoEventsFound";

        private bool _noEventsFound = false;

        /// <summary>
        /// Gets the NoEventsFound property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool NoEventsFound
        {
            get
            {
                return _noEventsFound;
            }

            set
            {
                if (_noEventsFound == value)
                {
                    return;
                }

                _noEventsFound = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(NoEventsFoundPropertyName);
            }
        }
        #endregion

        #region Commands
        public RelayCommand<Event> EventSelectionChangedCommand
        {
            get;
            private set;
        }
        #endregion

        #region Helper
        private void FetchUserEvents()
        {
            AllEvents.Clear();

            var db = new UserEventsDb();
            var lst = db.FetchAll();
            if (lst != null && lst.Count > 0)
            {
                NoEventsFound = false;

                foreach (var e in lst)
                {
                    AllEvents.Add(e);
                }
            }
            else
            {
                NoEventsFound = true;
            }
            IsLoading = false;
        }

        private void BindEventSelectionChanged()
        {
            this.EventSelectionChangedCommand = new RelayCommand<Event>(e =>
            {
                if (e != null)
                {
                    if (EventTrackerStorage.SaveEvent(e))
                    {
                        this.SendNavigationRequestMessage(
                            new Uri("/Views/EventDetailPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        throw new Exception("Error while navigating to details page");
                        // TODO: redirect to error page
                    }
                }
            });
        }
        #endregion
    }
}
