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
using System.ComponentModel;
using EventTracker.Phone.Services;
using EventTracker.Phone.Api;
using GalaSoft.MvvmLight.Command;
using EventTracker.Phone.Storage;
using EventTracker.Phone.Services.GPS;

namespace EventTracker.Phone
{
    public class FeaturedEventViewModel : EventTrackerViewModelBase
    {
        private IEventTrakcerService _service;
        public FeaturedEventViewModel(IEventTrakcerService service)
        {
            _service = service;
            BindEventSelectionChanged();

            this.PageTitle = "featured";
            this.LoadFeaturedEvents();
        }

        #region Featured events
        /// <summary>
        /// The <see cref="FeaturedEvents" /> property's name.
        /// </summary>
        public const string FeaturedEventsPropertyName = "FeaturedEvents";

        private ObservableCollection<Event> _featuredEvents = null;

        /// <summary>
        /// Gets the FeaturedEvents property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<Event> FeaturedEvents
        {
            get
            {
                return _featuredEvents;
            }

            set
            {
                if (_featuredEvents == value)
                {
                    return;
                }

                _featuredEvents = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(FeaturedEventsPropertyName);
            }
        }
        #endregion

        #region IsFeaturedEventsLoading INotify property
        /// <summary>
        /// The <see cref="IsFeaturedEventsLoading" /> property's name.
        /// </summary>
        public const string IsFeaturedEventsLoadingPropertyName = "IsFeaturedEventsLoading";

        private bool _isFeaturedEventsLoading = false;

        /// <summary>
        /// Gets the IsFeaturedEventsLoading property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool IsFeaturedEventsLoading
        {
            get
            {
                return _isFeaturedEventsLoading;
            }

            set
            {
                if (_isFeaturedEventsLoading == value)
                {
                    return;
                }

                _isFeaturedEventsLoading = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsFeaturedEventsLoadingPropertyName);
            }
        }
        #endregion

        #region Load featured events method
        private void LoadFeaturedEvents()
        {
            this.FeaturedEvents = new ObservableCollection<Event>();

            // Sample data; replace with real data
            if (DesignerProperties.IsInDesignTool || this.IsInDesignMode)
            {
                for (int i = 0; i < 10; i++)
                {
                    this.FeaturedEvents.Add(new Event
                    {
                        Title = "Featured event " + i.ToString(),
                        Country = "Finland" + i.ToString(),
                        CityName = "Helsinki",
                        StartTime = DateTime.Today.ToShortDateString()
                    });
                }
            }
            else
            {
                EventSearchParams searchParams = new EventSearchParams();
                searchParams.Location = string.Format("{0},{1}", AppState.Latitude, AppState.Longitude);
                searchParams.Within = 250;
                searchParams.Date = DateTime.Today.ToShortDateString();
                _service.GetEvents(searchParams, eventSet =>
                {
                    NoEventsFound = false;
                    if (eventSet != null && eventSet.Events.Length != 0)
                    {
                        foreach (var e in eventSet.Events)
                        {
                            FeaturedEvents.Add(e);
                        }
                    }
                    else
                    {
                        NoEventsFound = true;
                    }
                },
                delegate
                {
                    IsFeaturedEventsLoading = true;
                },
                delegate(Exception exception)
                {
                    System.Diagnostics.Debug.WriteLine(exception);
                },
                delegate
                {
                    IsFeaturedEventsLoading = false;
                });
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
