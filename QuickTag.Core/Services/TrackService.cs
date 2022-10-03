using QuickTag.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuickTag.Services
{
    public class TrackService
    {
        private readonly AudioFileService _audioFileService = new();
        private readonly List<string> _audioExtensions = new() { ".mp3", ".flac", ".m4a", ".opus", ".wav" };

        public IEnumerable<Track> LoadTracks(string directory) => GetAudioFiles(directory).Select(path => _audioFileService.LoadAudioTags(path));
        public int CountTracks(string directory) => GetAudioFiles(directory).Count();

        private IEnumerable<string> GetAudioFiles(string directory)
        {
            return Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories)
                            .Where(path => IsMusicFile(path));
        }

        private bool IsMusicFile(string path) => _audioExtensions.Contains(Path.GetExtension(path));
    }
}
