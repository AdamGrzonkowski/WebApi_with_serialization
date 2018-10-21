using log4net;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    [Route("api")]
    public class ExampleController : ApiController
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(ExampleController));

        /// <summary>
        /// This endpoint receives a collection of serialized JSON models
        /// and stores them in a database created using automated database migrations.
        /// </summary>
        [HttpPost]
        [Route("data")]
        public async Task<IHttpActionResult> SaveToDatabase()
        {
            _logger.Fatal("NotImplemented!");
            throw new NotImplementedException();
        }

        /// <summary>
        /// This endpoint invokes an internal job of selecting the
        /// database records and then serializes each table record as an XML file.The files are
        /// saved in App_Data\xml\yyyy-MM-dd based on the date value in the models.
        /// </summary>
        [HttpGet]
        [Route("jobs/saveFiles")]
        public async Task<IHttpActionResult> SaveDbRecordsToFile()
        {
            _logger.Fatal("NotImplemented!");
            throw new NotImplementedException();
        }
    }
}