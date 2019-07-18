using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeCurrency.Models
{
    public class Cotizaciones
    {
        public string Moneda { get; set; }
        public double Precio { get; set; }

        public void SetMoneda(string moneda)
        {
            switch(moneda)
            {
                case "USD":
                    Moneda = "Dolar";
                    break;
                case "EUR":
                    Moneda = "Euro";
                    break;
                case "BRL":
                    Moneda = "Real";
                    break;
            }
        }
    } 
}
