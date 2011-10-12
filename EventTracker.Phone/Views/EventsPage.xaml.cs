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

namespace EventTracker.Phone.Views
{
    public partial class EventsPage : PhoneApplicationPage
    {
        public EventsPage()
        {
            InitializeComponent();

            Messenger.Default.Register<Uri>(this, "NavigationRequest", (uri) =>
            {
                NavigationService.Navigate(uri);
            });
        }

        #region OnNavigatedTo
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("CategoryId"))
            {
                Messenger.Default.Send<string>(NavigationContext.QueryString["CategoryId"], "CategoryId");
            }
        }
        #endregion

        #region OnBackKeyPress
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Send<bool>(true, "DisposeEventsViewModel");
        }
        #endregion
    }
}