﻿using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.RecomendationDao;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecomendationService
{
	public interface IRecomendationService
    {
        [Dependency]
        IRecomendationDao RecomendationDao { set; }

        [Transactional]
        long CreateRecomendation(long userId, long eventId, List<long> groupsId, String description);

		[Transactional]
		long CreateRecomendation(long userId, long eventId, long groupId, string description);

		[Transactional]
        RecomendationBlock GetRecomendationsByUser(long userId, int startIndex, int count);
    }
}
