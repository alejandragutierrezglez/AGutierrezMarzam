using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Medicamento
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.MedicamentoGetAll().ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var resultMedicamento in query)
                        {
                            ML.Medicamento medicamento = new ML.Medicamento();
                            medicamento.IdSKUMedicamento = resultMedicamento.IdSKUMedicamento;
                            medicamento.Precio = resultMedicamento.Precio.Value;
                            medicamento.Nombre = resultMedicamento.Nombre;
                            medicamento.Imagen = resultMedicamento.Imagen;


                            result.Objects.Add(medicamento);
                        }
                    }
                    result.Correct = true;

                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdSKUMedicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.MedicamentoGetById(IdSKUMedicamento).FirstOrDefault();
                    if (query != null)
                    {
                        ML.Medicamento medicamento = new ML.Medicamento();
                        medicamento.IdSKUMedicamento = query.IdSKUMedicamento;
                        medicamento.Precio = query.Precio.Value;
                        medicamento.Nombre = query.Nombre;

                        result.Object = medicamento;
                    }
                }
                result.Correct = true;
            }
            catch (Exception Ex)
            {
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
                result.Correct = false;
            }
            return result;
        }
        public static ML.Result Add(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.MedicamentoAdd(medicamento.Precio, medicamento.Nombre, medicamento.Imagen);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.MedicamentoUpdate(medicamento.IdSKUMedicamento, medicamento.Precio, medicamento.Nombre, medicamento.Imagen);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdSKUMedicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.MedicamentoDelete(IdSKUMedicamento);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }

                }
            }
            catch (Exception Ex)
            {
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result ClienteGetByUsername(string Username)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.ClienteGetByUsername(Username).FirstOrDefault();
                    if (query != null)
                    {
                        ML.Cliente cliente = new ML.Cliente();
                        cliente.IdCliente = query.IdCliente;
                        cliente.Nombre = query.Nombre;
                        cliente.ApellidoPaterno = query.ApellidoPaterno;
                        cliente.ApellidoMaterno= query.ApellidoMaterno;
                        cliente.Username = query.Username;
                        cliente.Password = query.Password;
                        
                        ML.Medicamento medicamento = new ML.Medicamento();
                        medicamento.IdSKUMedicamento = query.IdSKUMedicamento;
                        medicamento.Precio = query.Precio.Value;
                        medicamento.Nombre = query.NombreMedicamento;

                        ML.ClienteMedicamento clienteMedicamento = new ML.ClienteMedicamento();
                        clienteMedicamento.IdClienteMedicamento = query.IdClienteMedicamento.Value;

                        result.Object= cliente;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
            }
            return result;

        }
    }
}
