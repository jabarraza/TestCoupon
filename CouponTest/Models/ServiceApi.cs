using CouponTest.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CouponTest.Models
{
    //Clase que implementa los metodos de la interfaz IServiceApi
    public class ServiceApi : IServiceApi
    {
        //Declaramos variable statica para obtener el path del api de mercado libre
        private static string path = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["PATH"];

        //Decalaramos la instancia de la interfaz para abrir un client 
        private readonly IHttpClientFactory _httpClienFactory;

        //Pasamos la instancia declarada (dependenacia) en el constructuor
        //para poder generar el client.
        public ServiceApi(IHttpClientFactory httpClientFactory)
        {
            _httpClienFactory = httpClientFactory;
        }
        
        List<ItemJson> result = new List<ItemJson>();

        // Obtiene la lista de items favoritos sin filtros o Id
        public async Task<List<ItemJson>> GetFavorities()
        {
            var client = _httpClienFactory.CreateClient("MercadoLibre");
            var response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();
            result = JsonConvert.DeserializeObject<List<ItemJson>>(response.Content.ReadAsStringAsync().Result);
            result = result.OrderByDescending(x => x.body.sold_quantity).Take(5).ToList();
            return result;
        }

        // Obtiene la lista de items favoritos filtrados por Id
        public async Task<List<ItemJson>> GetFavoritiesById(string id)
        {
            string parametro = "ids=" + string.Join(",", id);
            var client = _httpClienFactory.CreateClient("MercadoLibre");
            var response = await client.GetAsync(path + '?'+ parametro);
            response.EnsureSuccessStatusCode();
            result = JsonConvert.DeserializeObject<List<ItemJson>>(response.Content.ReadAsStringAsync().Result);
            result = result.Where(r => r.code == "200").OrderByDescending(x => x.body.sold_quantity).Take(5).ToList();
            return result;
        }

        // Obtiene la lista de items enviados y
        // regresa el valor igual o menor al cupon junto a los items a seleccionar
        public async Task<Response> GetListItems(Items item)
        {

            decimal limite = 0;
            decimal total = 0;
            Response respuesta = new Response();
            string parametro = "ids=" + string.Join(",", item.Item_ids);
            var client = _httpClienFactory.CreateClient("MercadoLibre");
            var response = await client.GetAsync(path + '?' + parametro);
            response.EnsureSuccessStatusCode();
            result = JsonConvert.DeserializeObject<List<ItemJson>>(response.Content.ReadAsStringAsync().Result);
            result = result.OrderBy(x => x.body.price).ToList();
            List<string> itemsp = new List<string>();

            foreach (var pr in result)
            {
                if (pr.body.price <= item.amount && limite <= item.amount)
                {
                    limite += pr.body.price;
                    if (limite <= item.amount)
                    {
                        string respuestaItem = null;
                        respuestaItem = pr.body.id;
                        itemsp.Add(respuestaItem);
                        total += pr.body.price;
                    }


                }
                respuesta.items = itemsp;
                respuesta.total = total;
            }
            return respuesta;
        }
    }
       
}
