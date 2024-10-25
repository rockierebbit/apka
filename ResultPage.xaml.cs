using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace apka
{
    public partial class ResultPage : ContentPage
    {
        private readonly string _audioFilePath;

        public ResultPage(string audioFilePath)
        {
            InitializeComponent();
            _audioFilePath = audioFilePath;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = AnalyzeAudioAsync();
        }

        private async Task AnalyzeAudioAsync()
        {
            // Wyœlij plik audio do modelu AI i pobierz wynik
            string result = await GetSoundClassificationAsync(_audioFilePath);

            // Zaktualizuj interfejs u¿ytkownika
            ResultLabel.Text = $"Wykryty dŸwiêk: {result}";
        }

        private async Task<string> GetSoundClassificationAsync(string _)
        {
            // TODO: Implementuj wysy³anie pliku do modelu AI i odbiór wyniku
            await Task.Delay(2000); // Symulacja czasu przetwarzania
            return "Bark"; // Przyk³adowy wynik
        }
    }
}
