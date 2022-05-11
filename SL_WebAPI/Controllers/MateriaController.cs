using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    public class MateriaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Materia.GetAllEF();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Ok(result.ErrorMesagge.ToString());
            }
        }
        [HttpPost]
        public IHttpActionResult Add([FromBody] ML.Materia materia)
        {
            ML.Result result = BL.Materia.AddEF(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Ok(result.ErrorMesagge);
            }
        }
        [HttpGet()]
        [Route("api/Materia/{IdMateria}")]
        public IHttpActionResult GetById(int IdMateria)
        {
            ML.Result result = BL.Materia.GetByIdEF(IdMateria);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Ok(result.ErrorMesagge);
            }
        }
        [HttpPut]
        public IHttpActionResult Update([FromBody] ML.Materia materia)
        {
            ML.Result result = BL.Materia.UpdateEF(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Ok(result.ErrorMesagge);
            }
        }
        [HttpDelete()]
        [Route("api/Materia/{IdMateria}")]
        public IHttpActionResult Delete(int IdMateria)
        {
            ML.Result result = BL.Materia.DeleteEF(IdMateria);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Ok(result.ErrorMesagge);
            }
        }
    }
}