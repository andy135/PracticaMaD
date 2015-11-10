using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.RecomendationDao;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecomendationService
{
    public class RecomendationService : IRecomendationService
    {
        [Dependency]
        public IRecomendationDao RecomendationDao { private get; set; }

        public Recomendation CreateRecomendation(Recomendation recomendation)
        {
            RecomendationDao.Create(recomendation);
            return recomendation;
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
