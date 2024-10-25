using System.Threading.Tasks;
using Android.Media;
using apka.Services;
using JavaIOFile = Java.IO.File; // Alias dla Java.IO.File
using Microsoft.Maui.Controls; // Dla atrybutu Dependency
using Android.Content;

[assembly: Dependency(typeof(apka.Platforms.Android.Services.MediaRecorderService))]

namespace apka.Platforms.Android.Services
{
    public class MediaRecorderService : IMediaRecorderService
    {
        private MediaRecorder _mediaRecorder;
        private string _filePath;

        public bool IsRecording { get; private set; }

        public async Task StartRecordingAsync()
        {
            Console.WriteLine("StartRecordingAsync wywołane na Android.");
            _mediaRecorder = new MediaRecorder();
            _mediaRecorder.SetAudioSource(AudioSource.Mic);
            _mediaRecorder.SetOutputFormat(OutputFormat.Mpeg4);
            _mediaRecorder.SetAudioEncoder(AudioEncoder.Aac);

            var audioDir = Android.App.Application.Context.GetExternalFilesDir(null);
            var audioFile = new JavaIOFile(audioDir, "audio.m4a");
            _filePath = audioFile.AbsolutePath;
            _mediaRecorder.SetOutputFile(_filePath);

            _mediaRecorder.Prepare();
            _mediaRecorder.Start();
            IsRecording = true;
        }

        public async Task<string> StopRecordingAsync()
        {
            Console.WriteLine("StopRecordingAsync wywołane na Android.");
            if (_mediaRecorder != null && IsRecording)
            {
                _mediaRecorder.Stop();
                _mediaRecorder.Release();
                _mediaRecorder = null;
                IsRecording = false;
            }

            return _filePath;
        }
    }
}
