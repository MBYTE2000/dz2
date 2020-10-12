using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Uiguuna_TG
{
    public class WeatherAPI
    {
        public class list
        {
            public class coord
            {
                public int lat { get; set; }
                public int lon { get; set; }
            }

            public coord Coord { get; set; }
            public int id { get; set; }
            public string name { get; set; }
        }


        public list[] List { get; set; } //лист не парсится
        public string message { get; set; }
        public int cod { get; set; }
        public int count { get; set; }
        
    }
}
