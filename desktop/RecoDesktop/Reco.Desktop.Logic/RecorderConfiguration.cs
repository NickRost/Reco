
using ScreenRecorderLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco.Desktop.Logic
{
    public class RecorderConfiguration
    {
        public bool IsAudioEnable { get; set; } = true;
        public string SelectedAudioInputDevice { get; set; }
        public string SelectedAudioOutputDevice { get; set; }
        public string RecorderWindowTitle { get; set; }
        public Tuple<int, int> Resolution { get; set; }
    }
}
