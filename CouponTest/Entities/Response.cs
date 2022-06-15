using System.Collections.Generic;

namespace CouponTest.Entities
{
    //Clase con las respuesta del metodo que obtiene el total del cupon y los items a comprar.
    public class Response
    {
        public List<string> items { get; set; }
        public decimal total { get; set; }
    }
}
