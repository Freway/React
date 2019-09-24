using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BR.POINTER.LASTPOSITION.API.Models;
using BR.POINTER.LASTPOSITION.API.Helpers;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace BR.POINTER.LASTPOSITION.API.Repository
{
    public class LastPositionRepository
    {
        public static int CommandTimeOut { get { int time = int.TryParse(AppSetting.Get("SQLTimeOut").ToString(), out time) ? time : 1000; return time; } set { CommandTimeOut = value; } }

        /// <summary>
        /// Executes 'Query..GetLastPosition' procedure by LastPositionInputModel arguments
        /// </summary>
        /// <param name="arg">Argumets for procedure</param>
        /// <returns>APIReturnModel with a list of LastPositionOutputModel if success</returns>
        public static async Task<APIReturnModel> GetLastPosition(LastPositionInputModel arg)
        {
            APIReturnModel ret = new APIReturnModel();
            List<LastPositionOutputModel> model = new List<LastPositionOutputModel>();

            try
            {
                using (var _connection = new SqlConnection(AppSetting.Get("ConnectionStrings", "Production").ToString()))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("LicencePlates", arg.InputData.Count() > 0 ? string.Join(",", arg.InputData) : null, DbType.String);
                    parameters.Add("TokenId", arg.TokenId, DbType.Int64);
                    parameters.Add("OutputString", "", direction: ParameterDirection.Output, dbType: DbType.String);
                    parameters.Add("ReturnValue", direction: ParameterDirection.ReturnValue, dbType: DbType.Int32);

                    var x = await _connection.QueryAsync<LastPositionOutputModel>(AppSetting.Get("Querys", "GetLastPosition"), parameters, commandTimeout: CommandTimeOut, commandType: CommandType.StoredProcedure);

                    model = x.OrderByDescending(m => m.RowReferenceTime).ToList();

                    ret.Message = parameters.Get<string>("OutputString");
                    ret.Success = parameters.Get<int>("ReturnValue") > 0 ? false : true;

                    if (ret.Success) ret.Data = model;

                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;

        }

        /// <summary>
        /// Executes 'Query..GetLastPositionDriver' procedure by LastPositionInputModel arguments
        /// </summary>
        /// <param name="arg">Argumets for procedure</param>
        /// <returns>APIReturnModel with a list of LastPositionOutputModel if success</returns>
        public static async Task<APIReturnModel> GetLastPositionDriver(LastPositionInputModel arg)
        {
            APIReturnModel ret = new APIReturnModel();
            List<LastPositionOutputModel> model = new List<LastPositionOutputModel>();

            try
            {
                using (var _connection = new SqlConnection(AppSetting.Get("ConnectionStrings", "Production").ToString()))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("DriverName", arg.InputData[0], dbType: DbType.String);
                    parameters.Add("OutputString", "", direction: ParameterDirection.Output, dbType: DbType.String);
                    parameters.Add("ReturnValue", direction: ParameterDirection.ReturnValue, dbType: DbType.Int32);

                    var x = await _connection.QueryAsync<LastPositionOutputModel>(AppSetting.Get("Querys", "GetLastPositionDriver"), parameters, commandTimeout: CommandTimeOut, commandType: CommandType.StoredProcedure);

                    model = x.OrderByDescending(m => m.RowReferenceTime).ToList();

                    ret.Message = parameters.Get<string>("OutputString");
                    ret.Success = parameters.Get<int>("ReturnValue") > 0 ? false : true;

                    if (ret.Success) ret.Data = model;

                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;

        }

        /// <summary>
        /// Executes 'Query..GetLastPositionNationalId' procedure by LastPositionInputModel arguments
        /// </summary>
        /// <param name="arg">Argumets for procedure</param>
        /// <returns>APIReturnModel with a list of LastPositionOutputModel if success</returns>
        public static async Task<APIReturnModel> GetLastPositionNationalId(LastPositionInputModel arg)
        {
            APIReturnModel ret = new APIReturnModel();
            List<LastPositionOutputModel> model = new List<LastPositionOutputModel>();

            try
            {
                using (var _connection = new SqlConnection(AppSetting.Get("ConnectionStrings", "Production").ToString()))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("NacionalId", arg.InputData.Count() > 0 ? string.Join(",", arg.InputData) : null, DbType.String);
                    parameters.Add("OutputString", "", direction: ParameterDirection.Output, dbType: DbType.String);
                    parameters.Add("ReturnValue", direction: ParameterDirection.ReturnValue, dbType: DbType.Int32);

                    var x = await _connection.QueryAsync<LastPositionOutputModel>(AppSetting.Get("Querys", "GetLastPositionNationalId"), parameters, commandTimeout: CommandTimeOut, commandType: CommandType.StoredProcedure);

                    model = x.OrderByDescending(m => m.RowReferenceTime).ToList();

                    ret.Message = parameters.Get<string>("OutputString");
                    ret.Success = parameters.Get<int>("ReturnValue") > 0 ? false : true;

                    if (ret.Success) ret.Data = model;

                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;

        }

        /// <summary>
        /// Executes 'Query..GetLastPosition' procedure by LastPositionInputModel arguments
        /// </summary>
        /// <param name="arg">Argumets for procedure</param>
        /// <returns>APIReturnModel with a list of LastPositionOutputModel if success</returns>
        public static async Task<APIReturnModel> GetLastPositionLight(LastPositionInputModel arg)
        {
            APIReturnModel ret = new APIReturnModel();
            List<LastPositionOutputModel> model = new List<LastPositionOutputModel>();

            try
            {
                using (var _connection = new SqlConnection(AppSetting.Get("ConnectionStrings", "Production").ToString()))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("InputData", arg.InputData.Count() > 0 ? string.Join(",", arg.InputData) : null, DbType.String);
                    parameters.Add("OutputString", "", direction: ParameterDirection.Output, dbType: DbType.String);
                    parameters.Add("ReturnValue", direction: ParameterDirection.ReturnValue, dbType: DbType.Int32);

                    var x = await _connection.QueryAsync<LastPositionOutputModel>(AppSetting.Get("Querys", "GetLastPositionLight"), parameters, commandTimeout: CommandTimeOut, commandType: CommandType.StoredProcedure);

                    model = x.OrderByDescending(m => m.RowReferenceTime).ToList();

                    ret.Message = parameters.Get<string>("OutputString");
                    ret.Success = parameters.Get<int>("ReturnValue") > 0 ? false : true;

                    if (ret.Success) ret.Data = model;

                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;

        }


        /// <summary>
        /// Executes 'Query..GetLastPosition' procedure by LastPositionInputModel arguments
        /// </summary>
        /// <param name="arg">Argumets for procedure</param>
        /// <returns>APIReturnModel with a list of LastPositionOutputModel if success</returns>
        public static async Task<APIReturnModel> GetGriversName(string arg)
        {
            APIReturnModel ret = new APIReturnModel();
            List<DriversModel> model = new List<DriversModel>();

            try
            {
                using (var _connection = new SqlConnection(AppSetting.Get("ConnectionStrings", "Production").ToString()))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("DriverName", !string.IsNullOrEmpty(arg) ? arg : null, DbType.String);
                    parameters.Add("OutputString", "", direction: ParameterDirection.Output, dbType: DbType.String);
                    parameters.Add("ReturnValue", direction: ParameterDirection.ReturnValue, dbType: DbType.Int32);

                    var x = await _connection.QueryAsync<DriversModel>(AppSetting.Get("Querys", "GetDriverName"), parameters, commandTimeout: CommandTimeOut, commandType: CommandType.StoredProcedure);

                    model = x.OrderBy(m => m.DriverName).ToList();

                    ret.Message = parameters.Get<string>("OutputString");
                    ret.Success = parameters.Get<int>("ReturnValue") > 0 ? false : true;

                    if (ret.Success) ret.Data = model;

                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;

        }

        public static async Task<APIReturnModel> GetAddress(AddressInputModel arg)
        {
            APIReturnModel ret = new APIReturnModel();
            List<LastPositionOutputModel> model = new List<LastPositionOutputModel>();

            try
            {
                using (var _connection = new SqlConnection(AppSetting.Get("ConnectionStrings", "Production").ToString()))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("Lat", "", direction: ParameterDirection.Input, dbType: DbType.String);
                    parameters.Add("Lng", "", direction: ParameterDirection.Input, dbType: DbType.String);
                    parameters.Add("Raio", "", direction: ParameterDirection.Input, dbType: DbType.String);
                    //parameters.Add("InputData", arg.InputData.Count() > 0 ? string.Join(",", arg.InputData) : null, DbType.String);
                    parameters.Add("OutputString", "", direction: ParameterDirection.Output, dbType: DbType.String);
                    parameters.Add("ReturnValue", direction: ParameterDirection.ReturnValue, dbType: DbType.Int32);

                    var x = await _connection.QueryAsync<LastPositionOutputModel>(AppSetting.Get("Querys", "GetAddress"), parameters, commandTimeout: CommandTimeOut, commandType: CommandType.StoredProcedure);

                    model = x.OrderByDescending(m => m.RowReferenceTime).ToList();

                    ret.Message = parameters.Get<string>("OutputString");
                    ret.Success = parameters.Get<int>("ReturnValue") > 0 ? false : true;

                    if (ret.Success) ret.Data = model;

                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;

        }

    }
}
