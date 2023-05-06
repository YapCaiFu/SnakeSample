
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace SnakeSample.Models
{
    public class Tick
    {
        [JsonProperty("velX")]
        public int VelX { get; set; }
        [JsonProperty("velY")]
        public int VelY{ get; set; }
        [JsonConstructor]
        public Tick() { }
    }
}
