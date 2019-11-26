using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BR.POINTER.LASTPOSITION.API.Helpers;
using BR.POINTER.LASTPOSITION.API.Models;

namespace BR.POINTER.LASTPOSITION.API.Models
{
    public class TokenValidate
    {
        public int statusCode { get; set; }
        public string message { get; set; }
    }

    /// <summary>
    /// Input method for validate Token
    /// </summary>
    public class Token
    {
        public static string HeaderToken => AppSetting.Get("Header", "Token");
        /// <summary>
        /// UserName for Token
        /// </summary>
        [Required(ErrorMessage="Please enter a valid UserName")]
        public string user { get; set; }
        /// <summary>
        /// Password for Token
        /// </summary>
        [Required(ErrorMessage = "Please enter a valid Password")]        
        public string password { get; set; }    

        public Token() { user = string.Empty; password = string.Empty ; }

        public override string ToString()
        {
            return $"{{user: {user}, password: {password}}}";
        }
    }

    public class TokenReturn
    {
        public bool success { get; set; }
        public string tokenId { get; set; }

        public TokenReturn() { success = false; tokenId = string.Empty; }
    }
}
