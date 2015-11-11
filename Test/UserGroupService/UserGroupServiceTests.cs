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
        private const long NON_EXISTENT_GROUP_ID = -1;
        private const String GROUP_NAME = "Grupo";
        private const String GROUP_DESCRIPTION = "Descripcion cutre";

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
                        
            long result = userGroupService.CreateGroup(GetValidUserProfile().usrId,GROUP_NAME, GROUP_DESCRIPTION);

            UserGroup result2 = userGroupDao.Find(result);

            Assert.IsTrue(result >= 0);
            Assert.AreEqual(result2.groupId, result);
            Assert.AreEqual(result2.groupName, GROUP_NAME);
            Assert.AreEqual(result2.description, GROUP_DESCRIPTION);
        }

        [TestMethod()]
        public void GetAllGroupsTest()
        {
            long result = userGroupService.CreateGroup(GetValidUserProfile().usrId, GROUP_NAME, GROUP_DESCRIPTION);

            GroupBlock block = userGroupService.GetAllGroups(0, 10);

            Assert.IsTrue(block.Groups.Count > 0);
            Assert.IsTrue(block.Groups.ElementAt(0).GroupId == result);
        }

        [TestMethod()]
        public void GetGroupsByUserTest()
        {

            long user = GetValidUserProfile().usrId;

            long result = userGroupService.CreateGroup(user, GROUP_NAME, GROUP_DESCRIPTION);

            long result2 = userGroupService.CreateGroup(user, GROUP_NAME, GROUP_DESCRIPTION);

            long result3 = userGroupService.CreateGroup(user, GROUP_NAME, GROUP_DESCRIPTION);

            GroupBlock block = userGroupService.GetGroupsByUser(user, 0, 10);

            Assert.IsTrue(block.Groups.Count == 3);
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

            long result = userGroupService.CreateGroup(u.usrId, GROUP_NAME, GROUP_DESCRIPTION);

            userGroupService.SubscribeUserToGroup(u.usrId, result);

            UserGroup group = userGroupDao.Find(result);

            Assert.AreEqual(group.UserProfile.ElementAt(0), u);

        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void SubscribeInvalidUserToGroupTest()
        {
            UserProfile u = GetValidUserProfile();

            long result = userGroupService.CreateGroup(u.usrId, GROUP_NAME, GROUP_DESCRIPTION);

            userGroupService.SubscribeUserToGroup(NON_EXISTENT_USER_ID, result);

        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void SubscribeUserToInvalidGroupTest()
        {
            UserProfile u = GetValidUserProfile();
            userGroupService.SubscribeUserToGroup(u.usrId, NON_EXISTENT_GROUP_ID);

        }

        [TestMethod()]
        public void UnsubscribeUserToGroupTest()
        {
            UserProfile u = GetValidUserProfile();

            long result = userGroupService.CreateGroup(u.usrId, GROUP_NAME, GROUP_DESCRIPTION);

            userGroupService.UnsubscribeUserToGroup(u.usrId, result);

            UserGroup group = userGroupDao.Find(result);

            Assert.IsTrue(group.UserProfile.Count == 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UnsubscribeInvalidUserToGroupTest()
        {
            UserProfile u = GetValidUserProfile();

            long result = userGroupService.CreateGroup(u.usrId, GROUP_NAME, GROUP_DESCRIPTION);

            userGroupService.UnsubscribeUserToGroup(NON_EXISTENT_USER_ID, result);

        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void UnsubscribeUserToInvalidGroupTest()
        {
            UserProfile u = GetValidUserProfile();
            userGroupService.UnsubscribeUserToGroup(u.usrId, NON_EXISTENT_GROUP_ID);

        }
    }
}