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

namespace Es.Udc.DotNet.PracticaMaD.Model.UserGroupService.Tests
{
    [TestClass()]
    public class EventServiceTests
    {
        private static IUnityContainer container;
        private static IEventService eventService;
        private static IEventDao eventDao;

        TransactionScope transaction;

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        // Variables used in several tests initialize here
        

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            Console.WriteLine(Assembly.GetExecutingAssembly().ToString());
            container = TestManager.ConfigureUnityContainer("unity");
            eventDao = container.Resolve<IEventDao>();
            eventService = container.Resolve<IEventService>();

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

        // -------------------------- TESTS ----------------------------

        [TestMethod()]
        public void SimpleFindEventsTest()
        {
                        
            

        }

        [TestMethod()]
        public void FindEventsNullCategoryTest()
        {



        }

        [TestMethod()]
        public void FindEventsNullKeysTest()
        {



        }

        [TestMethod()]
        public void FindEventsVoidKeysTest()
        {



        }

    }
}