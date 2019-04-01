using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainDelay.Trafikverket;

namespace TrainDelay.Models
{
    public class ResultItem
    {
        public List<TrainStation> TrainStation { get; set; }
        public List<TrainAnnouncement> TrainAnnouncement { get; set; }
    }
}
