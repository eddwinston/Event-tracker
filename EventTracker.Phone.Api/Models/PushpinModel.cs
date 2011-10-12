using System;
using System.Device.Location;

namespace EventTracker.Phone.Api
{
    /// <summary>
    /// Represents a pushpin data model.
    /// </summary>
    public class PushpinModel
    {
        /// <summary>
        /// Gets or sets the pushpin location.
        /// </summary>
        public GeoCoordinate Location { get; set; }

        /// <summary>
        /// Gets or sets the pushpin icon uri.
        /// </summary>
        public Uri Icon { get; set; }

        /// <summary>
        /// Gets or sets the pushpin type name.
        /// </summary>
        public string TypeName { get; set; }

        public PushpinModel Clone(GeoCoordinate location)
        {
            return new PushpinModel
            {
                Location = location,
                TypeName = TypeName,
                Icon = Icon
            };
        }
    }
}