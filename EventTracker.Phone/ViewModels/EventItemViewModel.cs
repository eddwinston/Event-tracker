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
using System.Collections.ObjectModel;
using EventTracker.Phone.Storage;
using GalaSoft.MvvmLight.Messaging;
using EventTracker.Phone.Services;
using GalaSoft.MvvmLight.Command;
using EventTracker.Phone.Api;
using System.Windows.Navigation;

namespace EventTracker.Phone
{
    public class EventItemViewModel : EventTrackerViewModelBase
    {
        private IEventTrakcerService _service;

        public EventItemViewModel(IEventTrakcerService service)
        {
            BindEventSelectionChanged();

            this.PageTitle = "results";
            _service = service;
            Events = new ObservableCollection<Event>();

            IsEventsLoading = true;

            Messenger.Default.Register<EventSearchParams>(this, "SearchParams", esp =>
            {
                GetEvents(esp);
            });

            if (IsInDesignMode)
            {
                GetEvents(new EventSearchParams());
                //for (int i = 0; i < 20; i++)
                //{
                //    Events.Add(new Event
                //    {
                //        Id = string.Format("{0} - {1}", "event", i),
                //        CityName = "City " + i,
                //        Country = "Country-" + i,
                //        Description = "Description-" + i,
                //        Latitude = "3430.0",
                //        Longitude = "89.023",
                //        PostalCode = "783" + i,
                //        Price = i.ToString(),
                //        RecurString = "Recuring String " + i,
                //        StartTime = DateTime.Now.ToLongDateString(),
                //        StopTime = DateTime.Now.AddDays(2).ToLongDateString(),
                //        Title = "Title - " + i,
                //        Url = "http://example.com",
                //        VenueAddress = "Venue Address " + i,
                //        VenueName = "Venue name " + i
                //    });
                //}
                //IsEventsLoading = false;
                //IsDataLoaded = true;
            }
        }

        public EventItemViewModel(string date, string categoryId, IEventTrakcerService service)
            : this(service)
        {
            this.PageTitle = date;
            GetEvents(date, categoryId);
        }

        #region GetEvents method by date and category
        private void GetEvents(string date, string categoryId)
        {
            EventSearchParams searchParams = new EventSearchParams();
            searchParams.Category = categoryId;
            searchParams.Date = date;
            searchParams.Location = string.Format("{0},{1}", AppState.Latitude, AppState.Longitude);
            this._service.GetEvents(searchParams, eventSet =>
            {
                NoEventsFound = false;
                if (eventSet != null && eventSet.Events.Length != 0)
                {
                    foreach (var e in eventSet.Events)
                    {
                        Events.Add(e);
                    }
                    IsDataLoaded = true;
                }
                else
                {
                    NoEventsFound = true;
                }
            },
            delegate()
            {
                IsEventsLoading = true;
            },
            delegate(Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            },
            delegate()
            {
                IsEventsLoading = false;
            });
        }

        private void GetEvents(EventSearchParams esp = null)
        {
            if (esp == null) return;

            Events.Clear();
            this._service.GetEvents(esp, eventSet =>
            {
                NoEventsFound = false;
                if (eventSet != null && eventSet.Events.Length != 0)
                {
                    foreach (var e in eventSet.Events)
                    {
                        Events.Add(e);
                    }
                    IsDataLoaded = true;
                }
                else
                {
                    NoEventsFound = true;
                }
            },
            delegate()
            {
                IsEventsLoading = true;
            },
            delegate(Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            },
            delegate()
            {
                IsEventsLoading = false;
            });
        }
        #endregion

        #region Events
        /// <summary>
        /// The <see cref="Events" /> property's name.
        /// </summary>
        public const string EventsPropertyName = "Events";

        private ObservableCollection<Event> _events = null;

        /// <summary>
        /// Gets the Events property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<Event> Events
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

        #region IsDataLoaded
        public bool IsDataLoaded
        {
            get;
            private set;
        }
        #endregion

        #region Category
        /// <summary>
        /// The <see cref="Category" /> property's name.
        /// </summary>
        public const string CategoryPropertyName = "Category";

        private string _category = string.Empty;

        /// <summary>
        /// Gets the Category property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string Category
        {
            get
            {
                return _category;
            }

            set
            {
                if (_category == value)
                {
                    return;
                }

                _category = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CategoryPropertyName);
            }
        }
        #endregion

        #region IsEventsLoading
        /// <summary>
        /// The <see cref="IsEventsLoading" /> property's name.
        /// </summary>
        public const string IsEventsLoadingPropertyName = "IsEventsLoading";

        private bool _isEventsLoading = false;

        /// <summary>
        /// Gets the IsEventsLoading property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool IsEventsLoading
        {
            get
            {
                return _isEventsLoading;
            }

            set
            {
                if (_isEventsLoading == value)
                {
                    return;
                }

                _isEventsLoading = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsEventsLoadingPropertyName);
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

        #region Helpers
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
