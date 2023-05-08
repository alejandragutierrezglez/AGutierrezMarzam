using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pedido
    {
        public static ML.Result MedicamentoDelete(int IdCliente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AGutierrezMarzamEntities context = new DL.AGutierrezMarzamEntities())
                {
                    var query = context.ClienteMedicamentoPedidosDelete(IdCliente);
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
    }
}
