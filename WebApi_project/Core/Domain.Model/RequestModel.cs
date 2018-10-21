using System;

namespace Domain.Model
{
    /// <summary>
    /// Basic, example class - describes request to the server.
    /// </summary>
    public class RequestModel
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public int? Visits { get; set; }
        public DateTime Date { get; set; }
    }
}
