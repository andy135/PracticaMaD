﻿using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
	public class UserProfileDaoEntityFramework :
        GenericDaoEntityFramework<UserProfile, Int64>, IUserProfileDao
    {

        public UserProfileDaoEntityFramework() { }

        #region IUserProfileDao Members

        public UserProfile FindByLoginName(string loginName)
        {
            UserProfile userProfile = null;

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            string sqlQuery = "Select * FROM UserProfile where loginName=@loginName";
            DbParameter loginNameParameter =
                new System.Data.SqlClient.SqlParameter("loginName", loginName);

            userProfile = Context.Database.SqlQuery<UserProfile>(sqlQuery, loginNameParameter).FirstOrDefault<UserProfile>();

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(UserProfile).FullName);

            return userProfile;
        }

        #endregion
    }

}

