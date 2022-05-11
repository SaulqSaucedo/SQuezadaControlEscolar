using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.SQuezadaControlEscolarEntities context = new DL_EF.SQuezadaControlEscolarEntities())
                {
                    var query = context.MateriaGetAll().ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = obj.IdMateria;
                            materia.Nombre = obj.Nombre;
                            materia.Costo = obj.Costo;

                            result.Objects.Add(materia);
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
                result.ErrorMesagge = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result AddEF(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.SQuezadaControlEscolarEntities context = new DL_EF.SQuezadaControlEscolarEntities())
                {
                    var query = context.MateriaAdd(materia.Nombre, materia.Costo);

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
                result.ErrorMesagge = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetByIdEF(int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.SQuezadaControlEscolarEntities context = new DL_EF.SQuezadaControlEscolarEntities())
                {
                    var query = context.MateriaGetById(IdMateria).FirstOrDefault();

                    if(query != null)
                    {
                        ML.Materia materia = new ML.Materia();
                        materia.IdMateria = query.IdMateria;
                        materia.Nombre = query.Nombre;
                        materia.Costo = query.Costo;

                        result.Object = materia;

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
                result.ErrorMesagge = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result UpdateEF(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.SQuezadaControlEscolarEntities context = new DL_EF.SQuezadaControlEscolarEntities())
                {
                    var query = context.MateriaUpdate(materia.IdMateria, materia.Nombre, materia.Costo);

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
                result.ErrorMesagge = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result DeleteEF(int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.SQuezadaControlEscolarEntities context = new DL_EF.SQuezadaControlEscolarEntities())
                {
                    var query = context.MateriaDelete(IdMateria);

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
                result.ErrorMesagge = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}