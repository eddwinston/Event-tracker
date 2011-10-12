using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Device.Location;
using Microsoft.Phone.Reactive;
using EventTracker.Phone.Api.Contracts;
//using LocationServiceSample;
using EventTracker.Phone.Api;
using System.Threading;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;

namespace EventTracker.Phone.Services.GPS
{
    public class EventTrackerLocationService : EventTracker.Phone.Services.GPS.IEventTrackerLocationService
    {
        static object obj = new object();
        private GeoCoordinateWatcher watcher;
        private int sampleInterval = 10000;

        // Threads for position and status emulation and a static bool
        private Thread positionEmulationThread;
        private Thread statusEmulationThread;

        // Hard coded array for emulated status values
        private static GeoPositionStatus[] emulatedStatusValues = { GeoPositionStatus.Initializing, GeoPositionStatus.Ready };
        // Delay after each emulated status value in the array above. These two arrays should be the same length.
        private static int[] emulatedStatusDelay = { 1000, 1000 };

        private Action<GeoPositionStatusChangedEventArgs> _locationServiceStatusChangedCallback;
        private Action<GeoPosition<GeoCoordinate>> _locationChangedCallback;

        public EventTrackerLocationService(Action<GeoPositionStatusChangedEventArgs> locationServiceStatusChangedCallback = null)
        {
            _locationServiceStatusChangedCallback = locationServiceStatusChangedCallback;

            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            watcher.MovementThreshold = 20;

            watcher.StatusChanged += StatusChanged;
            watcher.PositionChanged += PositionChanged;
        }

        // Async version of the GetCurrentLocation method
        public void GetCurrentLocationAsync(Action<GeoPosition<GeoCoordinate>> locationChangedCallback)
        {
            _locationChangedCallback = locationChangedCallback;

#if DEBUG
            // Start the thread on which emulated location data is generated.
            // The method StartEmulation is defined next.
            statusEmulationThread = new Thread(StartStatusEmulation);
            statusEmulationThread.Start();
#else
            watcher.Start();
#endif
        }

        // Blocking version of the GetCurrentLocation method
        public GeoPosition<GeoCoordinate> GetCurrentLocation()
        {
#if DEBUG
            var geoCoordinates = ReadLocationDataFile();
            if (geoCoordinates != null && geoCoordinates.Count > 0)
            {
                return new GeoPosition<GeoCoordinate>
                {
                    Location = geoCoordinates[new Random().Next(0, geoCoordinates.Count)],
                    Timestamp = DateTime.Now
                };
            }
#else
            // Real
             // Get coordinate from watcher
            bool started = watcher.TryStart(true, TimeSpan.FromMilliseconds(60000));

            // Check to see if the service was started successfully.
            if (started)
            {
                // Check to see if location data is available.
                if (watcher.Status == GeoPositionStatus.Ready)
                {
                    return watcher.Position;
                }
                else
                {
                    //statusTextBlock.Text = "Location data is not currently available. Please try again later.";
                }
            }
            else
            {
                //statusTextBlock.Text = "The location service could not be started.";
            }
#endif
            return null;
        }

        #region StartEmulation
        /// <summary>
        /// Called from the startButton click handler when emulation is turned on. This method
        /// calls EmulateStatusEvents which returns an IEnumerable sequence of GeoPositionStatus
        /// objects. ToObservable converts this stream to an Observable sequence. The stream is 
        /// subscribed to, registering a handler that is called when new status data arrives.
        /// </summary>
        void StartStatusEmulation()
        {
            var statusEventsToObservable = EmulateStatusEvents().ToObservable();

            var statusFromEventArgs = from s in statusEventsToObservable
                                      select s.Status;

            statusFromEventArgs.Subscribe(status => InvokeStatusChanged(status));
        }
        #endregion

        #region EmulateStatusEvents
        /// <summary>
        /// Returns the status values defined in the emulatedStatusValues variable and
        /// then sleeps for the amount of time specified in the emulatedStatusDelay variable
        /// </summary>
        /// <returns></returns>
        IEnumerable<GeoPositionStatusChangedEventArgs> EmulateStatusEvents()
        {
            // Loop over the emulated status values until the end of the list
            for (int i = 0; i < emulatedStatusValues.Length; i++)
            {

                // return the current status value
                yield return new GeoPositionStatusChangedEventArgs(emulatedStatusValues[i]);

                // sleep for the specified delay for each status value in the array
                Thread.Sleep(emulatedStatusDelay[i]);
            }

        }
        #endregion

