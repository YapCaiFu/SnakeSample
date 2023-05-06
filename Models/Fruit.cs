using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SnakeSample.Models
{
    public class Fruit
    {
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
        public Fruit(int w, int h)
        {
            Random rnd = new Random();
            int x = rnd.Next(0, w);
            int y = rnd.Next(0, h);
            while(x == 0 && y == 0)
            {
                x = rnd.Next(0, w);
                y = rnd.Next(0, h);
            }
            this.X = x;
            this.Y = y;
        }
        public Fruit() { }
    }
}
