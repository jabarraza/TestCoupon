using CouponTest.Entities;
using CouponTest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CouponTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MercadoLibreController : ControllerBase
    {
        //Decalaramos la instancia de la interfaz de los metodos a utilizar
        private readonly IServiceApi serviceApi;

        //Pasamos la instancia declarada (dependencia) en el constructuor y utilizar los metodos
        public MercadoLibreController(IServiceApi serviceApi) { 
            this.serviceApi = serviceApi;
        }
        /// <summary>
        /// post method that obtains the amount of items based on the amount of the coupon
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/coupon/")]
        public async Task<ActionResult> Post(Items items)
        {
            var result = await serviceApi.GetListItems(items);
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// get method that gets all items by favorite
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/coupon/stats")]
        public async Task<ActionResult> Get()
        {
            var result = await serviceApi.GetFavorities();
            return result != null ? Ok(result) : NotFound();
        }

        /// <summary>
        /// get method that gets all items by Id (favorite)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/coupon/stats/{id?}")]
        public async Task<ActionResult> GetItems(string id)
        {
            var result = await  serviceApi.GetFavoritiesById(id);
            return result != null ? Ok(result) : NotFound();
        }
    }
}
