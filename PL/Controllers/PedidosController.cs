using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult GetAll()
        {

            ML.Result result = BL.Medicamento.GetAll();
            ML.ClienteMedicamento clienteMedicamento = new ML.ClienteMedicamento();
            clienteMedicamento.Medicamento = new ML.Medicamento();
            clienteMedicamento.Cliente = new ML.Cliente();
            ML.Result resultCliente = BL.Cliente.GetAll();

            clienteMedicamento.Medicamento.Medicamentos = result.Objects;
            if (result.Correct == true)
            {
                clienteMedicamento.Cliente.Clientes = resultCliente.Objects;
                clienteMedicamento.Medicamento.Medicamentos = result.Objects;
                return View(clienteMedicamento);
            }
            else
            {
                return View(clienteMedicamento);

            }
        }
        public ActionResult ResumenCompra(ML.Cliente cliente)
        {
            cliente = (ML.Cliente)Session["IdCliente"];

            ML.Result result = BL.ClienteMedicamento.MedicamentoGetByIdCliente(cliente.IdCliente);

            ML.ClienteMedicamento clienteMedicamento = new ML.ClienteMedicamento();
            clienteMedicamento.Medicamento = new ML.Medicamento();            

            clienteMedicamento.Medicamento.ConteoTotal = BL.ClienteMedicamento.ConteoTotal(cliente.IdCliente);
            clienteMedicamento.Medicamento.TotalAPagar = BL.ClienteMedicamento.TotalAPagar(cliente.IdCliente);
            clienteMedicamento.Cliente = new ML.Cliente();
            ML.Result resultMedicamento = BL.Cliente.GetById(cliente.IdCliente);
            clienteMedicamento.ClientesMedicamentos = result.Objects;
            clienteMedicamento.Cliente = (ML.Cliente)resultMedicamento.Object;

            return View(clienteMedicamento);


        }
        public ActionResult MedicamentosDelete(int IdClienteMedicamento)
        {
            ML.ClienteMedicamento clienteMedicamento = new ML.ClienteMedicamento();
            clienteMedicamento.IdClienteMedicamento = IdClienteMedicamento;
            ML.Result result = BL.Pedido.MedicamentoDelete(IdClienteMedicamento);

            if (result.Correct)
            {
                ViewBag.message = "Se ha eliminado el medicamento del carrito";
            }
            else
            {
                ViewBag.message = "Error al eliminar debido a: " + result.ErrorMessage;

            }
            return PartialView("Modal");
        }
    }
}