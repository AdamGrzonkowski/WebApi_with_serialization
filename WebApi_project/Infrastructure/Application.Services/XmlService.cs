using Application.Services.Base;
using Application.Services.Interfaces;
using Domain.Model;
using Domain.Services.Interfaces;
using Domain.Services.Interfaces.Base;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Application.Services
{
    public class XmlService : BaseBl, IXmlService
    {
        private readonly IRequestRepository _repo;

        public XmlService(IUnitOfWork uow, IRequestRepository repo) : base(uow)
        {
            _repo = repo;
        }

        public async Task WriteRequestsToFiles(string directoryToSave)
        {
            List<Request> requests = await _repo.GetAllAsync().ConfigureAwait(false);

            var groupedRequests = requests
                .GroupBy(u => u.Date)
                .Select(grp => grp.ToList())
                .ToList();

            // run in parallel creation of files - one thread for one date
            var tasks = new List<Task>();
            foreach (var list in groupedRequests)
            {
                tasks.Add(WriteToFile(list, directoryToSave));
            }
            await Task.WhenAll(tasks);
        }

        private async Task WriteToFile(List<Request> records, string directoryToSave)
        {
            if (records.Count == 0)
            {
                return;
            }

            string date = records[0].Date.ToString("yyyy-MM-dd"); // all records should have same date, so take from 1st one
            string filePath = Path.Combine(directoryToSave, date);

            using (XmlWriter writer = XmlWriter.Create(filePath, new XmlWriterSettings{Async = true}))
            {
                await writer.WriteStartDocumentAsync().ConfigureAwait(false);
                await writer.WriteStartElementAsync(null, "requests",null).ConfigureAwait(false);

                foreach (Request record in records)
                {
                    writer.WriteStartElement("request");
                        writer.WriteElementString("ix", record.Index.ToString());
                        writer.WriteStartElement("content");
                            writer.WriteElementString("name", record.Name);
                            writer.WriteElementString("visits", record.Visits?.ToString());
                            writer.WriteElementString("dateRequested", date);
                        writer.WriteEndElement();
                    writer.WriteEndElement();
                }

                await writer.WriteEndElementAsync().ConfigureAwait(false);
                await writer.WriteEndDocumentAsync().ConfigureAwait(false);
            }
        }
    }
}
