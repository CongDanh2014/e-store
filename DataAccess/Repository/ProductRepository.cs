using BussinessObject.Models;
using System.Collections.Generic;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void CreateProduct(Product product) => ProductDAO.Instance.CreateProcduct(product);

        public Product GetProductByID(int id) => ProductDAO.Instance.GetProductByID(id);
        public void DeleteProduct(int id) => ProductDAO.Instance.DeleteProduct(id);

        public IEnumerable<Product> GetProducts() => ProductDAO.Instance.GetProducts();

        public void UpdateProduct(Product product) => ProductDAO.Instance.UpdateProduct(product);

        public IEnumerable<Product> Search(string name, string price) => ProductDAO.Instance.SearchWithNameAndPrice(name, price);


    }
}
