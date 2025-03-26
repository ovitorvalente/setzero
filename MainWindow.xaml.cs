using System.Windows;
using System.Windows.Media;
using SetZero.Application.Services;
using SetZero.Infrastructure.Data;
using SetZero.src.Domain.Entities;
using SetZero.src.Infrastructure.Services;

namespace SetZero;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void ResetMovimentValue(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(inputFilial.Text) || string.IsNullOrEmpty(inputSequencia.Text) || string.IsNullOrEmpty(inputNItem.Text))
        {
            ShowStatus("Preencha todos os campos!", Brushes.DarkRed);
            return;
        }

        var data = new MovimentData
        {
            Filial__Codigo = int.Parse(inputFilial.Text),
            Sequencia = int.Parse(inputSequencia.Text),
            Linha = int.Parse(inputNItem.Text),
        };

        try
        {
            ShowStatus("Aguarde um momento...", Brushes.DarkOrange);

            string folderPath = AppDomain.CurrentDomain.BaseDirectory;
            var databaseConfig = FileReader.ReadConfig(folderPath);

            var databaseConnection = new DatabaseConnection();
            var databaseService = new DatabaseService();

            using (var connection = databaseConnection.ConnectToDatabase(databaseConfig))
            {
                await databaseService.UpdateMoviments(data, connection);
            }

            ShowStatus("Procedimento realizado com sucesso!", Brushes.DarkGreen);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }



    private bool IsTextNumeric(string text)
    {
        return int.TryParse(text, out _);
    }
    private async void ShowStatus(string message, SolidColorBrush color)
    {
        statusBar.Background = color;
        statusText.Text = message;
        await Task.Delay(3000);
        statusBar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC0078D7"));
        statusText.Text = "Feito por Vitor Valente";
    }
    private void OnPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = !IsTextNumeric(e.Text);
    }
}