using Es.Udc.DotNet.ModelUtil.Dao;
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
