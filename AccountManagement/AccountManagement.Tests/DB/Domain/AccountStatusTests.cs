using AccountManagement.DB.Domain;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using System;

namespace AccountManagement.Tests.DB.Domain
{
    [TestFixture]
    public class AccountStatusTests
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new AccountStatus());
            //---------------Test Result -----------------------
        }

        [TestCase("Code", typeof(int))]
        [TestCase("Status", typeof(string))]
        [TestCase("AccountCode", typeof(int))]
        [TestCase("Account", typeof(Account))]
        public void AccountStatus_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(AccountStatus);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}
