using DirectShowLib;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reco.Desktop.Logic
{
    public class CameraService
    {
        private static CameraService _instance;

        private VideoCapture _capture;
        private DsDevice[] _webCams;

        public bool IsCapturing { get; private set; }

        public event EventHandler ImageGrabbed;

        public delegate void SelectionChanged();

        public event SelectionChanged Notify;

        private CameraService()
        {
            CvInvoke.UseOpenCL = false;
        }

        public static CameraService GetInstance()
        {
            return _instance ??= new CameraService();
        }

        public void StartCapture(int selectedCameraId)
        {
            IsCapturing = true;
            _capture ??= new VideoCapture(selectedCameraId, VideoCapture.API.DShow);

            if (_capture == null)
            {
                return;
            }

            _capture.ImageGrabbed += ImageGrabbed;
            _capture.Start();
        }

        public void StopCapture()
        {
            if (IsCapturing)
            {
                _capture.ImageGrabbed -= ImageGrabbed;
                Notify.Invoke();
                _capture.Pause();
                _capture.Stop();
                _capture.Dispose();
                _capture = null;
                IsCapturing = false;
            }
        }

        public ICollection<string> GetCameras()
        {
            _webCams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            List<string> webCamsNames = _webCams.Select(el => el.Name).ToList();
            return webCamsNames;
        }

        public bool Retrieve(Mat m)
        {
            lock (new object())
            {
                if (_capture != null)
                {
                    return _capture.Retrieve(m);
                }
                return false;
            }
        }

        public Mat QueryFrame()
        {
            if (_capture != null)
            {
                return _capture.QueryFrame();
            }
            return null;
        }

    }
}
