using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class CartDeserializeModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string supplier { get; set; }
        public string category { get; set;}
        public string quanity { get; set; }


    }
}
