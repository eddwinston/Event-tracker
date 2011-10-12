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
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Shell;

namespace EventTracker.Phone
{
    public class EventTrackerViewModelBase : ViewModelBase
    {
        public EventTrackerViewModelBase()
        {
        }       

        #region Title
        /// <summary>
        /// The <see cref="PageTitle" /> property's name.
        /// </summary>
        public const string PageTitlePropertyName = "PageTitle";

        private string _pageTitle = string.Empty;

        /// <summary>
        /// Gets the PageTitle property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }

            set
            {
                if (_pageTitle == value)
                {
                    return;
                }

                _pageTitle = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(PageTitlePropertyName);
            }
        }
        #endregion

        #region DefaultAppBar
        private ApplicationBar _defaultAppBar;
        public ApplicationBar DefaultAppBar
        {
            get
            {
                return _defaultAppBar;
            }
            set
            {
                if (_defaultAppBar != value)
                {
                    _defaultAppBar = value;
                    RaisePropertyChanged("DefaultAppBar");
                }
            }
        }
        #endregion

        #region ShowAppBar
        /// <summary>
        /// The <see cref="ShowAppBar" /> property's name.
        /// </summary>
        public const string ShowAppBarPropertyName = "ShowAppBar";

        private bool _showAppBar = false;

        /// <summary>
        /// Gets the ShowAppBar property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool ShowAppBar
        {
            get
            {
                return _showAppBar;
            }

            set
            {
                if (_showAppBar == value)
                {
                    return;
                }

                _showAppBar = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(ShowAppBarPropertyName);
            }
        }
        #endregion

        protected void SendNavigationRequestMessage(Uri uri)
        {
            Messenger.Default.Send<Uri>(uri, "NavigationRequest");
        }

        protected void NavigatedToAnimation()
        {
            Messenger.Default.Send<object>(null, "NavigatedToAnimation");
        }
    }
}
