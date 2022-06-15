using CouponTest.CustomExceptionsMid;
using CouponTest.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CouponTest.Middlewares
{
    //Clase que maneja el metodo para el uso de excepciones custom y poder registrar en el servicio (Startup.cs)
    public static class ErrorMiddleware
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app) {

            app.UseMiddleware<ExceptionMid>();

        }
    }
}
