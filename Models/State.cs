
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace SnakeSample.Models
{
    public class State
    {
        [Key]
        [JsonProperty("gameID")]
        public string GameID { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("score")]
        public int Score { get; set; }
        [JsonProperty("fruit")]
        public Fruit Fruit { get; set; }
        [JsonProperty("snake")]
        public Snake Snake { get; set; }
        public State(int w,int h) {
            var gameID = generateUniqueId();
            GameID = gameID;
            Snake = new Snake();
            Fruit = new Fruit(w, h);
            Score = 0;
            Width = w;
            Height = h;
        }
        [JsonConstructor]
        public State() { }

        public string generateUniqueId()
        {
            String date= DateTime.Now.Millisecond.ToString();
            using(var hash = SHA256.Create())
            {
                byte[] data =hash.ComputeHash(Encoding.UTF8.GetBytes(date));
                var sBuilder =new StringBuilder();
                for(int i=0;i<data.Length;i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

    }
}
