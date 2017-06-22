using System;
using ReckonMe.Helpers;

namespace ReckonMe.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.Helpers.ObservableObject" />
    public class BaseDataObject : ObservableObject
    {
        /// <summary>
        /// Id for item
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Azure created at time stamp
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Azure UpdateAt timestamp for online/offline sync
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Azure version for online/offline sync
        /// </summary>
        public string AzureVersion { get; set; }
    }
}
