namespace apka.Services
{
    public interface IMediaRecorderService
    {
        Task StartRecordingAsync();
        Task<string> StopRecordingAsync();
        bool IsRecording { get; }
    }
}
