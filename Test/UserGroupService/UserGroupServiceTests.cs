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

        // Variables used in several tests initialize here
        private const long NON_EXISTENT_USER_ID = -1;

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


        private UserProfile GetValidUserProfile()
        {
            long userId = userService.RegisterUser("andy.qmelo", "Password",
               new UserProfileDetails("Andy", "Quintero", "andy.qmelo@udc.es", "es", "ES"));

            return userProfileDao.Find(userId);
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
            UserGroup g = new UserGroup();
            g.groupName = "Amigos";
            g.description = "Grupo de mis amigos";
            UserGroup result = userGroupService.CreateGroup(g);

            GroupBlock block = userGroupService.GetAllGroups(0, 10);

            Assert.IsTrue(block.Groups.Count > 0);
            Assert.IsTrue(block.Groups.ElementAt(0).Name == result.groupName);
        }

        [TestMethod()]
        public void GetGroupsByUserTest()
        {

            UserProfile u = GetValidUserProfile();
            UserGroup g = new UserGroup();
            g.groupName = "Amigos";
            g.description = "Grupo de mis amigos";
            g.UserProfile.Add(u);
            userGroupService.CreateGroup(g);

            UserGroup g2 = new UserGroup();
            g2.groupName = "Familia";
            g2.description = "Grupo de mi familia";
            g2.UserProfile.Add(u);
            userGroupService.CreateGroup(g2);
            GroupBlock result = userGroupService.GetGroupsByUser(u.usrId,0,10);

            Assert.IsTrue(result.Groups.Count == 2);
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void GetGroupsOfInvalidUserTest()
        {

            userGroupService.GetGroupsByUser(NON_EXISTENT_USER_ID, 0, 10);

        }

        [TestMethod()]
        public void SubscribeUserToGroupTest()
        {
            UserProfile u = GetValidUserProfile();

            UserGroup g = new UserGroup();
            g.groupName = "Amigos";
            g.description = "Grupo de mis amigos";
            userGroupService.CreateGroup(g);

            userGroupService.SubscribeUserToGroup(u.usrId, g.groupId);

            UserGroup result = userGroupDao.Find(g.groupId);

            Assert.AreEqual(result.UserProfile.ElementAt(0), u);

        }

        [TestMethod()]
        public void UnsubscribeUserToGroupTest()
        {
            UserProfile u = GetValidUserProfile();

            UserGroup g = new UserGroup();
            g.groupName = "Amigos";
            g.description = "Grupo de mis amigos";
            userGroupDao.Create(g);

            UserGroup result;

            userGroupService.SubscribeUserToGroup(u.usrId, g.groupId);

            result = userGroupDao.Find(g.groupId);

            Assert.IsTrue(result.UserProfile.Count == 1);

            userGroupService.UnsubscribeUserToGroup(u.usrId, g.groupId);

            result = userGroupDao.Find(g.groupId);

            Assert.IsTrue(result.UserProfile.Count == 0);
        }
    }
}