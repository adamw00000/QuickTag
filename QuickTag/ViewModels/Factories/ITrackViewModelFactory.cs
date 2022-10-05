using QuickTag.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTag.ViewModels.Factories
{
    public interface ITrackViewModelFactory
    {
        ITrackViewModel Create(Track track);
    }

    public class TrackViewModelFactory: ITrackViewModelFactory
    {
        public ITrackViewModel Create(Track track)
        {
            return new TrackViewModel(track);
        }
    }
}
