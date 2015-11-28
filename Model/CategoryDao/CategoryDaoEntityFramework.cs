using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
	class CategoryDaoEntityFramework :
        GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {
        public List<Category> GetAllCategories()
        {
            return GetAllElements();
        }
    }
}
