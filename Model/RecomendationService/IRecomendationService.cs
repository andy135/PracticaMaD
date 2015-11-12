using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.RecomendationDao;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecomendationService
{
    public interface IRecomendationService
    {
        [Dependency]
        IRecomendationDao RecomendationDao { set; }

        [Transactional]
        long CreateRecomendation(long eventId, long groupId, String description);

        [Transactional]
        RecomendationBlock GetRecomendationsByUser(long userId, int startIndex, int count);
    }
}
