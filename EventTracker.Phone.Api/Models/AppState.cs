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
using GalaSoft.MvvmLight;
using EventTracker.Phone.Api.Contracts;

namespace EventTracker.Phone.Api
{
    public static class AppState
    {
        public static double Longitude
        {
            get;
            set;
        }

        public static double Latitude
        {
            get;
            set;
        }

        public static EventTrackerGeoCoordinateStatus Status
        {
            get;
            set;
        }
    }
}
