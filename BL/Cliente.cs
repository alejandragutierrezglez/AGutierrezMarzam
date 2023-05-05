using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cliente
    {
        public static ML.Result GetById(int IdCliente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.ClienteGetById(IdCliente).FirstOrDefault();
                    if (query != null)
                    {
                        ML.Cliente cliente = new ML.Cliente();
                        cliente.IdCliente = query.IdCliente;
                        cliente.Nombre = query.Nombre;
                        cliente.ApellidoPaterno = query.ApellidoPaterno;
                        cliente.ApellidoMaterno = query.ApellidoMaterno;

                        result.Object = cliente;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo realizar la consulta";
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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.ClienteGetAll().ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var resultCliente in query)
                        {
                            ML.Cliente cliente = new ML.Cliente();
                            cliente.IdCliente = resultCliente.IdCliente;
                            cliente.Nombre = resultCliente.Nombre;
                            cliente.ApellidoPaterno = resultCliente.ApellidoPaterno;
                            cliente.ApellidoMaterno = resultCliente.ApellidoMaterno;
                            cliente.NombreCompleto = cliente.Nombre + " " + cliente.ApellidoPaterno + " "+ cliente.ApellidoMaterno;
                            result.Object = cliente;
                            result.Objects.Add(cliente);
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

    }
}
