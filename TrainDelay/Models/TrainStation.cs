using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainDelay.Models
{
    public class TrainStation
    {
        public string AdvertisedLocationName { get; set; }
        public Geometry Geometry { get; set; }
    }
}
