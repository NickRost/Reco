using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Reco.Desktop.Models.Auth;
using ScreenRecorderLib;

namespace Reco.Desktop.Logic
{
    public class RecorderService
    {
        public RecorderService(Token token)
        {
            _token = token;
        }
        private RecorderConfiguration _options;
        private readonly Token _token;
        private string filePath;
        public void Configure(RecorderConfiguration options)
        {
            _options = options;
        }

        private Recorder recorder;
        public event Action StartRec;

        public void StartRecording()
        {
            List<RecordingSourceBase> source = new List<RecordingSourceBase>();
            source.Add(Recorder.GetWindows().FirstOrDefault(w => w.Title == _options.RecorderWindowTitle));

            var options = new RecorderOptions
            {
                AudioOptions = new AudioOptions
                {
                    AudioInputDevice = Recorder.GetSystemAudioDevices(AudioDeviceSource.InputDevices)
                    .FirstOrDefault(d => d.FriendlyName == _options.SelectedAudioInputDevice)?
                    .DeviceName,

                    AudioOutputDevice = Recorder.GetSystemAudioDevices(AudioDeviceSource.OutputDevices)
                    .FirstOrDefault(d => d.FriendlyName == _options.SelectedAudioOutputDevice)?
                    .DeviceName,

                    IsAudioEnabled = _options.IsAudioEnable,
                    IsInputDeviceEnabled = true,
                    IsOutputDeviceEnabled = true,
                },
                SourceOptions = source.FirstOrDefault() == null 
                ? SourceOptions.MainMonitor 
                : new SourceOptions { RecordingSources = source },
                OutputOptions = new OutputOptions
                {
                    RecorderMode = RecorderMode.Video,
                    OutputFrameSize = new ScreenSize(_options.Resolution.Item1, _options.Resolution.Item2),
                    Stretch = StretchMode.Uniform,
                }
            };

            recorder = Recorder.CreateRecorder(options);
            recorder.OnRecordingFailed += Rec_OnRecordingFailed;
            recorder.OnRecordingComplete += Rec_OnRecordingComplete;
            recorder.OnStatusChanged += Rec_OnStatusChanged;
            filePath = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetRandomFileName(), ".mp4"));
            recorder.Record(filePath);

            StartRec?.Invoke();
        }

        public void PauseRecording()
        {
            recorder?.Pause();
        }

        public void ResumeRecording()
        {
            if (recorder?.Status == RecorderStatus.Paused)
            {
                recorder?.Resume();
            }
        }

        public async Task StopRecording()
        {
            recorder?.Stop();
            await UploadVideo(filePath);
        }

        public void RestartRecording()
        {
            recorder?.Stop();
            recorder?.Dispose();
            File.Delete(filePath);
            StartRecording();
        }

        public void CancelRecording()
        {
            recorder?.Stop();
            File.Delete(filePath);
        } 

        public List<string> GetInputAudioDevices()
        {
            return Recorder.GetSystemAudioDevices(AudioDeviceSource.InputDevices)
                ?.Select(d=>d.FriendlyName)
                ?.ToList();
        }

        public List<string> GetOutputAudioDevices()
        {
            return Recorder.GetSystemAudioDevices(AudioDeviceSource.OutputDevices)
                ?.Select(d => d.FriendlyName)
                ?.ToList();
        }

        public List<string> GetWindows()
        { 
            return Recorder.GetWindows().Select(w => w.Title).ToList();
        }

        public List<RecordingSourceBase> GetCameras()
        {
            var sources = new List<RecordingSourceBase>();
            sources.AddRange(Recorder.GetSystemVideoCaptureDevices());

            return sources;
        }

        private static void Rec_OnStatusChanged(object sender, RecordingStatusEventArgs e)
        {
            switch (e.Status)
            {
                case RecorderStatus.Recording:
                    break;
                case RecorderStatus.Paused:
                    break;
                case RecorderStatus.Finishing:
                    break;
                default:
                    break;
            }
        }

        private async void Rec_OnRecordingComplete(object sender, RecordingCompleteEventArgs e)
        {
         
        }

        private static void Rec_OnRecordingFailed(object sender, RecordingFailedEventArgs e)
        {
            string error = e.Error;
        }

        private void OpenBrowser(string url)
        {
            var psi = new ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = url;
            Process.Start(psi);
        }

        private async Task UploadVideo(string path)
        {
            string blobApi = ConfigurationManager.AppSettings["blobApi"];
            string mainApi = ConfigurationManager.AppSettings["mainApi"];

            ///
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HeaderNames.Authorization, _token.AccessToken.Trim('"'));
                var responseMainApi = await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, mainApi + $"File"));
                var videoId = await responseMainApi.Content.ReadAsStringAsync();

                if (videoId != null)
                {
                    // Open browser with video id, but video is just starting to be saved on the blob storage
                    OpenBrowser(ConfigurationManager.AppSettings["RecoUrl"] + $"video/{videoId}");

                    var filestream = File.OpenRead(path);
                    var inputData = new StreamContent(filestream);
                    client.DefaultRequestHeaders.Add("videoId", videoId);

                    var responseBlobApi = await client.PostAsync(blobApi, inputData);
                    var id = await responseBlobApi.Content.ReadAsStringAsync();
                    if (id is not null)
                    {
                        //when video is saved on the Blob storage
                    }
                }
                else
                {
                    throw new NullReferenceException("video id is null");
                }
            }
            ///
        }
    }
}
