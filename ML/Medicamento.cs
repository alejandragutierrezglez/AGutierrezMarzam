﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Medicamento
    {
        public int? IdSKUMedicamento { get; set; }
        public decimal? Precio { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public int ConteoTotal { get; set; }
        public decimal TotalAPagar { get; set; }
        public int Cantidad { get; set; }
        public List<object> Medicamentos { get; set; }
    }
}
