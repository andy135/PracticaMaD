using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.RecomendationDao;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.UserGroupDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecomendationService
{
    public class RecomendationService : IRecomendationService
    {
        [Dependency]
        public IRecomendationDao RecomendationDao { private get; set; }
        [Dependency]
        public IUserGroupDao UserGroupDao { private get; set; }


        public long CreateRecomendation(long userId, long eventId, List<long> groupsId, string description)
        {
            return RecomendationDao.CreateRecomendationToGroups(userId,eventId,groupsId,description);
        }

		public long CreateRecomendation(long userId, long eventId, long groupId, string description)
		{
			List<long> groups = new List<long>();
			groups.Add(groupId);
			return RecomendationDao.CreateRecomendationToGroups(userId, eventId, groups, description);
		}

		public RecomendationBlock GetRecomendationsByUser(long userId, int startIndex, int count)
        {
            List<Recomendation> recomendations = RecomendationDao.FindRecomendationByUserId(userId, startIndex, count+1);

            bool existMoreRecomendations = (recomendations.Count == count + 1);

            if (existMoreRecomendations)
                recomendations.RemoveAt(count);

            return new RecomendationBlock(recomendations, existMoreRecomendations);
        }
    }
}
