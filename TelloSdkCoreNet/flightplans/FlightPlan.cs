using Newtonsoft.Json;
using System.Collections.Generic;
namespace TelloSdkCoreNet.flightplans
{
    public class FlightPlan
    {
        public string Name { get; set; }
        public List<FlightPlanItem> Items { get; set; }

        public FlightPlan()
        {
            Items = new List<FlightPlanItem>();
        }
        public void Save(string path)
        {
            if (Name == null)
            {
                Name = $"FP_{System.DateTime.Now.ToString().Replace("/","-").Replace(":","")}_{System.Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8)}";
            }
            var contents = JsonConvert.SerializeObject(this);
            var dir = $@"{System.IO.Directory.GetCurrentDirectory()}\{this.Name}.json.fp";
            System.IO.File.WriteAllText(dir, contents);
        }

        public static FlightPlan Materialize(string planJson)
        {
            return JsonConvert.DeserializeObject<FlightPlan>(planJson);
        }
    }
}
