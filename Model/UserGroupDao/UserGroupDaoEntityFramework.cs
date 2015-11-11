using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserGroupDao
{
    class UserGroupDaoEntityFramework :
        GenericDaoEntityFramework<UserGroup, Int64>, IUserGroupDao
    {
        public List<UserGroup> FindGroupsByUserId(long userId, int startIndex, int count)
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

            return result;

        }

        public List<UserGroup> GetGroups(int startIndex, int count)
        {
            DbSet<UserGroup> groups = Context.Set<UserGroup>();

            List<UserGroup> result =
                (from g in groups
                 orderby g.groupId
                 select g).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public void SubscribeUserInGroup(long userId, long groupId)
        {
            DbSet<UserGroup> groups = Context.Set<UserGroup>();
            DbSet<UserProfile> users = Context.Set<UserProfile>();

            UserGroup group =
                (from g in groups
                 where g.groupId == groupId
                 select g).Single();

            UserProfile user = (from u in users
                                where u.usrId == userId
                                select u).Single();
           

            group.UserProfile.Add(user);

            /*No sabemos si aqui habra que añadir mas mierda*/

            Context.SaveChanges();

        }

        public void UnsubscribeUserInGroup(long userId, long groupId)
        {
            DbSet<UserGroup> groups = Context.Set<UserGroup>();
            DbSet<UserProfile> users = Context.Set<UserProfile>();

            UserGroup group =
                (from g in groups
                 where g.groupId == groupId
                 select g).Single();

            group.UserProfile.Remove((from u in users
                                    where u.usrId == userId
                                    select u).Single());

            /*No sabemos si aqui habra que añadir mas mierda*/

            Context.SaveChanges();
        }
    }
}
