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
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;

namespace EventTracker.Phone
{
    public class SearchResultViewModel : EventTrackerViewModelBase
    {
        public SearchResultViewModel()
        {
            PageTitle = "result";

            Events = new List<Event>();
            for (int i = 0; i < 5; i++)
                Events.Add(new Event { Id = i.ToString(), Title = i.ToString() + "--" + i.ToString() });

            IsLoading = true;

            ItemSelectionCommand = new RelayCommand<Event>((e) =>
            {
                //SendNavigationRequestMessage(new Uri(
            }, (e) =>
            {
                return true;
            });
        }

        #region Events
        /// <summary>
        /// The <see cref="Events" /> property's name.
        /// </summary>
        public const string EventsPropertyName = "Events";

        private List<Event> _events = null;

        /// <summary>
        /// Gets the Events property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public List<Event> Events
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

        #region Command
        public RelayCommand<Event> ItemSelectionCommand
        {
            get;
            private set;
        }
        #endregion
    }
}
