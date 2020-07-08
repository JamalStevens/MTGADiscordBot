using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MTGADiscordBot
{
    class JsonCard
    {
        [JsonProperty(PropertyName = "set")]
        public string Setabbr { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Cardname { get; set; }

        [JsonProperty(PropertyName = "set_name")]
        public string Setname { get; set; }

        [JsonProperty(PropertyName = "image_uris")]
        public imageuris Imageurl { get; set; }
        //public list colors { get; set; }



        public class imageuris
        {
            public string normal { get; set; }
        }

    }
}
