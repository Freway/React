using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using BR.POINTER.LASTPOSITION.API.Models;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using BR.POINTER.LASTPOSITION.API.Helpers;
using BR.POINTER.LASTPOSITION.API.Repository;

namespace BR.POINTER.LASTPOSITION.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ControlRequestsFilter]
    //[Produces("application/json")]
    public class TokenController : ControllerBase
    {
        //public string Logging { get; set; }
        public string Err { get; set; }

        [HttpPost("Get")]
        public async Task<ActionResult<TokenReturn>> GetByAPI(Token body)
        {
            //  VALIDATING MODEL
            if (!ModelState.IsValid) return CustomError(502, ModelState.Root.Errors);

            string json = JsonConvert.SerializeObject(body);
            TokenReturn _body = new TokenReturn();

            Log.AddLogging(body.ToString() + "; ", HttpContext.Connection.Id);

            try
            {
                //  CREATE A REQUEST
                var client = new RestClient(AppSetting.Get("Token", "Url"));
                var request = new RestRequest(Method.OPTIONS);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Content-Type", AppSetting.Get("Token", "ContentType"));
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                var response = client.Execute<TokenReturn>(request);
                _body = response.Data;
            }
            catch (Exception ex)
            {
                return CustomError(500, $"{ex.Message}|Parameters: {json}");
            }

            if (!_body.success) return CustomNotFound(_body);

            return CustomOk(_body);
        }

        //[HttpPost("Get")]
        //[NonAction]
        //public async Task<ActionResult<TokenReturn>> GetByDatabase(Token body)
        //{
        //    //  VALIDATING MODEL
        //    if (!ModelState.IsValid) return CustomError(502, ModelState.Root.Errors);

        //    string json = JsonConvert.SerializeObject(body);
        //    TokenReturn _body = new TokenReturn();

        //    Log.AddLogging(body.ToString() + "; ", HttpContext.Connection.Id);

        //    try
        //    {
        //        _body = await TokenRepository.GetToken(body);
        //    }
        //    catch (Exception ex)
        //    {
        //        return CustomError(500, $"{ex.Message}|Parameters: {json}");
        //    }

        //    //if (!_body.success) return CustomNoContent();

        //    return CustomOk(_body);
        //}

        #region Override Methods

        [NonAction]
        public OkObjectResult CustomOk(object value)
        {
            Log.AddLogging($"Sending To Server ... ; ", HttpContext.Connection.Id);   
            Log.Info(HttpContext.Connection.Id);
            return base.Ok(value);
        }

        [NonAction]
        public ObjectResult CustomError(int statusCode, object message, object exception = null)
        {
            Log.AddLogging($"ERROR: {statusCode} : {(exception ?? message)}; ", HttpContext.Connection.Id);
            //Logging += $"ERROR: {statusCode} : {exception}; ";
            //Logging += "END; ";
            Log.Error(HttpContext.Connection.Id);
            return base.StatusCode(statusCode, message);
        }

        [NonAction]
        public NoContentResult CustomNoContent()
        {
            Log.AddLogging("NoContent Found ... ; ", HttpContext.Connection.Id);
            //Logging += "END; ";
            Log.Info(HttpContext.Connection.Id);
            return base.NoContent();
        }

        [NonAction]
        public NotFoundResult CustomNotFound()
        {
            Log.AddLogging("NotFound ... ;", HttpContext.Connection.Id);
            //Logging += "NotFound ... ;";
            //Logging += "END; ";
            Log.Warn(HttpContext.Connection.Id);
            return base.NotFound();
        }

        [NonAction]
        public NotFoundObjectResult CustomNotFound(object value)
        {
            Log.AddLogging($"NotFound ...;", HttpContext.Connection.Id);
            //Logging += "NotFound ... ;";
            //Logging += "END; ";
            Log.Warn(HttpContext.Connection.Id);
            return base.NotFound(value);
        }

        #endregion
    }
}