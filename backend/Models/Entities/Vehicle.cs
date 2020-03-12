using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace backend.Models.Entities
{
    public class Vehicle
    {
        [JsonProperty(Order = 1)]
        public int VehicleId { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [JsonIgnore]
        public virtual List<DriverVehicle> DriverVehicles { get; set; }
    }

    public class VehicleResponse: Vehicle
    {
        [JsonProperty(Order = 2)]
        public virtual List<Driver> Drivers { get; set; }
    }

    public class VehicleRequest : Vehicle
    {
        [JsonProperty(Order = 2)]
        public virtual Driver Driver { get; set; }
    }
}
