using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserGroupService
{
	public class GroupBlock
    {
        public List<GroupInfo> Groups { get; private set; }
        public bool ExistMoreGroups { get; private set; }

        public GroupBlock(List<GroupInfo> groups, bool existMoreGroups)
        {
            this.Groups = groups;
            this.ExistMoreGroups = existMoreGroups;
        }
    }
}
