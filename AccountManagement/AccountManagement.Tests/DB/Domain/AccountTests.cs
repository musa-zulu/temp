using AccountManagement.DB.Domain;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using System;
using System.Collections.Generic;

namespace AccountManagement.Tests.DB.Domain
{
    [TestFixture]
    public class AccountTests
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new Account());
            //---------------Test Result -----------------------
        }

        [TestCase("Code", typeof(int))]
        [TestCase("PersonCode", typeof(int))]
        [TestCase("AccountNumber", typeof(string))]
        [TestCase("OutstandingBalance", typeof(decimal))]
        [TestCase("PersonCodeNavigation", typeof(Person))]
        [TestCase("AccountStatus", typeof(ICollection<AccountStatus>))]
        [TestCase("Transactions", typeof(ICollection<Transaction>))]        
        public void Account_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(Account);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}
