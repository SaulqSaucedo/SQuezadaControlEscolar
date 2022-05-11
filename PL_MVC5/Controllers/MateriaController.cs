using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;

namespace PL_MVC.Controllers
{
    public class MateriaController : Controller
    {
        string urlWebApi = System.Configuration.ConfigurationManager.AppSettings["WebApi"];
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlWebApi);
                var response = client.GetAsync("Materia");
                response.Wait();
                var resultApi = response.Result;
                if (resultApi.IsSuccessStatusCode)
                {
                    var readJson = resultApi.Content.ReadAsAsync<ML.Result>();
                    readJson.Wait();
                    foreach(var resultItem in readJson.Result.Objects)
                    {
                        ML.Materia resultDeserealizacion = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultItem.ToString());
                        result.Objects.Add(resultDeserealizacion);
                    }
                    result.Correct = readJson.Result.Correct;
                    result.ErrorMesagge = readJson.Result.ErrorMesagge;
                    
                    if (result.Correct)
                    {
                        materia.Materias = result.Objects;
                        return View(materia);
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al recuperar la información " + result.ErrorMesagge;
                    }
                }
            }
            return PartialView("ValidationModal");
        }
        [HttpGet]
        public ActionResult Form(int? IdMateria)
        {
            ML.Result result = new ML.Result();
            ML.Materia materia = new ML.Materia();
            if(IdMateria == null)
            {
                return View(materia);
            }
            else // GetById
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlWebApi);
                    var postTask = client.GetAsync("Materia/" + IdMateria);
                    postTask.Wait();
                    var resultApi = postTask.Result;
                    if (resultApi.IsSuccessStatusCode)
                    {
                        var readJson = resultApi.Content.ReadAsAsync<ML.Result>();
                        readJson.Wait();
                        ML.Materia resultDeserealizacion = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(readJson.Result.Object.ToString());
                        
                        result.Object = resultDeserealizacion;

                        result.Correct = readJson.Result.Correct;
                        result.ErrorMesagge = readJson.Result.ErrorMesagge;

                        if (result.Correct)
                        {
                            materia = (ML.Materia)result.Object;

                            return View(materia);
                        }
                        else
                        {
                            ViewBag.Message = "No es posible mostrar la informacion de la materia " + result.ErrorMesagge;
                        }
                    }
                }
            }
            return PartialView("ValidationModal");
        }
        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            if(materia.IdMateria == 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlWebApi);
                    // HttpPost
                    var postTask = client.PostAsJsonAsync<ML.Materia>("Materia", materia);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "El registro se inserto correctamente ";
                        //return RedirectToAction("GetAll");
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al intentar insertar el registro " + result.RequestMessage;
                    }
                }
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlWebApi);
                    // HttpPut
                    var PutTask = client.PutAsJsonAsync<ML.Materia>("Materia", materia);
                    PutTask.Wait();

                    var result = PutTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "El registro se actualizo correctamente ";
                        //return RedirectToAction("GetAll");
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al intentar actualizar el registro " + result.RequestMessage;
                    }
                }
            }
            return View("ValidationModal");
        }

        // GET: Materia/Delete/5
        public ActionResult Delete(int IdMateria)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlWebApi);
                // HttpDelete
                var DeleteTask = client.DeleteAsync("Materia/" + IdMateria);
                DeleteTask.Wait();

                var result = DeleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El registro se elimino correctamente ";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al intentar eliminar el registro " + result.RequestMessage;
                }
            }
            return PartialView("ValidationModal");
        }
    }
}