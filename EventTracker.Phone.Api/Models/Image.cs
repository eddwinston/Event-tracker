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
    #region ImageBase
    public class ImageBase : ViewModelBase
    {
        public ImageBase()
        {
            _url = "/Assets/NoImage.png";
        }

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
        [XmlElement("url")]
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

        public bool IsLocal
        {
            get
            {
                if (_url.StartsWith("http:") || _url.StartsWith("https:"))
                    return false;
                return true;
            }
        }
    }
    #endregion

    [XmlRoot("small")]
    public class Small : ImageBase { }

    [XmlRoot("thumb")]
    public class Thumbnail : ImageBase { }

    [XmlRoot("medium")]
    public class Medium : ImageBase { }

    [XmlRoot("image")]
    public class Image : ImageBase
    {
        public Image()
        {
            Small = new Small();
            Thumbnail = new Thumbnail();
            Medium = new Medium();
        }

        #region Small
        /// <summary>
        /// The <see cref="Small" /> property's name.
        /// </summary>
        public const string SmallPropertyName = "Small";

        private Small _small = null;

        /// <summary>
        /// Gets the Small property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement("small", Type = typeof(Small))]
        public Small Small
        {
            get
            {
                return _small;
            }

            set
            {
                if (_small == value)
                {
                    return;
                }

                _small = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(SmallPropertyName);
            }
        }
        #endregion

        #region Thumbnail
        /// <summary>
        /// The <see cref="Thumbnail" /> property's name.
        /// </summary>
        public const string ThumbnailPropertyName = "Thumbnail";

        private Thumbnail _thumbnail = null;

        /// <summary>
        /// Gets the Thumbnail property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement("thumb")]

        public Thumbnail Thumbnail
        {
            get
            {
                return _thumbnail;
            }

            set
            {
                if (_thumbnail == value)
                {
                    return;
                }

                _thumbnail = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ThumbnailPropertyName);
            }
        }
        #endregion

        #region Medium
        /// <summary>
        /// The <see cref="Medium" /> property's name.
        /// </summary>
        public const string MediumPropertyName = "Medium";

        private Medium _medium = null;

        /// <summary>
        /// Gets the Medium property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        [XmlElement("medium")]
        public Medium Medium
        {
            get
            {
                return _medium;
            }

            set
            {
                if (_medium == value)
                {
                    return;
                }

                _medium = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(MediumPropertyName);
            }
        }
        #endregion

    }
}
