﻿using System;
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
using Microsoft.Silverlight.Testing;

namespace EventTracker.Phone.Api.Test
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // set up unit testing
            Content = UnitTestSystem.CreateTestPage();
            IMobileTestPage imtp = Content as IMobileTestPage;

            if (imtp != null)
            {
                BackKeyPress += (x, xe) => xe.Cancel = imtp.NavigateBack();
            }
        }
    }
}