using CouponTest.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CouponTest.CustomExceptionsMid
{
    //Clase que crea los Errores customizados para los Errores que puedan ocurrir su ejecucion
    public class ExceptionMid
    {
        private readonly RequestDelegate _next;

        public ExceptionMid(RequestDelegate next) { 
            _next = next;

        }

        public async Task Invoke(HttpContext httpContext)
        {

            try {
                await _next(httpContext);
            }
            catch (Exception)
            {
                await HandleExceptionAsync(httpContext);
            }
        }

        private Task HandleExceptionAsync(HttpContext context) {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(new ErrorDetails 
            { 
            StatusCode = context.Response.StatusCode,
            Message ="Internal Server Error From Custom"
            }.ToString());    
        }
    }
}
