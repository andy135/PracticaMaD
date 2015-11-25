using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecomendationDao
{
    public interface IRecomendationDao : IGenericDao<Recomendation, Int64>
    {
        List<Recomendation> FindRecomendationByUserId(long userId, int startIndex, int count);

		long CreateRecomendationToGroups(long userId, long eventId, List<long> groupsId, string description);
    }
}
