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
using EventTracker.Phone.Api;
using GalaSoft.MvvmLight.Command;
using System.Collections;

namespace EventTracker.Phone.UserControls
{
    public partial class EventsListUserControl : UserControl
    {
        public EventsListUserControl()
        {
            InitializeComponent();
        }

        public List<Event> EventsSource
        {
            get { return (List<Event>)GetValue(EventSourceProperty); }
            set { SetValue(EventSourceProperty, value); }
        }

        public RelayCommand<Event> ItemSelectionChangedCommand
        {
            get;
            set;
        }

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public static readonly DependencyProperty EventSourceProperty =
            DependencyProperty.Register("EventSource", typeof(List<Event>), typeof(EventsListUserControl), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(EventsListUserControl),
                new PropertyMetadata(null));
    }
}
