using QuickTag.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTag.Design.ViewModels
{
    internal class MainWindowViewModelDesign: MainWindowViewModel
    {
        public MainWindowViewModelDesign(): base(DesignViewModelLocator.TrackList) { }
    }
}
