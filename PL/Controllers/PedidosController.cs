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
            clienteMedicamento.Medicamento.ConteoTotal = int.Parse(BL.ClienteMedicamento.ConteoTotal(cliente.IdCliente).ToString());
            clienteMedicamento.Cliente = new ML.Cliente();
            ML.Result resultMedicamento = BL.Cliente.GetById(clienteMedicamento.Cliente.IdCliente);
            clienteMedicamento.ClientesMedicamentos = result.Objects;
            clienteMedicamento.Cliente = (ML.Cliente)resultMedicamento.Object;

            return View(clienteMedicamento);


        }
    }
}