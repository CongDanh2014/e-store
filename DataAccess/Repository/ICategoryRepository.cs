using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface ICategoryRepository
    {
        List<int> GetCategoriesID();
    }
}
