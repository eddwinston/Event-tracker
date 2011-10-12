using System;
using System.Device.Location;
namespace EventTracker.Phone.Services.GPS
{
    public interface IEventTrackerLocationService
    {
        GeoPosition<GeoCoordinate> GetCurrentLocation();
        void GetCurrentLocationAsync(Action<GeoPosition<GeoCoordinate>> locationChangedCallback);
    }
}
