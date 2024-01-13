using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransformationObjects.Payloads
{
    public class HistoryOrderOutput
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public string StatusDate { get; set; }

        public string Status { get; set; }  
    }
}
