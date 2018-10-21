using Domain.Model.Base;
using System;

namespace Domain.Model
{
    /// <summary>
    /// Basic, example class - describes request to the server.
    /// </summary>
    public class Request : BaseEntity
    {
        public string Name { get; set; }
        public int? Visits { get; set; }
        public DateTime Date { get; set; }
    }
}
