using BussinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eStore.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        IProductRepository productRepository = new ProductRepository();
        ICategoryRepository categoryRepository = new CategoryRepository();
        IMemberRepository memberRepository = new MemberRepository();
        IOderRepository orderRepository = new OrderRepository();
        IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        // GET: HomeController
        [Route("members")]
        public ActionResult Member()
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            @ViewBag.Active = "members";
            @ViewBag.Members = memberRepository.GetMembers();

            return View();
        }

        [Route("member/create")]
        [HttpGet]
        public ActionResult CreateMember()
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            return View();
        }

        [Route("member/create")]
        [HttpPost]
        public ActionResult CreateMember(string memberId, string email, string companyName, string city, string country, string password)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            try
            {
                Member member = new Member
                {
                    MemberId = int.Parse(memberId),
                    Email = email,
                    CompanyName = companyName,
                    City = city,
                    Country = country,
                    Password = password,
                };
                memberRepository.CreateMember(member);
                return RedirectToAction(nameof(Member));
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                ViewBag.MemberId = memberId;
                ViewBag.Email = email;
                ViewBag.CompanyName = companyName;
                ViewBag.City = city;
                ViewBag.Country = country;
                ViewBag.Password = password;
                return View();
            }

        }

        [HttpGet]
        [Route("/home/member/delete")]
        public ActionResult DeleteMember(int id)
        {
            if (HttpContext.Session.GetString("role") != "admin") Response.Redirect("/");
            if (id == null) return NotFound();
            try
            {
                memberRepository.DeleteMember(id);
                return RedirectToAction(nameof(Member));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Member));
            }
        }

        [HttpGet]
        [Route("/home/member/update")]
        public ActionResult UpdateMember(int id)
        {

            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            try
            {
                var item = memberRepository.GetMemberByID(id);
                return View(item);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Member));
            }
        }

        [HttpPost]
        [Route("/home/member/update")]
        public ActionResult UpdateMember(string memberId, string email, string companyName, string city, string country, string password)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            try
            {
                Member member = new Member
                {
                    MemberId = int.Parse(memberId),
                    Email = email,
                    CompanyName = companyName,
                    City = city,
                    Country = country,
                    Password = password,
                };
                memberRepository.UpdateMember(member);
                return RedirectToAction(nameof(Member));
            }
            catch (Exception e)
            {
                var item = memberRepository.GetMemberByID(int.Parse(memberId));
                ViewBag.Message = e.Message;
                return View(item);
            }
        }

        [Route("products")]
        public ActionResult Product()
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            @ViewBag.Active = "product";
            @ViewBag.Products = productRepository.GetProducts();
            return View();
        }

        [Route("product/create")]
        [HttpGet]
        public ActionResult CreateProduct()
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            ViewBag.Categories = categoryRepository.GetCategoriesID();
            return View();
        }

        [Route("product/create")]
        [HttpPost]
        public ActionResult CreateProduct(string productId, string categoryId, string productName, string weight, string unitPrice, string unitsInStock)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            try
            {
                Product product = new Product
                {
                    ProductId = int.Parse(productId),
                    CategoryId = int.Parse(categoryId),
                    ProductName = productName,
                    Weight = weight,
                    UnitPrice = decimal.Parse(unitPrice),
                    UnitsInStock = int.Parse(unitsInStock)
                };
                productRepository.CreateProduct(product);
                return RedirectToAction(nameof(Product));
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                ViewBag.ProductId = productId;
                ViewBag.CategoryId = categoryId;
                ViewBag.ProductName = productName;
                ViewBag.Weight = weight;
                ViewBag.UnitPrice = unitPrice;
                ViewBag.UnitsInStock = unitsInStock;
                ViewBag.Categories = categoryRepository.GetCategoriesID();
                return View();
            }

        }

        [HttpGet]
        [Route("/home/delete")]
        public ActionResult DeleteProduct(int id)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            if (id == null) return NotFound();
            try
            {
                productRepository.DeleteProduct(id);
                return RedirectToAction(nameof(Product));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Product));
            }
        }

        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {

            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            try
            {
                var item = productRepository.GetProductByID(id);
                ViewBag.Categories = categoryRepository.GetCategoriesID();
                return View(item);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Product));
            }
        }

        [HttpPost]
        public ActionResult UpdateProduct(string productId, string categoryId, string productName, string weight, string unitPrice, string unitsInStock)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            try
            {
                ViewBag.Categories = categoryRepository.GetCategoriesID();
                Product product = new Product
                {
                    ProductId = int.Parse(productId),
                    CategoryId = int.Parse(categoryId),
                    ProductName = productName,
                    Weight = weight,
                    UnitPrice = decimal.Parse(unitPrice),
                    UnitsInStock = int.Parse(unitsInStock)
                };
                productRepository.UpdateProduct(product);
                return RedirectToAction(nameof(Product));
            }
            catch (Exception e)
            {
                var item = productRepository.GetProductByID(int.Parse(productId));
                ViewBag.Message = e.Message;
                return View(item);
            }
        }

        [Route("search")]
        [HttpGet]
        public ActionResult Search()
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            @ViewBag.Active = "search";
            return View();
        }

        [Route("search")]
        [HttpPost]
        public ActionResult Search(string searchProduct, string searchPrice)
        {

            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            try
            {
                @ViewBag.Active = "search";
                List<Product> products = productRepository.Search(searchProduct, searchPrice).ToList();
                ViewBag.Products = products;
                ViewBag.SearchProduct = searchProduct;
                ViewBag.SearchPrice = searchPrice;
                return View();

            }
            catch (Exception e)
            {
                return View();
            }

        }

        [Route("orders")]
        public ActionResult Order()
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            @ViewBag.Active = "orders";
            ViewBag.Orders = orderRepository.GetOrders();
            return View();
        }

        [Route("orders/create")]
        [HttpGet]
        public ActionResult CreateOrder()
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            return View();
        }

        [Route("orders/create")]
        [HttpPost]
        public ActionResult CreateOrder(string orderId, string memberId, string requiredDate, string shippedDate, string freight)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            try
            {
                Order order = new Order
                {
                    OrderId = int.Parse(orderId),
                    MemberId = int.Parse(memberId),
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Parse(requiredDate),
                    ShippedDate = DateTime.Parse(shippedDate),
                    Freight = decimal.Parse(freight),
                };
                orderRepository.CreateOrder(order);
                return RedirectToAction(nameof(Order));
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                ViewBag.OrderId = orderId;
                ViewBag.MemberId = memberId;
                ViewBag.RequiredDate = requiredDate;
                ViewBag.ShippedDate = shippedDate;
                ViewBag.Freight = freight;
                return View();
            }

        }

        [HttpGet]
        [Route("/home/order/delete")]
        public ActionResult DeleteOrder(int id)
        {
            if (HttpContext.Session.GetString("role") != "admin") Response.Redirect("/");
            if (id == null) return NotFound();
            try
            {
                orderRepository.DeleteOrder(id);
                return RedirectToAction(nameof(Order));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Order));
            }
        }

        [HttpGet]
        [Route("/home/order/update")]
        public ActionResult UpdateOrder(int id)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            try
            {
                var item = orderRepository.GetOrderByID(id);
                return View(item);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Order));
            }
        }

        [HttpPost]
        [Route("/home/order/update")]
        public ActionResult UpdateOrder(string orderId, string memberId, string requiredDate, string shippedDate, string freight)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            try
            {
                Order order = new Order
                {
                    OrderId = int.Parse(orderId),
                    MemberId = int.Parse(memberId),
                    OrderDate = DateTime.Now,
                    RequiredDate = DateTime.Parse(requiredDate),
                    ShippedDate = DateTime.Parse(shippedDate),
                    Freight = decimal.Parse(freight),
                };
                orderRepository.UpdateOrder(order);
                return RedirectToAction(nameof(Member));
            }
            catch (Exception e)
            {
                var item = orderRepository.GetOrderByID(int.Parse(orderId));
                ViewBag.Message = e.Message;
                return View(item);
            }
        }

        [Route("order-details")]
        public ActionResult OrderDetail(int id)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            ViewBag.OrderDetails = orderDetailRepository.GetOrderDetailsByID(id);
            return View();
        }

        [Route("orders-details/create")]
        [HttpGet]
        public ActionResult CreateOrderDetail(int orderId)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            ViewBag.OrderDetails = orderDetailRepository.GetOrderDetailsByID(orderId);
            ViewBag.OrderId = orderId;
            ViewBag.Products = productRepository.GetProducts();
            return View();
        }

        [Route("orders-details/create")]
        [HttpPost]
        public ActionResult CreateOrderDetail(string orderId, string productId, string unitPrice, string quantity, string discount)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");


            try
            {
                ViewBag.OrderDetails = orderDetailRepository.GetOrderDetailsByID(int.Parse(orderId));
                OrderDetail orderDetail = new OrderDetail
                {
                    OrderId = int.Parse(orderId),
                    ProductId = int.Parse(productId),
                    UnitPrice = decimal.Parse(unitPrice),
                    Quantity = int.Parse(quantity),
                    Discount = double.Parse(discount),
                };
                orderDetailRepository.CreateOrderDetail(orderDetail);
                return RedirectToAction("OrderDetail", "home", new { @id = orderId });
            }
            catch (Exception e)
            {
                ViewBag.OrderId = orderId;
                ViewBag.Products = productRepository.GetProducts();
                ViewBag.Message = e.Message;
                ViewBag.OrderId = orderId;
                ViewBag.ProductId = productId;
                ViewBag.UnitPrice = unitPrice;
                ViewBag.Quantity = quantity;
                ViewBag.Discount = discount;
                return View();
            }

        }

        [HttpGet]
        [Route("/home/order-details/delete")]
        public ActionResult DeleteOrderDetail(int orderId, int productId)
        {
            if (HttpContext.Session.GetString("role") != "admin") Response.Redirect("/");

            if (productId == null || orderId == null) return NotFound();
            try
            {
                orderDetailRepository.DeleteOrderDetail(productId, orderId);
                return RedirectToAction("OrderDetail", "home", new { @id = orderId });
            }
            catch (Exception e)
            {
                return RedirectToAction("OrderDetail", "home", new { @id = orderId });
            }
        }

        [HttpGet]
        [Route("/home/order-details/update")]
        public ActionResult UpdateOrderDetail(int orderId, int productId)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            ViewBag.OrderDetails = orderDetailRepository.GetOrderDetailsByID(orderId);
            try
            {
                ViewBag.OrderId = orderId;
                ViewBag.Products = productRepository.GetProducts();
                var item = orderDetailRepository.GetOrderDetailByID(productId, orderId);
                return View(item);
            }
            catch (Exception e)
            {
                return RedirectToAction("OrderDetail", "home", new { @id = orderId });
            }
        }

        [HttpPost]
        [Route("/home/order-details/update")]
        public ActionResult UpdateOrderDetail(string orderId, string productId, string unitPrice, string quantity, string discount)
        {
            if (HttpContext.Session.GetString("email") == null) Response.Redirect("/");
            try
            {
                ViewBag.OrderDetails = orderDetailRepository.GetOrderDetailsByID(int.Parse(orderId));
                OrderDetail orderDetail = new OrderDetail
                {
                    OrderId = int.Parse(orderId),
                    ProductId = int.Parse(productId),
                    UnitPrice = decimal.Parse(unitPrice),
                    Quantity = int.Parse(quantity),
                    Discount = double.Parse(discount),
                };
                orderDetailRepository.UpdateOrderDetail(orderDetail);
                return RedirectToAction("OrderDetail", "home", new { @id = orderId });
            }
            catch (Exception e)
            {
                ViewBag.OrderId = orderId;
                ViewBag.Products = productRepository.GetProducts();
                var item = orderDetailRepository.GetOrderDetailByID(int.Parse(orderId), int.Parse(productId));
                ViewBag.Message = e.Message;
                return View(item);
            }
        }

    }
}
