using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class Alumno
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexión.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AlumnoGetAll";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Connection.Open();

                        DataTable AlumnoTable = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        
                        da.Fill(AlumnoTable);
                        if(AlumnoTable.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in AlumnoTable.Rows)
                            {
                                ML.Alumno alumno = new ML.Alumno();
                                alumno.IdAlumno = int.Parse(row[0].ToString());
                                alumno.Nombre = row[1].ToString();
                                alumno.ApellidoPaterno = row[2].ToString();
                                alumno.ApellidoMaterno = row[3].ToString();

                                result.Objects.Add(alumno);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
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
        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexión.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AlumnoAdd";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Connection.Open();

                        SqlParameter[] parameters = new SqlParameter[3];

                        parameters[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        parameters[0].Value = alumno.Nombre;

                        parameters[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        parameters[1].Value = alumno.ApellidoPaterno;

                        parameters[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        parameters[2].Value = alumno.ApellidoMaterno;

                        cmd.Parameters.AddRange(parameters);

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMesagge = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexión.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AlumnoGetById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = connection;
                        cmd.Connection.Open();

                        DataTable AlumnoTable = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdAlumno", SqlDbType.Int);
                        collection[0].Value = IdAlumno;
                        cmd.Parameters.AddRange(collection);

                        adapter.Fill(AlumnoTable);

                        if(AlumnoTable.Rows.Count > 0)
                        {
                            ML.Alumno alumno = new ML.Alumno();
                            DataRow row = AlumnoTable.Rows[0];
                            alumno.IdAlumno = int.Parse(row[0].ToString());
                            alumno.Nombre = row[1].ToString();
                            alumno.ApellidoPaterno = row[2].ToString();
                            alumno.ApellidoMaterno = row[3].ToString();

                            result.Object = alumno;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
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
        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexión.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "AlumnoUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();

                        SqlParameter[] parameters = new SqlParameter[4];

                        parameters[0] = new SqlParameter("IdAlumno", SqlDbType.VarChar);
                        parameters[0].Value = alumno.IdAlumno;

                        parameters[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        parameters[1].Value = alumno.Nombre;

                        parameters[2] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        parameters[2].Value = alumno.ApellidoPaterno;

                        parameters[3] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        parameters[3].Value = alumno.ApellidoMaterno;

                        cmd.Parameters.AddRange(parameters);

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if(RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
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
        public static ML.Result Delete(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection connection = new SqlConnection(DL.Conexión.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "AlumnoDelete";
                        cmd.Connection = connection;
                        cmd.Connection.Open();

                        SqlParameter[] parameter = new SqlParameter[1];
                        parameter[0] = new SqlParameter("IdAlumno", SqlDbType.VarChar);
                        parameter[0].Value = IdAlumno;

                        cmd.Parameters.AddRange(parameter);

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if(RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
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
        
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.SQuezadaControlEscolarEntities context = new DL_EF.SQuezadaControlEscolarEntities())
                {
                    var query = context.AlumnoGetAll().ToList();

                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Alumno alumno = new ML.Alumno();
                            alumno.IdAlumno = item.IdAlumno;
                            alumno.Nombre = item.Nombre;
                            alumno.ApellidoPaterno = item.ApellidoPaterno;
                            alumno.ApellidoMaterno = item.ApellidoMaterno;

                            result.Objects.Add(alumno);
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
        public static ML.Result GetByIdEF(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.SQuezadaControlEscolarEntities context = new DL_EF.SQuezadaControlEscolarEntities())
                {
                    var query = context.AlumnoGetById(IdAlumno).FirstOrDefault();

                    if(query!= null)
                    {
                        ML.Alumno alumno = new ML.Alumno();
                        alumno.IdAlumno = query.IdAlumno;
                        alumno.Nombre = query.Nombre;
                        alumno.ApellidoPaterno = query.ApellidoPaterno;
                        alumno.ApellidoMaterno = query.ApellidoMaterno;

                        result.Object = alumno;

                        result.Correct = true;
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