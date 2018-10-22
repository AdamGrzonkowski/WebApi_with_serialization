using Domain.Model.Base;
using System;

namespace Domain.Model
{
    /// <summary>
    /// Basic, example class - describes request to the server.
    /// </summary>
    public class Request : BaseEntity
    {
        /// <summary>
        /// Name of request.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of visits.
        /// </summary>
        public int? Visits { get; set; }

        /// <summary>
        /// Date of request.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
