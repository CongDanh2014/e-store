using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class ProductDAO
    {
        FStoreDBContext db = new FStoreDBContext();

        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }

        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Product> GetProducts()
        {
            List<Product> products = null;
            try
            {
                products = db.Products.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return products;
        }

        public Product GetProductByID(int id)
        {
            Product product = db.Products.Find(id);
            return product;
        }

        public void CreateProcduct(Product product)
        {
            try
            {
                if (product.ProductId == null || product.ProductName == null || product.Weight == null || product.UnitPrice == null || product.UnitsInStock == null)
                {
                    throw new Exception("All fields must be required.");
                }
                var checkID = (from c in GetProducts() where product.ProductId == c.ProductId select c).SingleOrDefault();
                if (checkID != null)
                {
                    throw new Exception("Duplicate product id, Please enter another id.");
                }
                db.Products.Add(product);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                var item = db.Products.SingleOrDefault(c => c.ProductId == id);
                db.Products.Remove(item);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void UpdateProduct(Product product)
        {
            try
            {
                var item = db.Products.Find(product.ProductId);
                if (item == null) throw new Exception("Not find id product, product isn't available.");
                item.ProductName = product.ProductName;
                item.CategoryId = product.CategoryId;
                item.UnitPrice = product.UnitPrice;
                item.UnitsInStock = product.UnitsInStock;
                item.Weight = product.Weight;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Product> SearchWithNameAndPrice(string name, string priceRange)
        {
            List<Product> result = new List<Product>();
            try
            {
                name = name == null ? "" : name;
                var listProcduct = GetProducts();
                switch (priceRange)
                {
                    case "1":
                        result = (from c in listProcduct where c.ProductName.Contains(name) && c.UnitPrice <= 10000 select c).ToList();
                        break;
                    case "2":
                        result = (from c in listProcduct where c.ProductName.Contains(name) && c.UnitPrice >= 10000 && c.UnitPrice <= 20000 select c).ToList();
                        break;
                    case "3":
                        result = (from c in listProcduct where c.ProductName.Contains(name) && c.UnitPrice >= 20000 select c).ToList();
                        break;
                    default:
                        result = (from c in listProcduct where c.ProductName.Contains(name) select c).ToList();
                        break;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }



    }
}
