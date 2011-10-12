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
using GalaSoft.MvvmLight.Command;
using System.Collections;
using EventTracker.Phone.Storage;
using EventTracker.Phone.Api;
using System.Collections.Generic;

namespace EventTracker.Phone
{
    public class EventCategoriesViewModel : EventTrackerViewModelBase
    {
        private IEventTrakcerService _service;
        public EventCategoriesViewModel(IEventTrakcerService service)
        {
            _service = service;
            this.PageTitle = "categories";
            this.LoadCategories();

            BindCategoriesSelectionChanged();
        }

        #region Categories list
        /// <summary>
        /// The <see cref="Categories" /> property's name.
        /// </summary>
        public const string CategoriesPropertyName = "Categories";

        private ObservableCollection<EventCategory> _categories = null;

        /// <summary>
        /// Gets the Categories property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<EventCategory> Categories
        {
            get
            {
                return _categories;
            }

            set
            {
                if (_categories == value)
                {
                    return;
                }

                _categories = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CategoriesPropertyName);
            }
        }
        #endregion

        #region IsCategoriesLoading dependency properties

        /// <summary>
        /// The <see cref="IsCategoriesLoading" /> property's name.
        /// </summary>
        public const string IsCategoriesLoadingPropertyName = "IsCategoriesLoading";

        private bool _isCategoriesLoading = false;

        /// <summary>
        /// Gets the IsCategoriesLoading property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool IsCategoriesLoading
        {
            get
            {
                return _isCategoriesLoading;
            }

            set
            {
                if (_isCategoriesLoading == value)
                {
                    return;
                }

                _isCategoriesLoading = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(IsCategoriesLoadingPropertyName);
            }
        }

        #endregion

        #region IsDataLoaded
        public bool IsDataLoaded
        {
            get;
            private set;
        }
        #endregion

        #region Load event categories method
        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadCategories()
        {
            this.Categories = new ObservableCollection<EventCategory>();

            // Sample data; replace with real data
            if (DesignerProperties.IsInDesignTool || IsInDesignMode)
            {
                this.PageTitle = "categories*";
                this.IsCategoriesLoading = true;
                for (int i = 0; i < 10; i++)
                {
                    Categories.Add(new EventCategory { Id = i.ToString(), Name = "Category #" + i.ToString() });
                }
                this.IsCategoriesLoading = false;
            }
            else
            {
                _service.GetCategories(categories =>
                {
                    foreach (var cat in categories)
                    {
                        Categories.Add(new EventCategory() { Id = cat.Id, Name = cat.Name });
                    }

                    EventTrackerStorage.SaveEventCategories(new List<EventCategory>(categories));
                },
                delegate
                {
                    this.IsCategoriesLoading = true;
                },
                delegate(Exception exception)
                {
                    IsCategoriesLoading = false;
                    System.Diagnostics.Debug.WriteLine(exception);
                },
                delegate
                {
                    IsCategoriesLoading = false;
                });
            }

            this.IsDataLoaded = true;
        }
        #endregion

        #region Commands
        public RelayCommand<EventCategory> SelectionChangedCommand
        {
            get;
            private set;
        }
        #endregion

        #region Helpers
        private void BindCategoriesSelectionChanged()
        {
            this.SelectionChangedCommand = new RelayCommand<EventCategory>(item =>
            {
                if (item != null)
                {
                    this.SendNavigationRequestMessage(new Uri("/Views/EventsPage.xaml?CategoryId=" + item.Id, UriKind.Relative));
                }
            });
        }
        #endregion
    }
}
