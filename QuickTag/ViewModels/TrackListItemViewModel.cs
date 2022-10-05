using Avalonia.Media.Imaging;
using QuickTag.Models;
using ReactiveUI;
using System.IO;

namespace QuickTag.ViewModels
{
    public class TrackListItemViewModel: ViewModelBase
    {
        private readonly Track _track;
        private Bitmap _cover;

        public TrackListItemViewModel(Track track)
        {
            _track = track;
        }

        public string Title { get => _track.Title; set => _track.Title = value; }
        public string Artist { get => _track.Artist; set => _track.Artist = value; }

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

        public void LoadCover(int displaySize)
        {
            if (_track.Cover == null)
                return;

            using var stream = new MemoryStream(_track.Cover);
            Cover = Bitmap.DecodeToWidth(stream, displaySize);
        }
    }
}
