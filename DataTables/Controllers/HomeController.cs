using DataTables.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace DataTables.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DataSource()
        {
            return View();
        }

        public ActionResult Miscellaneous()
        {
            return View();
        }

        public ActionResult ServerSide()
        {
            return View();
        }

        public ActionResult Resources()
        {
            return View();
        }

        public JsonResult GetTODOs()
        {
            JArray todos = new JArray();
            List<ToDo> items = new List<ToDo>();
            var filteredItems = new List<ToDo>();

            var start = int.Parse(Request.QueryString["start"]);
            var size = int.Parse(Request.QueryString["length"]);
            var searchTerm = Request.QueryString["search[value]"];
            var orderColumn = int.Parse(Request.QueryString["order[0][column]"]);
            var orderDir = Request.QueryString["order[0][dir]"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                var responseTask = client.GetAsync("todos");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    todos = JArray.Parse(readTask.Result);
                    items = todos.Select(x => new ToDo {
                        userId = (int)x["userId"],
                        id = (int)x["id"],
                        title = (string)x["title"],
                        completed = (bool)x["completed"]
                    }).ToList();

                    if(orderDir == "asc")
                    {
                        switch (orderColumn)
                        {
                            case 0:
                                filteredItems = items.FindAll(x => x.title.Contains(searchTerm)).OrderBy(o => o.userId).ToList();
                                break;
                            case 1:
                                filteredItems = items.FindAll(x => x.title.Contains(searchTerm)).OrderBy(o => o.id).ToList();
                                break;
                            case 2:
                                filteredItems = items.FindAll(x => x.title.Contains(searchTerm)).OrderBy(o => o.title).ToList();
                                break;
                            case 3:
                                filteredItems = items.FindAll(x => x.title.Contains(searchTerm)).OrderBy(o => o.completed).ToList();
                                break;
                        }
                    } 
                    else
                    {
                        switch (orderColumn)
                        {
                            case 0: 
                                filteredItems = items.FindAll(x => x.title.Contains(searchTerm)).OrderByDescending(o => o.userId).ToList();
                                break;
                            case 1:
                                filteredItems = items.FindAll(x => x.title.Contains(searchTerm)).OrderByDescending(o => o.id).ToList();
                                break;
                            case 2:
                                filteredItems = items.FindAll(x => x.title.Contains(searchTerm)).OrderByDescending(o => o.title).ToList();
                                break;
                            case 3:
                                filteredItems = items.FindAll(x => x.title.Contains(searchTerm)).OrderByDescending(o => o.completed).ToList();
                                break;
                        }
                    }
                    

                    return Json(new
                    {
                        recordsTotal = todos.Count,
                        recordsFiltered = filteredItems.Count,
                        data = filteredItems.Skip(start).Take(size)
                    }, JsonRequestBehavior.AllowGet); ;
                } 
                else
                {
                    return Json(new
                    {
                        recordsTotal = 0, recordsFiltered = 0, data = items
                    }, JsonRequestBehavior.AllowGet); ;
                }
            }
        }
    }
}