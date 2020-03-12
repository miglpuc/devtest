using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace backend.Models.Entities
{
    public class Driver
    {
        [JsonProperty(Order = 1)]
        public int DriverId { get; set; }
        public string Dni { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Points { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [JsonIgnore]
        public virtual List<DriverVehicle> DriverVehicles { get; set; }
    }

    public class DriverResponse: Driver
    {
        [JsonProperty(Order = 2)]
        public virtual List<Vehicle> Vehicles { get; set; }
    }

    public class DriverRequest : Driver
    {
        [JsonProperty(Order = 2)]
        public virtual Vehicle Vehicle { get; set; }
    }

    public class DriverVehicle
    {
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
