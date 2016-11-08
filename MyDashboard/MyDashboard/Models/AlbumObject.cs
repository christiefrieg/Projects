using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{    public class AlbumObject
    {
        public string album_type { get; set; }
        public string[] artists { get; set; }
        public string[] available_markets { get; set; }
        public ExternalUrl external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public ImageObject[] images { get; set; }
    }
    public class ImageObject
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }
}