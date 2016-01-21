using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.RecomendationService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecomendationDao
{
	class RecomendationDaoEntityFramework :
        GenericDaoEntityFramework<Recomendation, Int64>, IRecomendationDao
    {
		public long CreateRecomendationToGroups(long userId, long eventId, List<long> groupsId, string description)
		{
			DbSet<UserGroup> groups = Context.Set<UserGroup>();
            List<UserGroup> listgroups = new List<UserGroup>();
                /*
				(from g in groups
				 where groupsId.Contains(g.groupId)
				 select g).ToList<UserGroup>();
                 */

            foreach(long id in groupsId)
            {
                UserGroup gr = (
                    from g in groups
                    where g.groupId == id
                    select g).Single();

                listgroups.Add(gr);
            }

			Recomendation r = new Recomendation();
			r.description = description;
			r.eventId = eventId;
            r.UserGroup = listgroups;
            r.date = DateTime.Now;
            Create(r);

            return r.recomendationId;
		}

		public List<RecomendationInfo> FindRecomendationByUserId(long userId, int startIndex, int count)
        {
            DbSet<Recomendation> recomendations = Context.Set<Recomendation>();

            List<Recomendation> result =
                (from r in recomendations
                 from g in r.UserGroup
                 from u in g.UserProfile
                 where u.usrId == userId
                 select r).Distinct().OrderByDescending(x => x.date).Skip(startIndex).Take(count).ToList();

            List<RecomendationInfo> recinfo = new List<RecomendationInfo>();
            RecomendationInfo ri;
            foreach (Recomendation r in result)
            {
                ri = new RecomendationInfo(r.recomendationId, r.date, r.description, r.eventId, r.Event.eventName,r.Event.Comment.Count>0);
                recinfo.Add(ri);
            }

            return recinfo;
        }
    }
}
