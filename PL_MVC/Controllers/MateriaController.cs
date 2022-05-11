using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class MateriaController : Controller
    {
        private IHostingEnvironment Environment;
        private IConfiguration Configuration;
        public MateriaController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            Environment = _environment;
            Configuration = _configuration;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Configuration["WebApi"]);
            }
            return View(materia);
        }

        [HttpGet]
        public ActionResult Form(int? IdMateria)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            return View();
        }

        // GET: Materia/Delete/5
        public ActionResult Delete(int IdMateria)
        {
            return View();
        }
    }
}
