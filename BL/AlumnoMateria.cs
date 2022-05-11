using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result GetAsignadas(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.SQuezadaControlEscolarEntities context = new DL_EF.SQuezadaControlEscolarEntities())
                {
                    var query = context.MateriaGetAsignadas(IdAlumno).ToList();

                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.IdAlumnoMateria = item.IdAlumnoMateria;
                            alumnoMateria.Alumno = new ML.Alumno();
                            alumnoMateria.Alumno.IdAlumno = item.IdAlumno;
                            alumnoMateria.Alumno.Nombre = item.AlumnoNombre;
                            alumnoMateria.Materia = new ML.Materia();
                            alumnoMateria.Materia.IdMateria = item.IdMateria;
                            alumnoMateria.Materia.Nombre = item.MateriaNombre;

                            result.Object = alumnoMateria.Alumno.IdAlumno;

                            result.Objects.Add(alumnoMateria);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMesagge = ex.Message;
            }
            return result;
        }
        public static ML.Result GetNoAsignadas(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.SQuezadaControlEscolarEntities context = new DL_EF.SQuezadaControlEscolarEntities())
                {
                    var query = context.MateriaGetNoAsignadas(IdAlumno).ToList();

                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.Materia = new ML.Materia();
                            alumnoMateria.Materia.IdMateria = item.IdMateria;
                            alumnoMateria.Materia.Nombre = item.Nombre;
                            alumnoMateria.Materia.Costo = item.Costo;

                            result.Objects.Add(alumnoMateria);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMesagge = ex.Message;
            }
            return result;
        }
        public static ML.Result AddEF(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.SQuezadaControlEscolarEntities context = new DL_EF.SQuezadaControlEscolarEntities())
                {
                    var query = context.AlumnoMateriaAdd(alumnoMateria.Materia.IdMateria, alumnoMateria.Alumno.IdAlumno);

                    if(query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMesagge = ex.Message;
            }
            return result;
        }
        public static ML.Result DeleteEF(int IdAlumnoMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.SQuezadaControlEscolarEntities context = new DL_EF.SQuezadaControlEscolarEntities())
                {
                    var query = context.AlumnoMateriaDelete(IdAlumnoMateria);

                    if(query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMesagge = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll(int IdAlumnoMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.SQuezadaControlEscolarEntities context = new DL_EF.SQuezadaControlEscolarEntities())
                {
                    var query = context.AlumnoMateriaGetAll(IdAlumnoMateria).FirstOrDefault();

                    if(query != null)
                    {
                        ML.Alumno alumnoMateria = new ML.Alumno();
                        //alumnoMateria.Alumno = new ML.Alumno();
                        alumnoMateria.IdAlumno = query.IdAlumno;

                        result.Object = alumnoMateria;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMesagge = ex.Message;
            }
            return result;
        }
    }
}