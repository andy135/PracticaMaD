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

namespace Es.Udc.DotNet.PracticaMaD.Model.UserGroupService.Tests
{
    [TestClass()]
    public class UserGroupServiceTests
    {
        private static IUnityContainer container;
        private static IUserService userService;
        private static IUserProfileDao userProfileDao;
        private static IUserGroupService userGroupService;
        private static IUserGroupDao userGroupDao;

        TransactionScope transaction;

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

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
        public void CreateGroupTest()
        {
            UserGroup g = new UserGroup();
            g.groupName = "Amigos";
            g.description = "Grupo de mis amigos";
            UserGroup result = userGroupService.CreateGroup(g);

            UserGroup result2 = userGroupDao.Find(result.groupId);

            Assert.IsTrue(result.groupId > 0);
            Assert.AreEqual(result2.groupId, result.groupId);
        }

        [TestMethod()]
        public void GetAllGroupsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetGroupsByUserTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SubscribeUserToGroupTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UnsubscribeUserToGroupTest()
        {
            Assert.Fail();
        }
    }
}