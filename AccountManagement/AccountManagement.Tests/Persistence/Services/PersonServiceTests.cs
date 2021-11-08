using AccountManagement.DB;
using AccountManagement.DB.Domain;
using AccountManagement.Persistence.Implementation;
using AccountManagement.Tests.Common.Builders.Domain;
using AccountManagement.Tests.Common.Helpers;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace AccountManagement.Tests.Persistence.Services
{
    [TestFixture]
    public class PersonServiceTests
    {
        private FakeDbContext _db;
        [SetUp]
        public void SetUp()
        {
            _db = new FakeDbContext(Guid.NewGuid().ToString());
            _db.DbContext.Database.EnsureCreated();
        }

        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() =>
                new PersonService(Substitute.For<IApplicationDbContext>()));
            //---------------Test Result -----------------------
        }

        [Test]
        public async Task GetPeopleAsync_GivenNoRecordsExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------            
            var personService = CreatePersonService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await personService.GetPeopleAsync();
            //---------------Test Result -----------------------
            Assert.IsEmpty(results);
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public async Task GetPeopleAsync_GivenOnePersonExist_ShouldReturnListWithThatPerson()
        {
            //---------------Set up test pack-------------------
            var person = CreateRandomPerson(1);
            await _db.Add(person);

            var personService = CreatePersonService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await personService.GetPeopleAsync();
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(person.Name, results[0].Name);
            Assert.AreEqual(person.Surname, results[0].Surname);
            Assert.AreEqual(person.IdNumber, results[0].IdNumber);
        }

        [Test]
        public async Task GetPeopleAsync_GivenTwoPeopleExist_ShouldReturnAListWithTwoPeople()
        {
            //---------------Set up test pack-------------------
            var firstPerson = CreateRandomPerson(1);
            var secondPerson = CreateRandomPerson(2);

            await _db.Add(firstPerson, secondPerson);

            var personService = CreatePersonService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await personService.GetPeopleAsync();
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public async Task GetPeopleAsync_GivenManyPeopleExist_ShouldReturnAListWithAllPeople()
        {
            //---------------Set up test pack-------------------
            var firstPerson = CreateRandomPerson(1);
            var secondPerson = CreateRandomPerson(2);
            var thirdPerson = CreateRandomPerson(3);
            var forthPerson = CreateRandomPerson(4);

            await _db.Add(firstPerson, secondPerson, thirdPerson, forthPerson);

            var personService = CreatePersonService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await personService.GetPeopleAsync();
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            Assert.AreEqual(4, results.Count);
        }

        [Test]
        public async Task CreatePersonAsync_GivenAPerson_ShouldAddPersonToRepo()
        {
            //---------------Set up test pack-------------------
            var person = CreateRandomPerson(1);

            var personService = CreatePersonService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await personService.CreatePersonAsync(person);
            //---------------Test Result -----------------------
            var personFromRepo = await personService.GetPersonByIdAsync(person.Code);
            Assert.IsTrue(results);
            Assert.AreEqual(personFromRepo.Code, person.Code);
            Assert.AreEqual(personFromRepo.Name, person.Name);
            Assert.AreEqual(personFromRepo.Surname, person.Surname);
        }

        [Test]
        public async Task CreatePersonAsync_GivenPersonHasBeenSavedSuccessfully_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var person = CreateRandomPerson(1);
            var personService = CreatePersonService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await personService.CreatePersonAsync(person);
            //---------------Test Result -----------------------            
            Assert.IsTrue(results);
        }

        [Test]
        public async Task GetPersonByIdAsync_GivenNoPersonExist_ShouldReturnNull()
        {
            //---------------Set up test pack-------------------  
            var personService = CreatePersonService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await personService.GetPersonByIdAsync(1);
            //---------------Test Result -----------------------
            Assert.IsNull(results);
        }

        [Test]
        public async Task GetPersonByIdAsync_GivenPersonExistInRepo_ShouldReturnThatPerson()
        {
            //---------------Set up test pack-------------------
            var person = CreateRandomPerson(1);
            var personService = CreatePersonService();
            await _db.Add(person);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await personService.GetPersonByIdAsync(person.Code);
            //---------------Test Result -----------------------         
            Assert.AreEqual(results.Name, person.Name);
            Assert.AreEqual(results.Code, person.Code);
            Assert.AreEqual(results.IdNumber, person.IdNumber);
        }

        [Test]
        public async Task DeletePersonAsync_GivenNoPersonExist_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------            
            var personService = CreatePersonService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await personService.DeletePersonAsync(0);
            //---------------Test Result -----------------------            
            Assert.IsFalse(results);
        }

        [Test]
        public async Task UpdatePersonAsync_GivenPersonExistInRepo_ShouldUpdateThatPerson()
        {
            //---------------Set up test pack-------------------
            var person = CreateRandomPerson(1);
            await _db.Add(person);
            var personService = CreatePersonService();
            //---------------Assert Precondition----------------
            person.Name = "This has been updated";
            //---------------Execute Test ----------------------
            var results = await personService.UpdatePersonAsync(person);
            //---------------Test Result -----------------------         
            var personFromRepo = await personService.GetPersonByIdAsync(person.Code);
            Assert.AreEqual(personFromRepo.Name, "This has been updated");
        }

        [Test]
        public async Task UpdatePersonAsync_GivenPersonHasBeenUpdatedSuccessfully_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var person = CreateRandomPerson(1);
            await _db.Add(person);
            var personService = CreatePersonService();
            //---------------Assert Precondition----------------
            person.Name = "This has been updated";
            //---------------Execute Test ----------------------
            var results = await personService.UpdatePersonAsync(person);
            //---------------Test Result -----------------------                     
            Assert.IsTrue(results);
        }

        private PersonService CreatePersonService()
        {
            return new PersonService(_db.DbContext);
        }

        private static Person CreateRandomPerson(int id)
        {
            var person = new PersonBuilder().WithId(id).WithRandomProps().Build();
            return person;
        }
        public void Dispose()
        {
            _db.DbContext.Database.EnsureDeleted();
            _db.DbContext.Dispose();
        }
    }
}
