using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BR.POINTER.LASTPOSITION.API.Helpers;
using BR.POINTER.LASTPOSITION.API.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace BR.POINTER.LASTPOSITION.API.Repository
{
    public class TokenRepository
    {
        public static int CommandTimeOut { get { int time = int.TryParse(AppSetting.Get("SQLTimeOut").ToString(), out time) ? time : 1000; return time; } set { CommandTimeOut = value; } }


        /// <summary>
        /// Return if the Token is valid.        
        /// </summary>
        /// <param name="tokenId">Token to validate</param>
        /// <returns>true if exists, false if not.</returns>
        public static bool ValidateToken(long tokenId)
        {
            bool ret = true;

            try
            {
                using (var _connection = new SqlConnection(AppSetting.Get("ConnectionStrings", "Production").ToString()))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@TokenId", tokenId);
                    parameters.Add("@Return", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);                    

                    var result = _connection.Execute(AppSetting.Get("Querys", "TokenAliveUpdate"), parameters, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeOut);                    

                    if (parameters.Get<int>("@Return") != 1) ret = !ret;

                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        public async static Task<TokenReturn> GetToken(Token arg, string connectionId = null)
        {
            TokenReturn ret = new TokenReturn();

            try
            {
                using (var _connection = new SqlConnection(AppSetting.Get("ConnectionStrings", "Production").ToString()))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Login", arg.user);
                    parameters.Add("@Password", arg.password);
                    parameters.Add("@ClientAppStringId", AppSetting.Get("Token", "ClientAppStringId"));
                    parameters.Add("@ClientAppId", int.Parse(AppSetting.Get("Token", "ClientAppId")));
                    parameters.Add("@ClientAppIP", (connectionId != null? Log.GetRequest(connectionId).IpAddress : string.Empty));
                    parameters.Add("@AliveInterval_sec", int.Parse(AppSetting.Get("Token", "AliveInterval_sec")));
                    parameters.Add("@LogFlags", int.Parse(AppSetting.Get("Token", "LogFlags")));                    
                    parameters.Add("@TokenId", 0, dbType: DbType.Int64, direction: ParameterDirection.InputOutput);

                    var result = await _connection.ExecuteAsync(AppSetting.Get("Querys", "GetToken"), parameters, commandType: CommandType.StoredProcedure, commandTimeout: CommandTimeOut);

                    ret.tokenId = parameters.Get<string>("@TokenId");

                    if (string.IsNullOrEmpty(ret.tokenId)) ret.success = !ret.success;

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
