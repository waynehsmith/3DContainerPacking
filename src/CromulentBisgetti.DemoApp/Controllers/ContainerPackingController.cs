using System.Collections.Generic;
using CromulentBisgetti.ContainerPacking;
using CromulentBisgetti.ContainerPacking.Entities;
using CromulentBisgetti.DemoApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;
using System;

namespace CromulentBisgetti.DemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerPackingController : ControllerBase
    {
        public ActionResult<PackingResultSummary> Post([FromBody] ContainerPackingRequest request)
        {
            var pio = new PackingResultSummary();

            var result = PackingService.Pack(
                request.Containers,
                request.ItemsToPack,
                request.AlgorithmTypeIDs);

            var input = DumpItems(request.ItemsToPack);

            var lstOutput = new List<PackSummary>();
            foreach (var item in result)
            {
                try
                {
                    var aprData = item.AlgorithmPackingResults[0];
                    var ps = new PackSummary
                    {
                        ContainerId = item.ContainerID,
                        Packed = DumpItems(aprData.PackedItems),
                        Unpacked = DumpItems(aprData.UnpackedItems)
                    };
                    lstOutput.Add(ps);
                }
                catch
                {

                }
            }
            
            pio.Results = result;
            pio.Input = input;
            pio.Output = lstOutput;
            return pio;
        }
        private static string DumpItems(List<Item> allItems)
        {
            var builder = new StringBuilder();

            foreach (var pItem in allItems)
            {
                builder.Append(pItem.DisplayValue(Item.FormatFlags.All | Item.FormatFlags.Formatted));
                var ip = new ItemOrientation(pItem);
                
                builder.Append(ip.ToString() + "\n");
            }

            return builder.ToString();
        }
    }
}