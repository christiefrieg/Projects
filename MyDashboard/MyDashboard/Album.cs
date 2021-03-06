//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dashboard
{
    using System;
    using System.Collections.Generic;
    
    public partial class Album
    {
        public Album()
        {
            this.Tracks = new HashSet<Track>();
        }
    
        public int AlbumID { get; set; }
        public Nullable<int> SpotifyArtistID { get; set; }
        public string AlbumName { get; set; }
        public string Link { get; set; }
        public Nullable<int> SpotifyID { get; set; }
        public string Image { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
    
        public virtual SpotifyArtist SpotifyArtist { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
