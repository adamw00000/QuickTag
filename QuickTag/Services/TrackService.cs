using QuickTag.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;

namespace QuickTag.Services
{
    public class TrackService
    {
        private readonly AudioFileService _audioFileService = new();
        private readonly List<string> _audioExtensions = new() { ".mp3", ".flac", ".m4a", ".opus", ".wav" };

        public IObservable<Track> LoadTracks(string directory) => GetAudioFiles(directory).Select(path => _audioFileService.LoadAudioTags(path));
        public IObservable<int> CountTracks(string directory) => GetAudioFiles(directory).Count();

        private IObservable<string> GetAudioFiles(string directory)
        {
            return Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories)
                            .ToObservable(RxApp.TaskpoolScheduler)
                            .Where(path => IsMusicFile(path));
        }

        private bool IsMusicFile(string path) => _audioExtensions.Contains(Path.GetExtension(path));
    }
}
