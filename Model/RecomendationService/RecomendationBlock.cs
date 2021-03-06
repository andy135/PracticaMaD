﻿using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecomendationService
{
	public class RecomendationBlock
    {
        public List<RecomendationInfo> Recomendations { get; private set; }
        public bool ExistMoreRecomendations { get; private set; }

        public RecomendationBlock(List<RecomendationInfo> recomendations, bool existMoreRecomendations)
        {
            this.Recomendations = recomendations;
            this.ExistMoreRecomendations = existMoreRecomendations;
        }
    }
}
