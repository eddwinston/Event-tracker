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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventTracker.Phone.Api.Test.TestClasses
{
    [TestClass]
    public class EventTest
    {
        [TestInitialize]
        public void SetUp()
        {
            
        }

        [TestCleanup]
        public void CleanUp()
        {
        }

        [TestMethod]
        public void TestEventIsInitialised_Successfully()
        {
            var e = new Event();

            Assert.IsNotNull(e);
        }

        [TestMethod]
        public void MakeSureEventPropertyReturnExpectedValues_Successfully()
        {
            var e = new Event
            {
                Title = "Title 1",
                CityName = "City 1",
                Latitude = "23.6746",
                Longitude = "66.6666"
            };

            Assert.AreEqual("Title 1", e.Title);
            Assert.AreEqual("City 1", e.CityName);
            Assert.AreEqual("23.6746", e.Latitude);
            Assert.AreEqual("66.6666", e.Longitude);
        }
    }
}
