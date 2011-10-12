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
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventTracker.Phone.Storage.Test.Mocks;
using EventTracker.Phone.Api;

namespace EventTracker.Phone.Storage.Test.TestClasses
{
    [TestClass]
    public class UserEventDbTest : SilverlightTest
    {
        private IUserEventsDb _db;
        private Event _event;

        [TestInitialize]
        public void SetUp()
        {
            _db = new UserEventsTestDb();
            _event = new Event
            {
                Id = "1234567899",
                CityName = "City 1",
                Country = "Country 1",
                Description = "Event 1 description",
                Latitude = "67.87972",
                Longitude = "52.9900",
                PostalCode = "10001",
                Price = "5",
                Title = "Event 1",
                VenueName = "Event 1 venue",
                VenueAddress = "Event 1 venue address"
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
        }

        [TestMethod]
        public void Constructor_InstantiatesSuccessfully()
        {
            Assert.IsNotNull(_db);
        }

        [TestMethod]
        public void CanSaveNewEvent()
        {
            Assert.IsTrue(_db.Insert(_event));
        }

        [TestMethod]
        public void CanFetchAllSavedEvent()
        {
            _db.Insert(_event);

            // Fetch the same event
            var all = _db.FetchAll();
            Assert.IsNotNull(all);
            Assert.AreEqual(1, all.Count);

            _db.Insert(new Event());
            all = _db.FetchAll();
            Assert.IsNotNull(all);
            Assert.AreEqual(2, all.Count);
        }

        [TestMethod]
        public void CheckExistingEvent_Successfully()
        {
            var newEvent = new Event { Id = "123" };
            _db.Insert(_event);
            _db.Insert(newEvent);

            Assert.IsTrue(_db.EventExist(_event));
            Assert.IsTrue(_db.EventExist(newEvent));

            Assert.IsFalse(_db.EventExist(new Event { Id = "NonExisting" }));
        }
    }
}
