using AccountManagement.Persistence.Implementation;
using AccountManagement.Persistence.V1.Requests.Queries;
using NUnit.Framework;
using System;

namespace AccountManagement.Tests.Persistence.Services
{
    [TestFixture]
    public class UriServiceTests
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new UriService(Guid.NewGuid().ToString()));
            //---------------Test Result -----------------------
        }

        [Test]
        public void GetAllUri_GivenPaginationQueryIsNull_ShouldReturnBaseUri()
        {
            //---------------Set up test pack-------------------
            var baseUri = "localhost:4000";
            var uriService = new UriService(baseUri);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = uriService.GetAllUri();
            //---------------Test Result -----------------------
            Assert.AreEqual(baseUri, results.AbsoluteUri);
        }

        [Test]
        public void GetAllUri_GivenPaginationQueryIsNotNull_ShouldReturnModifiedUri()
        {
            //---------------Set up test pack-------------------
            var baseUri = "localhost:4000";
            var paginationQuery = new PaginationQuery
            {
                PageNumber = 1,
                PageSize = 10
            };
            var uriService = new UriService(baseUri);
            var modifiedUrl = "localhost:4000?pageNumber=1&pageSize=10";
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = uriService.GetAllUri(paginationQuery);
            //---------------Test Result -----------------------            
            Assert.AreEqual(modifiedUrl, results.AbsoluteUri);
        }
    }
}
