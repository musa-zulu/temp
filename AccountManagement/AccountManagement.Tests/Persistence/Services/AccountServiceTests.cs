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
    public class AccountServiceTests
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
                new AccountService(Substitute.For<IApplicationDbContext>()));
            //---------------Test Result -----------------------
        }

        [Test]
        public async Task GetAccountsAsync_GivenNoRecordsExist_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------            
            var accountService = CreateAccountService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await accountService.GetAccountsAsync();
            //---------------Test Result -----------------------
            Assert.IsEmpty(results);
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public async Task GetAccountsAsync_GivenOneAccountExist_ShouldReturnListWithThatAccount()
        {
            //---------------Set up test pack-------------------
            var account = CreateRandomAccount(1);
            await _db.Add(account);

            var accountService = CreateAccountService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await accountService.GetAccountsAsync();
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(account.AccountNumber, results[0].AccountNumber);
        }

        [Test]
        public async Task GetAccountsAsync_GivenTwoAccountsExist_ShouldReturnAListWithTwoAccounts()
        {
            //---------------Set up test pack-------------------
            var firstAccount = CreateRandomAccount(1);
            var secondAccount = CreateRandomAccount(2);

            await _db.Add(firstAccount, secondAccount);

            var accountService = CreateAccountService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await accountService.GetAccountsAsync();
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public async Task GetAccountsAsync_GivenManyAccountsExist_ShouldReturnAListWithAllAccounts()
        {
            //---------------Set up test pack-------------------
            var firstAccount = CreateRandomAccount(1);
            var secondAccount = CreateRandomAccount(2);
            var thirdAccount = CreateRandomAccount(3);
            var forthAccount = CreateRandomAccount(4);

            await _db.Add(firstAccount, secondAccount, thirdAccount, forthAccount);

            var accountService = CreateAccountService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await accountService.GetAccountsAsync();
            //---------------Test Result -----------------------
            Assert.IsNotNull(results);
            Assert.AreEqual(4, results.Count);
        }

        [Test]
        public async Task CreateAccountAsync_GivenAAccount_ShouldAddAccountToRepo()
        {
            //---------------Set up test pack-------------------
            var account = CreateRandomAccount(1);

            var accountService = CreateAccountService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await accountService.CreateAccountAsync(account);
            //---------------Test Result -----------------------
            var accountFromRepo = await accountService.GetAccountByIdAsync(account.Code);
            Assert.IsTrue(results);
            Assert.AreEqual(accountFromRepo.Code, account.Code);
            Assert.AreEqual(accountFromRepo.AccountNumber, account.AccountNumber);
        }

        [Test]
        public async Task CreateAccountAsync_GivenAccountHasBeenSavedSuccessfully_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var account = CreateRandomAccount(1);
            var accountService = CreateAccountService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await accountService.CreateAccountAsync(account);
            //---------------Test Result -----------------------            
            Assert.IsTrue(results);
        }

        [Test]
        public async Task GetAccountByIdAsync_GivenNoAccountExist_ShouldReturnNull()
        {
            //---------------Set up test pack-------------------  
            var accountService = CreateAccountService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await accountService.GetAccountByIdAsync(1);
            //---------------Test Result -----------------------
            Assert.IsNull(results);
        }

        [Test]
        public async Task GetAccountByIdAsync_GivenAccountExistInRepo_ShouldReturnThatAccount()
        {
            //---------------Set up test pack-------------------
            var account = CreateRandomAccount(1);
            var accountService = CreateAccountService();
            await _db.Add(account);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await accountService.GetAccountByIdAsync(account.Code);
            //---------------Test Result -----------------------         
            Assert.AreEqual(results.AccountNumber, account.AccountNumber);
            Assert.AreEqual(results.Code, account.Code);
        }

        [Test]
        public async Task DeleteAccountAsync_GivenNoAccountExist_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------            
            var accountService = CreateAccountService();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var results = await accountService.DeleteAccountAsync(0);
            //---------------Test Result -----------------------            
            Assert.IsFalse(results);
        }

        [Test]
        public async Task UpdateAccountAsync_GivenAccountExistInRepo_ShouldUpdateThatAccount()
        {
            //---------------Set up test pack-------------------
            var account = CreateRandomAccount(1);
            await _db.Add(account);
            var accountService = CreateAccountService();
            //---------------Assert Precondition----------------
            account.AccountNumber = "This has been updated";
            //---------------Execute Test ----------------------
            var results = await accountService.UpdateAccountAsync(account);
            //---------------Test Result -----------------------         
            var accountFromRepo = await accountService.GetAccountByIdAsync(account.Code);
            Assert.AreEqual(accountFromRepo.AccountNumber, "This has been updated");
        }

        [Test]
        public async Task UpdateAccountAsync_GivenAccountHasBeenUpdatedSuccessfully_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var account = CreateRandomAccount(1);
            await _db.Add(account);
            var accountService = CreateAccountService();
            //---------------Assert Precondition----------------
            account.AccountNumber = "This has been updated";
            //---------------Execute Test ----------------------
            var results = await accountService.UpdateAccountAsync(account);
            //---------------Test Result -----------------------                     
            Assert.IsTrue(results);
        }

        private AccountService CreateAccountService()
        {
            return new AccountService(_db.DbContext);
        }

        private static Account CreateRandomAccount(int id)
        {
            var account = new AccountBuilder().WithId(id).WithRandomProps().Build();
            return account;
        }
        public void Dispose()
        {
            _db.DbContext.Database.EnsureDeleted();
            _db.DbContext.Dispose();
        }
    }
}