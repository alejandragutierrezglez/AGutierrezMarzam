using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ClienteMedicamento
    {
        public int IdClienteMedicamento { get; set; }
        public ML.Cliente Cliente { get; set; }
        public ML.Medicamento Medicamento { get; set; }
        public string Nombre_Medicamento { get; set; }
        public List<object> ClientesMedicamentos { get; set; }
    }
}
