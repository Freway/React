using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BR.POINTER.LASTPOSITION.API.Models
{
    public class APIReturnModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public APIReturnModel(){ Success = false; Message = null; Data = null; }
    }
}
