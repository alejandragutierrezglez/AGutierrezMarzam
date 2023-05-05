using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SL.Controllers
{
    public class MedicamentoController : ApiController
    {
        // GET: Medicamento
        [HttpGet]
        [Route("api/Medicamento/GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Medicamento medicamento = new ML.Medicamento();

            ML.Result result = BL.Medicamento.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api/Medicamento/GetById/{IdSKUMedicamento}")]
        public IHttpActionResult GetById(int IdSKUMedicamento)
        {
            ML.Result result = BL.Medicamento.GetById(IdSKUMedicamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("api/Medicamento/Add")]
        public IHttpActionResult Add([FromBody] ML.Medicamento medicamento)
        {
            ML.Result result = BL.Medicamento.Add(medicamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/Medicamento/Update/{IdSKUMedicamento}")]
        public IHttpActionResult Update([FromBody] ML.Medicamento medicamento)
        {

            ML.Result result = BL.Medicamento.Update(medicamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api/Materia/Delete/{IdSKUMedicamento}")]
        public IHttpActionResult Delete(int IdSKUMedicamento)
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            medicamento.IdSKUMedicamento = IdSKUMedicamento;
            ML.Result result = BL.Medicamento.Delete(IdSKUMedicamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}