using Application.Model.Base;
using System;

namespace Application.Model
{
    public class RequestModel : BaseModel
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
