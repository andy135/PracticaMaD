﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.UserGroupDao;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserGroupService
{
    public class UserGroupService : IUserGroupService
    {
        [Dependency]
        public IUserGroupDao UserGroupDao { private get; set; }

        [Dependency]
        public IUserProfileDao UserProfileDao { private get; set; }

        public long CreateGroup(long userId, String name, String description)
        {
            UserGroup group = new UserGroup();
            UserProfile user = UserProfileDao.Find(userId);
            if (user == null) throw new InstanceNotFoundException(user, typeof(UserProfile).FullName);
            group.UserProfile.Add(user);
            group.groupName = name;
            group.description = description;

            UserGroupDao.Create(group);
            return group.groupId;
        }

        public GroupBlock GetAllGroups(int startIndex, int count)
        {
            List<UserGroup> groups = UserGroupDao.GetGroups(startIndex, count+1);

            bool existMoreGroups = (groups.Count == count + 1);

            if (existMoreGroups)
                groups.RemoveAt(count);

            List<GroupInfo> groupsinfo = new List<GroupInfo>();
            GroupInfo gi;
            foreach(UserGroup g in groups) {
                gi = new GroupInfo(g.groupId, g.groupName, g.description, g.UserProfile.Count, g.Recomendation.Count);
                groupsinfo.Add(gi);
            }

            return new GroupBlock(groupsinfo, existMoreGroups);

        }

        public GroupBlock GetGroupsByUser(long userId, int startIndex, int count)
        {
            List<UserGroup> groups = UserGroupDao.FindGroupsByUserId(userId ,startIndex, count + 1);

            bool existMoreGroups = (groups.Count == count + 1);

            if (existMoreGroups)
                groups.RemoveAt(count);

            List<GroupInfo> groupsinfo = new List<GroupInfo>();
            GroupInfo gi;
            foreach (UserGroup g in groups)
            {
                gi = new GroupInfo(g.groupId, g.groupName, g.description, g.UserProfile.Count, g.Recomendation.Count);
                groupsinfo.Add(gi);
            }

            return new GroupBlock(groupsinfo, existMoreGroups);
        }

        public void SubscribeUserToGroup(long userId, long groupId)
        {
            UserGroupDao.SubscribeUserInGroup(userId, groupId);
        }

        public void UnsubscribeUserToGroup(long userId, long groupId)
        {
            UserGroupDao.UnsubscribeUserInGroup(userId, groupId);
        }
    }
}
