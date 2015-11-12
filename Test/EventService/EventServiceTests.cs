using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Model.UserGroupService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserGroupDao;
using System.Transactions;
using System.Reflection;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserGroupService.Tests
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
            e.date = DateTime.Now;
            e.review = EVENT_REVIEW;
            e.categoryId = GetValidCategory("Deportes");
            eventDao.Create(e);

            return e;
        }

        private Event GetValidEvent(String name, long categoryId)
        {
            Event e = new Event();
            e.eventName = name;
            e.date = DateTime.Now;
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

            List<String> keys = new List<string>();

            Assert.IsTrue(eventDao.FindEvents(keys, cat, 0, 10).Count == 0);

            keys.Add("h");

            Assert.IsTrue(eventDao.FindEvents(keys, cat, 0, 10).Count>0);

            Assert.IsTrue(eventDao.FindEvents(keys, NOT_VALID_CATEGORY_ID, 0, 10).Count == 0);

        }

        [TestMethod()]
        public void FindEventsNullCategoryTest()
        {
            long cat = GetValidCategory("Games");
            long cat1 = GetValidCategory("Books");
            GetValidEvent("hola", cat);
            GetValidEvent("adiohs", cat1);

            List<String> keys = new List<string>();

            Assert.IsTrue(eventDao.FindEvents(keys, cat, 0, 10).Count == 0);

            keys.Add("h");

            Assert.IsTrue(eventDao.FindEvents(keys, cat, 0, 10).Count == 1);
            Assert.IsTrue(eventDao.FindEvents(keys, cat1, 0, 10).Count == 1);
            Assert.IsTrue(eventDao.FindEvents(keys, null, 0, 10).Count == 2);
            Assert.IsTrue(eventDao.FindEvents(keys, NOT_VALID_CATEGORY_ID, 0, 10).Count == 0);
        }

        [TestMethod()]
        public void FindEventsNullKeysTest()
        {
            long cat = GetValidCategory("Games");
            GetValidEvent("hola", cat);
            GetValidEvent("adios", cat);

            Assert.IsTrue(eventDao.FindEvents(null, cat, 0, 10).Count == 2);

        }

        [TestMethod()]
        public void FindEventsVoidKeysTest()
        {
            long cat = GetValidCategory("Games");
            GetValidEvent("hola", cat);
            GetValidEvent("adios", cat);

            List<String> keys = new List<string>();

            Assert.IsTrue(eventDao.FindEvents(keys, cat, 0, 10).Count == 2);
        }

    }
}