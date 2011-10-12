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

namespace EventTracker.Phone.Services
{
    public enum SortOrder
    {
        Popularity, Date, Title, Relevance, Venue_Name
    }

    public enum SortDirection
    {
        Ascending, Descending
    }

    public class EventSearchParams
    {
        public EventSearchParams()
        {
            Location = "Finland"; //TODO: Get value from GPS
            Date = "Today"; //DateTime.Now.ToShortDateString();
            SortOrder = SortOrder.Relevance;
            SortDirection = SortDirection.Descending;
            PageSize = 5000;
            Within = 200; // meters
        }

        public string Keyword { get; set; }

        public string Location { get; set; }

        public string Date { get; set; }

        public string Category { get; set; }

        public SortOrder SortOrder { get; set; }

        public SortDirection SortDirection { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int Within { get; set; }
    }
}
