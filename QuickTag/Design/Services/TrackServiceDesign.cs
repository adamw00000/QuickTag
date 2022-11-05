using QuickTag.Models;
using QuickTag.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTag.Design.Services
{
    public class TrackServiceDesign: ITrackService
    {
        public int CountTracks(string directory)
        {
            return DesignData.Tracks.Count + 3;
        }

        public IEnumerable<MusicTrack> LoadTracks(string directory)
        {
            return DesignData.Tracks;
        }
    }
}
