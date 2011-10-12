using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using EventTracker.Phone.Services;


namespace EventTracker.Phone
{
    public class MainViewModel : EventTrackerViewModelBase
    {
        IEventTrakcerService _service;
        public MainViewModel(IEventTrakcerService service)
        {
            _service = service;
            this.CategoriesViewModel = new EventCategoriesViewModel(_service);
            this.FeaturedEventViewModel = new FeaturedEventViewModel(_service);
            this.MyEventsViewModel = new MyEventsViewModel();
            this.GoToSearchViewModel = new GoToSearchViewModel();
            //NavigatedToAnimation();
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        #region Categories View Model
        /// <summary>
        /// The <see cref="CategoriesViewModel" /> property's name.
        /// </summary>
        public const string CategoriesViewModelPropertyName = "CategoriesViewModel";

        private EventCategoriesViewModel _categoriesViewModel = null;

        /// <summary>
        /// Gets the CategoriesViewModel property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public EventCategoriesViewModel CategoriesViewModel
        {
            get
            {
                return _categoriesViewModel;
            }

            set
            {
                if (_categoriesViewModel == value)
                {
                    return;
                }

                _categoriesViewModel = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CategoriesViewModelPropertyName);
            }
        }
        #endregion

        #region Featured Events View Model
        /// <summary>
        /// The <see cref="FeaturedEventViewModel" /> property's name.
        /// </summary>
        public const string FeaturedEventViewModelPropertyName = "FeaturedEventViewModel";

        private FeaturedEventViewModel _featuredEventViewModel = null;

        /// <summary>
        /// Gets the FeaturedEventViewModel property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public FeaturedEventViewModel FeaturedEventViewModel
        {
            get
            {
                return _featuredEventViewModel;
            }

            set
            {
                if (_featuredEventViewModel == value)
                {
                    return;
                }

                _featuredEventViewModel = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(FeaturedEventViewModelPropertyName);
            }
        }
        #endregion

        #region MyEvent View Model
        /// <summary>
        /// The <see cref="MyEventsViewModel" /> property's name.
        /// </summary>
        public const string MyEventsViewModelPropertyName = "MyEventsViewModel";

        private MyEventsViewModel _myEventsViewModel = null;

        /// <summary>
        /// Gets the MyEventsViewModel property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public MyEventsViewModel MyEventsViewModel
        {
            get
            {
                return _myEventsViewModel;
            }

            set
            {
                if (_myEventsViewModel == value)
                {
                    return;
                }

                _myEventsViewModel = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(MyEventsViewModelPropertyName);
            }
        }
        #endregion

        #region GoToSearch View Model
        /// <summary>
        /// The <see cref="GoToSearchViewModel" /> property's name.
        /// </summary>
        public const string GoToSearchViewModelPropertyName = "GoToSearchViewModel";

        private GoToSearchViewModel _goToSearchViewModel = null;

        /// <summary>
        /// Gets the GoToSearchViewModel property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public GoToSearchViewModel GoToSearchViewModel
        {
            get
            {
                return _goToSearchViewModel;
            }

            set
            {
                if (_goToSearchViewModel == value)
                {
                    return;
                }

                _goToSearchViewModel = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(GoToSearchViewModelPropertyName);
            }
        }
        #endregion
    }
}
