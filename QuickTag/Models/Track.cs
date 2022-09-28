using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTag.Models
{
    public class Track
    {
        public Track(string title, string artist, byte[] cover)
        {
            Title = title;
            Artist = artist;
            Cover = cover;
        }

        public string Title { get; set; }
        public string Artist { get; set; }
        public byte[] Cover { get; set; }
    }

}
