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

        /// <summary>
        /// Creates a recomendation of an event to a group.
        /// </summary>
        /// <param name="recomendation">The recomendation to get stored.</param>
        [Transactional]
        Recomendation CreateRecomendation(Recomendation recomendation);

        /// <summary>
        /// Find the recomendations of an user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="startIndex">the index (starting from 0) of the first 
        /// object to return.</param>
        /// <param name="count">The maximum number of objects to return.</param>
        [Transactional]
        RecomendationBlock GetRecomendationsByUser(long userId, int startIndex, int count);
    }
}
