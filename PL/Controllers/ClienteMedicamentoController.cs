using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ClienteMedicamentoController : Controller
    {
        // GET: Cliente

        [HttpGet]
        public ActionResult GetAll(ML.ClienteMedicamento clienteMedicamento)
        {

            ML.Result result = BL.ClienteMedicamento.GetAll();


            if (result.Correct)
            {
                clienteMedicamento.ClientesMedicamentos = result.Objects;
                return View(clienteMedicamento);
            }
            else
            {
                return View(clienteMedicamento);
            }
        }


        [HttpGet]
        public ActionResult MedicamentosGetByCliente(int IdCliente)
        {
            ML.Result result = BL.ClienteMedicamento.MedicamentoGetByIdCliente(IdCliente);
            ML.ClienteMedicamento clienteMedicamento = new ML.ClienteMedicamento();

            ML.Result resultMedicamento = BL.Cliente.GetById(IdCliente);
            clienteMedicamento.ClientesMedicamentos = result.Objects;
            clienteMedicamento.Cliente = (ML.Cliente)resultMedicamento.Object;

            return View(clienteMedicamento);
        }
        public ActionResult MedicamentosDelete(int IdCliente, int IdSKUMedicamento)
        {
            ML.ClienteMedicamento clienteMedicamento = new ML.ClienteMedicamento();
            clienteMedicamento.IdClienteMedicamento = IdCliente;
            ML.Result result = BL.ClienteMedicamento.Delete(IdCliente, IdSKUMedicamento);


            if (result.Correct)
            {
                ViewBag.message = "Se ha eliminado el registro";
            }
            else
            {
                ViewBag.message = "Error al eliminar debido a: " + result.ErrorMessage;

            }
            return PartialView("Modal");
        }


        [HttpGet]
        public ActionResult MedicamentoAdd(int IdSKUMedicamento)

        {
            ML.Cliente cliente = new ML.Cliente();

            cliente = (ML.Cliente)Session["IdCliente"];
            ML.Result result = new ML.Result();

            ML.ClienteMedicamento clienteMedicamento = new ML.ClienteMedicamento();
            clienteMedicamento.Medicamento = new ML.Medicamento();

            clienteMedicamento.Medicamento.IdSKUMedicamento = IdSKUMedicamento;

            if (clienteMedicamento.Medicamento.IdSKUMedicamento != null)
            {
                ML.Result resultt = BL.ClienteMedicamento.MedicamentoAdd(cliente.IdCliente, IdSKUMedicamento);
            }

            else
            {
                result.Correct = false;
                result.ErrorMessage = "No se pudo agregar el medicamento, reintentalo";

            }
            result.Correct = true;
            ViewBag.Message = "Se ha agregado el medicamento";
            return PartialView("Modal");
        }

       
    }
}