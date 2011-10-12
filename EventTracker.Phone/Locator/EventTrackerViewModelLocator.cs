/*
  In App.xaml:
  <Application.Resources>
      <vm:EventTrackerViewModelLocator xmlns:vm="clr-namespace:EventTracker.Phone.Locator"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using EventTracker.Phone.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace EventTracker.Phone.Locator
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    /// to this locator.
    /// </para>
    /// <para>
    /// In Silverlight and WPF, place the EventTrackerViewModelLocator in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:EventTrackerViewModelLocator xmlns:vm="clr-namespace:EventTracker.Phone.Locator"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// You can also use Blend to do all this with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class EventTrackerViewModelLocator
    {
        static IEventTrakcerService _service;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the EventTrackerViewModelLocator class.
        /// </summary>
        public EventTrackerViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view models
                _service = new FakeHttpService();
            }
            else
            {
                _service = new EventTrakcerService();
            }

            RegisterMessageCallbacks();
        }
        #endregion

        private void RegisterMessageCallbacks()
        {
            #region Dispose Events page view model (EventsViewModel)
            Messenger.Default.Register<bool>(this, "DisposeEventsViewModel", bKeyPressed =>
            {
                if (bKeyPressed)
                {
                    ClearEventsViewModelPropertyName();
                }
            });
            #endregion

            #region Dispose Event detail page view model (EventDetailviewModel)
            Messenger.Default.Register<bool>(this, "DisposeEventDetailsViewModel", bKeyPressed =>
            {
                if (bKeyPressed)
                {
                    ClearEventDetailViewModelPropertyName();
                }
            });
            #endregion

            #region  Dispose Map page view model (MapViewModel)
            Messenger.Default.Register<bool>(this, "DisposeEventMapViewModel", bKeyPressed =>
            {
                if (bKeyPressed)
                {
                    ClearMapViewModelPropertyName();
                }
            });
            #endregion
        }

        #region EventsViewModel
        private static EventsViewModel _eventsViewModel;

        /// <summary>
        /// Gets the EventsViewModelPropertyName property.
        /// </summary>
        public static EventsViewModel EventsViewModelPropertyNameStatic
        {
            get
            {
                if (_eventsViewModel == null)
                    CreateEventsViewModelPropertyName();

                return _eventsViewModel;
            }
        }

        /// <summary>
        /// Gets the EventsViewModelPropertyName property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EventsViewModel EventsViewModelPropertyName
        {
            get
            {
                return EventsViewModelPropertyNameStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the EventsViewModelPropertyName property.
        /// </summary>
        public static void ClearEventsViewModelPropertyName()
        {
            _eventsViewModel.Cleanup();
            _eventsViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the EventsViewModelPropertyName property.
        /// </summary>
        public static void CreateEventsViewModelPropertyName()
        {
            if (_eventsViewModel == null)
                _eventsViewModel = new EventsViewModel(_service);
        }

        #endregion

        #region MainViewModel
        private static MainViewModel _mainViewModel;

        /// <summary>
        /// Gets the MainViewModelPropertyName property.
        /// </summary>
        public static MainViewModel MainViewModelPropertyNameStatic
        {
            get
            {
                //if (_mainViewModel == null)
                    CreateMainViewModelPropertyName();

                return _mainViewModel;
            }
        }

        /// <summary>
        /// Gets the MainViewModelPropertyName property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel MainViewModelPropertyName
        {
            get
            {
                return MainViewModelPropertyNameStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the MainViewModelPropertyName property.
        /// </summary>
        public static void ClearMainViewModelPropertyName()
        {
            _mainViewModel.Cleanup();
            _mainViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the MainViewModelPropertyName property.
        /// </summary>
        public static void CreateMainViewModelPropertyName()
        {
            //if (_mainViewModel == null)
                _mainViewModel = new MainViewModel(_service);
        }

        #endregion

        #region EventDetailViewModel
        private static EventDetailViewModel _eventDetailViewModel;

        /// <summary>
        /// Gets the EventDetailViewModelPropertyName property.
        /// </summary>
        public static EventDetailViewModel EventDetailViewModelPropertyNameStatic
        {
            get
            {
                if (_eventDetailViewModel == null)
                    CreateEventDetailViewModelPropertyName();

                return _eventDetailViewModel;
            }
        }

        /// <summary>
        /// Gets the EventDetailViewModelPropertyName property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EventDetailViewModel EventDetailViewModelPropertyName
        {
            get
            {
                return EventDetailViewModelPropertyNameStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the EventDetailViewModelPropertyName property.
        /// </summary>
        public static void ClearEventDetailViewModelPropertyName()
        {
            _eventDetailViewModel.Cleanup();
            _eventDetailViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the EventDetailViewModelPropertyName property.
        /// </summary>
        public static void CreateEventDetailViewModelPropertyName()
        {
            if (_eventDetailViewModel == null)
                _eventDetailViewModel = new EventDetailViewModel();
        }

        #endregion

        #region MapViewModel
        private static MapViewModel _mapViewModel;

        /// <summary>
        /// Gets the MapViewModelPropertyName property.
        /// </summary>
        public static MapViewModel MapViewModelPropertyNameStatic
        {
            get
            {
                ClearMapViewModelPropertyName();
                CreateMapViewModelPropertyName();

                return _mapViewModel;
            }
        }

        /// <summary>
        /// Gets the MapViewModelPropertyName property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MapViewModel MapViewModelPropertyName
        {
            get
            {
                return MapViewModelPropertyNameStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the MapViewModelPropertyName property.
        /// </summary>
        public static void ClearMapViewModelPropertyName()
        {
            if (_mapViewModel != null)
            {
                _mapViewModel.Cleanup();
                _mapViewModel = null;
            }
        }

        /// <summary>
        /// Provides a deterministic way to create the MapViewModelPropertyName property.
        /// </summary>
        public static void CreateMapViewModelPropertyName()
        {
            _mapViewModel = new MapViewModel();
        }
        #endregion

        #region GoToSearchViewModel
        private static GoToSearchViewModel _goToSearchViewModelPropertyName;

        /// <summary>
        /// Gets the GoToSearchViewModelPropertyName property.
        /// </summary>
        public static GoToSearchViewModel GoToSearchViewModelPropertyNameStatic
        {
            get
            {
                if (_goToSearchViewModelPropertyName == null)
                {
                    CreateGoToSearchViewModelPropertyName();
                }

                return _goToSearchViewModelPropertyName;
            }
        }

        /// <summary>
        /// Gets the GoToSearchViewModelPropertyName property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public GoToSearchViewModel GoToSearchViewModelPropertyName
        {
            get
            {
                return GoToSearchViewModelPropertyNameStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the GoToSearchViewModelPropertyName property.
        /// </summary>
        public static void ClearGoToSearchViewModelPropertyName()
        {
            _goToSearchViewModelPropertyName.Cleanup();
            _goToSearchViewModelPropertyName = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the GoToSearchViewModelPropertyName property.
        /// </summary>
        public static void CreateGoToSearchViewModelPropertyName()
        {
            if (_goToSearchViewModelPropertyName == null)
            {
                _goToSearchViewModelPropertyName = new GoToSearchViewModel();
            }
        }
        #endregion

        #region SearchViewModel
        private static SearchViewModel _searchModelPropertyName;

        /// <summary>
        /// Gets the SearchViewModelPropertyName property.
        /// </summary>
        public static SearchViewModel SearchViewModelPropertyNameStatic
        {
            get
            {
                if (_searchModelPropertyName == null)
                {
                    CreateSearchViewModelPropertyName();
                }

                return _searchModelPropertyName;
            }
        }

        /// <summary>
        /// Gets the SearchViewModelPropertyName property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SearchViewModel SearchViewModelPropertyName
        {
            get
            {
                return SearchViewModelPropertyNameStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the SearchViewModelPropertyName property.
        /// </summary>
        public static void ClearSearchViewModelPropertyName()
        {
            _searchModelPropertyName.Cleanup();
            _searchModelPropertyName = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the SearchViewModelPropertyName property.
        /// </summary>
        public static void CreateSearchViewModelPropertyName()
        {
            if (_searchModelPropertyName == null)
            {
                _searchModelPropertyName = new SearchViewModel();
            }
        }

        #endregion

        #region SearchResultViewModel
        private static EventItemViewModel _searchResultViewModelPropertyName;

        /// <summary>
        /// Gets the SearchResultViewModelPropertyName property.
        /// </summary>
        public static EventItemViewModel SearchResultViewModelPropertyNameStatic
        {
            get
            {
                if (_searchResultViewModelPropertyName == null)
                {
                    CreateSearchResultViewModelPropertyName();
                }

                return _searchResultViewModelPropertyName;
            }
        }

        /// <summary>
        /// Gets the SearchResultViewModelPropertyName property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public EventItemViewModel SearchResultViewModelPropertyName
        {
            get
            {
                return SearchResultViewModelPropertyNameStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the SearchResultViewModelPropertyName property.
        /// </summary>
        public static void ClearSearchResultViewModelPropertyName()
        {
            _searchResultViewModelPropertyName.Cleanup();
            _searchResultViewModelPropertyName = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the SearchResultViewModelPropertyName property.
        /// </summary>
        public static void CreateSearchResultViewModelPropertyName()
        {
            if (_searchResultViewModelPropertyName == null)
            {
                _searchResultViewModelPropertyName = new EventItemViewModel(_service);
            }
        }

        #endregion

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            ClearEventsViewModelPropertyName();
            ClearMainViewModelPropertyName();
            ClearEventDetailViewModelPropertyName();
            ClearMapViewModelPropertyName();
        }
    }
}