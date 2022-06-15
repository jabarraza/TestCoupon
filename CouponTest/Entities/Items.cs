using System.Collections.Generic;

namespace CouponTest.Entities
{
    //Clase con la cual generamos los items enviados por mercado libre para su procesamiento
    public class Items
    {
        public decimal amount { get; set; }
        public List<string> Item_ids { get; set; }
    }
}
