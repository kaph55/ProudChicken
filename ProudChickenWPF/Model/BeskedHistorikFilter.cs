using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProudChickenWPF.Model
{
    public class BeskedHistorikFilter
    {
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public required string PostNummer { get; set; }
        public required string KundeNavn { get; set; }
       
        public required string ByNavn { get; set; }
    }
}
