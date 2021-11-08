using AccountManagement.DB.Domain;
using PeanutButter.RandomGenerators;

namespace AccountManagement.Tests.Common.Builders.Domain
{
    public class PersonBuilder : GenericBuilder<PersonBuilder, Person>
    {
        public PersonBuilder WithId(int id)
        {
            return WithProp(x => x.Code = id);
        }
    }
}