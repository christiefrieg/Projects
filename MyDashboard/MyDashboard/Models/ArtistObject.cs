using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class ArtistObject
    {
        public ExternalUrl external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public string name { get; set; }
    }
    public class ExternalUrl
    {
        public string key {get;set;}
        public string value {get;set;}
    }
}