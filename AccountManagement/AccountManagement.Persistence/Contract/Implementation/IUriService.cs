using AccountManagement.Persistence.V1.Requests.Queries;
using System;

namespace AccountManagement.Persistence.Contract.Implementation
{
    public interface IUriService
    {
        Uri GetAllUri(PaginationQuery pagination = null);
    }
}
