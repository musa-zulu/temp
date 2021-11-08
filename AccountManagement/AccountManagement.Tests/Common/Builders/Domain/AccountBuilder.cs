using AccountManagement.DB.Domain;
using PeanutButter.RandomGenerators;

namespace AccountManagement.Tests.Common.Builders.Domain
{
    public class AccountBuilder : GenericBuilder<AccountBuilder, Account>
    {
        public AccountBuilder WithId(int id)
        {
            return WithProp(x => x.Code = id);
        }
    }
}