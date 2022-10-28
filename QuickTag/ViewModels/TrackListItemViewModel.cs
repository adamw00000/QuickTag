using Avalonia.Media.Imaging;
using QuickTag.Models;
using ReactiveUI;
using System.IO;
using System.Reactive.Linq;
using System.Windows.Input;

namespace QuickTag.ViewModels
{
    public class TrackListItemViewModel: ViewModelBase
    {
        private MusicTrack _track;
        private Bitmap? _cover;

        public TrackListItemViewModel(MusicTrack track)
        {
            _track = track;
            ShowTrackWindow = new Interaction<TrackWindowViewModel, MusicTrack?>();

            EditTrackCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new TrackWindowViewModel(_track);
                var result = await ShowTrackWindow.Handle(store);

                if (result != null)
                {
                    _track = result;
                    await Observable.Start(() => LoadCover(Constants.CoverListMiniatureSize));
                }
            });
        }

        public string Title { get => _track.Title; set => _track.Title = value; }
        public string Artist { get => _track.Artist; set => _track.Artist = value; }
        public ICommand EditTrackCommand { get; }
        public Interaction<TrackWindowViewModel, MusicTrack?> ShowTrackWindow { get; }

        public Bitmap? Cover
        {
            get => _cover;
            set
            {
                this.RaiseAndSetIfChanged(ref _cover, value);

                if (value == null)
                {
                    _track.Cover = null;
                    return;
                }

                using var stream = new MemoryStream();
                value.Save(stream);
                _track.Cover = stream.ToArray();
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
