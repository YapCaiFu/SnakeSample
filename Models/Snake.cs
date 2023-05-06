using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SnakeSample.Models
{
    public class Snake
    {
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
        [JsonProperty("velX")]
        public int VelX { get; set; }
        [JsonProperty("velY")]
        public int VelY { get; set; }
        public Snake(int x, int y, int velX, int velY)
        {
            X = x;
            Y = y;
            VelX = velX;
            VelY = velY;
        }
        public Snake()
        {
            X = 0;
            Y = 0;
            VelY = 0;
            VelX = 1;
        }
        }
    }
