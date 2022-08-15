using BussinessObject.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        Product GetProductByID(int id);
        IEnumerable<Product> GetProducts();
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        IEnumerable<Product> Search(string name, string price);
    }
}
