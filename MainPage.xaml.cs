using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel.Communication; // Dla Permissions
using apka.Services; // Dla IMediaRecorderService

namespace apka
{
    public partial class MainPage : ContentPage
    {
        private const string V = "Record";
        private readonly IMediaRecorderService _recorderService;

        public MainPage()
        {
            InitializeComponent();
            _recorderService = DependencyService.Get<IMediaRecorderService>()
                                ?? throw new InvalidOperationException("IMediaRecorderService nie zostało zaimplementowane.");

            // Opcjonalne logowanie
            Console.WriteLine($"RecorderService initialized: {_recorderService != null}");
        }

        private async void OnRecordButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Sprawdź i poproś o uprawnienia
                if (!await CheckAndRequestMicrophonePermission())
                {
                    await DisplayAlert("Brak uprawnień", "Aplikacja nie ma dostępu do mikrofonu.", "OK");
                    return;
                }

                if (!_recorderService.IsRecording)
                {
                    // Rozpocznij nagrywanie
                    RecordButton.Text = "Stop";
                    await _recorderService.StartRecordingAsync();
                }
                else
                {
                    // Zatrzymaj nagrywanie
                    var filePath = await _recorderService.StopRecordingAsync();
                    RecordButton.Text = V;

                    // Przejdź do strony z wynikami
                    await Navigation.PushAsync(new ResultPage(filePath));
                }
            }
            catch (Exception ex)
            {
                // Obsłuż wyjątek i wyświetl komunikat
                await DisplayAlert("Błąd", $"Wystąpił błąd: {ex.Message}", "OK");
            }
        }

        private async Task<bool> CheckAndRequestMicrophonePermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.Microphone>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.Microphone>();
            }

            return status == PermissionStatus.Granted;
        }
    }
}
