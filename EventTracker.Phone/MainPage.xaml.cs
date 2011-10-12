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

namespace EventTracker.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Messenger.Default.Register<Uri>(this, "NavigationRequest", (uri) =>
            {
                // set categories selected index to -1
                EventCategoriesListBox.SelectedIndex = -1;

                // set my events list selected index to -1
                MyEventsListBox.SelectedIndex = -1;

                // set featured events selected index to -1
                FeaturedEventsListBox.SelectedIndex = -1;

                NavigationService.Navigate(uri);
            });
        }
    }
}