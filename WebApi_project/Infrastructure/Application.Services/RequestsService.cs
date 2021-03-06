﻿using Application.Model;
using Application.Services.Base;
using Application.Services.Interfaces;
using Application.Services.Interfaces.Mappers;
using Domain.Model;
using Domain.Services.Interfaces;
using Domain.Services.Interfaces.Base;
using Helper.Common.ConfigStrings;
using Helper.Common.Files;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Application.Services
{
    public class RequestsService : BaseBl, IRequestsService
    {
        private readonly IRequestRepository _repo;
        private readonly IRequestsMapper _mapper;

        public RequestsService(IUnitOfWork uow, IRequestRepository repo, IRequestsMapper mapper) : base(uow)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> SaveRequestsToDbAsync(IEnumerable<RequestModel> requests)
        {
            foreach (Request req in _mapper.ModelsToEntities(requests))
            {
                _repo.Insert(req);
            }

            return await SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task WriteRequestsToFilesAsync(string directoryToSave)
        {
            IEnumerable<Request> requests = await _repo.GetAllAsync().ConfigureAwait(false);

            var groupedRequests = _mapper.EntitiesToModels(requests)
                .GroupBy(u => u.Date.Date)
                .Select(grp => grp.ToList())
                .ToList();

            // run creation of files in parallel - one thread for one date/file
            var tasks = new List<Task>();
            foreach (var list in groupedRequests)
            {
                tasks.Add(WriteToFile(list, directoryToSave));
            }
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        private async Task WriteToFile(List<RequestModel> records, string directoryToSave)
        {
            if (records.Count == 0)
            {
                return;
            }

            string date = records[0].Date.ToString(Formatter.ShortDateFormat); // all records after grouping have same date, so take from 1st one
            string filePath = Path.Combine(directoryToSave, date + FileExtension.Xml);

            if (!Directory.Exists(directoryToSave))
            {
                Directory.CreateDirectory(directoryToSave);
            }

            using (XmlWriter writer = XmlWriter.Create(filePath, Xml.GetXmlWriterSettings()))
            {
                await writer.WriteStartDocumentAsync().ConfigureAwait(false);
                await writer.WriteStartElementAsync(null, "requests",null).ConfigureAwait(false); //adding another lvl as there may be multiple records with same date

                foreach (RequestModel record in records)
                {
                    writer.WriteStartElement("request");
                        writer.WriteElementString("ix", record.Id.ToString());
                        writer.WriteStartElement("content");
                            writer.WriteElementString("name", record.Name);
                            if (record.Visits.HasValue)
                            {
                                writer.WriteElementString("visits", record.Visits.ToString());
                            }
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