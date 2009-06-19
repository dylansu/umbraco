﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;
using umbraco.Test;

namespace umbraco.Linq.Core.Tests
{
    /// <summary>
    /// Summary description for ChildTest
    /// </summary>
    [TestClass, DeploymentItem("umbraco.config")]
    public class ChildTest
    {
        public ChildTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod, Isolated]
        public void ChildTest_ChildExist()
        {
            MockHelpers.SetupFakeHttpContext();
            using (var ctx = new MyumbracoDataContext())
            {
                var pages = ctx.CwsHomes.First().CwsTextpages;

                Assert.AreNotEqual(0, pages.Count());
            }
        }

        [TestMethod, Isolated]
        public void ChildTest_ChildToParent()
        {
            MockHelpers.SetupFakeHttpContext();
            using (var ctx = new MyumbracoDataContext())
            {
                var page = ctx.CwsHomes.First().CwsTextpages.First();

                Assert.AreEqual(ctx.CwsHomes.First().Id, page.Parent<CwsHome>().Id);
            }
        }

        [TestMethod, Isolated]
        public void ChildTest_AllChildren()
        {
            MockHelpers.SetupFakeHttpContext();
            using (var ctx = new MyumbracoDataContext())
            {
                var hp = ctx.CwsHomes.First();

                Assert.IsTrue(hp.Children.Count() > 0);
                Assert.IsInstanceOfType(hp.Children.First(), typeof(CwsTextpage));
                Assert.IsInstanceOfType(hp.Children.Skip(1).First(), typeof(CwsGallerylist));
            }
        }
    }
}
