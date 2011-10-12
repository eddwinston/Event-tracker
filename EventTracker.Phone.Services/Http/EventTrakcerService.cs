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
using System.Xml.Serialization;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using EventTracker.Phone.Api;

namespace EventTracker.Phone.Services
{
    public class EventTrakcerService : IEventTrakcerService
    {
        private static string API_KEY = "Eventful api key";
        private static string _eventSearchUrlFormat =
            "http://api.eventful.com/rest/events/search?app_key={0}&location={1}&category={2}&date={3}&sort_order={4}&sort_direction={5}&page_size={6}&within={7}&units=km";
        private static string _eventSearchUrlFormatNonGeo =
            "http://api.eventful.com/rest/events/search?app_key={0}&location={1}&category={2}&date={3}&sort_order={4}&sort_direction={5}&page_size={6}";

        private static string _categorySearchUrlFormat = 
            "http://api.evdb.com/rest/categories/list?app_key={0}";

        private static XmlSerializer _xmlSerializer = new XmlSerializer(typeof(EventSet));

        public EventTrakcerService()
        {

        }

        #region GetEvents method
        public void GetEvents(EventSearchParams searchParams, Action<EventSet> onGetCompleted = null, Action onBegin = null, Action<Exception> onError = null, Action onFinally = null)
        {
            if (searchParams == null)
                throw new ArgumentNullException("Search paramater cannot be null");

            WebClient webClient = new WebClient();
            webClient.OpenReadCompleted += delegate(object sender, OpenReadCompletedEventArgs e)
            {
                try
                {
                    if (e.Error != null)
                    {
                        if (onError != null)
                        {
                            onError(e.Error);
                        }
                        return;
                    }

                    using (XmlReader xmlReader = XmlReader.Create(e.Result))
                    {
                        var eventSet = (EventSet)_xmlSerializer.Deserialize(xmlReader);
                        if (onGetCompleted != null)
                        {
                            onGetCompleted(eventSet);
                        }
                    }
                }
                finally
                {
                    if (onFinally != null)
                    {
                        onFinally();
                    }
                }
            };

            if (onBegin != null)
            {
                onBegin();
            }

            var url = string.Empty;

            if (searchParams.Within != 0)
                url = GetEventSearchUrl(searchParams);
            else
                url = GetEventSearchUrl(searchParams, true);

            webClient.OpenReadAsync(new Uri(url));
        }
        #endregion

        #region GetCategories method
        public void GetCategories(Action<IEnumerable<EventCategory>> onGetCompleted = null, Action onBegin = null, Action<Exception> onError = null, Action onFinally = null)
        {
            WebClient webClient = new WebClient();
            webClient.OpenReadCompleted += delegate(object sender, OpenReadCompletedEventArgs e)
            {
                try
                {
                    if (e.Error != null)
                    {
                        if (onError != null)
                        {
                            onError(e.Error);
                        }
                        return;
                    }

                    XDocument xDoc = XDocument.Load(e.Result);
                    if (xDoc != null)
                    {
                        var categories = xDoc.Descendants("category").Select(c => new EventCategory()
                                         {
                                             Id = c.Element("id").Value,
                                             Name = c.Element("name").Value
                                         }).ToList();

                        if (categories != null)
                        {
                            onGetCompleted(categories.AsEnumerable<EventCategory>());
                        }
                        else
                        {
                            onGetCompleted(null);
                        }
                    }
                }
                finally
                {
                    if (onFinally != null)
                    {
                        onFinally();
                    }
                }
            };

            if (onBegin != null)
            {
                onBegin();
            }

            webClient.OpenReadAsync(new Uri(GetCategorySearchUrl()));
        }
        #endregion

        public void GetVenue(string eventId, Action<Venue> onGetCompleted = null, Action onBegin = null, Action<Exception> onError = null, Action onFinally = null)
        {

        }

        #region Helper

        private string GetEventSearchUrl(EventSearchParams esp, bool nonGeo = false)
        {
            if (string.IsNullOrEmpty(API_KEY))
                throw new InvalidOperationException("ApiKey is invalid.");

            if (!nonGeo)
                //var str = string.Format(_eventSearchUrlFormat, API_KEY, esp.Location, esp.Category, esp.Date, esp.SortOrder.ToString(), esp.SortDirection.ToString(), esp.PageSize, esp.Within);
                return string.Format(_eventSearchUrlFormat, 
                    API_KEY, esp.Location, 
                    esp.Category, esp.Date, 
                    esp.SortOrder.ToString(), 
                    esp.SortDirection.ToString(), 
                    esp.PageSize,
                    esp.Within);
            else
                return string.Format(_eventSearchUrlFormatNonGeo,
                   API_KEY, esp.Location,
                   esp.Category, esp.Date,
                   esp.SortOrder.ToString(),
                   esp.SortDirection.ToString(),
                   esp.PageSize);
        }

        private string GetCategorySearchUrl()
        {
            if (string.IsNullOrEmpty(API_KEY))
                throw new InvalidOperationException("ApiKey is invalid.");

            return string.Format(_categorySearchUrlFormat, API_KEY);
        }
        #endregion

    }
}
