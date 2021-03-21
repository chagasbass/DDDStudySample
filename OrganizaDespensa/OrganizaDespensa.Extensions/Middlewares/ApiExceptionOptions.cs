using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OrganizaDespensa.Extensions.Entities;
using System;

namespace OrganizaDespensa.Extensions.Middlewares
{
    /// <summary>
    /// Classe que contém as opções para add os dados do erro
    /// </summary>
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ProblemDetail> AddResponseDetails { get; set; }
        public Func<Exception,LogLevel> DetermineLogLevel { get; set; }
    }
}
