using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using GalaSoft.MvvmLight.Messaging;
using EventTracker.Phone.Api;

namespace EventTracker.Phone.Views
{
    public partial class SearchPage : PhoneApplicationPage
    {
        public SearchPage()
        {
            InitializeComponent();

            Messenger.Default.Register<Uri>(this, "NavigationRequest", (uri) =>
            {
                NavigationService.Navigate(uri);
            });

            CategoryListPicker.SelectionChanged += new SelectionChangedEventHandler(CategoryListPicker_SelectionChanged);
        }

        void CategoryListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var eCategory = e.OriginalSource as EventCategory;
            if (eCategory != null)
            {
                Messenger.Default.Send<EventCategory>(eCategory, "CategoryPickerSelectionChanged");
            }
        }
    }
}