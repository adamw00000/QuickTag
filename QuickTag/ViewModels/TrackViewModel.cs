using Avalonia.Media.Imaging;
using QuickTag.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTag.ViewModels
{
    public class TrackViewModel: ViewModelBase
    {
        private readonly Track _track;
        private Bitmap _cover;

        public TrackViewModel(Track track)
        {
            _track = track;
        }

        public string Title { get => _track.Title; set => _track.Title = value; }
        public string Artist { get => _track.Artist; set => _track.Artist = value; }

        public void LoadCover(int displaySize)
        {
            if (_track.Cover == null)
                return;

            using var stream = new MemoryStream(_track.Cover);
            Cover = Bitmap.DecodeToWidth(stream, displaySize);
        }

        public Bitmap Cover
        {
            get => _cover;
            set
            {
                this.RaiseAndSetIfChanged(ref _cover, value);
                using var stream = new MemoryStream(_track.Cover);
                value.Save(stream);
            }
        }
    }
}
