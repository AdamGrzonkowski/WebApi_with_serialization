using Application.Services.Interfaces;
using Domain.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;

namespace Api.Controllers
{
    [RoutePrefix("api")]
    public class ExampleController : ApiController
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ExampleController));

        private readonly IRequestsService _requestsService;

        /// <summary>
        /// Public constructor.
        /// </summary>
        /// <param name="requestsService"></param>
        public ExampleController(IRequestsService requestsService)
        {
            _requestsService = requestsService;
        }
        /// <summary>
        /// This endpoint receives a collection of serialized JSON models
        /// and stores them in a database created using automated database migrations.
        /// </summary>
        /// <param name="requests">Requests in chosen format.</param>
        /// <remarks>
        /// Asynchronously saves records to database.
        /// </remarks>
        /// <response code="200"></response>
        [HttpPost]
        [Route("data")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> SaveToDatabase(IEnumerable<Request> requests) 
        {
            try
            {
                int recordsSaved = await _requestsService.SaveRequestsToDbAsync(requests);
                return Ok($"Created {recordsSaved} records.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// This endpoint invokes an internal job of selecting the
        /// database records and then serializes each table record as an XML file.The files are
        /// saved in App_Data\xml\yyyy-MM-dd based on the date value in the models.
        /// </summary>
        /// <remarks>
        /// Asynchronously saves records stored in database to .xml files.
        /// </remarks>
        /// <response code="200"></response>
        [HttpGet]
        [Route("jobs/saveFiles")]
        [ResponseType(null)]
        public async Task<IHttpActionResult> SaveDbRecordsToFile()
        {
            try
            {
                string appDataPath = HostingEnvironment.MapPath(@"~/App_Data");
                await _requestsService.WriteRequestsToFilesAsync(appDataPath);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return InternalServerError();
            }
        }
    }
}