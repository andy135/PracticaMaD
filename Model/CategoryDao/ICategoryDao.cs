using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
	public interface ICategoryDao : IGenericDao<Category, Int64>
    {
        List<Category> GetAllCategories();

    }
}
