using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.UserGroupService;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserGroupDao
{
	public interface IUserGroupDao : IGenericDao<UserGroup, Int64>
    {

        List<GroupInfo> FindGroupsByUserId(long userId, int startIndex, int count);

        void SubscribeUserInGroup(long userId, long groupId);

        void UnsubscribeUserInGroup(long userId, long groupId);

        List<GroupInfo> GetGroups(int startIndex, int count);
        bool isMember(long? userId, long groupId);
    }
}
