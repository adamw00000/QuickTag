using QuickTag.Models;

namespace QuickTag.Services
{
    public class AudioFileService
    {
        public Track LoadAudioTags(string filePath)
        {
            var file = TagLib.File.Create(filePath);
            var tag = file.Tag;

            var coverImage = tag.Pictures[0]?.Data.Data;
            // might optimize later - memory usage is quite big and can become problematic
            // https://codereview.stackexchange.com/questions/9785/adding-many-items-generated-from-taglib-is-incredibly-slow-and-expensive-how-c
            // suggestion - loading already resized Bitmap

            return new Track(tag.Title, tag.FirstPerformer, coverImage);
        }
    }
}
