
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace SnakeSample.Models
{
    [Serializable]
    public class ValidateData
    {
        public State state { get; set; }

        public List<Tick> ticks { get; set; }
       

    }
}
