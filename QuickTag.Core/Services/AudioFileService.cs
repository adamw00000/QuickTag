using QuickTag.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTag.Services
{
    public class AudioFileService
    {
        public Track LoadAudioTags(string filePath)
        {
            var file = TagLib.File.Create(filePath);
            var tag = file.Tag;

            return new Track(tag.Title, tag.FirstPerformer, tag.Pictures[0]?.Data.Data);
        }
    }
}
