using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Test.Context;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        SentencesContext context=new SentencesContext();
        public ActionResult Index()
        {
        
            return View(context.Results);
    
        }

        [HttpPost]
        public JsonResult Upload()
        {
           
            var search = Request.Params["text"];
            var upload = Request.Files["file"];
            var resultList=new List<Result>();
            if (upload != null)
            {
                // получаем имя файла
                string fileName = Path.GetFileName(upload.FileName);
                
                // сохраняем файл в папку Files в проекте
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
                using (var stream = new StreamReader(Server.MapPath("~/Files/" + fileName)))
                {
                    var texttoParce = stream.ReadToEnd();
                    var sentenceses = texttoParce.Split('.');
                    foreach (var sentence in sentenceses)
                    {
                            var builder = new StringBuilder();
                            var words = sentence.Split(' ',',');
                        if (words.Any(x => x == search))
                        {
                            var count = words.Count(x => x == search);
                            foreach (var word in words)
                            {
                                var charArray = word.ToCharArray();
                                Array.Reverse(charArray);
                                var str = new string(charArray);
                                builder.Append(str);
                                builder.Append(' ');
                            }
                            var result = new Result
                            {
                                Count = count,
                                ReverceSentence = builder.ToString()
                            };
                            resultList.Add(result);
                        }
                    }
                    
                }

                context.Results.AddRange(resultList);
                context.SaveChanges();

            }
            return Json(resultList);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}