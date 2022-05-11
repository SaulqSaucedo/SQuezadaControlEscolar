using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC5.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        public ActionResult GetAll()
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            ML.Result result = BL.Alumno.GetAllEF();
            if (result.Correct)
            {
                alumnoMateria.Alumno.Alumnos = result.Objects;
                return View(alumnoMateria);
            }
            return PartialView("ValidationModal");
        }
        public ActionResult MateriaGetAsignadas(ML.Alumno alumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            ML.Result resultAlumno = BL.Alumno.GetByIdEF(alumno.IdAlumno);
            if (resultAlumno.Correct)
            {
                alumnoMateria.Alumno = (ML.Alumno)resultAlumno.Object; // unboxing datos alumno

                ML.Result resultAsignadas = BL.AlumnoMateria.GetAsignadas(alumno.IdAlumno);
                if (resultAsignadas.Correct)
                {
                    alumnoMateria.AlumnoMaterias = resultAsignadas.Objects;
                    alumnoMateria.Alumno = ((ML.Alumno)resultAlumno.Object);
                    return View(alumnoMateria);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al mostrar la lista de resultados " + resultAsignadas.ErrorMesagge;
                }
            }
            return PartialView("ValidationModal");
        }
        [HttpGet]
        public ActionResult MateriaGetNoAsignadas(int? IdAlumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Materia = new ML.Materia();
            alumnoMateria.Alumno = new ML.Alumno();
            ML.Result resultAlumno = BL.Alumno.GetByIdEF(IdAlumno.Value);
            if (resultAlumno.Correct)
            {
                alumnoMateria.Alumno = (ML.Alumno)resultAlumno.Object;
                ML.Result result = BL.AlumnoMateria.GetNoAsignadas(IdAlumno.Value);
                if (result.Correct)
                {
                    alumnoMateria.AlumnoMaterias = result.Objects;
                    alumnoMateria.Alumno.Alumnos = result.Objects;
                    return View(alumnoMateria);
                }
            }
            return PartialView("ValidationModal");
        }
        [HttpPost]
        public ActionResult MateriaGetNoAsignadas(ML.AlumnoMateria alumnoMateria)
        {
            ML.Alumno alumno = new ML.Alumno();
            if (alumnoMateria.AlumnoMaterias != null && alumnoMateria.Alumno.IdAlumno != null)
            {
                ML.Result result = new ML.Result();
                ML.AlumnoMateria alumnoMaterias = new ML.AlumnoMateria();
                
                foreach (string IdMateria in alumnoMateria.AlumnoMaterias)
                {
                    alumnoMaterias.Alumno = new ML.Alumno();
                    alumnoMaterias.Materia = new ML.Materia();
                    alumnoMaterias.Materia.IdMateria = int.Parse(IdMateria);
                    alumnoMaterias.Alumno.IdAlumno = alumnoMateria.Alumno.IdAlumno;
                    result = BL.AlumnoMateria.AddEF(alumnoMaterias);
                }
                if (result.Correct)
                {
                    alumnoMaterias.AlumnoMaterias = result.Objects;
                    
                    alumno.IdAlumno = alumnoMateria.Alumno.IdAlumno;
                    //ViewBag.Message = "Materia registrada satisfactoriamente para ";
                    //return RedirectToAction("MateriaGetAsignadas", alumnoMateria.Alumno.IdAlumno);
                }
                else
                {
                    //ViewBag.Message = "Ocurrio un error al intentar insertar la materia para " + result.ErrorMesagge;
                }
                
            }
            return RedirectToAction("MateriaGetAsignadas", alumno);
            //return PartialView("ValidationModal");
        }
        public ActionResult DeleteAsignadas(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();
            result = BL.AlumnoMateria.GetAll(alumnoMateria.IdAlumnoMateria);
            ML.Alumno alumno = new ML.Alumno();
            if (result.Correct)
            {
                alumno = (ML.Alumno)result.Object;
                result = BL.AlumnoMateria.DeleteEF(alumnoMateria.IdAlumnoMateria);
                if (result.Correct)
                {
                    ViewBag.Message = "Materia eliminada correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al intentar eliminar la materia " + result.ErrorMesagge;
                }
            }
            //alumno = alumnoMateria.Alumno;
            return RedirectToAction("MateriaGetAsignadas", alumno);
        }
    }
}