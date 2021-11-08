using AccountManagement.DB.Domain;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using System;

namespace AccountManagement.Tests.DB.Domain
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new Transaction());
            //---------------Test Result -----------------------
        }

        [TestCase("Code", typeof(int))]
        [TestCase("AccountCode", typeof(int))]
        [TestCase("TransactionDate", typeof(DateTime))]
        [TestCase("CaptureDate", typeof(DateTime))]
        [TestCase("Amount", typeof(decimal))]
        [TestCase("Description", typeof(string))]
        [TestCase("Account", typeof(Account))]
        public void Transaction_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(Transaction);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}