        #region InvokeStatusChanged
        /// <summary>
        /// InvokeStatusChanged is called when new location status is available (either live
        /// or emulated). It uses BeginInvoke to call another handler on the Page's UI thread.
        /// </summary>
        /// <param name="status"></param>
        void InvokeStatusChanged(GeoPositionStatus status)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                StatusChanged(null, new GeoPositionStatusChangedEventArgs(status)));
        }
        #endregion

        #region StatusChanged
        /// <summary>
        /// This is where the application responds to new status information. In this case, it simply
        /// displays the location information.  If emulation is enabled and the status is Ready, a
        /// new thread is launched for position emulation. This mimics the behaviour of the live data,
        /// where location data doesn't arrive until the LocationService status is Ready.
        /// </summary>
        /// <param name="status"></param>
        void StatusChanged(object sender, GeoPositionStatusChangedEventArgs status)
        {
            // If emulation is being used and the status is Ready, start the position emulation thread
            if (status.Status == GeoPositionStatus.Ready)
            {
#if DEBUG
                positionEmulationThread = new Thread(StartPositionEmulation);
                positionEmulationThread.Start();
#else
                if (_locationServiceStatusChangedCallback != null)
                    _locationServiceStatusChangedCallback(status);
#endif
            }
        }
        #endregion

        #region StartPositionEmulation
        /// <summary>
        /// This method calls EmulateLocationEvents which returns an IEnumerable sequence of GeoCoordinates.
        /// ToObservable converts this stream to an Observable sequence. Once the Observable is
        /// created, the method is the same as the non-emulation method. The stream is sampled
        /// and then Subscribe registers a handler that is called when new location data arrives.
        /// </summary>
        void StartPositionEmulation()
        {
            var emulatedEventsToObservable = EmulatePositionEvents().ToObservable();

            var sampledEvents = emulatedEventsToObservable.Sample(TimeSpan.FromMilliseconds(sampleInterval));

            sampledEvents.Subscribe(coordinate => InvokePositionChanged(coordinate));

        }
        #endregion

        #region EmulatePositionEvents
        /// <summary>
        /// Delivers emulated position events. Coordinates are read from a data file and returned
        /// one at a time, through the IEnumerable interface.
        /// </summary>
        /// <returns></returns>
        IEnumerable<GeoCoordinate> EmulatePositionEvents()
        {
            // Get a list of coordinates from a data file
            var coordinates = ReadLocationDataFile();

            // Loop through all of the coordinates
            int index = 0;
            for (; ; )
            {
                // Return the next coordinate in the list
                yield return coordinates[index];



                // Increment the array index
                index = (index + 1) % coordinates.Count;

                // Sleep for a moment. You can adjust this to change the rate at which the events fire
                Thread.Sleep(200);
            }
        }
        #endregion

        #region InvokePositionChanged
        /// <summary>
        /// InvokePositionChanged is called when new location data is available (either live
        /// or emulated). It uses BeginInvoke to call another handler on the Page's UI thread.
        /// </summary>
        /// <param name="coordinate"></param>
        void InvokePositionChanged(GeoCoordinate coordinate)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                PositionChanged(null, new GeoPositionChangedEventArgs<GeoCoordinate>(
                    new GeoPosition<GeoCoordinate>(new DateTimeOffset(DateTime.Now), coordinate))));
        }
        #endregion

        #region PositionChanged
        /// <summary>
        /// This is where the application responds to new location data. In this case, it simply
        /// displays the location information
        /// </summary>
        /// <param name="coordinate"></param>
        void PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> positionArg)
        {
            if (_locationChangedCallback != null)
            {
                _locationChangedCallback(positionArg.Position);
            }

            //lock (obj)
            //{
            //    AppState.Latitude = coordinate.Latitude;
            //    AppState.Longitude = coordinate.Longitude;
            //}
        }
        #endregion

        #region ReadLocationData
        /// <summary>
        /// This method is called when location emulation is started. It first attempts to read the file
        /// that users can save in isolated storage. If that file doesn't exist, the resource file packaged
        /// with the application is used, LocationData.txt in the Solution Explorer ---->
        /// </summary>
        /// <returns></returns>
        List<GeoCoordinate> ReadLocationDataFile()
        {
            // Initialize the coordinate list
            var coordinates = new List<GeoCoordinate>();

            // Open the isolated storage file
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication();

            Stream dataFileStream = null;

            // First, check for a file in isolated storage
            if (isoStore.FileExists("LocationData.txt"))
            {
                dataFileStream = new IsolatedStorageFileStream("LocationData.txt", FileMode.OpenOrCreate, isoStore);
            }
            else
            {
                var url = string.Format("/{0};component/LocationData.txt", "EventTracker.Phone");
                // If there is no file in isolated storage, use the resource file
                Uri resourceUri = new Uri(url, UriKind.Relative);
                System.Windows.Resources.StreamResourceInfo resourceInfo = Application.GetResourceStream(resourceUri);
                dataFileStream = resourceInfo.Stream;
            }

            // read the file and add the coordinates to the list
            using (StreamReader sr = new StreamReader(dataFileStream))
            {
                while (!sr.EndOfStream)
                {
                    string[] latlong = sr.ReadLine().Split(',');
                    coordinates.Add(new GeoCoordinate(Double.Parse(latlong[0]), Double.Parse(latlong[1])));
                }
                sr.Close();
            }
            return coordinates;
        }
        #endregion

    }
}
