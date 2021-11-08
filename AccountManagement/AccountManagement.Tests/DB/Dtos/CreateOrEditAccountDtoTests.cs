using AccountManagement.DB.Dtos;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using System;

namespace AccountManagement.Tests.DB.Dtos
{
    [TestFixture]
    public class CreateOrEditAccountDtoTests
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new CreateOrEditAccountDto());
            //---------------Test Result -----------------------
        }

        [TestCase("Code", typeof(int))]
        [TestCase("PersonCode", typeof(int))]
        [TestCase("AccountNumber", typeof(string))]
        [TestCase("OutstandingBalance", typeof(decimal))]
        [TestCase("Person", typeof(PersonDto))]
        public void CreateOrEditAccountDto_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(CreateOrEditAccountDto);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}
