using Fantasy.API.Utilities.ServicesHandler;
using System;
using System.Collections.Generic;

namespace Fantasy.API.Host.Models
{
    /// <summary>
    /// A simple class to hold details of an HTTP call.
    /// </summary>
    public sealed class AdminAccLog
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="AdminAccLog" /> class.
        /// </summary>
        public AdminAccLog() { }

        /// <summary>
        /// An unique identifier for the HTTP call.
        /// </summary>
        public Guid TrackingId { get; set; }

        /// <summary>
        /// Identity of the caller.
        /// </summary>
        public UserInfo CallerIdentity { get; set; }

        /// <summary>
        /// Timestamp at which the HTTP call took place.
        /// </summary>
        public DateTime CallDateTime { get; set; }

        /// <summary>
        /// Verb associated with the HTTP call.
        /// </summary>
        public string Verb { get; set; }

        /// <summary>
        /// Http request URI.
        /// </summary>
        public Uri RequestUri { get; set; }

        /// <summary>
        /// Http request headers.
        /// </summary>
        public IDictionary<String, String[]> RequestHeaders { get; set; }


        /// <summary>
        /// Http response status code.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Http response status line.
        /// </summary>
        public string ReasonPhrase { get; set; }

        /// <summary>
        /// Http response headers.
        /// </summary>
        public IDictionary<String, String[]> ResponseHeaders { get; set; }

        /// <summary>
        /// Timestamp representing the duration of the HTTP call.
        /// </summary>
        public TimeSpan CallDuration { get; set; }

        public string Request { get; set; }
        public string Response { get; set; }
    }
}