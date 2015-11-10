using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{
    class EventDaoEntityFramework :
        GenericDaoEntityFramework<Event, Int64>, IEventDao
    {
        public List<Event> FindEvents(List<string> keywords, long? categoryId, int startIndex, int count)
        {
            
            String query = "SELECT VALUE e FROM Event WHERE ";
		    if(keywords.Count>=1){
			    query+="AND lower(e.name) LIKE @k0 ";
		    }
		    for(int i = 1;i< keywords.Count; i++){
			    query+="AND lower(e.name) LIKE @k"+i.ToString()+" ";
		    }
		    if(categoryId != null){
			    query+="AND e.category.categoryId = @categoryId ";
		    }
		    query += "ORDER BY (e.date)";

            List<DbParameter> q = new List<DbParameter>();
            for (int i = 0;i< keywords.Count; i++){
                q.Add(new System.Data.SqlClient.SqlParameter("k" + i.ToString(), keywords.ElementAt(i)));
		    }
		    if(categoryId!=null){
                q.Add(new System.Data.SqlClient.SqlParameter("categoryId", categoryId));
		    }

            List<Event> result = Context.Database.SqlQuery<Event>(query, q).Skip(startIndex).Take(count).ToList<Event>();

            return result;
        }
    }
}
