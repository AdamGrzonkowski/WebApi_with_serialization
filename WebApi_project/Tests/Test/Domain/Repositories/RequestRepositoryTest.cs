using Domain.Model;
using Domain.Services;
using Domain.Services.Interfaces;
using Domain.Services.Interfaces.Base;
using Domain.Services.Repositories;
using Domain.Services.Repositories.Base;
using Effort;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Test.Domain.Repositories
{
    public class RequestRepositoryTest : IDisposable
    {
        private DbConnection _connection;

        private IDbContext _dbContext;
        private IRequestRepository _repository;
        private IUnitOfWork _uow;
        private IDbSet<Request> _set;

        public RequestRepositoryTest()
        {
            _connection = DbConnectionFactory.CreateTransient();
            _dbContext = new ApplicationContext(_connection);
            _repository = new RequestRepository(_dbContext);
            _uow = new UnitOfWork(_dbContext);
            _set = _dbContext.Set<Request>();
        }

        [Fact]
        public async Task Insert_Request_To_Db()
        {
            string name = "TestName";
            Request req = new Request
            {
                Name = name
            };

            Assert.Equal(req.Index, default(int));

            _repository.Insert(req);
            await _uow.CommitAsync();

            Assert.NotEqual(req.Index, default(int)); // value should be autoincremented after persisting
            Assert.Equal(_set.Count(), 1);
            Request addedReq = _set.Find(req.Index);
            Assert.Equal(addedReq.Name, name);
        }

        [Fact]
        public async Task Get_All_requests()
        {
            Request req = new Request();
            Request req2 = new Request();
            Request req3 = new Request();

            _set.Add(req);
            _set.Add(req2);
            _set.Add(req3);

            await _dbContext.SaveChangesAsync();


            ICollection<Request> requests = await _repository.GetAllAsync();

            Assert.Equal(_set.Count(), requests.Count);
            Assert.InRange(requests.Count, 3, 10);
            Assert.Contains(req, requests);
            Assert.Contains(req2, requests);
            Assert.Contains(req3, requests);
        }

        [Fact]
        public async Task Unit_of_work_works_in_batches()
        {
            Request req = new Request();
            Request req2 = new Request();

            Assert.False(_set.Any());

            _repository.Insert(req);
            _repository.Insert(req2);

            Assert.False(_set.Any());

            await _uow.CommitAsync();

            Assert.Equal(_set.Count(), 2);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            _connection?.Dispose();
            _dbContext?.Dispose();
            _uow?.Dispose();
        }
    }
}
