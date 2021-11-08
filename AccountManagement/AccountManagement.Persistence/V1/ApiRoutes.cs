using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Persistence.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class People
        {
            public const string GetAll = Base + "/people";
            public const string Update = Base + "/people";
            public const string Delete = Base + "/people/{personId}";
            public const string Get = Base + "/people/{personId}";
            public const string Create = Base + "/people";
        }

        public static class Accounts
        {
            public const string GetAll = Base + "/accounts";
            public const string Update = Base + "/accounts";
            public const string Delete = Base + "/accounts/{accountId}";
            public const string Get = Base + "/accounts/{accountId}";
            public const string Create = Base + "/accounts";
        }

    }
}
