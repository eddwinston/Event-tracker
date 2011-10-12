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
using GalaSoft.MvvmLight.Messaging;
using EventTracker.Phone.Services;

namespace EventTracker.Phone
{
    public class EventsViewModel : EventTrackerViewModelBase
    {
        private string[] _dates = new string[] { "today", "tomorrow", "weekend", "next week" };

        private IEventTrakcerService _service;
        public EventsViewModel(IEventTrakcerService service)
        {
            _service = service;
            EventsCollectionViewModel = new ObservableCollection<EventItemViewModel>();
            Messenger.Default.Register<string>(this, "CategoryId", MessageReceiver);

            if (IsInDesignMode)
                MessageReceiver("category1");
        }

        public void MessageReceiver(string categoryId)
        {
            EventsCollectionViewModel.Clear();
            foreach (var date in _dates)
            {
                EventItemViewModel item = new EventItemViewModel(date, categoryId, _service);
                item.PageTitle = date;
                try
                {
                    EventsCollectionViewModel.Add(item);
                }
                catch { }
            }

            Messenger.Default.Unregister<string>(this, new Action<string>(MessageReceiver));
        }

        #region EventsItemViewModel
        /// <summary>
        /// The <see cref="EventsCollectionViewModel" /> property's name.
        /// </summary>
        public const string EventsItemViewModelPropertyName = "EventsCollectionViewModel";

        private ObservableCollection<EventItemViewModel> _eventsCollectionViewModel = null;

        /// <summary>
        /// Gets the EventsItemViewModel property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<EventItemViewModel> EventsCollectionViewModel
        {
            get
            {
                return _eventsCollectionViewModel;
            }

            set
            {
                if (_eventsCollectionViewModel == value)
                {
                    return;
                }

                _eventsCollectionViewModel = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(EventsItemViewModelPropertyName);
            }
        }
        #endregion

        #region CategoryId
        /// <summary>
        /// The <see cref="CategoryId" /> property's name.
        /// </summary>
        public const string CategoryIdPropertyName = "CategoryId";

        private string _categoryId = string.Empty;

        /// <summary>
        /// Gets the CategoryId property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string CategoryId
        {
            get
            {
                return _categoryId;
            }

            set
            {
                if (_categoryId == value)
                {
                    return;
                }

                _categoryId = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CategoryIdPropertyName);
            }
        }
        #endregion

    }
}
