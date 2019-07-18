using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeCurrency.Models
{
    public class RespuestaApi
    {
        public Resultado result { get; set; }
        public string status { get; set; }
    }

    public class Resultado
    {
        public string update { get; set; }
        public string source { get; set; }
        public string target { get; set; }
        public double value { get; set; }
        public double quantity { get; set; }
        public double amount { get; set; }

    }


}
