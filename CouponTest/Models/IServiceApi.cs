using CouponTest.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouponTest.Models
{
    /// <summary>
    /// Interface que se contiene declaraciones de metodos
    /// </summary>
    public interface IServiceApi
    {
        Task<Response> GetListItems(Items item);
        Task<List<ItemJson>> GetFavorities();
        Task<List<ItemJson>> GetFavoritiesById(string id);
    }
}
