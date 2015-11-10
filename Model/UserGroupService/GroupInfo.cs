using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserGroupService
{
    [Serializable()]
    public class GroupInfo
    {
        public long GroupId { get; private set; }
        public String Name { get; private set; }
        public String Description { get; private set; }
        public long NumMembers { get; private set; }
        public long NumRecomendations { get; private set; }

        public GroupInfo(long groupId, String name, String description, long numMembers, long numRecomendations)
        {
            this.GroupId = groupId;
            this.Name = name;
            this.Description = description;
            this.NumMembers = numMembers;
            this.NumRecomendations = numRecomendations;
        }

    }
}
