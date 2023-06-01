using System;
using System.Collections.Generic;
using System.Linq;

namespace Reco.Desktop.Logic
{
    public class RecorderHelper
    {
        static RecorderHelper()
        {
            resolutions = new Dictionary<string, Tuple<int, int>>();
            resolutions.Add("2160p 4K", Tuple.Create(3840, 2160));
            resolutions.Add("1440p HD", Tuple.Create(2560, 1440));
            resolutions.Add("1080p HD", Tuple.Create(1920, 1080));
            resolutions.Add("720p (Auto)", Tuple.Create(1280, 720));
        }
        private static Dictionary<string, Tuple<int, int>> resolutions;
        public static Tuple<int,int> GetResolutionByName(string nameOfResolution)
        {
            return resolutions[nameOfResolution];
        }

        public static List<string> GetNamesOfResolutions(double width, double height)
        {
            return resolutions.Where(r => r.Value.Item1 <= width)
                .Select(r => r.Key)
                .ToList();
        }
    }
}
