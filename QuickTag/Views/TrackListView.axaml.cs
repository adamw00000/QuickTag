using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using QuickTag.Models;
using QuickTag.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace QuickTag.Views
{
    public partial class TrackListView: ReactiveUserControl<TrackListViewModel>
    {
        public TrackListView()
        {
            InitializeComponent();
            this.WhenActivated(d => d(ViewModel!.Tracks.ActOnEveryObject(tvm => tvm.ShowTrackWindow.RegisterHandler(DoShowTrackWindowAsync), tvm => { })));
        }
        private async Task DoShowTrackWindowAsync(InteractionContext<TrackWindowViewModel, MusicTrack?> interaction)
        {
            var dialog = new TrackWindow
            {
                DataContext = interaction.Input
            };

            var parentWindow = (Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

            var result = await dialog.ShowDialog<MusicTrack?>(parentWindow);
            interaction.SetOutput(result);
        }
    }
}
