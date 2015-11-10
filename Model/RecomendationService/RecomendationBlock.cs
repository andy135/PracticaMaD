using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecomendationService
{
    public class RecomendationBlock
    {
        public List<Recomendation> Recomendations { get; private set; }
        public bool ExistMoreRecomendations { get; private set; }

        public RecomendationBlock(List<Recomendation> recomendations, bool existMoreRecomendations)
        {
            this.Recomendations = recomendations;
            this.ExistMoreRecomendations = existMoreRecomendations;
        }
    }
}
