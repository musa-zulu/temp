using AccountManagement.DB.Dtos;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using System;
using System.Collections.Generic;

namespace AccountManagement.Tests.DB.Dtos
{
    [TestFixture]
    public class PersonDtoTests
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new PersonDto());
            //---------------Test Result -----------------------
        }

        [TestCase("Code", typeof(int))]
        [TestCase("Name", typeof(string))]
        [TestCase("Surname", typeof(string))]
        [TestCase("IdNumber", typeof(string))]
        [TestCase("Accounts", typeof(ICollection<AccountDto>))]
        public void PersonDto_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(PersonDto);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}
