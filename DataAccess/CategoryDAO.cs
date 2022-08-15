using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class CategoryDAO
    {
        FStoreDBContext db = new FStoreDBContext();

        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        private CategoryDAO() { }

        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

        public List<int> GetCategoriesID()
        {
            List<int> item = null;
            try
            {
                item = (from c in db.Categories select c.CategoryId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return item;
        }
    }
}
