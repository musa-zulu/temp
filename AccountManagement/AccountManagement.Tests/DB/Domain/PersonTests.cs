using AccountManagement.DB.Domain;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using System;
using System.Collections.Generic;

namespace AccountManagement.Tests.DB.Domain
{
    [TestFixture]
    public class PersonTests
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new Person());
            //---------------Test Result -----------------------
        }

        [TestCase("Code", typeof(int))]
        [TestCase("Name", typeof(string))]
        [TestCase("Surname", typeof(string))]
        [TestCase("IdNumber", typeof(string))]
        [TestCase("Accounts", typeof(ICollection<Account>))]
        public void Person_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(Person);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}
