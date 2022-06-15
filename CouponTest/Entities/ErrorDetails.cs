using Newtonsoft.Json;

namespace CouponTest.Entities
{
    //Clase utiliaza pra regresar un error cuando ocurre una excepcion
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
