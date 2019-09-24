using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BR.POINTER.LASTPOSITION.API.Models
{
    /// <summary>
    /// Model of Input
    /// </summary>    
    public class LastPositionInputModel
    {
        /// <summary>
        /// TokenId
        /// </summary>
        public string TokenId { get; set; }
        /// <summary>
        /// List of Plates to Search.
        /// If empty, retrives all Plates
        /// </summary>
        [Required(ErrorMessage = "Please enter a valid array of InputData")]
        public string[] InputData { get; set; }
       
        public override string ToString()
        {
            return $"InputData : {string.Join(",", InputData)}; TokenId : {TokenId}";
        }

    }

    public class AddressInputModel
    {      
        public double?Lat { get; set; }

        public double? Lng { get; set; }

        public int? Raio { get; set; }
    }

    public class LastPositionOutputModel
    {
        /// <summary>
        /// Plate
        /// </summary>
        public string LicencePlate { get; set; }
        /// <summary>
        /// Last Communication
        /// </summary>
        public DateTime RowReferenceTime { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public double? Lat { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public double? Long { get; set; }

        //public int Raio { get; set; }

    }

    public class DriversModel {
        public int DriverId { get; set; }
        public string DriverName { get; set; }
    }
}
