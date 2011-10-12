using GalaSoft.MvvmLight;
using System;
using System.Device.Location;
using EventTracker.Phone.Api;
using EventTracker.Phone.Storage;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;

namespace EventTracker.Phone
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MapViewModel : EventTrackerViewModelBase
    {
        private const double DefaultZoomLevel = 18.0;
        private const double MaxZoomLevel = 21.0;
        private const double MinZoomLevel = 1.0;

        /// <summary>
        /// Initializes a new instance of the MapViewModel class.
        /// </summary>
        public MapViewModel()
        {
            CredentialsProvider = "Aht9aTfC0vXiGj3AXX8eKB26JfOin8bNpTHav0A-Zm3DzB5anTazAXi4qioAb92G";
            Zoom = DefaultZoomLevel;
            this.PageTitle = "Location";

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                this.Event = new Event
                {
                    Title = "Example title",
                    VenueName = "Example Venue",
                    Latitude = "60.24",
                    Longitude = "78.392"
                };
            }
            else
            {
                // Code runs "for real": Connect to service, etc...
                this.Event = EventTrackerStorage.LoadEvent();
                if (this.Event != null)
                {
                    Center = new GeoCoordinate(
                        double.Parse(Event.Latitude),
                        double.Parse(Event.Longitude));

                    PushPin = new PushpinModel { Location = Center };
                    //PushPins = new ObservableCollection<PushpinModel>();
                    //PushPins.Add(new PushpinModel { Location = Center });
                }

                ZoomInCommand = new RelayCommand(() =>
                {
                    Zoom += 1;
                });

                ZoomOutCommand = new RelayCommand(() =>
                {
                    Zoom -= 1;
                });
            }
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

        #region CredentialsProvider
        /// <summary>
        /// The <see cref="CredentialsProvider" /> property's name.
        /// </summary>
        public const string CredentialsProviderPropertyName = "CredentialsProvider";

        private string _credentialsProvider = string.Empty;

        /// <summary>
        /// Gets the CredentialsProvider property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string CredentialsProvider
        {
            get
            {
                return _credentialsProvider;
            }

            private set
            {
                if (_credentialsProvider == value)
                {
                    return;
                }

                _credentialsProvider = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CredentialsProviderPropertyName);
            }
        }
        #endregion

        #region Zoom
        /// <summary>
        /// The <see cref="Zoom" /> property's name.
        /// </summary>
        public const string ZoomPropertyName = "Zoom";

        private double _zoom = 0;

        /// <summary>
        /// Gets the Zoom property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public double Zoom
        {
            get
            {
                return _zoom;
            }

            set
            {
                var coercedZoom = Math.Max(MinZoomLevel, Math.Min(MaxZoomLevel, value));
                if (_zoom == coercedZoom)
                {
                    return;
                }

                _zoom = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ZoomPropertyName);
            }
        }
        #endregion

        #region Center
        /// <summary>
        /// The <see cref="Center" /> property's name.
        /// </summary>
        public const string CenterPropertyName = "Center";

        private GeoCoordinate _center = null;

        /// <summary>
        /// Gets the Center property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public GeoCoordinate Center
        {
            get
            {
                return _center;
            }

            set
            {
                if (_center == value)
                {
                    return;
                }

                _center = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CenterPropertyName);
            }
        }
        #endregion

        #region Pushpin collection
        /// <summary>
        /// The <see cref="PushPins" /> property's name.
        /// </summary>
        public const string PushPinsPropertyName = "PushPins";

        private ObservableCollection<PushpinModel> _pushPins = null;

        /// <summary>
        /// Gets the PushPins property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<PushpinModel> PushPins
        {
            get
            {
                return _pushPins;
            }

            set
            {
                if (_pushPins == value)
                {
                    return;
                }

                _pushPins = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(PushPinsPropertyName);
            }
        }
        #endregion

        #region PushPin
        /// <summary>
        /// The <see cref="PushPin" /> property's name.
        /// </summary>
        public const string PushPinPropertyName = "PushPin";

        private PushpinModel _pushPin = null;

        /// <summary>
        /// Gets the Pushpin property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public PushpinModel PushPin
        {
            get
            {
                return _pushPin;
            }

            set
            {
                if (_pushPin == value)
                {
                    return;
                }

                _pushPin = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(PushPinPropertyName);
            }
        }
        #endregion

        #region Cleanup
        public override void Cleanup()
        {
            base.Cleanup();
        }
        #endregion

        #region Commands
        public RelayCommand ZoomInCommand { get; set; }
        public RelayCommand ZoomOutCommand { get; set; }
        #endregion
    }
}