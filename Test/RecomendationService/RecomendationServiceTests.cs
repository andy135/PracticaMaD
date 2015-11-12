using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Model.RecomendationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Test;
using System.Reflection;
using Es.Udc.DotNet.PracticaMaD.Model.RecomendationDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserGroupService;
using Es.Udc.DotNet.PracticaMaD.Model.UserGroupDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecomendationService.Tests
{
    [TestClass()]
    public class RecomendationServiceTests
    {

        private static IUnityContainer container;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;
        private static IUserGroupService userGroupService;
        private static IUserGroupDao userGroupDao;
        private static IRecomendationService recomendationService;
        private static IRecomendationDao recomendationDao;
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
        private const long NON_EXISTENT_USER_ID = -1;
        private const long NON_EXISTENT_GROUP_ID = -1;
        private const String GROUP_NAME = "Grupo";
        private const String GROUP_DESCRIPTION = "Descripcion cutre";
        private const String EVENT_REVIEW = "Mejor evento del curso";
        private const String RECOMENDATION_DESCRIPTION = "Es genial, lo recomiendo";

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            Console.WriteLine(Assembly.GetExecutingAssembly().ToString());
            container = TestManager.ConfigureUnityContainer("unity");
            userProfileDao = container.Resolve<IUserProfileDao>();
            userService = container.Resolve<IUserService>();
            userGroupDao = container.Resolve<IUserGroupDao>();
            userGroupService = container.Resolve<IUserGroupService>();
            recomendationDao = container.Resolve<IRecomendationDao>();
            recomendationService = container.Resolve<IRecomendationService>();
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

        private UserProfile GetValidUserProfile()
        {
            long userId = userService.RegisterUser("andy.qmelo", "Password",
               new UserProfileDetails("Andy", "Quintero", "andy.qmelo@udc.es", "es", "ES"));

            return userProfileDao.Find(userId);
        }

        private long GetValidCategory()
        {
            Category c = new Category();
            c.categoryName = "Deportes";
            categoryDao.Create(c);

            return c.categoryId;
        }

        private Event GetValidEvent(String name)
        {
            Event e = new Event();
            e.eventName = name;
            e.date = DateTime.Now;
            e.review = EVENT_REVIEW;
            e.categoryId = GetValidCategory();
            eventDao.Create(e);

            return e;
        }

        private UserGroup GetValidUserGroup()
        {
            long groupId = userGroupService.CreateGroup(GetValidUserProfile().usrId, GROUP_NAME, GROUP_DESCRIPTION);
            
            return userGroupDao.Find(groupId);
        }

        // ---------------------- TESTS -----------------------


        [TestMethod()]
        public void CreateRecomendationTest()
        {
            Event e = GetValidEvent("Evento 1");
            UserGroup g = GetValidUserGroup();
            long rId = recomendationService.CreateRecomendation(e.eventId, g.groupId, RECOMENDATION_DESCRIPTION);

            Recomendation r = recomendationDao.Find(rId);

            Assert.AreEqual(r.recomendationId, rId);
            Assert.AreEqual(r.eventId, e.eventId);
            Assert.AreEqual(r.description, RECOMENDATION_DESCRIPTION);

        }

        [TestMethod()]
        public void GetRecomendationsByUserTest()
        {
            Event e = GetValidEvent("Evento 1");
            Event e2 = GetValidEvent("Evento 2");
            UserGroup g = GetValidUserGroup();
            long rId = recomendationService.CreateRecomendation(e.eventId, g.groupId, RECOMENDATION_DESCRIPTION);
            long rId2 = recomendationService.CreateRecomendation(e2.eventId, g.groupId, RECOMENDATION_DESCRIPTION);

            RecomendationBlock result = recomendationService.GetRecomendationsByUser(g.UserProfile.ElementAt(0).usrId, 0, 10);

            Assert.IsTrue(result.Recomendations.Count == 2);
        }
    }
}