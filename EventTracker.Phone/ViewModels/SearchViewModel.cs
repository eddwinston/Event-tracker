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
using System.Collections.Generic;
using EventTracker.Phone.Api;
using GalaSoft.MvvmLight.Command;
using EventTracker.Phone.Services;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;

namespace EventTracker.Phone
{
    public class SearchViewModel : EventTrackerViewModelBase
    {
        public SearchViewModel()
        {
            PageTitle = "search";

            EventCategories = new List<EventCategory>();

            PopulateCategories();

            BindSearchCommand();

            Messenger.Default.Register<EventCategory>(this, "CategoryPickerSelectionChanged", eCategory =>
            {
                if (eCategory != null)
                    SelectedCategory = eCategory;
            });
        }

        #region Keyword
        /// <summary>
        /// The <see cref="Keyword" /> property's name.
        /// </summary>
        public const string KeywordPropertyName = "Keyword";

        private string _keyword = string.Empty;

        /// <summary>
        /// Gets the Keyword property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string Keyword
        {
            get
            {
                return _keyword;
            }

            set
            {
                if (_keyword == value)
                {
                    return;
                }

                _keyword = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(KeywordPropertyName);
            }
        }
        #endregion
        
        #region EventCategories
        /// <summary>
        /// The <see cref="EventCategories" /> property's name.
        /// </summary>
        public const string EventCategoriesPropertyName = "EventCategories";

        private List<EventCategory> _eventCategories = null;

        /// <summary>
        /// Gets the EventCategories property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public List<EventCategory> EventCategories
        {
            get
            {
                return _eventCategories;
            }

            set
            {
                if (_eventCategories == value)
                {
                    return;
                }

                _eventCategories = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(EventCategoriesPropertyName);
            }
        }
        #endregion

        #region SelectedCategory
        /// <summary>
        /// The <see cref="SelectedCategory" /> property's name.
        /// </summary>
        public const string SelectedCategoryPropertyName = "SelectedCategory";

        private EventCategory _selectedCategory = null;

        /// <summary>
        /// Gets the SelectedCategory property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public EventCategory SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }

            set
            {
                if (_selectedCategory == value)
                {
                    return;
                }

                _selectedCategory = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SelectedCategoryPropertyName);
            }
        }
        #endregion

        #region Location
        /// <summary>
        /// The <see cref="Location" /> property's name.
        /// </summary>
        public const string LocationPropertyName = "Location";

        private string _location = string.Empty;

        /// <summary>
        /// Gets the Location property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string Location
        {
            get
            {
                return _location;
            }

            set
            {
                if (_location == value)
                {
                    return;
                }

                _location = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(LocationPropertyName);
            }
        }
        #endregion

        #region Event date
        /// <summary>
        /// The <see cref="EventDate" /> property's name.
        /// </summary>
        public const string EventDatePropertyName = "EventDate";

        private DateTime _eventDate = DateTime.Now;

        /// <summary>
        /// Gets the EventDate property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public DateTime EventDate
        {
            get
            {
                return _eventDate;
            }

            set
            {
                if (_eventDate == value)
                {
                    return;
                }

                _eventDate = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(EventDatePropertyName);
            }
        }
        #endregion

        #region Command
        public RelayCommand SearchCommand
        {
            get;
            private set;
        }
        #endregion

        #region Helpers
        private void BindSearchCommand()
        {
            this.SearchCommand = new RelayCommand(() =>
            {
                GoToResult();
            }, () =>
            {
                return true;
            });
        }

        private void PopulateCategories()
        {
            if (IsInDesignMode)
            {
                var eCategories = new List<EventCategory>
                {
                    new EventCategory { Id="cat1", Name="Category one"},
                    new EventCategory { Id="cat2", Name="Category two"},
                    new EventCategory { Id="cat3", Name="Category three"},
                    new EventCategory { Id="cat4", Name="Category four"},
                    new EventCategory { Id="cat5", Name="Category five"},
                    new EventCategory { Id="cat6", Name="Category six"},
                    new EventCategory { Id="cat7", Name="Category seven"},
                    new EventCategory { Id="cat8", Name="Category eight"},
                    new EventCategory { Id="cat9", Name="Category nine"},
                    new EventCategory { Id="cat10", Name="Category ten"}
                };

                foreach (var cat in eCategories)
                    EventCategories.Add(cat);
            }
            else
            {
                var eCategories = EventTrackerStorage.LoadEventCategories();
                EventCategories.Clear();
                EventCategories.Add(new EventCategory { Id = "All", Name = "All" });
                foreach (var cat in eCategories)
                    EventCategories.Add(cat);
            }
        }

        private void GoToResult()
        {
            var url = string.Format(
                "/Views/SearchResultPage.xaml?keyword={0}&category={1}&location={2}&date={3}",
                Keyword, SelectedCategory, Location, EventDate.ToShortDateString());

            SendNavigationRequestMessage(new Uri(url, UriKind.Relative));
        }

        //private void GetEvents(string date, string categoryId)
        //{
        //    Events = new ObservableCollection<Event>();

        //    EventSearchParams searchParams = new EventSearchParams();
        //    searchParams.Category = categoryId;
        //    searchParams.Date = date;
        //    searchParams.Location = string.Format("{0},{1}", AppState.Latitude, AppState.Longitude);
        //    this._service.GetEvents(searchParams, eventSet =>
        //    {
        //        NoEventsFound = false;
        //        if (eventSet != null && eventSet.Events.Length != 0)
        //        {
        //            foreach (var e in eventSet.Events)
        //            {
        //                Events.Add(e);
        //            }
        //            IsDataLoaded = true;
        //        }
        //        else
        //        {
        //            NoEventsFound = true;
        //        }
        //    },
        //    delegate()
        //    {
        //        IsEventsLoading = true;
        //    },
        //    delegate(Exception exception)
        //    {
        //        System.Diagnostics.Debug.WriteLine(exception);
        //    },
        //    delegate()
        //    {
        //        IsEventsLoading = false;
        //    });
        //}
        #endregion

    }
}
