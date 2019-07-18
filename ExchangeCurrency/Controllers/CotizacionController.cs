using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ExchangeCurrency.Models;
using ExchangeCurrency.Configuration;
using Microsoft.Extensions.Options;

namespace ExchangeCurrency.Controllers
{
    [Route("api/[controller]")]
    public class CotizacionController : Controller
    {
        public const string ApiKey = "1567|VJGZ*uNrQdShZKHXqOn^pJ4Fid0DfsTF";// api key de cambio.today otra 1507|1FL~3eLSEXid42YTB1HLm73i5pOLDMUk

        // retorna la cotizacionde las tres monedas solicitadas
        [HttpGet("[action]")]
        public async Task<List<Cotizaciones>> Exchange()
        {
            List<Cotizaciones> listaCotizaciones = new List<Cotizaciones>();
            listaCotizaciones.Add(await GetCotizacion("USD"));
            listaCotizaciones.Add(await GetCotizacion("EUR"));
            listaCotizaciones.Add(await GetCotizacion("BRL"));
            return listaCotizaciones;            
        }

        // obtiene la cotizacion para un tipo de moneda, con respecto al peso argentino, usando api de cambio.today
        // en caso de error el valor sera 0
        private async Task<Cotizaciones> GetCotizacion(string moneda)
        {
            Cotizaciones cotizacion = new Cotizaciones();
            using (var cliente = new HttpClient())
            {
                try
                {
                    var url = "http://api.wahrungsrechner.org/v1/quotes/" + moneda + "/ARS/json?key="+ ApiKey; 
                    var response = await cliente.GetAsync(url);
                    if (response.IsSuccessStatusCode == true)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var model = JsonConvert.DeserializeObject<RespuestaApi>(json);
                        if (model.result != null)
                        {
                            cotizacion.SetMoneda(model.result.source);
                            cotizacion.Precio = model.result.value;
                        }
                        else
                        {
                            cotizacion.Moneda = "Servicio API No Disponible";
                            cotizacion.Precio = 0;
                        }
                    }
                    else
                    {
                        cotizacion.Moneda = "Error: Bad Request";
                        cotizacion.Precio = 0;
                    }
                }
                catch (HttpRequestException httpError)
                {
                    cotizacion.Moneda = "Error: " + httpError.Message;
                    cotizacion.Precio = 0;
                }
            }
            return cotizacion;
        }


        /*De acuerdo al ejercicio se asume que las peticiones
         * 
         * /cotizacion/dolar
         * /cotizacion/euro
         * /cotizacion/real
         * 
         * corresponden al patron api/controlador/action, asi que entonces
         * se deben crear estas acciones, otra formal era, crear una accion como Consultar
         * 
         * /cotizacion/consultar/dolar
         * 
         * y pasar la moneda como parametro
         */

        [HttpGet("[action]")]
        public async Task<Cotizaciones> Dolar()
        {
            return await GetCotizacion("USD");
        }

        [HttpGet("[action]")]
        public async Task<Cotizaciones> Euro()
        {
            return await GetCotizacion("EUR");
        }

        [HttpGet("[action]")]
        public async Task<Cotizaciones> Real()
        {
            return await GetCotizacion("BRL");
        }
    }
}
