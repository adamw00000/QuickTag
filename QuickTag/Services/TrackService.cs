using QuickTag.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTag.Services
{
    public class TrackService
    {
        private readonly AudioFileService _audioFileService = new();
        private readonly List<string> _audioExtensions = new() { ".mp3", ".flac", ".m4a", ".opus", ".wav" };

        public IEnumerable<Track> LoadTracks(string directory)
        {
            var extensionRegex = $"*.({string.Join('|', _audioExtensions)})";

            return Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories)
                .Where(path => _audioExtensions.Contains(Path.GetExtension(path)))
                .Select(path => _audioFileService.LoadAudioTags(path));
        }
    }
}
