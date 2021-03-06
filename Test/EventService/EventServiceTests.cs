﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Practices.Unity;
using System.Transactions;
using System.Reflection;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService.Tests
{
	[TestClass()]
    public class EventServiceTests
    {
        private static IUnityContainer container;
        private static IEventService eventService;
        private static IEventDao eventDao;
        private static ICategoryDao categoryDao;

        TransactionScope transaction;

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        // Variables used in several tests initialize here
        private const String EVENT_REVIEW = "Mejor evento del curso";
        private const long NOT_VALID_CATEGORY_ID = -1;


        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            Console.WriteLine(Assembly.GetExecutingAssembly().ToString());
            container = TestManager.ConfigureUnityContainer("unity");
            eventDao = container.Resolve<IEventDao>();
            eventService = container.Resolve<IEventService>();
            categoryDao = container.Resolve<ICategoryDao>();

        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearUnityContainer(container);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        private long GetValidCategory(String name)
        {
            Category c = new Category();
            c.categoryName = name;
            categoryDao.Create(c);

            return c.categoryId;
        }

        private Event GetValidEvent(String name)
        {
            Event e = new Event();
            e.eventName = name;
            e.date = DateTime.Now.AddDays(5);
            e.review = EVENT_REVIEW;
            e.categoryId = GetValidCategory("Deportes");
            eventDao.Create(e);

            return e;
        }

        private Event GetValidEvent(String name, long categoryId)
        {
            Event e = new Event();
            e.eventName = name;
            e.date = DateTime.Now.AddDays(5);
            e.review = EVENT_REVIEW;
            e.categoryId = categoryId;
            eventDao.Create(e);

            return e;
        }

        // -------------------------- TESTS ----------------------------

        [TestMethod()]
        public void SimpleFindEventsTest()
        {

            long cat = GetValidCategory("Games");
            GetValidEvent("hola",cat);

			String keys = "h";

            Assert.IsTrue(eventService.FindEvents(keys, cat, 0, 10).Events.Count >0);

            Assert.IsTrue(eventService.FindEvents(keys, NOT_VALID_CATEGORY_ID, 0, 10).Events.Count == 0);

        }

        [TestMethod()]
        public void FindEventsNullCategoryTest()
        {
            long cat = GetValidCategory("Games");
            long cat1 = GetValidCategory("Books");
            GetValidEvent("hola", cat);
            GetValidEvent("adiohs", cat1);

			String keys = "h";

			Assert.IsTrue(eventService.FindEvents(keys, cat, 0, 10).Events.Count == 1);
            Assert.IsTrue(eventService.FindEvents(keys, cat1, 0, 10).Events.Count == 1);
            Assert.IsTrue(eventService.FindEvents(keys, null, 0, 10).Events.Count == 2);
            Assert.IsTrue(eventService.FindEvents(keys, NOT_VALID_CATEGORY_ID, 0, 10).Events.Count == 0);
        }

        [TestMethod()]
        public void FindEventsNullKeysTest()
        {
            long cat = GetValidCategory("Games");
            GetValidEvent("hola", cat);
            GetValidEvent("adios", cat);

            Assert.IsTrue(eventService.FindEvents(null, cat, 0, 10).Events.Count == 2);

        }

		[TestMethod()]
		public void GetCategoriesTest()
		{
			GetValidCategory("Games");
			GetValidCategory("Books");
			GetValidCategory("Bikes");

			Assert.AreEqual(eventService.GetCategories().Count,3);

		}

        [TestMethod()]
        public void GetNumberOfEventsTest()
        {

            long cat = GetValidCategory("Games");
            GetValidEvent("hola", cat);

            String keys = "";

            Assert.AreEqual(eventService.GetNumberOfEvents(keys, cat), 1);

            GetValidEvent("hola1", cat);

            Assert.AreEqual(eventService.GetNumberOfEvents(keys, cat), 2);

        }

    }
}