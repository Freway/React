using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BR.POINTER.LASTPOSITION.API.Repository;
using BR.POINTER.LASTPOSITION.API.Helpers;
using BR.POINTER.LASTPOSITION.API.Models;
using Microsoft.AspNetCore.Cors;

namespace BR.POINTER.LASTPOSITION.API.Controllers
{
    [EnableCors("AllowOrigin"), Route("api/[controller]")]
    [ApiController]
    [ControlRequestsFilter]
    public class PositionController : Controller
    {
        [HttpPost("GetVehicleStatus")]
        public async Task<ActionResult<APIReturnModel>> GetVehicle(LastPositionInputModel body)
        {
            if (!ModelState.IsValid) return CustomError(502, ModelState.Root.Errors);

            var _body = new APIReturnModel() { Data = new List<LastPositionOutputModel>()};

            Log.AddLogging(body.ToString(), HttpContext.Connection.Id);

            try
            {
                _body = await LastPositionRepository.GetLastPosition(body);
            }
            catch (Exception ex)
            {
                return CustomError(500, $"${ex.Message} | Parameters: {body.ToString()}", ex);
            }

            Log.AddLogging($"Success: {_body.Success}, Message: {_body.Message}", HttpContext.Connection.Id);

            if (!_body.Success) return CustomNotFound(_body);

            return CustomOk(_body);
        }

        [HttpPost("GetVehicleStatusByDriverName")]
        public async Task<ActionResult<APIReturnModel>> GetVehicleByDriver(LastPositionInputModel body)
        {
            if (!ModelState.IsValid) return CustomError(502, ModelState.Root.Errors);

            var _body = new APIReturnModel() { Data = new List<LastPositionOutputModel>() };

            Log.AddLogging(body.ToString(), HttpContext.Connection.Id);

            try
            {
                _body = await LastPositionRepository.GetLastPositionDriver(body);
            }
            catch (Exception ex)
            {
                return CustomError(500, $"${ex.Message} | Parameters: {body.ToString()}", ex);
            }

            Log.AddLogging($"Success: {_body.Success}, Message: {_body.Message}", HttpContext.Connection.Id);

            if (!_body.Success) return CustomNotFound(_body);

            return CustomOk(_body);
        }

        [HttpPost("GetVehicleStatusByNationalId")]
        public async Task<ActionResult<APIReturnModel>> GetVehicleStatusByNationalId(LastPositionInputModel body)
        {
            if (!ModelState.IsValid) return CustomError(502, ModelState.Root.Errors);

            var _body = new APIReturnModel() { Data = new List<LastPositionOutputModel>() };

            Log.AddLogging(body.ToString(), HttpContext.Connection.Id);

            try
            {
                _body = await LastPositionRepository.GetLastPositionNationalId(body);
            }
            catch (Exception ex)
            {
                return CustomError(500, $"${ex.Message} | Parameters: {body.ToString()}", ex);
            }

            Log.AddLogging($"Success: {_body.Success}, Message: {_body.Message}", HttpContext.Connection.Id);

            if (!_body.Success) return CustomNotFound(_body);

            return CustomOk(_body);
        }

        [HttpPost("GetVehicleStatusLight")]
        public async Task<ActionResult<APIReturnModel>> GetVehicleLight(LastPositionInputModel body)
        {
            if (!ModelState.IsValid) return CustomError(502, ModelState.Root.Errors);

            var _body = new APIReturnModel() { Data = new List<LastPositionOutputModel>() };

            Log.AddLogging(body.ToString(), HttpContext.Connection.Id);

            try
            {
                _body = await LastPositionRepository.GetLastPositionLight(body);
            }
            catch (Exception ex)
            {
                return CustomError(500, $"${ex.Message} | Parameters: {body.ToString()}", ex);
            }

            Log.AddLogging($"Success: {_body.Success}, Message: {_body.Message}", HttpContext.Connection.Id);

            if (!_body.Success) return CustomNotFound(_body);

            return CustomOk(_body);
        }


        [HttpPost("GetDriversName")]
        public async Task<ActionResult<APIReturnModel>> GetDriversName(string name)
        {
            if (!ModelState.IsValid) return CustomError(502, ModelState.Root.Errors);

            var _body = new APIReturnModel() { Data = new List<DriversModel>() };

            Log.AddLogging(name, HttpContext.Connection.Id);

            try
            {
                _body = await LastPositionRepository.GetGriversName(name);
            }
            catch (Exception ex)
            {
                return CustomError(500, $"${ex.Message} | Parameters: {name}", ex);
            }

            Log.AddLogging($"Success: {_body.Success}, Message: {_body.Message}", HttpContext.Connection.Id);

            if (!_body.Success) return CustomNotFound(_body);

            return CustomOk(_body);
        }

        [HttpPost("GetAddressbyRadius")]
        public async Task<ActionResult<APIReturnModel>> GetAddress(AddressInputModel body)
        {
            if (!ModelState.IsValid) return CustomError(502, ModelState.Root.Errors);

            var _body = new APIReturnModel() { Data = new List<LastPositionOutputModel>() };

            Log.AddLogging(body.ToString(), HttpContext.Connection.Id);

            try
            {
                _body = await LastPositionRepository.GetAddress(body);
            }
            catch (Exception ex)
            {
                return CustomError(500, $"${ex.Message} | Parameters: {body}", ex);
            }

            Log.AddLogging($"Success: {_body.Success}, Message: {_body.Message}", HttpContext.Connection.Id);

            if (!_body.Success) return CustomNotFound(_body);

            return CustomOk(_body);
        }

        #region Override Methods

        [NonAction]
        public OkObjectResult CustomOk(object value)
        {
            Log.AddLogging($"Sending ... ", HttpContext.Connection.Id);
            Log.Info(HttpContext.Connection.Id);
            return base.Ok(value);
        }

        [NonAction]
        public ObjectResult CustomError(int statusCode, object message, object exception = null)
        {
            Log.AddLogging($"ERROR: {statusCode} : {(exception ?? message)}", HttpContext.Connection.Id);
            //Logging += $"ERROR: {statusCode} : {exception}; ";
            //Logging += "END; ";
            Log.Error(HttpContext.Connection.Id);
            return base.StatusCode(statusCode, message);
        }

        [NonAction]
        public NoContentResult CustomNoContent()
        {
            Log.AddLogging("NoContent Found ... ", HttpContext.Connection.Id);
            //Logging += "END; ";
            Log.Info(HttpContext.Connection.Id);
            return base.NoContent();
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