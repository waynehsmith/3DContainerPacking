using CromulentBisgetti.ContainerPacking.Entities;
using System.Collections.Generic;

namespace CromulentBisgetti.DemoApp.Models
{

    public class PackSummary
    {
        public int ContainerId { get; set; }
        public string Packed { get; set; }
        public string Unpacked { get; set; }
    }
    public class PackingResultSummary
    {
        public List<ContainerPackingResult> Results { get; set; }
        public string Input { get; set; }
        public List<PackSummary> Output { get; set; }  
    }
}
