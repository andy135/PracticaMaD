using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.UserGroupDao;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserGroupService
{
    public interface IUserGroupService
    {
        [Dependency]
        IUserGroupDao UserGroupDao { set; }

        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="group">Group to create.</param>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        UserGroup CreateGroup(UserGroup group);

        /// <summary>
        /// Find all groups.
        /// </summary>
        /// <param name="startIndex">the index (starting from 0) of the first 
        /// object to return.</param>
        /// <param name="count">The maximum number of objects to return.</param>
        [Transactional]
        GroupBlock GetAllGroups(int startIndex, int count);

        /// <summary>
        /// Find groups by user.
        /// </summary>
        /// <param name="startIndex">the index (starting from 0) of the first 
        /// object to return.</param>
        /// <param name="count">The maximum number of objects to return.</param>
        /// <param name="userId">Identifier of the user.</param>
        [Transactional]
        GroupBlock GetGroupsByUser(long userId, int startIndex, int count);

        /// <summary>
        /// Subscribes a user to a group.
        /// </summary>
        /// <param name="userId">Identifier of the user.</param>
        /// <param name="groupId">Identifier of the group.</param>
        [Transactional]
        void SubscribeUserToGroup(long userId, long groupId);

        /// <summary>
        /// Unsubscribes a user to a group.
        /// </summary>
        /// <param name="userId">Identifier of the user.</param>
        /// <param name="groupId">Identifier of the group.</param>
        [Transactional]
        void UnsubscribeUserToGroup(long userId, long groupId);
    }
}
