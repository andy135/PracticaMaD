using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserGroupService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserGroupDao
{
	class UserGroupDaoEntityFramework :
        GenericDaoEntityFramework<UserGroup, Int64>, IUserGroupDao
    {
        /// <exception cref="InstanceNotFoundException"
        public List<GroupInfo> FindGroupsByUserId(long userId, int startIndex, int count)
        {
            DbSet<UserGroup> groups = Context.Set<UserGroup>();
            DbSet<UserProfile> users = Context.Set<UserProfile>();

            int user =
                (from u in users
                 where u.usrId == userId
                 select u).Count();

            if (user == 0)
                throw new InstanceNotFoundException(userId,
                    typeof(UserProfile).FullName);


            List<UserGroup> result =
                (from g in groups
                 from u in g.UserProfile
                 where u.usrId==userId
                 orderby g.groupId
                 select g).Skip(startIndex).Take(count).ToList();

			List<GroupInfo> groupsinfo = new List<GroupInfo>();
			GroupInfo gi;
			foreach (UserGroup g in result)
			{
				gi = new GroupInfo(g.groupId, g.groupName, g.description, g.UserProfile.Count, g.Recomendation.Count);
				groupsinfo.Add(gi);
			}

			return groupsinfo;

        }

        public List<GroupInfo> GetGroups(int startIndex, int count)
        {
            DbSet<UserGroup> groups = Context.Set<UserGroup>();

            List<UserGroup> result =
                (from g in groups
                 orderby g.groupId
                 select g).Skip(startIndex).Take(count).ToList();

			List<GroupInfo> groupsinfo = new List<GroupInfo>();
			GroupInfo gi;

			foreach (UserGroup g in result)
			{
				gi = new GroupInfo(g.groupId, g.groupName, g.description, g.UserProfile.Count, g.Recomendation.Count);
				groupsinfo.Add(gi);
			}

			return groupsinfo;
        }

        public bool isMember(long? userId, long groupId)
        {
            if (userId != null)
            {
                DbSet<UserGroup> groups = Context.Set<UserGroup>();
                DbSet<UserProfile> users = Context.Set<UserProfile>();

                UserProfile user = (from u in users
                                        where u.usrId == userId
                                        select u).Single();

                UserGroup group = (from g in groups
                     where g.groupId == groupId
                     select g).Single();

                if (user == null || group == null) return false;

                return group.UserProfile.Contains(user);

            }
            return false;
        }

        /// <exception cref="InstanceNotFoundException"
        public void SubscribeUserInGroup(long userId, long groupId)
        {
            DbSet<UserGroup> groups = Context.Set<UserGroup>();
            DbSet<UserProfile> users = Context.Set<UserProfile>();

            UserGroup group;
            UserProfile user;

            try {
                group =
                    (from g in groups
                        where g.groupId == groupId
                        select g).Single();
            }catch(Exception)
            {
                throw new InstanceNotFoundException(groupId, typeof(UserGroup).FullName);
            }

            try
            {
                user = (from u in users
                                    where u.usrId == userId
                                    select u).Single();
                
            }
            catch(Exception)
            {
                throw new InstanceNotFoundException(userId, typeof(UserProfile).FullName);
            }


            group.UserProfile.Add(user);


            /*No sabemos si aqui habra que añadir mas mierda*/

            Context.SaveChanges();

        }

        /// <exception cref="InstanceNotFoundException"
        public void UnsubscribeUserInGroup(long userId, long groupId)
        {
            DbSet<UserGroup> groups = Context.Set<UserGroup>();
            DbSet<UserProfile> users = Context.Set<UserProfile>();

            UserGroup group;
            UserProfile user;

            try {
                group =
                    (from g in groups
                     where g.groupId == groupId
                     select g).Single();
            }catch(Exception)
            {
                throw new InstanceNotFoundException(groupId, typeof(UserGroup).FullName);
            }

            try
            {
                user = (from u in users
                        where u.usrId == userId
                        select u).Single();
            }
            catch(Exception)
            {
                throw new InstanceNotFoundException(userId, typeof(UserProfile).FullName);
            }

            group.UserProfile.Remove(user);

            /*No sabemos si aqui habra que añadir mas mierda*/

            Context.SaveChanges();
        }
    }
}
