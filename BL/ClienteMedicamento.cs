using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ClienteMedicamento
    {
        public static ML.Result MedicamentoGetByIdCliente(int IdCliente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.MedicamentosGetByIdCliente(IdCliente).ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.ClienteMedicamento clienteMedicamento = new ML.ClienteMedicamento();
                            clienteMedicamento.IdClienteMedicamento = obj.IdClienteMedicamento;

                            clienteMedicamento.Cliente = new ML.Cliente();
                            clienteMedicamento.Cliente.IdCliente = obj.IdCliente.Value;
                            clienteMedicamento.Cliente.Nombre = obj.Nombre;
                            clienteMedicamento.Cliente.ApellidoPaterno = obj.ApellidoPaterno;
                            clienteMedicamento.Cliente.ApellidoMaterno = obj.ApellidoMaterno;
                            clienteMedicamento.Cliente.Username = obj.Username;
                            clienteMedicamento.Cliente.Password = obj.Password;

                            clienteMedicamento.Medicamento = new ML.Medicamento();
                            clienteMedicamento.Medicamento.IdSKUMedicamento = obj.IdSKUMedicamento;
                            clienteMedicamento.Medicamento.Precio = obj.Precio.Value;
                            clienteMedicamento.Medicamento.Nombre = obj.Nombre_Medicamento;

                            result.Objects.Add(clienteMedicamento);
                        }

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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            {
                try
                {
                    using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                    {
                        var query = context.ClienteGetAll().ToList();

                        if (query != null)
                        {
                            result.Objects = new List<object>();

                            foreach (var resultAlumno in query)
                            {
                                ML.ClienteMedicamento clienteMedicamento = new ML.ClienteMedicamento();
                                clienteMedicamento.Cliente = new ML.Cliente();
                                clienteMedicamento.Cliente.IdCliente = resultAlumno.IdCliente;
                                clienteMedicamento.Cliente.Nombre = resultAlumno.Nombre;
                                clienteMedicamento.Cliente.ApellidoPaterno = resultAlumno.ApellidoPaterno;
                                clienteMedicamento.Cliente.ApellidoMaterno = resultAlumno.ApellidoMaterno;


                                result.Objects.Add(clienteMedicamento);
                            }
                            result.Correct = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Ex = ex;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            }
        }
        public static ML.Result Delete(int IdCliente, int IdSKUMedicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.ClienteMedicamentoDelete(IdCliente, IdSKUMedicamento);
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
                result.Ex = Ex;
                result.ErrorMessage = Ex.Message;
            }
            return result;

        }

        public static ML.Result MedicamentoAdd(int IdCliente, int IdSKUMedicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.ClienteMedicamentoAdd(IdCliente, IdSKUMedicamento);
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
                result.Correct = false;
                result.Ex = Ex;
            }
            return result;
        }

        public ML.Result ConteoTotal(int IdCliente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.ConteoTotalMedicamentosByCliente(IdCliente).FirstOrDefault(); 
                    if (query != null)
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
                result.Correct = false;
                result.Ex = Ex;
            }
            return result;
           
        }
    }
}
