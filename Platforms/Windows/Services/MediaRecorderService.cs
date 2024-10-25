using System.Threading.Tasks;
using apka.Services;
using Microsoft.Maui.Controls; // Dla atrybutu Dependency

[assembly: Dependency(typeof(apka.Platforms.Windows.Services.MediaRecorderService))]

namespace apka.Platforms.Windows.Services
{
    public class MediaRecorderService : IMediaRecorderService
    {
        public bool IsRecording { get; private set; }

        public Task StartRecordingAsync()
        {
            // Implementacja nagrywania dźwięku dla Windows lub pozostawienie jako stub
            Console.WriteLine("StartRecordingAsync wywołane na Windows.");
            IsRecording = true;
            return Task.CompletedTask;
        }

        public Task<string> StopRecordingAsync()
        {
            // Implementacja zatrzymania nagrywania dźwięku lub zwrócenie dummy path
            Console.WriteLine("StopRecordingAsync wywołane na Windows.");
            IsRecording = false;
            return Task.FromResult("dummy_audio_path.m4a");
        }
    }
}
