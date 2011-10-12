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
    public partial class EventDetailPage : PhoneApplicationPage
    {
        public EventDetailPage()
        {
            InitializeComponent();

            Messenger.Default.Register<Uri>(this, "NavigationRequest", (uri) =>
            {
                NavigationService.Navigate(uri);
            });

        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //EventTracker.Phone.Locator.EventTrackerViewModelLocator.ClearEventDetailViewModelPropertyName();
            Messenger.Default.Send<bool>(true, "DisposeEventDetailsViewModel");
            NavigationService.GoBack();
            base.OnBackKeyPress(e);
        }
    }
}