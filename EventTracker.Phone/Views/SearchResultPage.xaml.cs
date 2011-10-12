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
using EventTracker.Phone.Services;
using GalaSoft.MvvmLight.Messaging;

namespace EventTracker.Phone.Views
{
    public partial class SearchResultPage : PhoneApplicationPage
    {
        public SearchResultPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            ExtractSearchParameters();
        }

        private void ExtractSearchParameters()
        {
            var qs = NavigationContext.QueryString;
            var searchParam = new EventSearchParams();

            searchParam.Keyword = qs["keyword"] != null ? qs["keyword"] : string.Empty;
            searchParam.Category = qs["category"] != null ? qs["category"] : "All";
            searchParam.Location = qs["location"] != null ? qs["location"] : string.Empty;
            searchParam.Date = qs["date"] != null ? qs["date"] : string.Empty;
            searchParam.Within = 0; // 

            Messenger.Default.Send<EventSearchParams>(searchParam, "SearchParams");
        }
    }
}