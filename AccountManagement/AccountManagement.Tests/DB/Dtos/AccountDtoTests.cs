using AccountManagement.DB.Dtos;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using System;
using System.Collections.Generic;

namespace AccountManagement.Tests.DB.Dtos
{
    [TestFixture]
    public class AccountDtoTests
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new AccountDto());
            //---------------Test Result -----------------------
        }

        [TestCase("Code", typeof(int))]
        [TestCase("PersonCode", typeof(int))]
        [TestCase("AccountNumber", typeof(string))]
        [TestCase("OutstandingBalance", typeof(decimal))]
        [TestCase("Person", typeof(PersonDto))]
        [TestCase("AccountStatus", typeof(ICollection<AccountStatusDto>))]
        [TestCase("Transactions", typeof(ICollection<TransactionDto>))]
        public void AccountDto_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(AccountDto);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}
