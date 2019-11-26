using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BR.POINTER.LASTPOSITION.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BR.POINTER.LASTPOSITION.API.Helpers
{    
    public class RequestData
    {
        public string IpAddress { get; set; }
        public string Port { get; set; }        
        public string ConnectionId { get; set; }
        public HttpRequest Request { get; set; }  
        public DateTime Start { get; set; }
        public TimeSpan Duration => DateTime.Now.Subtract(Start);
        public string Logging { get; set; }        
    }

    public class ControlRequestsFilter : ActionFilterAttribute
    {
        //public static RequestData Request = new RequestData();
        //public static RequestData EndRequest = new RequestData();
        /// <summary>
        /// This method is called before a controller action is executed.
        /// </summary>        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Log.log.Info("OnActionExecuting");
            RequestData Request = new RequestData
            {
                IpAddress = context.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString(),
                Port = context.HttpContext.Features.Get<IHttpConnectionFeature>().RemotePort.ToString(),
                ConnectionId = context.HttpContext.Connection.Id,
                Request = context.HttpContext.Request
            };

            Log.Request.Add(Request);
            base.OnActionExecuting(context);
        }
        /// <summary>
        /// This method is called after a controller action is executed.
        /// </summary> 
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //Log.log.Info("OnActionExecuted");
            base.OnActionExecuted(context);
        }
        /// <summary>
        /// This method is called before a controller action result is executed.
        /// </summary> 
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            //Log.log.Info("OnResultExecuting");
            base.OnResultExecuting(context);
        }
        /// <summary>
        /// This method is called after a controller action result is executed.
        /// </summary> 
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            //Log.log.Info("OnResultExecuted");
            base.OnResultExecuted(context);
        }

    }

    /// <summary>
    /// Operation filter to add the requirement of the custom header
    /// </summary>
    public class TokenHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = Token.HeaderToken,
                In = "header",
                Type = "long",
                Required = false // set to false if this is optional
            });
        }
    }
}
