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
using GalaSoft.MvvmLight.Messaging;

namespace EventTracker.Phone.Api
{
    [XmlRoot("event")]
    public class Event : ViewModelBase
    {
        #region
        /// <summary>
        /// The <see cref="Id" /> property's name.
        /// </summary>
        public const string IdPropertyName = "Id";

        private string _id = string.Empty;

        /// <summary>
        /// Gets the Id property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlAttribute(AttributeName="id")]
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                if (_id == value)
                {
                    return;
                }

                _id = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IdPropertyName);
            }
        }
        #endregion

        #region Title
        /// <summary>
        /// The <see cref="Title" /> property's name.
        /// </summary>
        public const string TitlePropertyName = "Title";

        private string _title = string.Empty;

        /// <summary>
        /// Gets the Title property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "title")]
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                if (_title == value)
                    return;

                _title = value;

                //if (_title == HttpUtility.HtmlDecode(value))
                //{
                //    return;
                //}

                //_title = HttpUtility.HtmlDecode(value);

                // Update bindings, no broadcast
                RaisePropertyChanged(TitlePropertyName);
            }
        }
        #endregion

        #region Description
        /// <summary>
        /// The <see cref="Description" /> property's name.
        /// </summary>
        public const string DescriptionPropertyName = "Description";

        private string _description = string.Empty;

        /// <summary>
        /// Gets the Description property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "description")]
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                if (_description == value)
                    return;
                _description = value;

                //if (_description == HttpUtility.HtmlDecode(value))
                //{
                //    return;
                //}

                //_description = HttpUtility.HtmlDecode(value);


                // Update bindings, no broadcast
                RaisePropertyChanged(DescriptionPropertyName);
            }
        }
        #endregion

        #region Url
        /// <summary>
        /// The <see cref="Url" /> property's name.
        /// </summary>
        public const string UrlPropertyName = "Url";

        private string _url = string.Empty;

        /// <summary>
        /// Gets the Url property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "url")]
        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                if (_url == value)
                {
                    return;
                }

                _url = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(UrlPropertyName);
            }
        }
        #endregion

        #region StartTime
        /// <summary>
        /// The <see cref="StartTime" /> property's name.
        /// </summary>
        public const string StartTimePropertyName = "StartTime";

        private string _startTime = string.Empty;

        /// <summary>
        /// Gets the StartTime property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "start_time")]
        public string StartTime
        {
            get
            {
                return _startTime;
            }

            set
            {
                if (_startTime == value)
                {
                    return;
                }

                _startTime = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(StartTimePropertyName);
            }
        }
        #endregion

        #region StopTime
        /// <summary>
        /// The <see cref="StopTime" /> property's name.
        /// </summary>
        public const string StopTimePropertyName = "StopTime";

        private string _stopTime = string.Empty;

        /// <summary>
        /// Gets the StopTime property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "stop_time")]
        public string StopTime
        {
            get
            {
                return _stopTime;
            }

            set
            {
                if (_stopTime == value)
                {
                    return;
                }

                _stopTime = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(StopTimePropertyName);
            }
        }
        #endregion

        #region VenueName
        /// <summary>
        /// The <see cref="VenueName" /> property's name.
        /// </summary>
        public const string VenueNamePropertyName = "VenueName";

        private string _venueName = string.Empty;

        /// <summary>
        /// Gets the VenueName property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "venue_name")]
        public string VenueName
        {
            get
            {
                return _venueName;
            }

            set
            {
                if (_venueName == value)
                {
                    return;
                }

                _venueName = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(VenueNamePropertyName);
            }
        }
        #endregion

        #region PostalCode
        /// <summary>
        /// The <see cref="PostalCode" /> property's name.
        /// </summary>
        public const string PostalCodePropertyName = "PostalCode";

        private string _postalCode = string.Empty;

        /// <summary>
        /// Gets the PostalCode property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "postal_code")]
        public string PostalCode
        {
            get
            {
                return _postalCode;
            }

            set
            {
                if (_postalCode == value)
                {
                    return;
                }

                _postalCode = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(PostalCodePropertyName);
            }
        }
        #endregion

        #region VenueAddress
        /// <summary>
        /// The <see cref="VenueAddress" /> property's name.
        /// </summary>
        public const string VenueAddressPropertyName = "VenueAddress";

        private string _venueAddress = string.Empty;

        /// <summary>
        /// Gets the VenueAddress property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "venue_address")]
        public string VenueAddress
        {
            get
            {
                return _venueAddress;
            }

            set
            {
                if (_venueAddress == value)
                {
                    return;
                }

                _venueAddress = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(VenueAddressPropertyName);
            }
        }
        #endregion

        #region CityName
        /// <summary>
        /// The <see cref="CityName" /> property's name.
        /// </summary>
        public const string CityNamePropertyName = "CityName";

        private string _cityName = string.Empty;

        /// <summary>
        /// Gets the CityName property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "city_name")]
        public string CityName
        {
            get
            {
                return _cityName;
            }

            set
            {
                if (_cityName == value)
                {
                    return;
                }

                _cityName = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CityNamePropertyName);
            }
        }
        #endregion

        #region Country
        /// <summary>
        /// The <see cref="CountryName" /> property's name.
        /// </summary>
        public const string CountryPropertyName = "Country";

        private string _country = string.Empty;

        /// <summary>
        /// Gets the CountryName property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "country_name")]
        public string Country
        {
            get
            {
                return _country;
            }

            set
            {
                if (_country == value)
                {
                    return;
                }

                _country = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CountryPropertyName);
            }
        }
        #endregion

        #region Latitude
        /// <summary>
        /// The <see cref="Latitude" /> property's name.
        /// </summary>
        public const string LatitudePropertyName = "Latitude";

        private string _latitude = string.Empty;

        /// <summary>
        /// Gets the Latitude property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "latitude")]
        public string Latitude
        {
            get
            {
                return _latitude;
            }

            set
            {
                if (_latitude == value)
                {
                    return;
                }

                _latitude = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(LatitudePropertyName);
            }
        }
        #endregion

        #region Longitude
        /// <summary>
        /// The <see cref="Longitude" /> property's name.
        /// </summary>
        public const string LongitudePropertyName = "Longitude";

        private string _longitude = string.Empty;

        /// <summary>
        /// Gets the Longitude property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "longitude")]
        public string Longitude
        {
            get
            {
                return _longitude;
            }

            set
            {
                if (_longitude == value)
                {
                    return;
                }

                _longitude = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(LongitudePropertyName);
            }
        }
        #endregion

        #region Image
        /// <summary>
        /// The <see cref="Image" /> property's name.
        /// </summary>
        public const string ImagePropertyName = "Image";

        private Image _image = null;

        /// <summary>
        /// Gets the Image property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "image")]
        public Image Image
        {
            get
            {
                return _image;
            }

            set
            {
                if (_image == value)
                {
                    return;
                }

                _image = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ImagePropertyName);
            }
        }
        #endregion

        #region Price
        /// <summary>
        /// The <see cref="Price" /> property's name.
        /// </summary>
        public const string PricePropertyName = "Price";

        private string _price = string.Empty;

        /// <summary>
        /// Gets the Price property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement(ElementName = "price")]
        public string Price
        {
            get
            {
                return _price;
            }

            set
            {
                if (_price == value)
                {
                    return;
                }

                _price = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(PricePropertyName);
            }
        }
        #endregion

        #region Recuring string
        /// <summary>
        /// The <see cref="RecurString" /> property's name.
        /// </summary>
        public const string RecurStringPropertyName = "RecurString";

        private string _recurString = string.Empty;

        /// <summary>
        /// Gets the RecurString property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string RecurString
        {
            get
            {
                return _recurString;
            }

            set
            {
                if (_recurString == value)
                {
                    return;
                }

                _recurString = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(RecurStringPropertyName);
            }
        }
        #endregion

        public bool StopTimeIsAvailable
        {
            get
            {
                if (!string.IsNullOrEmpty(this.StopTime))
                {
                    return true;
                }

                return false;
            }
        }

        public bool PriceIsAvailable
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Price))
                    return true;

                return false;
            }
        }

        public bool RecurStringIsAvailable
        {
            get
            {
                if (!string.IsNullOrEmpty(RecurString))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
