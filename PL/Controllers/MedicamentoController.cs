using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class MedicamentoController : Controller
    {
        // GET: Medicamento
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (var client = new HttpClient())
                {
                    string str = System.Configuration.ConfigurationManager.AppSettings["WebApi"];
                    client.BaseAddress = new Uri(str);

                    var responseTask = client.GetAsync("Medicamento/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Medicamento resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Medicamento>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    medicamento.Medicamentos = result.Objects;
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
            }

            return View(medicamento);
        }

        [HttpGet]
        public ActionResult Form(int? IdSKUMedicamento)
        {

            ML.Result resultMedicamentos = BL.Medicamento.GetAll();
            ML.Medicamento medicamento = new ML.Medicamento();

            if (resultMedicamentos.Correct)
            {
                medicamento.Medicamentos = resultMedicamentos.Objects;
            }
            if (IdSKUMedicamento == null)
            {
                //add //formulario vacio
                return View(medicamento);
            }
            else
            {
                ML.Result result = new ML.Result();
                using (var client = new HttpClient())
                    try
                    {
                        client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);
                        var responseTask = client.GetAsync("Medicamento/GetById/" + IdSKUMedicamento);
                        responseTask.Wait();

                        var resultAPI = responseTask.Result;
                        if (resultAPI.IsSuccessStatusCode)

                        {
                            var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            ML.Medicamento resultItemList = new ML.Medicamento();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Medicamento>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;


                            medicamento = (ML.Medicamento)result.Object;//unboxing
                            medicamento.Medicamentos = resultMedicamentos.Objects;


                            //update
                            return View(medicamento);
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrio al consultar la información del medicamento";
                            return View("Modal");
                        }
                    }
                    catch (Exception Ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = Ex.Message;
                        result.Ex = Ex;
                    }

                return View(medicamento);
            }
        }


        [HttpPost] 
        public ActionResult Form(ML.Medicamento medicamento)
        {
            HttpPostedFileBase file = Request.Files["fuImage"];

            if (file != null)
            {
                byte[] imagen = ConvertToBytes(file);

                medicamento.Imagen = Convert.ToBase64String(imagen);
            }
            if (medicamento.IdSKUMedicamento != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Medicamento>("Medicamento/Update/" + medicamento.IdSKUMedicamento, medicamento);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha modificado el registro";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha modificado el registro";
                        return PartialView("Modal");
                    }

                }

            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Medicamento>("Medicamento/Add", medicamento);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha agregado el registro";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha agregado el registro";
                        return PartialView("Modal");
                    }
                }
            }
        }


        public ActionResult Delete(int IdSKUMedicamento) 
        {
            ML.Result resultMedicamento = new ML.Result();
            ML.Medicamento medicamento = new ML.Medicamento();
            medicamento.IdSKUMedicamento = IdSKUMedicamento;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                var postTask = client.GetAsync("Materia/Delete/" + IdSKUMedicamento);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    resultMedicamento = BL.Medicamento.GetAll();
                    ViewBag.Message = "Se ha eliminado el registro";
                    return PartialView("Modal");
                }
            }
            resultMedicamento = BL.Medicamento.GetAll();
            return View("GetAll", resultMedicamento);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            ML.Result result = BL.Medicamento.ClienteGetByUsername(username);
            if (result.Correct)
            {
                ML.Cliente cliente = (ML.Cliente)result.Object;
                
                if (password == cliente.Password)
                {
                    Session["IdCliente"] = cliente;
                    return RedirectToAction("GetAll", "Medicamento");
                }
                else
                {
                    ViewBag.Message = "Las credenciales no coinciden";
                    return PartialView("ModalLogin");
                }
            }
            else
            {
                ViewBag.Message = "El cliente no existe, verifica tus datos nuevamente.";
                return PartialView("ModalLogin");
            }
        }
        public byte[] ConvertToBytes(HttpPostedFileBase Foto)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            data = reader.ReadBytes((int)Foto.ContentLength);

            return data;
        }



    }
}