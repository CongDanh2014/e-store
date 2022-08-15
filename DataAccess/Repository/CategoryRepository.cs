using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<int> GetCategoriesID() => CategoryDAO.Instance.GetCategoriesID();
    }
}
