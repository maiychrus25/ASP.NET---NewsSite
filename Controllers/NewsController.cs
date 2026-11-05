using Microsoft.AspNetCore.Mvc;

namespace NewsSite.Controllers;

public class NewsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Latest()
    {
        ViewBag.NewsTitle = "Sinh viên bắt đầu học ASP.NET Core MVC";
        return View();
    }

    public IActionResult List()
    {
        ViewData["Titles"] = new List<string>
        {
            "ASP.NET Core MVC là gì?",
            "Tìm hiểu Controller và View",
            "Sử dụng ViewBag và ViewData"
        };

        return View();
    }
}