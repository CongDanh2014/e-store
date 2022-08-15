using BussinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace eStore.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        IMemberRepository memberRepository = new MemberRepository();
        // GET: MemberController
        [Route("")]
        [Route("index")]
        [Route("~/")]
        public ActionResult Index()
        {
            return View();
        }


        [Route("login")]
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            try
            {
                if (username != null && password != null)
                {
                    string fileName = "appsettings2.json";
                    string json = System.IO.File.ReadAllText(fileName);
                    var adminAccount = JsonSerializer.Deserialize<DefaultAccount>(json, null);
                    Member member = null;
                    if (username == adminAccount?.Email && password == adminAccount?.Password)
                    {
                        HttpContext.Session.SetString("email", adminAccount?.Email);
                        HttpContext.Session.SetString("role", "admin");
                        Response.Redirect("/home/products");
                    }
                    else if (memberRepository.Login(username, password) != null)
                    {
                        HttpContext.Session.SetString("email", username);
                        Response.Redirect("/home/products");

                    }
                }
                else
                {
                    throw new Exception("Username and password not null");
                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
            }
            return View("Index");

        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("role");
            return RedirectToAction("Index");
        }





        // POST: MemberController/Create

    }
}
