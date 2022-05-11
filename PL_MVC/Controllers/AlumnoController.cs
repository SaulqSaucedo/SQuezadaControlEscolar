using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            AlumnoService.AlumnoClient servicio = new AlumnoService.AlumnoClient();
            var result = servicio.GetAll();
            //ML.Result resultList = new ML.Result();
            //resultList.Objects =  new List<object>();
            if(result.Correct)
            {
                //resultList.Objects = result.Objects.ToList();
                alumno.Alumnos = result.Objects.ToList();
                return View(alumno);
            }
            return PartialView("ValidationModal");
        }
        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            if(IdAlumno == null)
            {
                return View(alumno);
            }
            else // GetById
            {
                AlumnoService.AlumnoClient servicio = new AlumnoService.AlumnoClient();
                var result = servicio.GetById(IdAlumno.Value);
                if (result.Correct)
                {
                    alumno = (ML.Alumno)result.Object;
                    return View(alumno);
                }
            }
            return PartialView("ValidationModal");
        }
        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            if(alumno.IdAlumno == 0)
            {
                AlumnoService.AlumnoClient servicio = new AlumnoService.AlumnoClient();
                var result = servicio.Add(alumno);
                if (result.Correct)
                {
                    ViewBag.Message = "Registro ingresado correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al intentar realizar el registro " + result.ErrorMesagge;
                }
            }
            else // Update
            {
                AlumnoService.AlumnoClient servicio = new AlumnoService.AlumnoClient();
                var result = servicio.Update(alumno);
                if (result.Correct)
                {
                    ViewBag.Message = "Registro actualizado correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al intentar actualizar el registro " + result.ErrorMesagge;
                }
            }
            return PartialView("ValidationModal");
        }
        public ActionResult Delete(int IdAlumno)
        {
            AlumnoService.AlumnoClient servicio = new AlumnoService.AlumnoClient();
            var result = servicio.Delete(IdAlumno);
            if (result.Correct)
            {
                ViewBag.Message = "Registro eliminado correctamente";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al intentar eliminar el registro " + result.ErrorMesagge;
            }
            return PartialView("ValidationModal");
        }
    }
}