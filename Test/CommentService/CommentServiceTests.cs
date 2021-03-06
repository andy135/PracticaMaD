﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using System.Reflection;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService.Tests
{
	[TestClass()]
	public class CommentServiceTests
	{
		private static IUnityContainer container;
		private static IEventService eventService;
		private static IEventDao eventDao;
		private static ICategoryDao categoryDao;
		private static IUserService userService;
		private static IUserProfileDao userProfileDao;
		private static ICommentService commentService;
		private static ICommentDao commentDao;
		private static ITagDao tagDao;


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
			userProfileDao = container.Resolve<IUserProfileDao>();
			userService = container.Resolve<IUserService>();
			commentDao = container.Resolve<ICommentDao>();
			commentService = container.Resolve<ICommentService>();
			tagDao = container.Resolve<ITagDao>();

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

		private UserProfile GetValidUserProfile()
		{
			long userId = userService.RegisterUser("andy.qmelo", "Password",
			   new UserProfileDetails("Andy", "Quintero", "andy.qmelo@udc.es", "es", "ES"));

			return userProfileDao.Find(userId);
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
		public void DeleteCommentTest()
		{
			Event e = GetValidEvent("Clásico");
			UserProfile u = GetValidUserProfile();

			long c = commentService.DoComment(u.usrId, e.eventId, "Ganará el Barcelona");

			Comment result = commentDao.Find(c);

			Assert.AreEqual(result.commentId, c);

			commentService.DeleteComment(c);

			Assert.IsFalse(commentDao.Exists(c));

		}

		[TestMethod()]
		public void DoCommentTest()
		{
			Event e = GetValidEvent("Clásico");
			UserProfile u = GetValidUserProfile();

			long c = commentService.DoComment(u.usrId, e.eventId, "Ganará el Barcelona");

			Comment result = commentDao.Find(c);

			Assert.AreEqual(result.commentId, c);
			Assert.AreEqual(result.eventId, e.eventId);
			Assert.AreEqual(result.usrId, u.usrId);
			Assert.AreEqual(result.texto, "Ganará el Barcelona");
		}

		[TestMethod()]
		public void GetCommentsOfEventTest()
		{
			Event e = GetValidEvent("Clásico");
			UserProfile u = GetValidUserProfile();

			long c1 = commentService.DoComment(u.usrId, e.eventId, "Ganará el Barcelona");
			long c2 = commentService.DoComment(u.usrId, e.eventId, "Ganará el Madrid");
			long c3 = commentService.DoComment(u.usrId, e.eventId, "Empatan");

			CommentBlock block = commentService.GetCommentsOfEvent(e.eventId, 0, 10);

			Assert.AreEqual(block.Comments.Count, 3);
		}

		[TestMethod()]
		public void Get2CommentsOfEventTest()
		{
			Event e = GetValidEvent("Clásico");
			Event e1 = GetValidEvent("Run");
			UserProfile u = GetValidUserProfile();

			long c1 = commentService.DoComment(u.usrId, e.eventId, "Ganará el Barcelona");
			long c2 = commentService.DoComment(u.usrId, e.eventId, "Ganará el Madrid");
			long c3 = commentService.DoComment(u.usrId, e1.eventId, "Empatan");

			CommentBlock block = commentService.GetCommentsOfEvent(e.eventId, 0, 10);

			Assert.AreEqual(block.Comments.Count, 2);
		}

		[TestMethod()]
		public void ModifyCommentTest()
		{
			Event e = GetValidEvent("Clásico");
			UserProfile u = GetValidUserProfile();

			long c = commentService.DoComment(u.usrId, e.eventId, "Ganará el Barcelona");

			commentService.ModifyComment(c, "Ganará el Madrid");

			Comment result = commentDao.Find(c);

			Assert.IsTrue(result.texto.Contains("Madrid"));

		}

		[TestMethod()]
		public void GetTopNTagsTest()
		{
			Event e = GetValidEvent("Partido");
			Event e1 = GetValidEvent("Run");
			UserProfile u = GetValidUserProfile();
			List<String> tags = new List<String>();
			tags.Add("match");
			tags.Add("tv");
			commentService.DoCommentWithTags(u.usrId, e.eventId, "Mola", tags);
			commentService.DoCommentWithTags(u.usrId, e.eventId, "Es de lo mejor", tags);
			tags.Remove("match");
			tags.Add("football");
			commentService.DoCommentWithTags(u.usrId, e1.eventId, "Shiiiiit", tags);

			List<Tag> result = commentService.GetTopNTags(10);

			Assert.AreEqual(result.Count, 3);

		}

		[TestMethod()]
		public void DoCommentWithTagsTest()
		{
			Event e = GetValidEvent("Partido");
			UserProfile u = GetValidUserProfile();
			List<String> tags = new List<String>();
			tags.Add("match");
			tags.Add("tv");
			long commentId = commentService.DoCommentWithTags(u.usrId, e.eventId, "Mola", tags);

			Comment c = commentDao.Find(commentId);

			Assert.IsTrue(c.Tag.Count == 2);
		}

		[TestMethod()]
		public void GetCommentsByTagTest()
		{
			Event e = GetValidEvent("Partido");
			UserProfile u = GetValidUserProfile();
			List<String> tags = new List<String>();
			tags.Add("match");
			tags.Add("tv");
			long commentId = commentService.DoCommentWithTags(u.usrId, e.eventId, "Mola", tags);
			Tag t = tagDao.FindTagByText("tv");

			CommentBlock block = commentService.GetCommentsByTag(t.tagId, 0, 10);

			Assert.IsTrue(block.Comments.First().commentId == commentId);
		}

		[TestMethod()]
		public void ModifyCommentWithTagsTest()
		{
			Event e = GetValidEvent("Important Match");
			UserProfile u = GetValidUserProfile();
			List<String> tags = new List<String>();
			tags.Add("match");
			tags.Add("tv");
			long commentId = commentService.DoCommentWithTags(u.usrId, e.eventId, "Mola", tags);

			Comment c = commentDao.Find(commentId);

			Assert.IsTrue(c.Tag.Count == 2);

			tags.Add("football");
			tags.Add("victory");
			tags.Remove("tv");

			commentService.ModifyCommentWithTags(commentId, "No Mola", tags);

			c = commentDao.Find(commentId);

            Assert.IsTrue(c.texto == "No Mola");
            Assert.IsTrue(c.Tag.Count == 3);
		}
	}
}