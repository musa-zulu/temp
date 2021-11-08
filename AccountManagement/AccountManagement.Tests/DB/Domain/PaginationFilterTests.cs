﻿using AccountManagement.DB.Domain;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;
using System;

namespace AccountManagement.Tests.DB.Domain
{
    [TestFixture]
    public class PaginationFilterTests
    {
        [Test]
        public void Construct()
        {
            //---------------Set up test pack-------------------
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            Assert.DoesNotThrow(() => new PaginationFilter());
            //---------------Test Result -----------------------
        }

        [TestCase("PageNumber", typeof(int))]
        [TestCase("PageSize", typeof(int))]
        public void PaginationFilter_ShouldHaveProperty(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof(PaginationFilter);
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------
        }
    }
}
