using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecomendationDao
{
    class RecomendationDaoEntityFramework :
        GenericDaoEntityFramework<Recomendation, Int64>, IRecomendationDao
    {
		public long CreateRecomendationToGroups(long userId, long eventId, List<long> groupsId, string description)
		{
			DbSet<UserGroup> groups = Context.Set<UserGroup>();
			List<UserGroup> listgroups = null;

			foreach(long id in groupsId)
			{
				listgroups =
				(from g in groups
				 from u in g.UserProfile
				 where groupsId.Contains(g.groupId) && u.usrId == userId
				 select g).ToList<UserGroup>();

				if (listgroups.Count != groupsId.Count) throw new InstanceNotFoundException(id, typeof(UserGroup).FullName);

			}

			Recomendation r = new Recomendation();
			r.description = description;
			r.eventId = eventId;
			foreach(UserGroup group in listgroups)
			{
				r.UserGroup.Add(group);
			}
			r.date = DateTime.Now;
			Create(r);


			return r.recomendationId;
		}

		public List<Recomendation> FindRecomendationByUserId(long userId, int startIndex, int count)
        {
            DbSet<Recomendation> recomendations = Context.Set<Recomendation>();

            List<Recomendation> result =
                (from r in recomendations
                 from g in r.UserGroup
                 from u in g.UserProfile
                 where u.usrId == userId
                 orderby r.date
                 select r).Skip(startIndex).Take(count).ToList();

            return result;
        }
    }
}
